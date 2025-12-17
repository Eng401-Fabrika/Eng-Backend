namespace Eng_Backend.DAL.Entities;

public class SolutionFile
{
    public Guid Id { get; set; }

    public Guid SolutionId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FileUrl { get; set; } = string.Empty;

    public string FileKey { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public long FileSizeBytes { get; set; }

    public DateTime UploadedAt { get; set; }

    // Navigation property
    public virtual Solution Solution { get; set; } = null!;
}