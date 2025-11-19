namespace Eng_Backend.DAL.Entities;

public class Permission
{
    public Guid Id { get; set; }
    
    /// <summary>
    /// Unique permission key (e.g., "Users.Create", "Posts.Delete")
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Display name for UI (e.g., "Kullanıcı Oluşturma")
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;
    
    /// <summary>
    /// Category/Module (e.g., "Users", "Posts", "Settings")
    /// </summary>
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of what this permission allows
    /// </summary>
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    // Navigation property
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
