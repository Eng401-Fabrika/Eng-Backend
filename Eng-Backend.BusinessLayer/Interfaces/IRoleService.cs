namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IRoleService
{
    Task<bool> AssignRoleAsync(string userId, string roleName);
    Task<bool> RemoveRoleAsync(string userId, string roleName);
    Task<List<string>> GetUserRolesAsync(string userId);
    Task<List<string>> GetAllRolesAsync();
}