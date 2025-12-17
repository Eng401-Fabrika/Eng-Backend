namespace Eng_Backend.DAL.Entities;

public class Solution
{
    public Guid Id { get; set; }

    public Guid AssignmentId { get; set; }

    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Submitted, UnderReview, Approved, Rejected
    /// </summary>
    public SolutionStatus Status { get; set; } = SolutionStatus.Submitted;

    public DateTime SubmittedAt { get; set; }

    public Guid? ReviewedByUserId { get; set; }

    public DateTime? ReviewedAt { get; set; }

    public string? ReviewNotes { get; set; }

    /// <summary>
    /// Points awarded for this solution (0-100)
    /// </summary>
    public int PointsAwarded { get; set; } = 0;

    // Navigation properties
    public virtual ProblemAssignment Assignment { get; set; } = null!;
    public virtual ApplicationUser? ReviewedByUser { get; set; }
    public virtual ICollection<SolutionFile> Files { get; set; } = new List<SolutionFile>();
}

public enum SolutionStatus
{
    Submitted = 1,
    UnderReview = 2,
    Approved = 3,
    Rejected = 4
}