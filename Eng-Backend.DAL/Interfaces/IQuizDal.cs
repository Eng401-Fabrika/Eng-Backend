using Eng_Backend.DAL.Entities;

namespace Eng_Backend.DAL.Interfaces;

public interface IQuestionDal : IGenericDal<Question>
{
    Task<List<Question>> GetByDocumentIdAsync(Guid documentId, bool activeOnly = true);
    Task<List<Question>> GetUnansweredByUserAsync(Guid documentId, Guid userId, int count);
    Task<Question?> GetByIdWithDocumentAsync(Guid id);
}

public interface IUserAnswerDal : IGenericDal<UserAnswer>
{
    Task<UserAnswer?> GetByUserAndQuestionAsync(Guid userId, Guid questionId);
    Task<List<UserAnswer>> GetByUserIdAsync(Guid userId, Guid? documentId = null);
    Task<List<Guid>> GetAnsweredQuestionIdsByUserAsync(Guid userId);
}

public interface IUserScoreDal : IGenericDal<UserScore>
{
    Task<UserScore?> GetByUserIdAsync(Guid userId);
    Task<List<UserScore>> GetTopScoresAsync(int count);
    Task<int> GetUserRankAsync(Guid userId);
}