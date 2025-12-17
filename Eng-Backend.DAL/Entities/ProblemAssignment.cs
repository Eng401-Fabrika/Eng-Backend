namespace Eng_Backend.DAL.Entities;

public class ProblemAssignment
{
    public Guid Id { get; set; }

    public Guid ProblemId { get; set; }

    public Guid AssignedToUserId { get; set; }

    public Guid AssignedByUserId { get; set; }

    public DateTime AssignedAt { get; set; }

    public DateTime? DueDate { get; set; }

    /// <summary>
    /// Pending, Accepted, Completed, Rejected
    /// </summary>
    public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;

    public bool IsNotified { get; set; } = false;

    public DateTime? NotifiedAt { get; set; }

    // Navigation properties
    public virtual Problem Problem { get; set; } = null!;
    public virtual ApplicationUser AssignedToUser { get; set; } = null!;
    public virtual ApplicationUser AssignedByUser { get; set; } = null!;
    public virtual ICollection<Solution> Solutions { get; set; } = new List<Solution>();
}

public enum AssignmentStatus
{
    Pending = 1,
    Accepted = 2,
    Completed = 3,
    Rejected = 4
}