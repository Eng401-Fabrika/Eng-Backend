using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Auth;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
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
        var result = await _roleService.AssignRoleAsync(dto.UserId, dto.RoleName);
        return result ? Ok("Role assigned") : BadRequest("Failed");
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRole(RemoveRoleDto dto)
    {
        var result = await _roleService.RemoveRoleAsync(dto.UserId, dto.RoleName);
        return result ? Ok("Role removed") : BadRequest("Failed");
    }

    [HttpGet("user-roles/{userId}")]
    public async Task<IActionResult> GetUserRoles(string userId)
    {
        var roles = await _roleService.GetUserRolesAsync(userId);
        return Ok(roles);
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }
}