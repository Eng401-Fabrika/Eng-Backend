using Eng_Backend.DAL.Entities;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IRolePermissionService
{
    Task<List<ApplicationRole>> GetRolesAsync();
    Task<List<Permission>> GetPermissionsAsync();
    Task<List<Permission>> GetPermissionsByRoleAsync(Guid roleId);
    Task<bool> AssignPermissionAsync(Guid roleId, Guid permissionId);
    Task<bool> RemovePermissionAsync(Guid roleId, Guid permissionId);
}