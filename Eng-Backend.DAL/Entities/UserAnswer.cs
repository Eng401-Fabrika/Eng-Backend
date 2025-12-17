namespace Eng_Backend.DAL.Entities;

public class UserAnswer
{
    public Guid Id { get; set; }

    public Guid QuestionId { get; set; }

    public Guid UserId { get; set; }

    /// <summary>
    /// Index of the selected answer (0-3)
    /// </summary>
    public int SelectedAnswerIndex { get; set; }

    public bool IsCorrect { get; set; }

    public int PointsEarned { get; set; }

    public DateTime AnsweredAt { get; set; }

    // Navigation properties
    public virtual Question Question { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;
}