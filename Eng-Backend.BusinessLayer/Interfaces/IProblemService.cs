using Eng_Backend.DtoLayer.Problems;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IProblemService
{
    // Problem CRUD
    Task<List<ProblemListDto>> GetAllProblemsAsync();
    Task<ProblemDto> GetProblemByIdAsync(Guid problemId);
    Task<ProblemDto> CreateProblemAsync(CreateProblemDto dto, Guid createdByUserId);
    Task<ProblemDto> UpdateProblemAsync(Guid problemId, UpdateProblemDto dto);
    Task DeleteProblemAsync(Guid problemId);

    // Assignments
    Task AssignProblemToUsersAsync(AssignProblemDto dto, Guid assignedByUserId);
    Task<List<MyAssignmentDto>> GetMyAssignmentsAsync(Guid userId);
    Task AcceptAssignmentAsync(Guid assignmentId, Guid userId);

    // Solutions
    Task<SolutionDto> SubmitSolutionAsync(Guid assignmentId, SubmitSolutionDto dto, Guid userId);
    Task<SolutionDto> UploadSolutionFileAsync(Guid solutionId, Stream fileStream, string fileName, string contentType, Guid userId);
    Task<SolutionDto> ReviewSolutionAsync(Guid solutionId, ReviewSolutionDto dto, Guid reviewerUserId);
    Task<List<SolutionDto>> GetSolutionsForAssignmentAsync(Guid assignmentId);
}