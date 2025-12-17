namespace Eng_Backend.DAL.Entities;

public class Problem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Low = 1, Medium = 2, High = 3, Critical = 4
    /// </summary>
    public ProblemPriority Priority { get; set; } = ProblemPriority.Medium;

    /// <summary>
    /// Open, InProgress, Resolved, Closed
    /// </summary>
    public ProblemStatus Status { get; set; } = ProblemStatus.Open;

    public DateTime? DueDate { get; set; }

    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public virtual ApplicationUser CreatedByUser { get; set; } = null!;
    public virtual ICollection<ProblemAssignment> Assignments { get; set; } = new List<ProblemAssignment>();
}

public enum ProblemPriority
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

public enum ProblemStatus
{
    Open = 1,
    InProgress = 2,
    Resolved = 3,
    Closed = 4
}