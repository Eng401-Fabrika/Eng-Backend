namespace Eng_Backend.DAL.Entities;

public class Document : IEntity<Document>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string FileUrl { get; set; } = string.Empty;
    public string FileKey { get; set; } = string.Empty; // S3 object key
    public string FileName { get; set; } = string.Empty;
    public string? ContentType { get; set; }
    public long? FileSizeBytes { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Created by user
    public Guid CreatedByUserId { get; set; }
    public ApplicationUser CreatedByUser { get; set; } = null!;

    // Role-based access: which roles can access this document
    public ICollection<DocumentRole> DocumentRoles { get; set; } = new List<DocumentRole>();
}

// Junction table for Document-Role many-to-many relationship
public class DocumentRole
{
    public Guid DocumentId { get; set; }
    public Document Document { get; set; } = null!;

    public Guid RoleId { get; set; }
    public ApplicationRole Role { get; set; } = null!;

    public DateTime AssignedAt { get; set; }
}
