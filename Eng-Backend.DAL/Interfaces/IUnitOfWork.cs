namespace Eng_Backend.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPermissionDal Permissions { get; }
    IRoleDal Roles { get; }
    IUserDal Users { get; }

    // Problem/Task repositories
    IProblemDal Problems { get; }
    IProblemAssignmentDal ProblemAssignments { get; }
    ISolutionDal Solutions { get; }
    ISolutionFileDal SolutionFiles { get; }

    // Quiz repositories
    IQuestionDal Questions { get; }
    IUserAnswerDal UserAnswers { get; }
    IUserScoreDal UserScores { get; }

    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
