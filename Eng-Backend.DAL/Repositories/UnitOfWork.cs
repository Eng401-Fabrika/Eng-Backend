using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Eng_Backend.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Permissions = new PermissionRepository(_context);
        Roles = new RoleRepository(_context);
        Users = new UserRepository(_context);

        // Problem/Task repositories
        Problems = new ProblemRepository(_context);
        ProblemAssignments = new ProblemAssignmentRepository(_context);
        Solutions = new SolutionRepository(_context);
        SolutionFiles = new SolutionFileRepository(_context);

        // Quiz repositories
        Questions = new QuestionRepository(_context);
        UserAnswers = new UserAnswerRepository(_context);
        UserScores = new UserScoreRepository(_context);
    }

    public IPermissionDal Permissions { get; }
    public IRoleDal Roles { get; }
    public IUserDal Users { get; }

    // Problem/Task repositories
    public IProblemDal Problems { get; }
    public IProblemAssignmentDal ProblemAssignments { get; }
    public ISolutionDal Solutions { get; }
    public ISolutionFileDal SolutionFiles { get; }

    // Quiz repositories
    public IQuestionDal Questions { get; }
    public IUserAnswerDal UserAnswers { get; }
    public IUserScoreDal UserScores { get; }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
