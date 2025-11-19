using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.DAL.Repositories;

public class RoleRepository : GenericRepository<ApplicationRole>, IRoleDal
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ApplicationRole?> GetByNameAsync(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name);
    }

    public async Task<ApplicationRole?> GetRoleWithPermissionsAsync(Guid roleId)
    {
        return await _context.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == roleId);
    }

    public async Task<bool> AssignPermissionsToRoleAsync(Guid roleId, List<Guid> permissionIds)
    {
        try
        {
            var rolePermissions = permissionIds.Select(permissionId => new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                GrantedAt = DateTime.UtcNow
            }).ToList();

            await _context.RolePermissions.AddRangeAsync(rolePermissions);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemovePermissionsFromRoleAsync(Guid roleId, List<Guid> permissionIds)
    {
        try
        {
            var rolePermissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == roleId && permissionIds.Contains(rp.PermissionId))
                .ToListAsync();

            _context.RolePermissions.RemoveRange(rolePermissions);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<ApplicationRole>> GetNonSystemRolesAsync()
    {
        return await _context.Roles
            .Where(r => !r.IsSystemRole)
            .OrderBy(r => r.Name)
            .ToListAsync();
    }
}
