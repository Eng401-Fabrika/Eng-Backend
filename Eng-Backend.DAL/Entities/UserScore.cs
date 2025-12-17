namespace Eng_Backend.DAL.Entities;

public class UserScore
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    /// <summary>
    /// Total points from quizzes
    /// </summary>
    public int QuizPoints { get; set; } = 0;

    /// <summary>
    /// Total points from completed tasks/solutions
    /// </summary>
    public int TaskPoints { get; set; } = 0;

    /// <summary>
    /// Total points (Quiz + Task)
    /// </summary>
    public int TotalPoints { get; set; } = 0;

    /// <summary>
    /// Number of quizzes completed
    /// </summary>
    public int QuizzesCompleted { get; set; } = 0;

    /// <summary>
    /// Number of tasks completed
    /// </summary>
    public int TasksCompleted { get; set; } = 0;

    /// <summary>
    /// User level based on total points
    /// </summary>
    public int Level { get; set; } = 1;

    public DateTime UpdatedAt { get; set; }

    // Navigation property
    public virtual ApplicationUser User { get; set; } = null!;
}