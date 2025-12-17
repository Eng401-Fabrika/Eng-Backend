using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.DAL.Repositories;

public class ProblemRepository : GenericRepository<Problem>, IProblemDal
{
    private readonly AppDbContext _context;

    public ProblemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Problem>> GetAllWithDetailsAsync()
    {
        return await _context.Problems
            .Include(p => p.CreatedByUser)
            .Include(p => p.Assignments)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Problem?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Problems
            .Include(p => p.CreatedByUser)
            .Include(p => p.Assignments)
                .ThenInclude(a => a.AssignedToUser)
            .Include(p => p.Assignments)
                .ThenInclude(a => a.Solutions)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}

public class ProblemAssignmentRepository : GenericRepository<ProblemAssignment>, IProblemAssignmentDal
{
    private readonly AppDbContext _context;

    public ProblemAssignmentRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ProblemAssignment>> GetByUserIdAsync(Guid userId)
    {
        return await _context.ProblemAssignments
            .Include(a => a.Problem)
            .Include(a => a.AssignedByUser)
            .Where(a => a.AssignedToUserId == userId)
            .OrderByDescending(a => a.AssignedAt)
            .ToListAsync();
    }

    public async Task<ProblemAssignment?> GetByProblemAndUserAsync(Guid problemId, Guid userId)
    {
        return await _context.ProblemAssignments
            .FirstOrDefaultAsync(a => a.ProblemId == problemId && a.AssignedToUserId == userId);
    }

    public async Task<ProblemAssignment?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.ProblemAssignments
            .Include(a => a.Problem)
            .Include(a => a.AssignedToUser)
            .Include(a => a.AssignedByUser)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}

public class SolutionRepository : GenericRepository<Solution>, ISolutionDal
{
    private readonly AppDbContext _context;

    public SolutionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<Solution>> GetByAssignmentIdAsync(Guid assignmentId)
    {
        return await _context.Solutions
            .Include(s => s.ReviewedByUser)
            .Include(s => s.Files)
            .Where(s => s.AssignmentId == assignmentId)
            .OrderByDescending(s => s.SubmittedAt)
            .ToListAsync();
    }

    public async Task<Solution?> GetByIdWithDetailsAsync(Guid id)
    {
        return await _context.Solutions
            .Include(s => s.Assignment)
                .ThenInclude(a => a.Problem)
            .Include(s => s.Assignment)
                .ThenInclude(a => a.AssignedToUser)
            .Include(s => s.ReviewedByUser)
            .Include(s => s.Files)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}

public class SolutionFileRepository : GenericRepository<SolutionFile>, ISolutionFileDal
{
    private readonly AppDbContext _context;

    public SolutionFileRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<SolutionFile>> GetBySolutionIdAsync(Guid solutionId)
    {
        return await _context.SolutionFiles
            .Where(f => f.SolutionId == solutionId)
            .OrderByDescending(f => f.UploadedAt)
            .ToListAsync();
    }
}