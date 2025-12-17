namespace Eng_Backend.DtoLayer.Problems;

// ============ Problem DTOs ============

public class CreateProblemDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Priority { get; set; } = 2; // Medium
    public DateTime? DueDate { get; set; }
    public List<Guid>? AssignToUserIds { get; set; }
}

public class UpdateProblemDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Priority { get; set; }
    public int? Status { get; set; }
    public DateTime? DueDate { get; set; }
}

public class ProblemListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public int AssignmentCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
}

public class ProblemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public List<AssignmentDto> Assignments { get; set; } = new();
}

// ============ Assignment DTOs ============

public class AssignProblemDto
{
    public Guid ProblemId { get; set; }
    public List<Guid> UserIds { get; set; } = new();
    public DateTime? DueDate { get; set; }
}

public class AssignmentDto
{
    public Guid Id { get; set; }
    public Guid AssignedToUserId { get; set; }
    public string AssignedToUserName { get; set; } = string.Empty;
    public string AssignedToUserEmail { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsNotified { get; set; }
    public int SolutionCount { get; set; }
}

public class MyAssignmentDto
{
    public Guid AssignmentId { get; set; }
    public Guid ProblemId { get; set; }
    public string ProblemTitle { get; set; } = string.Empty;
    public string ProblemDescription { get; set; } = string.Empty;
    public string ProblemPriority { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime AssignedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public string AssignedByUserName { get; set; } = string.Empty;
}

// ============ Solution DTOs ============

public class SubmitSolutionDto
{
    public string Description { get; set; } = string.Empty;
}

public class ReviewSolutionDto
{
    public bool Approve { get; set; }
    public string? ReviewNotes { get; set; }
    public int PointsAwarded { get; set; } = 0;
}

public class SolutionDto
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; set; }
    public Guid? ReviewedByUserId { get; set; }
    public string? ReviewedByUserName { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? ReviewNotes { get; set; }
    public int PointsAwarded { get; set; }
    public List<SolutionFileDto> Files { get; set; } = new();
}

public class SolutionFileDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FileUrl { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public DateTime UploadedAt { get; set; }
}