using Eng_Backend.DAL.Entities;

namespace Eng_Backend.DAL.Interfaces;

public interface IProblemDal : IGenericDal<Problem>
{
    Task<List<Problem>> GetAllWithDetailsAsync();
    Task<Problem?> GetByIdWithDetailsAsync(Guid id);
}

public interface IProblemAssignmentDal : IGenericDal<ProblemAssignment>
{
    Task<List<ProblemAssignment>> GetByUserIdAsync(Guid userId);
    Task<ProblemAssignment?> GetByProblemAndUserAsync(Guid problemId, Guid userId);
    Task<ProblemAssignment?> GetByIdWithDetailsAsync(Guid id);
}

public interface ISolutionDal : IGenericDal<Solution>
{
    Task<List<Solution>> GetByAssignmentIdAsync(Guid assignmentId);
    Task<Solution?> GetByIdWithDetailsAsync(Guid id);
}

public interface ISolutionFileDal : IGenericDal<SolutionFile>
{
    Task<List<SolutionFile>> GetBySolutionIdAsync(Guid solutionId);
}