using Eng_Backend.DAL.Entities;

namespace Eng_Backend.DAL.Interfaces;

public interface IPermissionDal : IGenericDal<Permission>
{
    Task<Permission?> GetByNameAsync(string name);
    Task<List<Permission>> GetByCategoryAsync(string category);
    Task<List<Permission>> GetPermissionsByRoleIdAsync(Guid roleId);
}
