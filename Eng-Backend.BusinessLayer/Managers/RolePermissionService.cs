using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Microsoft.EntityFrameworkCore;

public class RolePermissionService : IRolePermissionService
{
    private readonly AppDbContext _context;

    public RolePermissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ApplicationRole>> GetRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<List<Permission>> GetPermissionsAsync()
    {
        return await _context.Permissions.ToListAsync();
    }

    public async Task<List<Permission>> GetPermissionsByRoleAsync(Guid roleId)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }

    public async Task<bool> AssignPermissionAsync(Guid roleId, Guid permissionId)
    {
        var exists = await _context.RolePermissions
            .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

        if (exists)
            return false;

        await _context.RolePermissions.AddAsync(new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId,
            GrantedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemovePermissionAsync(Guid roleId, Guid permissionId)
    {
        var rp = await _context.RolePermissions
            .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

        if (rp == null)
            return false;

        _context.RolePermissions.Remove(rp);
        await _context.SaveChangesAsync();
        return true;
    }
}