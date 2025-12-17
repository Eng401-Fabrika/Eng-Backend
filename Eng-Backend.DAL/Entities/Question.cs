namespace Eng_Backend.DAL.Entities;

public class Question
{
    public Guid Id { get; set; }

    /// <summary>
    /// The document this question is related to
    /// </summary>
    public Guid DocumentId { get; set; }

    public string QuestionText { get; set; } = string.Empty;

    /// <summary>
    /// JSON array of options: ["Option A", "Option B", "Option C", "Option D"]
    /// </summary>
    public string Options { get; set; } = "[]";

    /// <summary>
    /// Index of the correct answer (0-3)
    /// </summary>
    public int CorrectAnswerIndex { get; set; }

    /// <summary>
    /// Points for correct answer
    /// </summary>
    public int Points { get; set; } = 10;

    /// <summary>
    /// Easy = 1, Medium = 2, Hard = 3
    /// </summary>
    public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Medium;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    // Navigation property
    public virtual Document Document { get; set; } = null!;
    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}

public enum QuestionDifficulty
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}