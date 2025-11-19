using Eng_Backend.DAL.Entities;

namespace Eng_Backend.DAL.Interfaces;

public interface IRoleDal : IGenericDal<ApplicationRole>
{
    Task<ApplicationRole?> GetByNameAsync(string name);
    Task<ApplicationRole?> GetRoleWithPermissionsAsync(Guid roleId);
    Task<bool> AssignPermissionsToRoleAsync(Guid roleId, List<Guid> permissionIds);
    Task<bool> RemovePermissionsFromRoleAsync(Guid roleId, List<Guid> permissionIds);
    Task<List<ApplicationRole>> GetNonSystemRolesAsync();
}
