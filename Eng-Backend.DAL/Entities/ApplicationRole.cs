using Microsoft.AspNetCore.Identity;

namespace Eng_Backend.DAL.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    /// <summary>
    /// Description of the role
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Indicates if this is a system role (Admin, User) that cannot be deleted
    /// </summary>
    public bool IsSystemRole { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    // Navigation property for permissions
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
