using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DtoLayer.Roles;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionController : ControllerBase
{
    private readonly IRolePermissionService _service;

    public RolePermissionController(IRolePermissionService service)
    {
        _service = service;
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _service.GetRolesAsync();
        return Ok(roles);
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        var permissions = await _service.GetPermissionsAsync();
        return Ok(permissions);
    }

    [HttpGet("roles/{roleId}/permissions")]
    public async Task<IActionResult> GetPermissionsByRole(Guid roleId)
    {
        var list = await _service.GetPermissionsByRoleAsync(roleId);
        return Ok(list);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignPermission(AssignPermissionDto dto)
    {
        var result = await _service.AssignPermissionAsync(dto.RoleId, dto.PermissionId);
        if (!result)
            return BadRequest("Permission already assigned.");

        return Ok("Permission assigned successfully.");
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemovePermission(RemovePermissionDto dto)
    {
        var result = await _service.RemovePermissionAsync(dto.RoleId, dto.PermissionId);
        if (!result)
            return BadRequest("Permission not found on this role.");

        return Ok("Permission removed successfully.");
    }
}