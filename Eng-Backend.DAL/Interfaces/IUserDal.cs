using Eng_Backend.DAL.Entities;

namespace Eng_Backend.DAL.Interfaces;

public interface IUserDal : IGenericDal<ApplicationUser>
{
    Task<ApplicationUser?> GetByEmailAsync(string email);
    Task<ApplicationUser?> GetByUsernameAsync(string username);
    Task<List<ApplicationUser>> GetUsersByRoleAsync(string roleName);
}
