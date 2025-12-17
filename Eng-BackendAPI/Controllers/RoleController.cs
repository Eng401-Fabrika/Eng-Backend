using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Auth;
using Eng_Backend.DtoLayer.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole(AssignRoleDto dto)
    {
        // Service layer will throw exceptions if validation fails
        await _roleService.AssignRoleAsync(dto.UserId, dto.RoleName);
        return Ok(ApiResponse.SuccessResponse("Role assigned successfully"));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRole(RemoveRoleDto dto)
    {
        // Service layer will throw exceptions if validation fails
        await _roleService.RemoveRoleAsync(dto.UserId, dto.RoleName);
        return Ok(ApiResponse.SuccessResponse("Role removed successfully"));
    }

    [HttpGet("user-roles/{userId}")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        // Service layer will throw exceptions if validation fails
        var roles = await _roleService.GetUserRolesAsync(userId);
        return Ok(ApiResponse<List<string>>.SuccessResponse(roles, "User roles retrieved successfully"));
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        // Service layer will throw exceptions if validation fails
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(ApiResponse<List<string>>.SuccessResponse(roles, "Roles retrieved successfully"));
    }
}