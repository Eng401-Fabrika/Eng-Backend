using Eng_Backend.DtoLayer.Users;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IUserService
{
    Task<List<UserListDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(Guid userId);
    Task<UserDto> UpdateUserAsync(Guid userId, UpdateUserDto dto);
    Task SetUserActiveStatusAsync(Guid userId, bool isActive);
    Task AssignRolesToUserAsync(Guid userId, List<string> roles);
    Task RemoveRolesFromUserAsync(Guid userId, List<string> roles);
}
