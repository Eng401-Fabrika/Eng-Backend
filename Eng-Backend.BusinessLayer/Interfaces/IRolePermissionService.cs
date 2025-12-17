using Eng_Backend.DAL.Entities;
using Eng_Backend.DtoLayer.Roles;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IRolePermissionService
{
    Task<List<RoleDto>> GetRolesAsync();
    Task<List<PermissionsByCategoryDto>> GetPermissionsAsync();
    Task<RoleDto> GetRoleByIdAsync(Guid roleId);
    Task<RoleDto> CreateRoleAsync(CreateRoleDto dto);
    Task<RoleDto> UpdateRolePermissionsAsync(Guid roleId, UpdateRolePermissionsDto dto);
    Task DeleteRoleAsync(Guid roleId);
    Task<bool> AssignPermissionAsync(Guid roleId, Guid permissionId);
    Task<bool> RemovePermissionAsync(Guid roleId, Guid permissionId);
}