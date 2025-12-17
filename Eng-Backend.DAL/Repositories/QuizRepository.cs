using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.DAL.Repositories;

public class QuestionRepository : GenericRepository<Question>, IQuestionDal
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetByDocumentIdAsync(Guid documentId, bool activeOnly = true)
    {
        var query = _context.Questions
            .Include(q => q.Document)
            .Where(q => q.DocumentId == documentId);

        if (activeOnly)
            query = query.Where(q => q.IsActive);

        return await query.OrderBy(q => q.Difficulty).ToListAsync();
    }

    public async Task<List<Question>> GetUnansweredByUserAsync(Guid documentId, Guid userId, int count)
    {
        var answeredQuestionIds = await _context.UserAnswers
            .Where(ua => ua.UserId == userId)
            .Select(ua => ua.QuestionId)
            .ToListAsync();

        return await _context.Questions
            .Include(q => q.Document)
            .Where(q => q.DocumentId == documentId && q.IsActive && !answeredQuestionIds.Contains(q.Id))
            .OrderBy(q => q.Difficulty)
            .Take(count)
            .ToListAsync();
    }

    public async Task<Question?> GetByIdWithDocumentAsync(Guid id)
    {
        return await _context.Questions
            .Include(q => q.Document)
            .FirstOrDefaultAsync(q => q.Id == id);
    }
}

public class UserAnswerRepository : GenericRepository<UserAnswer>, IUserAnswerDal
{
    private readonly AppDbContext _context;

    public UserAnswerRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserAnswer?> GetByUserAndQuestionAsync(Guid userId, Guid questionId)
    {
        return await _context.UserAnswers
            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.QuestionId == questionId);
    }

    public async Task<List<UserAnswer>> GetByUserIdAsync(Guid userId, Guid? documentId = null)
    {
        var query = _context.UserAnswers
            .Include(ua => ua.Question)
            .Where(ua => ua.UserId == userId);

        if (documentId.HasValue)
            query = query.Where(ua => ua.Question.DocumentId == documentId.Value);

        return await query.OrderByDescending(ua => ua.AnsweredAt).ToListAsync();
    }

    public async Task<List<Guid>> GetAnsweredQuestionIdsByUserAsync(Guid userId)
    {
        return await _context.UserAnswers
            .Where(ua => ua.UserId == userId)
            .Select(ua => ua.QuestionId)
            .ToListAsync();
    }
}

public class UserScoreRepository : GenericRepository<UserScore>, IUserScoreDal
{
    private readonly AppDbContext _context;

    public UserScoreRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserScore?> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserScores
            .Include(us => us.User)
            .FirstOrDefaultAsync(us => us.UserId == userId);
    }

    public async Task<List<UserScore>> GetTopScoresAsync(int count)
    {
        return await _context.UserScores
            .Include(us => us.User)
            .OrderByDescending(us => us.TotalPoints)
            .Take(count)
            .ToListAsync();
    }

    public async Task<int> GetUserRankAsync(Guid userId)
    {
        var userScore = await _context.UserScores.FirstOrDefaultAsync(us => us.UserId == userId);
        if (userScore == null)
            return 0;

        return await _context.UserScores
            .CountAsync(us => us.TotalPoints > userScore.TotalPoints) + 1;
    }
}