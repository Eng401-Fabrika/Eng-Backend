using System.Security.Claims;
using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Common;
using Eng_Backend.DtoLayer.Quiz;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuizController : ControllerBase
{
    private readonly IQuizService _quizService;

    public QuizController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    // ============ Questions (Admin) ============

    [HttpPost("questions")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateQuestion(CreateQuestionDto dto)
    {
        var question = await _quizService.CreateQuestionAsync(dto);
        return Ok(ApiResponse<QuestionWithAnswerDto>.SuccessResponse(question, SuccessMessages.QuestionCreated, 201));
    }

    [HttpGet("questions/document/{documentId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetQuestionsByDocument(Guid documentId)
    {
        var questions = await _quizService.GetQuestionsByDocumentAsync(documentId);
        return Ok(ApiResponse<List<QuestionWithAnswerDto>>.SuccessResponse(questions, SuccessMessages.QuestionsRetrieved));
    }

    [HttpDelete("questions/{questionId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteQuestion(Guid questionId)
    {
        await _quizService.DeleteQuestionAsync(questionId);
        return Ok(ApiResponse.SuccessResponse(SuccessMessages.QuestionDeleted));
    }

    // ============ Quiz Session (User) ============

    [HttpPost("start/{documentId:guid}")]
    public async Task<IActionResult> StartQuiz(Guid documentId, [FromQuery] int questionCount = 5)
    {
        var userId = GetCurrentUserId();
        var session = await _quizService.StartQuizAsync(documentId, userId, questionCount);
        return Ok(ApiResponse<QuizSessionDto>.SuccessResponse(session, SuccessMessages.QuizStarted));
    }

    [HttpPost("answer")]
    public async Task<IActionResult> SubmitAnswer(SubmitAnswerDto dto)
    {
        var userId = GetCurrentUserId();
        var result = await _quizService.SubmitAnswerAsync(dto, userId);
        return Ok(ApiResponse<AnswerResultDto>.SuccessResponse(result, SuccessMessages.AnswerSubmitted));
    }

    // ============ Scores & Leaderboard ============

    [HttpGet("my-score")]
    public async Task<IActionResult> GetMyScore()
    {
        var userId = GetCurrentUserId();
        var score = await _quizService.GetUserScoreAsync(userId);
        return Ok(ApiResponse<UserScoreDto>.SuccessResponse(score, SuccessMessages.ScoreRetrieved));
    }

    [HttpGet("leaderboard")]
    public async Task<IActionResult> GetLeaderboard([FromQuery] int top = 10)
    {
        var userId = GetCurrentUserId();
        var leaderboard = await _quizService.GetLeaderboardAsync(userId, top);
        return Ok(ApiResponse<LeaderboardDto>.SuccessResponse(leaderboard, SuccessMessages.LeaderboardRetrieved));
    }

    // ============ User's Quiz History ============

    [HttpGet("my-answers")]
    public async Task<IActionResult> GetMyAnswers([FromQuery] Guid? documentId = null)
    {
        var userId = GetCurrentUserId();
        var answers = await _quizService.GetUserAnswersAsync(userId, documentId);
        return Ok(ApiResponse<List<UserAnswerDto>>.SuccessResponse(answers, SuccessMessages.AnswersRetrieved));
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("User ID not found in token");
        return userId;
    }
}