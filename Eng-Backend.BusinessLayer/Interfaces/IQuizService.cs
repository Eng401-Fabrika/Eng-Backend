using Eng_Backend.DtoLayer.Quiz;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IQuizService
{
    // Questions (Admin)
    Task<QuestionWithAnswerDto> CreateQuestionAsync(CreateQuestionDto dto);
    Task<List<QuestionWithAnswerDto>> GetQuestionsByDocumentAsync(Guid documentId);
    Task DeleteQuestionAsync(Guid questionId);

    // Quiz Session (User)
    Task<QuizSessionDto> StartQuizAsync(Guid documentId, Guid userId, int questionCount = 5);
    Task<AnswerResultDto> SubmitAnswerAsync(SubmitAnswerDto dto, Guid userId);

    // Scores & Leaderboard
    Task<UserScoreDto> GetUserScoreAsync(Guid userId);
    Task<LeaderboardDto> GetLeaderboardAsync(Guid? currentUserId, int top = 10);

    // User's quiz history
    Task<List<UserAnswerDto>> GetUserAnswersAsync(Guid userId, Guid? documentId = null);
}