using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DAL.Interfaces;
using Eng_Backend.DtoLayer.Problems;
using Microsoft.AspNetCore.Identity;

namespace Eng_Backend.BusinessLayer.Managers;

public class ProblemService : IProblemService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly INotificationService _notificationService;
    private readonly IS3Service _s3Service;

    public ProblemService(
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        INotificationService notificationService,
        IS3Service s3Service)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _notificationService = notificationService;
        _s3Service = s3Service;
    }

    public async Task<List<ProblemListDto>> GetAllProblemsAsync()
    {
        var problems = await _unitOfWork.Problems.GetAllWithDetailsAsync();

        return problems.Select(p => new ProblemListDto
        {
            Id = p.Id,
            Title = p.Title,
            Priority = p.Priority.ToString(),
            Status = p.Status.ToString(),
            DueDate = p.DueDate,
            AssignmentCount = p.Assignments.Count,
            CreatedAt = p.CreatedAt,
            CreatedByUserName = p.CreatedByUser.FullName
        }).ToList();
    }

    public async Task<ProblemDto> GetProblemByIdAsync(Guid problemId)
    {
        var problem = await _unitOfWork.Problems.GetByIdWithDetailsAsync(problemId);

        if (problem == null)
            throw new NotFoundException(string.Format(ErrorMessages.ProblemNotFound, problemId));

        return MapToProblemDto(problem);
    }

    public async Task<ProblemDto> CreateProblemAsync(CreateProblemDto dto, Guid createdByUserId)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new BadRequestException(ErrorMessages.ProblemTitleRequired);

        var problem = new Problem
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            Priority = (ProblemPriority)dto.Priority,
            Status = ProblemStatus.Open,
            DueDate = dto.DueDate,
            CreatedByUserId = createdByUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Problems.AddAsync(problem);
        await _unitOfWork.SaveChangesAsync();

        // If users are specified, assign them
        if (dto.AssignToUserIds != null && dto.AssignToUserIds.Any())
        {
            await AssignProblemToUsersAsync(new AssignProblemDto
            {
                ProblemId = problem.Id,
                UserIds = dto.AssignToUserIds,
                DueDate = dto.DueDate
            }, createdByUserId);
        }

        return await GetProblemByIdAsync(problem.Id);
    }

    public async Task<ProblemDto> UpdateProblemAsync(Guid problemId, UpdateProblemDto dto)
    {
        var problem = await _unitOfWork.Problems.GetByIdAsync(problemId);
        if (problem == null)
            throw new NotFoundException(string.Format(ErrorMessages.ProblemNotFound, problemId));

        if (dto.Title != null)
            problem.Title = dto.Title;

        if (dto.Description != null)
            problem.Description = dto.Description;

        if (dto.Priority.HasValue)
            problem.Priority = (ProblemPriority)dto.Priority.Value;

        if (dto.Status.HasValue)
            problem.Status = (ProblemStatus)dto.Status.Value;

        if (dto.DueDate.HasValue)
            problem.DueDate = dto.DueDate;

        problem.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.Problems.UpdateAsync(problem);
        await _unitOfWork.SaveChangesAsync();

        return await GetProblemByIdAsync(problemId);
    }

    public async Task DeleteProblemAsync(Guid problemId)
    {
        var problem = await _unitOfWork.Problems.GetByIdAsync(problemId);
        if (problem == null)
            throw new NotFoundException(string.Format(ErrorMessages.ProblemNotFound, problemId));

        await _unitOfWork.Problems.DeleteAsync(problem);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task AssignProblemToUsersAsync(AssignProblemDto dto, Guid assignedByUserId)
    {
        var problem = await _unitOfWork.Problems.GetByIdAsync(dto.ProblemId);
        if (problem == null)
            throw new NotFoundException(string.Format(ErrorMessages.ProblemNotFound, dto.ProblemId));

        foreach (var userId in dto.UserIds)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new NotFoundException(string.Format(ErrorMessages.UserNotFound, userId));

            // Check if already assigned
            var existingAssignment = await _unitOfWork.ProblemAssignments.GetByProblemAndUserAsync(dto.ProblemId, userId);

            if (existingAssignment != null)
                continue; // Skip if already assigned

            var assignment = new ProblemAssignment
            {
                Id = Guid.NewGuid(),
                ProblemId = dto.ProblemId,
                AssignedToUserId = userId,
                AssignedByUserId = assignedByUserId,
                AssignedAt = DateTime.UtcNow,
                DueDate = dto.DueDate ?? problem.DueDate,
                Status = AssignmentStatus.Pending,
                IsNotified = false
            };

            await _unitOfWork.ProblemAssignments.AddAsync(assignment);

            // Send notification
            await _notificationService.SendProblemAssignedNotificationAsync(
                userId,
                user.Email ?? "",
                problem.Title,
                assignment.DueDate);

            assignment.IsNotified = true;
            assignment.NotifiedAt = DateTime.UtcNow;
        }

        // Update problem status if it was Open
        if (problem.Status == ProblemStatus.Open)
        {
            problem.Status = ProblemStatus.InProgress;
            problem.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.Problems.UpdateAsync(problem);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<MyAssignmentDto>> GetMyAssignmentsAsync(Guid userId)
    {
        var assignments = await _unitOfWork.ProblemAssignments.GetByUserIdAsync(userId);

        return assignments.Select(a => new MyAssignmentDto
        {
            AssignmentId = a.Id,
            ProblemId = a.ProblemId,
            ProblemTitle = a.Problem.Title,
            ProblemDescription = a.Problem.Description,
            ProblemPriority = a.Problem.Priority.ToString(),
            Status = a.Status.ToString(),
            AssignedAt = a.AssignedAt,
            DueDate = a.DueDate,
            AssignedByUserName = a.AssignedByUser.FullName
        }).ToList();
    }

    public async Task AcceptAssignmentAsync(Guid assignmentId, Guid userId)
    {
        var assignment = await _unitOfWork.ProblemAssignments.GetByIdAsync(assignmentId);
        if (assignment == null)
            throw new NotFoundException(string.Format(ErrorMessages.AssignmentNotFound, assignmentId));

        if (assignment.AssignedToUserId != userId)
            throw new ForbiddenException(ErrorMessages.CanOnlyAcceptOwnAssignments);

        if (assignment.Status != AssignmentStatus.Pending)
            throw new BadRequestException(ErrorMessages.AssignmentNotPending);

        assignment.Status = AssignmentStatus.Accepted;
        await _unitOfWork.ProblemAssignments.UpdateAsync(assignment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<SolutionDto> SubmitSolutionAsync(Guid assignmentId, SubmitSolutionDto dto, Guid userId)
    {
        var assignment = await _unitOfWork.ProblemAssignments.GetByIdWithDetailsAsync(assignmentId);

        if (assignment == null)
            throw new NotFoundException(string.Format(ErrorMessages.AssignmentNotFound, assignmentId));

        if (assignment.AssignedToUserId != userId)
            throw new ForbiddenException(ErrorMessages.CanOnlySubmitOwnSolutions);

        if (assignment.Status == AssignmentStatus.Completed)
            throw new BadRequestException(ErrorMessages.AssignmentAlreadyCompleted);

        var solution = new Solution
        {
            Id = Guid.NewGuid(),
            AssignmentId = assignmentId,
            Description = dto.Description,
            Status = SolutionStatus.Submitted,
            SubmittedAt = DateTime.UtcNow
        };

        await _unitOfWork.Solutions.AddAsync(solution);

        // Update assignment status if it was Pending
        if (assignment.Status == AssignmentStatus.Pending)
        {
            assignment.Status = AssignmentStatus.Accepted;
            await _unitOfWork.ProblemAssignments.UpdateAsync(assignment);
        }

        await _unitOfWork.SaveChangesAsync();

        return await GetSolutionByIdAsync(solution.Id);
    }

    public async Task<SolutionDto> UploadSolutionFileAsync(Guid solutionId, Stream fileStream, string fileName, string contentType, Guid userId)
    {
        var solution = await _unitOfWork.Solutions.GetByIdWithDetailsAsync(solutionId);

        if (solution == null)
            throw new NotFoundException(string.Format(ErrorMessages.SolutionNotFound, solutionId));

        if (solution.Assignment.AssignedToUserId != userId)
            throw new ForbiddenException(ErrorMessages.CanOnlyUploadToOwnSolutions);

        // Upload to S3
        var uploadResult = await _s3Service.UploadFileAsync(fileStream, fileName, contentType);
        if (!uploadResult.Success)
            throw new InternalServerException(uploadResult.ErrorMessage ?? ErrorMessages.FileUploadFailed);

        var solutionFile = new SolutionFile
        {
            Id = Guid.NewGuid(),
            SolutionId = solutionId,
            FileName = fileName,
            FileUrl = uploadResult.FileUrl,
            FileKey = uploadResult.FileKey,
            ContentType = contentType,
            FileSizeBytes = fileStream.Length,
            UploadedAt = DateTime.UtcNow
        };

        await _unitOfWork.SolutionFiles.AddAsync(solutionFile);
        await _unitOfWork.SaveChangesAsync();

        return await GetSolutionByIdAsync(solutionId);
    }

    public async Task<SolutionDto> ReviewSolutionAsync(Guid solutionId, ReviewSolutionDto dto, Guid reviewerUserId)
    {
        var solution = await _unitOfWork.Solutions.GetByIdWithDetailsAsync(solutionId);

        if (solution == null)
            throw new NotFoundException(string.Format(ErrorMessages.SolutionNotFound, solutionId));

        solution.Status = dto.Approve ? SolutionStatus.Approved : SolutionStatus.Rejected;
        solution.ReviewedByUserId = reviewerUserId;
        solution.ReviewedAt = DateTime.UtcNow;
        solution.ReviewNotes = dto.ReviewNotes;
        solution.PointsAwarded = dto.Approve ? dto.PointsAwarded : 0;

        if (dto.Approve)
        {
            solution.Assignment.Status = AssignmentStatus.Completed;
            await _unitOfWork.ProblemAssignments.UpdateAsync(solution.Assignment);

            // Update user score
            await UpdateUserScoreAsync(solution.Assignment.AssignedToUserId, 0, dto.PointsAwarded);
        }

        await _unitOfWork.Solutions.UpdateAsync(solution);
        await _unitOfWork.SaveChangesAsync();

        // Send notification
        await _notificationService.SendSolutionReviewedNotificationAsync(
            solution.Assignment.AssignedToUserId,
            solution.Assignment.AssignedToUser.Email ?? "",
            solution.Assignment.Problem.Title,
            dto.Approve,
            dto.PointsAwarded);

        return await GetSolutionByIdAsync(solutionId);
    }

    public async Task<List<SolutionDto>> GetSolutionsForAssignmentAsync(Guid assignmentId)
    {
        var solutions = await _unitOfWork.Solutions.GetByAssignmentIdAsync(assignmentId);

        return solutions.Select(MapToSolutionDto).ToList();
    }

    private async Task<SolutionDto> GetSolutionByIdAsync(Guid solutionId)
    {
        var solution = await _unitOfWork.Solutions.GetByIdWithDetailsAsync(solutionId);

        if (solution == null)
            throw new NotFoundException(string.Format(ErrorMessages.SolutionNotFound, solutionId));

        return MapToSolutionDto(solution);
    }

    private async Task UpdateUserScoreAsync(Guid userId, int quizPoints, int taskPoints)
    {
        var userScore = await _unitOfWork.UserScores.GetByUserIdAsync(userId);

        if (userScore == null)
        {
            userScore = new UserScore
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                QuizPoints = quizPoints,
                TaskPoints = taskPoints,
                TotalPoints = quizPoints + taskPoints,
                QuizzesCompleted = 0,
                TasksCompleted = taskPoints > 0 ? 1 : 0,
                Level = 1,
                UpdatedAt = DateTime.UtcNow
            };
            await _unitOfWork.UserScores.AddAsync(userScore);
        }
        else
        {
            userScore.QuizPoints += quizPoints;
            userScore.TaskPoints += taskPoints;
            userScore.TotalPoints = userScore.QuizPoints + userScore.TaskPoints;
            if (taskPoints > 0) userScore.TasksCompleted++;
            userScore.Level = CalculateLevel(userScore.TotalPoints);
            userScore.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.UserScores.UpdateAsync(userScore);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private int CalculateLevel(int totalPoints)
    {
        // Simple level calculation: every 100 points = 1 level
        return (totalPoints / 100) + 1;
    }

    private ProblemDto MapToProblemDto(Problem problem)
    {
        return new ProblemDto
        {
            Id = problem.Id,
            Title = problem.Title,
            Description = problem.Description,
            Priority = problem.Priority.ToString(),
            Status = problem.Status.ToString(),
            DueDate = problem.DueDate,
            CreatedAt = problem.CreatedAt,
            CreatedByUserId = problem.CreatedByUserId,
            CreatedByUserName = problem.CreatedByUser.FullName,
            Assignments = problem.Assignments.Select(a => new AssignmentDto
            {
                Id = a.Id,
                AssignedToUserId = a.AssignedToUserId,
                AssignedToUserName = a.AssignedToUser.FullName,
                AssignedToUserEmail = a.AssignedToUser.Email ?? "",
                Status = a.Status.ToString(),
                AssignedAt = a.AssignedAt,
                DueDate = a.DueDate,
                IsNotified = a.IsNotified,
                SolutionCount = a.Solutions.Count
            }).ToList()
        };
    }

    private SolutionDto MapToSolutionDto(Solution solution)
    {
        return new SolutionDto
        {
            Id = solution.Id,
            AssignmentId = solution.AssignmentId,
            Description = solution.Description,
            Status = solution.Status.ToString(),
            SubmittedAt = solution.SubmittedAt,
            ReviewedByUserId = solution.ReviewedByUserId,
            ReviewedByUserName = solution.ReviewedByUser?.FullName,
            ReviewedAt = solution.ReviewedAt,
            ReviewNotes = solution.ReviewNotes,
            PointsAwarded = solution.PointsAwarded,
            Files = solution.Files.Select(f => new SolutionFileDto
            {
                Id = f.Id,
                FileName = f.FileName,
                FileUrl = f.FileUrl,
                ContentType = f.ContentType,
                FileSizeBytes = f.FileSizeBytes,
                UploadedAt = f.UploadedAt
            }).ToList()
        };
    }
}