using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DtoLayer.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.BusinessLayer.Managers;

public class RolePermissionService : IRolePermissionService
{
    private readonly AppDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RolePermissionService(AppDbContext context, RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<List<RoleDto>> GetRolesAsync()
    {
        try
        {
            var roles = await _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .ToListAsync();

            return roles.Select(r => MapToRoleDto(r)).ToList();
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task<List<PermissionsByCategoryDto>> GetPermissionsAsync()
    {
        try
        {
            var permissions = await _context.Permissions.ToListAsync();

            return permissions
                .GroupBy(p => p.Category)
                .Select(g => new PermissionsByCategoryDto
                {
                    Category = g.Key,
                    Permissions = g.Select(p => new PermissionDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        DisplayName = p.DisplayName,
                        Category = p.Category,
                        Description = p.Description
                    }).ToList()
                })
                .OrderBy(c => c.Category)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task<RoleDto> GetRoleByIdAsync(Guid roleId)
    {
        try
        {
            if (roleId == Guid.Empty)
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "Role"));

            var role = await _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
                throw new NotFoundException(string.Format(ErrorMessages.RoleNotFound, roleId));

            return MapToRoleDto(role);
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task<RoleDto> CreateRoleAsync(CreateRoleDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BadRequestException(ErrorMessages.RoleNameRequired);

            var existingRole = await _roleManager.FindByNameAsync(dto.Name);
            if (existingRole != null)
                throw new BadRequestException(string.Format(ErrorMessages.RoleAlreadyExists, dto.Name));

            if (dto.PermissionIds.Any())
            {
                var validPermissionIds = await _context.Permissions
                    .Where(p => dto.PermissionIds.Contains(p.Id))
                    .Select(p => p.Id)
                    .ToListAsync();

                var invalidIds = dto.PermissionIds.Except(validPermissionIds).ToList();
                if (invalidIds.Any())
                    throw new BadRequestException(string.Format(ErrorMessages.InvalidPermissionIds, string.Join(", ", invalidIds)));
            }

            var role = new ApplicationRole
            {
                Name = dto.Name,
                NormalizedName = dto.Name.ToUpperInvariant(),
                Description = dto.Description,
                IsSystemRole = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(ErrorMessages.RoleCreationFailed, errors);
            }

            if (dto.PermissionIds.Any())
            {
                var rolePermissions = dto.PermissionIds.Select(permissionId => new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permissionId,
                    GrantedAt = DateTime.UtcNow
                }).ToList();

                await _context.RolePermissions.AddRangeAsync(rolePermissions);
                await _context.SaveChangesAsync();
            }

            return await GetRoleByIdAsync(role.Id);
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task<RoleDto> UpdateRolePermissionsAsync(Guid roleId, UpdateRolePermissionsDto dto)
    {
        try
        {
            if (roleId == Guid.Empty)
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "Role"));

            var role = await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
                throw new NotFoundException(string.Format(ErrorMessages.RoleNotFound, roleId));

            if (dto.PermissionIds.Any())
            {
                var validPermissionIds = await _context.Permissions
                    .Where(p => dto.PermissionIds.Contains(p.Id))
                    .Select(p => p.Id)
                    .ToListAsync();

                var invalidIds = dto.PermissionIds.Except(validPermissionIds).ToList();
                if (invalidIds.Any())
                    throw new BadRequestException(string.Format(ErrorMessages.InvalidPermissionIds, string.Join(", ", invalidIds)));
            }

            _context.RolePermissions.RemoveRange(role.RolePermissions);

            if (dto.PermissionIds.Any())
            {
                var rolePermissions = dto.PermissionIds.Select(permissionId => new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId,
                    GrantedAt = DateTime.UtcNow
                }).ToList();

                await _context.RolePermissions.AddRangeAsync(rolePermissions);
            }

            role.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return await GetRoleByIdAsync(roleId);
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task DeleteRoleAsync(Guid roleId)
    {
        try
        {
            if (roleId == Guid.Empty)
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "Role"));

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);

            if (role == null)
                throw new NotFoundException(string.Format(ErrorMessages.RoleNotFound, roleId));

            if (role.IsSystemRole)
                throw new BadRequestException(string.Format(ErrorMessages.CannotDeleteSystemRole, role.Name));

            var usersWithRole = await _context.UserRoles.AnyAsync(ur => ur.RoleId == roleId);
            if (usersWithRole)
                throw new BadRequestException(ErrorMessages.CannotDeleteRoleWithUsers);

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(ErrorMessages.RoleDeletionFailed, errors);
            }
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task<bool> AssignPermissionAsync(Guid roleId, Guid permissionId)
    {
        try
        {
            if (roleId == Guid.Empty)
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "Role"));

            if (permissionId == Guid.Empty)
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "Permission"));

            var roleExists = await _context.Roles.AnyAsync(r => r.Id == roleId);
            if (!roleExists)
                throw new NotFoundException(string.Format(ErrorMessages.RoleNotFound, roleId));

            var permissionExists = await _context.Permissions.AnyAsync(p => p.Id == permissionId);
            if (!permissionExists)
                throw new NotFoundException(string.Format(ErrorMessages.PermissionNotFound, permissionId));

            var exists = await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (exists)
                throw new BadRequestException(ErrorMessages.PermissionAlreadyAssigned);

            await _context.RolePermissions.AddAsync(new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId,
                GrantedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return true;
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    public async Task<bool> RemovePermissionAsync(Guid roleId, Guid permissionId)
    {
        try
        {
            if (roleId == Guid.Empty)
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "Role"));

            if (permissionId == Guid.Empty)
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "Permission"));

            var rp = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rp == null)
                throw new NotFoundException(ErrorMessages.PermissionNotAssigned);

            _context.RolePermissions.Remove(rp);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (BadRequestException)
        {
            throw;
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    private static RoleDto MapToRoleDto(ApplicationRole role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty,
            Description = role.Description,
            IsSystemRole = role.IsSystemRole,
            CreatedAt = role.CreatedAt,
            Permissions = role.RolePermissions.Select(rp => new PermissionDto
            {
                Id = rp.Permission.Id,
                Name = rp.Permission.Name,
                DisplayName = rp.Permission.DisplayName,
                Category = rp.Permission.Category,
                Description = rp.Permission.Description
            }).ToList()
        };
    }
}