namespace Eng_Backend.DAL.Entities;

/// <summary>
/// Junction table for many-to-many relationship between Roles and Permissions
/// </summary>
public class RolePermission
{
    public Guid RoleId { get; set; }
    
    public Guid PermissionId { get; set; }
    
    public DateTime GrantedAt { get; set; }
    
    // Navigation properties
    public virtual ApplicationRole Role { get; set; } = null!;
    
    public virtual Permission Permission { get; set; } = null!;
}
