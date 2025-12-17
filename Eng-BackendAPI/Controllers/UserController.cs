using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Common;
using Eng_Backend.DtoLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(ApiResponse<List<UserListDto>>.SuccessResponse(users, "Users retrieved successfully"));
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);
        return Ok(ApiResponse<UserDto>.SuccessResponse(user, "User retrieved successfully"));
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserDto dto)
    {
        var user = await _userService.UpdateUserAsync(userId, dto);
        return Ok(ApiResponse<UserDto>.SuccessResponse(user, "User updated successfully"));
    }

    [HttpPatch("{userId:guid}/status")]
    public async Task<IActionResult> ToggleUserStatus(Guid userId, ToggleUserStatusDto dto)
    {
        await _userService.SetUserActiveStatusAsync(userId, dto.IsActive);
        var status = dto.IsActive ? "activated" : "deactivated";
        return Ok(ApiResponse.SuccessResponse($"User {status} successfully"));
    }

    [HttpPost("{userId:guid}/roles")]
    public async Task<IActionResult> AssignRoles(Guid userId, [FromBody] List<string> roles)
    {
        await _userService.AssignRolesToUserAsync(userId, roles);
        return Ok(ApiResponse.SuccessResponse("Roles assigned successfully"));
    }

    [HttpDelete("{userId:guid}/roles")]
    public async Task<IActionResult> RemoveRoles(Guid userId, [FromBody] List<string> roles)
    {
        await _userService.RemoveRolesFromUserAsync(userId, roles);
        return Ok(ApiResponse.SuccessResponse("Roles removed successfully"));
    }
}
