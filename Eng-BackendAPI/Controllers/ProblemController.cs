using System.Security.Claims;
using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Common;
using Eng_Backend.DtoLayer.Problems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProblemController : ControllerBase
{
    private readonly IProblemService _problemService;

    public ProblemController(IProblemService problemService)
    {
        _problemService = problemService;
    }

    // ============ Problem CRUD (Admin) ============

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllProblems()
    {
        var problems = await _problemService.GetAllProblemsAsync();
        return Ok(ApiResponse<List<ProblemListDto>>.SuccessResponse(problems, SuccessMessages.ProblemsRetrieved));
    }

    [HttpGet("{problemId:guid}")]
    public async Task<IActionResult> GetProblemById(Guid problemId)
    {
        var problem = await _problemService.GetProblemByIdAsync(problemId);
        return Ok(ApiResponse<ProblemDto>.SuccessResponse(problem, SuccessMessages.ProblemRetrieved));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProblem(CreateProblemDto dto)
    {
        var userId = GetCurrentUserId();
        var problem = await _problemService.CreateProblemAsync(dto, userId);
        return Ok(ApiResponse<ProblemDto>.SuccessResponse(problem, SuccessMessages.ProblemCreated, 201));
    }

    [HttpPut("{problemId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProblem(Guid problemId, UpdateProblemDto dto)
    {
        var problem = await _problemService.UpdateProblemAsync(problemId, dto);
        return Ok(ApiResponse<ProblemDto>.SuccessResponse(problem, SuccessMessages.ProblemUpdated));
    }

    [HttpDelete("{problemId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProblem(Guid problemId)
    {
        await _problemService.DeleteProblemAsync(problemId);
        return Ok(ApiResponse.SuccessResponse(SuccessMessages.ProblemDeleted));
    }

    // ============ Assignments ============

    [HttpPost("assign")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignProblem(AssignProblemDto dto)
    {
        var userId = GetCurrentUserId();
        await _problemService.AssignProblemToUsersAsync(dto, userId);
        return Ok(ApiResponse.SuccessResponse(SuccessMessages.ProblemAssigned));
    }

    [HttpGet("my-assignments")]
    public async Task<IActionResult> GetMyAssignments()
    {
        var userId = GetCurrentUserId();
        var assignments = await _problemService.GetMyAssignmentsAsync(userId);
        return Ok(ApiResponse<List<MyAssignmentDto>>.SuccessResponse(assignments, SuccessMessages.AssignmentsRetrieved));
    }

    [HttpPost("assignments/{assignmentId:guid}/accept")]
    public async Task<IActionResult> AcceptAssignment(Guid assignmentId)
    {
        var userId = GetCurrentUserId();
        await _problemService.AcceptAssignmentAsync(assignmentId, userId);
        return Ok(ApiResponse.SuccessResponse(SuccessMessages.AssignmentAccepted));
    }

    // ============ Solutions ============

    [HttpPost("assignments/{assignmentId:guid}/solutions")]
    public async Task<IActionResult> SubmitSolution(Guid assignmentId, SubmitSolutionDto dto)
    {
        var userId = GetCurrentUserId();
        var solution = await _problemService.SubmitSolutionAsync(assignmentId, dto, userId);
        return Ok(ApiResponse<SolutionDto>.SuccessResponse(solution, SuccessMessages.SolutionSubmitted, 201));
    }

    [HttpPost("solutions/{solutionId:guid}/files")]
    [RequestSizeLimit(50 * 1024 * 1024)] // 50MB limit
    public async Task<IActionResult> UploadSolutionFile(Guid solutionId, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ApiResponse.ErrorResponse(ErrorMessages.FileRequired));

        var userId = GetCurrentUserId();
        using var stream = file.OpenReadStream();
        var solution = await _problemService.UploadSolutionFileAsync(solutionId, stream, file.FileName, file.ContentType, userId);
        return Ok(ApiResponse<SolutionDto>.SuccessResponse(solution, SuccessMessages.SolutionFileUploaded));
    }

    [HttpPost("solutions/{solutionId:guid}/review")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ReviewSolution(Guid solutionId, ReviewSolutionDto dto)
    {
        var userId = GetCurrentUserId();
        var solution = await _problemService.ReviewSolutionAsync(solutionId, dto, userId);
        return Ok(ApiResponse<SolutionDto>.SuccessResponse(solution, SuccessMessages.SolutionReviewed));
    }

    [HttpGet("assignments/{assignmentId:guid}/solutions")]
    public async Task<IActionResult> GetSolutionsForAssignment(Guid assignmentId)
    {
        var solutions = await _problemService.GetSolutionsForAssignmentAsync(assignmentId);
        return Ok(ApiResponse<List<SolutionDto>>.SuccessResponse(solutions, SuccessMessages.SolutionsRetrieved));
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("User ID not found in token");
        return userId;
    }
}