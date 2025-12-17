using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Eng_Backend.BusinessLayer.Managers;

public class RoleManager : IRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleManager(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AssignRoleAsync(string userId, string roleName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "User"));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new BadRequestException(ErrorMessages.RoleNameRequired);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException(string.Format(ErrorMessages.UserNotFound, userId));

            if (!await _roleManager.RoleExistsAsync(roleName))
                throw new NotFoundException(string.Format(ErrorMessages.RoleNotFoundByName, roleName));

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException("Failed to assign role", errors);
            }

            return result.Succeeded;
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

    public async Task<bool> RemoveRoleAsync(string userId, string roleName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "User"));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new BadRequestException(ErrorMessages.RoleNameRequired);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException(string.Format(ErrorMessages.UserNotFound, userId));

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException("Failed to remove role", errors);
            }

            return result.Succeeded;
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

    public async Task<List<string>> GetUserRolesAsync(string userId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadRequestException(string.Format(ErrorMessages.IdRequired, "User"));

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new NotFoundException(string.Format(ErrorMessages.UserNotFound, userId));

            return (await _userManager.GetRolesAsync(user)).ToList();
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

    public async Task<List<string>> GetAllRolesAsync()
    {
        try
        {
            return await Task.FromResult(_roleManager.Roles.Select(r => r.Name).ToList());
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }
}