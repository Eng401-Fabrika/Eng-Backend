using Microsoft.AspNetCore.Identity;

namespace Eng_Backend.DAL.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    
    /// <summary>
    /// Profile image URL (optional)
    /// </summary>
    public string? ProfileImageUrl { get; set; }
    
    /// <summary>
    /// User is active or deactivated
    /// </summary>
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
    
    /// <summary>
    /// Last login timestamp
    /// </summary>
    public DateTime? LastLoginAt { get; set; }

}
