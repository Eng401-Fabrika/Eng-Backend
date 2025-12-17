using System.Text.Json;
using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DAL.Interfaces;
using Eng_Backend.DtoLayer.Quiz;
using Microsoft.AspNetCore.Identity;

namespace Eng_Backend.BusinessLayer.Managers;

public class QuizService : IQuizService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDocumentService _documentService;

    public QuizService(
        IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager,
        IDocumentService documentService)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _documentService = documentService;
    }

    public async Task<QuestionWithAnswerDto> CreateQuestionAsync(CreateQuestionDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.QuestionText))
            throw new BadRequestException(ErrorMessages.QuestionTextRequired);

        if (dto.Options == null || dto.Options.Count < 2)
            throw new BadRequestException(ErrorMessages.MinimumOptionsRequired);

        if (dto.CorrectAnswerIndex < 0 || dto.CorrectAnswerIndex >= dto.Options.Count)
            throw new BadRequestException(ErrorMessages.InvalidCorrectAnswerIndex);

        var document = await _documentService.GetDocumentByIdAsync(dto.DocumentId);

        var question = new Question
        {
            Id = Guid.NewGuid(),
            DocumentId = dto.DocumentId,
            QuestionText = dto.QuestionText,
            Options = JsonSerializer.Serialize(dto.Options),
            CorrectAnswerIndex = dto.CorrectAnswerIndex,
            Points = dto.Points,
            Difficulty = (QuestionDifficulty)dto.Difficulty,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Questions.AddAsync(question);
        await _unitOfWork.SaveChangesAsync();

        return MapToQuestionWithAnswerDto(question, document.Title);
    }

    public async Task<List<QuestionWithAnswerDto>> GetQuestionsByDocumentAsync(Guid documentId)
    {
        var document = await _documentService.GetDocumentByIdAsync(documentId);

        var questions = await _unitOfWork.Questions.GetByDocumentIdAsync(documentId);

        return questions.Select(q => MapToQuestionWithAnswerDto(q, document.Title)).ToList();
    }

    public async Task DeleteQuestionAsync(Guid questionId)
    {
        var question = await _unitOfWork.Questions.GetByIdAsync(questionId);
        if (question == null)
            throw new NotFoundException(string.Format(ErrorMessages.QuestionNotFound, questionId));

        question.IsActive = false; // Soft delete
        await _unitOfWork.Questions.UpdateAsync(question);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<QuizSessionDto> StartQuizAsync(Guid documentId, Guid userId, int questionCount = 5)
    {
        var document = await _documentService.GetDocumentByIdAsync(documentId);

        // Get questions that user hasn't answered yet
        var answeredQuestionIds = await _unitOfWork.UserAnswers.GetAnsweredQuestionIdsByUserAsync(userId);

        var availableQuestions = await _unitOfWork.Questions.GetByDocumentIdAsync(documentId);
        var unansweredQuestions = availableQuestions
            .Where(q => !answeredQuestionIds.Contains(q.Id))
            .OrderBy(q => Guid.NewGuid()) // Random order
            .Take(questionCount)
            .ToList();

        // If not enough unanswered questions, include some answered ones
        if (unansweredQuestions.Count < questionCount)
        {
            var additionalCount = questionCount - unansweredQuestions.Count;
            var additionalQuestions = availableQuestions
                .Where(q => answeredQuestionIds.Contains(q.Id))
                .OrderBy(q => Guid.NewGuid())
                .Take(additionalCount)
                .ToList();

            unansweredQuestions.AddRange(additionalQuestions);
        }

        return new QuizSessionDto
        {
            DocumentId = documentId,
            DocumentTitle = document.Title,
            TotalQuestions = unansweredQuestions.Count,
            Questions = unansweredQuestions.Select(q => MapToQuestionDto(q, document.Title)).ToList()
        };
    }

    public async Task<AnswerResultDto> SubmitAnswerAsync(SubmitAnswerDto dto, Guid userId)
    {
        var question = await _unitOfWork.Questions.GetByIdWithDocumentAsync(dto.QuestionId);

        if (question == null)
            throw new NotFoundException(string.Format(ErrorMessages.QuestionNotFound, dto.QuestionId));

        // Check if already answered
        var existingAnswer = await _unitOfWork.UserAnswers.GetByUserAndQuestionAsync(userId, dto.QuestionId);

        if (existingAnswer != null)
            throw new BadRequestException(ErrorMessages.AlreadyAnsweredQuestion);

        var options = JsonSerializer.Deserialize<List<string>>(question.Options) ?? new List<string>();
        var isCorrect = dto.SelectedAnswerIndex == question.CorrectAnswerIndex;
        var pointsEarned = isCorrect ? question.Points : 0;

        var userAnswer = new UserAnswer
        {
            Id = Guid.NewGuid(),
            QuestionId = dto.QuestionId,
            UserId = userId,
            SelectedAnswerIndex = dto.SelectedAnswerIndex,
            IsCorrect = isCorrect,
            PointsEarned = pointsEarned,
            AnsweredAt = DateTime.UtcNow
        };

        await _unitOfWork.UserAnswers.AddAsync(userAnswer);

        // Update user score
        if (isCorrect)
        {
            await UpdateUserScoreAsync(userId, pointsEarned);
        }

        await _unitOfWork.SaveChangesAsync();

        return new AnswerResultDto
        {
            QuestionId = dto.QuestionId,
            IsCorrect = isCorrect,
            PointsEarned = pointsEarned,
            CorrectAnswerIndex = question.CorrectAnswerIndex,
            CorrectAnswer = options.Count > question.CorrectAnswerIndex ? options[question.CorrectAnswerIndex] : ""
        };
    }

    public async Task<UserScoreDto> GetUserScoreAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new NotFoundException(string.Format(ErrorMessages.UserNotFound, userId));

        var userScore = await _unitOfWork.UserScores.GetByUserIdAsync(userId);

        if (userScore == null)
        {
            // Return default score
            return new UserScoreDto
            {
                UserId = userId,
                UserName = user.FullName,
                Email = user.Email ?? "",
                QuizPoints = 0,
                TaskPoints = 0,
                TotalPoints = 0,
                QuizzesCompleted = 0,
                TasksCompleted = 0,
                Level = 1,
                Rank = 0
            };
        }

        var rank = await _unitOfWork.UserScores.GetUserRankAsync(userId);

        return new UserScoreDto
        {
            UserId = userId,
            UserName = user.FullName,
            Email = user.Email ?? "",
            QuizPoints = userScore.QuizPoints,
            TaskPoints = userScore.TaskPoints,
            TotalPoints = userScore.TotalPoints,
            QuizzesCompleted = userScore.QuizzesCompleted,
            TasksCompleted = userScore.TasksCompleted,
            Level = userScore.Level,
            Rank = rank
        };
    }

    public async Task<LeaderboardDto> GetLeaderboardAsync(Guid? currentUserId, int top = 10)
    {
        var topScores = await _unitOfWork.UserScores.GetTopScoresAsync(top);

        var rankings = topScores.Select((us, index) => new UserScoreDto
        {
            UserId = us.UserId,
            UserName = us.User.FullName,
            Email = us.User.Email ?? "",
            QuizPoints = us.QuizPoints,
            TaskPoints = us.TaskPoints,
            TotalPoints = us.TotalPoints,
            QuizzesCompleted = us.QuizzesCompleted,
            TasksCompleted = us.TasksCompleted,
            Level = us.Level,
            Rank = index + 1
        }).ToList();

        UserScoreDto? currentUserRanking = null;
        if (currentUserId.HasValue)
        {
            currentUserRanking = await GetUserScoreAsync(currentUserId.Value);
        }

        return new LeaderboardDto
        {
            Rankings = rankings,
            CurrentUserRanking = currentUserRanking
        };
    }

    public async Task<List<UserAnswerDto>> GetUserAnswersAsync(Guid userId, Guid? documentId = null)
    {
        var answers = await _unitOfWork.UserAnswers.GetByUserIdAsync(userId, documentId);

        return answers.Select(ua =>
        {
            var options = JsonSerializer.Deserialize<List<string>>(ua.Question.Options) ?? new List<string>();
            return new UserAnswerDto
            {
                Id = ua.Id,
                QuestionId = ua.QuestionId,
                QuestionText = ua.Question.QuestionText,
                SelectedAnswerIndex = ua.SelectedAnswerIndex,
                SelectedAnswer = options.Count > ua.SelectedAnswerIndex ? options[ua.SelectedAnswerIndex] : "",
                IsCorrect = ua.IsCorrect,
                PointsEarned = ua.PointsEarned,
                AnsweredAt = ua.AnsweredAt
            };
        }).ToList();
    }

    private async Task UpdateUserScoreAsync(Guid userId, int quizPoints)
    {
        var userScore = await _unitOfWork.UserScores.GetByUserIdAsync(userId);

        if (userScore == null)
        {
            userScore = new UserScore
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                QuizPoints = quizPoints,
                TaskPoints = 0,
                TotalPoints = quizPoints,
                QuizzesCompleted = 1,
                TasksCompleted = 0,
                Level = 1,
                UpdatedAt = DateTime.UtcNow
            };
            await _unitOfWork.UserScores.AddAsync(userScore);
        }
        else
        {
            userScore.QuizPoints += quizPoints;
            userScore.TotalPoints = userScore.QuizPoints + userScore.TaskPoints;
            userScore.QuizzesCompleted++;
            userScore.Level = CalculateLevel(userScore.TotalPoints);
            userScore.UpdatedAt = DateTime.UtcNow;
            await _unitOfWork.UserScores.UpdateAsync(userScore);
        }
    }

    private int CalculateLevel(int totalPoints)
    {
        return (totalPoints / 100) + 1;
    }

    private QuestionDto MapToQuestionDto(Question question, string documentTitle)
    {
        var options = JsonSerializer.Deserialize<List<string>>(question.Options) ?? new List<string>();
        return new QuestionDto
        {
            Id = question.Id,
            DocumentId = question.DocumentId,
            DocumentTitle = documentTitle,
            QuestionText = question.QuestionText,
            Options = options,
            Difficulty = question.Difficulty.ToString(),
            Points = question.Points
        };
    }

    private QuestionWithAnswerDto MapToQuestionWithAnswerDto(Question question, string documentTitle)
    {
        var options = JsonSerializer.Deserialize<List<string>>(question.Options) ?? new List<string>();
        return new QuestionWithAnswerDto
        {
            Id = question.Id,
            DocumentId = question.DocumentId,
            DocumentTitle = documentTitle,
            QuestionText = question.QuestionText,
            Options = options,
            Difficulty = question.Difficulty.ToString(),
            Points = question.Points,
            CorrectAnswerIndex = question.CorrectAnswerIndex
        };
    }
}