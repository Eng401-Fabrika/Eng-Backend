namespace Eng_Backend.DtoLayer.Quiz;

// ============ Question DTOs ============

public class QuestionDto
{
    public Guid Id { get; set; }
    public Guid DocumentId { get; set; }
    public string DocumentTitle { get; set; } = string.Empty;
    public string QuestionText { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public string Difficulty { get; set; } = string.Empty;
    public int Points { get; set; }
}

public class QuestionWithAnswerDto : QuestionDto
{
    public int CorrectAnswerIndex { get; set; }
}

public class CreateQuestionDto
{
    public Guid DocumentId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public int CorrectAnswerIndex { get; set; }
    public int Points { get; set; } = 10;
    public int Difficulty { get; set; } = 2; // Medium
}

// ============ Answer DTOs ============

public class SubmitAnswerDto
{
    public Guid QuestionId { get; set; }
    public int SelectedAnswerIndex { get; set; }
}

public class AnswerResultDto
{
    public Guid QuestionId { get; set; }
    public bool IsCorrect { get; set; }
    public int PointsEarned { get; set; }
    public int CorrectAnswerIndex { get; set; }
    public string CorrectAnswer { get; set; } = string.Empty;
}

public class UserAnswerDto
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    public int SelectedAnswerIndex { get; set; }
    public string SelectedAnswer { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public int PointsEarned { get; set; }
    public DateTime AnsweredAt { get; set; }
}

// ============ Score/Leaderboard DTOs ============

public class UserScoreDto
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int QuizPoints { get; set; }
    public int TaskPoints { get; set; }
    public int TotalPoints { get; set; }
    public int QuizzesCompleted { get; set; }
    public int TasksCompleted { get; set; }
    public int Level { get; set; }
    public int Rank { get; set; }
}

public class LeaderboardDto
{
    public List<UserScoreDto> Rankings { get; set; } = new();
    public UserScoreDto? CurrentUserRanking { get; set; }
}

// ============ Quiz Session DTOs ============

public class StartQuizDto
{
    public Guid DocumentId { get; set; }
    public int QuestionCount { get; set; } = 5;
}

public class QuizSessionDto
{
    public Guid DocumentId { get; set; }
    public string DocumentTitle { get; set; } = string.Empty;
    public List<QuestionDto> Questions { get; set; } = new();
    public int TotalQuestions { get; set; }
}

public class QuizResultDto
{
    public int TotalQuestions { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalPointsEarned { get; set; }
    public double PercentageScore { get; set; }
    public List<AnswerResultDto> Answers { get; set; } = new();
}