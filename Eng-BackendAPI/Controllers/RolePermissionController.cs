using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Roles;
using Eng_Backend.DtoLayer.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
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
        return Ok(ApiResponse<List<RoleDto>>.SuccessResponse(roles, "Roles retrieved successfully"));
    }

    [HttpGet("roles/{roleId:guid}")]
    public async Task<IActionResult> GetRoleById(Guid roleId)
    {
        var role = await _service.GetRoleByIdAsync(roleId);
        return Ok(ApiResponse<RoleDto>.SuccessResponse(role, "Role retrieved successfully"));
    }

    [HttpPost("roles")]
    public async Task<IActionResult> CreateRole(CreateRoleDto dto)
    {
        var role = await _service.CreateRoleAsync(dto);
        return CreatedAtAction(nameof(GetRoleById), new { roleId = role.Id },
            ApiResponse<RoleDto>.SuccessResponse(role, "Role created successfully"));
    }

    [HttpPut("roles/{roleId:guid}/permissions")]
    public async Task<IActionResult> UpdateRolePermissions(Guid roleId, UpdateRolePermissionsDto dto)
    {
        var role = await _service.UpdateRolePermissionsAsync(roleId, dto);
        return Ok(ApiResponse<RoleDto>.SuccessResponse(role, "Role permissions updated successfully"));
    }

    [HttpDelete("roles/{roleId:guid}")]
    public async Task<IActionResult> DeleteRole(Guid roleId)
    {
        await _service.DeleteRoleAsync(roleId);
        return Ok(ApiResponse.SuccessResponse("Role deleted successfully"));
    }

    [HttpGet("permissions")]
    public async Task<IActionResult> GetPermissions()
    {
        var permissions = await _service.GetPermissionsAsync();
        return Ok(ApiResponse<List<PermissionsByCategoryDto>>.SuccessResponse(permissions, "Permissions retrieved successfully"));
    }

    [HttpPost("roles/{roleId:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> AssignPermission(Guid roleId, Guid permissionId)
    {
        await _service.AssignPermissionAsync(roleId, permissionId);
        return Ok(ApiResponse.SuccessResponse("Permission assigned successfully"));
    }

    [HttpDelete("roles/{roleId:guid}/permissions/{permissionId:guid}")]
    public async Task<IActionResult> RemovePermission(Guid roleId, Guid permissionId)
    {
        await _service.RemovePermissionAsync(roleId, permissionId);
        return Ok(ApiResponse.SuccessResponse("Permission removed successfully"));
    }
}
