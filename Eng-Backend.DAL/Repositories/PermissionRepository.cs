using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.DAL.Repositories;

public class PermissionRepository : GenericRepository<Permission>, IPermissionDal
{
    private readonly AppDbContext _context;

    public PermissionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByNameAsync(string name)
    {
        return await _context.Permissions
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<List<Permission>> GetByCategoryAsync(string category)
    {
        return await _context.Permissions
            .Where(p => p.Category == category)
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<Permission>> GetPermissionsByRoleIdAsync(Guid roleId)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Include(rp => rp.Permission)
            .Select(rp => rp.Permission)
            .ToListAsync();
    }
}
