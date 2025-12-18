using Eng_Backend.BusinessLayer.Constants;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DAL.DbContext;
using Eng_Backend.DtoLayer.Navigation;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.BusinessLayer.Managers;

public class NavigationService : INavigationService
{
    private readonly AppDbContext _context;

    public NavigationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<NavigationGroupDto>> GetNavigationAsync(IReadOnlyCollection<string> roles)
    {
        try
        {
            var roleSet = new HashSet<string>(
                roles.Where(r => !string.IsNullOrWhiteSpace(r)),
                StringComparer.OrdinalIgnoreCase);

            var permissionCategorySet = await GetPermissionCategoriesForRolesAsync(roleSet);

            var visibleItems = NavDefinitions
                .Where(d => IsAllowed(d, roleSet, permissionCategorySet))
                .ToList();

            return visibleItems
                .GroupBy(d => new { d.GroupId, d.GroupLabel, d.GroupOrder })
                .OrderBy(g => g.Key.GroupOrder)
                .Select(g => new NavigationGroupDto
                {
                    Id = g.Key.GroupId,
                    Label = g.Key.GroupLabel,
                    Order = g.Key.GroupOrder,
                    Items = g.OrderBy(i => i.ItemOrder)
                        .Select(i => new NavigationItemDto
                        {
                            Id = i.Id,
                            Label = i.Label,
                            Path = i.Path,
                            Order = i.ItemOrder
                        })
                        .ToList()
                })
                .ToList();
        }
        catch (Exception ex)
        {
            throw new InternalServerException(string.Format(ErrorMessages.InternalErrorWithDetails, ex.Message));
        }
    }

    private async Task<HashSet<string>> GetPermissionCategoriesForRolesAsync(HashSet<string> roleNames)
    {
        if (roleNames.Count == 0)
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var roleIds = await _context.Roles
            .Where(r => r.Name != null && roleNames.Contains(r.Name))
            .Select(r => r.Id)
            .ToListAsync();

        if (roleIds.Count == 0)
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        var categories = await _context.RolePermissions
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.Permission.Category)
            .Distinct()
            .ToListAsync();

        return new HashSet<string>(
            categories.Where(c => !string.IsNullOrWhiteSpace(c)),
            StringComparer.OrdinalIgnoreCase);
    }

    private static bool IsAllowed(
        NavDefinition definition,
        HashSet<string> roleSet,
        HashSet<string> permissionCategorySet)
    {
        if (definition.RequiredRoles is { Length: > 0 } &&
            !definition.RequiredRoles.Any(roleSet.Contains))
            return false;

        if (definition.RequiredPermissionCategories is { Length: > 0 } &&
            !definition.RequiredPermissionCategories.Any(permissionCategorySet.Contains))
            return false;

        return true;
    }

    private sealed record NavDefinition(
        string GroupId,
        string GroupLabel,
        int GroupOrder,
        string Id,
        string Label,
        string Path,
        int ItemOrder,
        string[]? RequiredRoles = null,
        string[]? RequiredPermissionCategories = null
    );

    private static readonly List<NavDefinition> NavDefinitions =
    [
        new NavDefinition(
            GroupId: "general",
            GroupLabel: "General",
            GroupOrder: 0,
            Id: "dashboard",
            Label: "Dashboard",
            Path: "/admin/dashboard",
            ItemOrder: 0),

        new NavDefinition(
            GroupId: "workspace",
            GroupLabel: "Workspace",
            GroupOrder: 10,
            Id: "my-documents",
            Label: "My Documents",
            Path: "/admin/my-documents",
            ItemOrder: 10),
        new NavDefinition(
            GroupId: "workspace",
            GroupLabel: "Workspace",
            GroupOrder: 10,
            Id: "my-assignments",
            Label: "My Assignments",
            Path: "/admin/my-assignments",
            ItemOrder: 20),
        new NavDefinition(
            GroupId: "workspace",
            GroupLabel: "Workspace",
            GroupOrder: 10,
            Id: "quiz",
            Label: "Quiz",
            Path: "/admin/quiz",
            ItemOrder: 30),
        new NavDefinition(
            GroupId: "workspace",
            GroupLabel: "Workspace",
            GroupOrder: 10,
            Id: "leaderboard",
            Label: "Leaderboard",
            Path: "/admin/leaderboard",
            ItemOrder: 40),

        new NavDefinition(
            GroupId: "admin",
            GroupLabel: "Administration",
            GroupOrder: 20,
            Id: "users",
            Label: "Users",
            Path: "/admin/users",
            ItemOrder: 10,
            RequiredPermissionCategories: ["Users"]),
        new NavDefinition(
            GroupId: "admin",
            GroupLabel: "Administration",
            GroupOrder: 20,
            Id: "roles",
            Label: "Roles",
            Path: "/admin/roles",
            ItemOrder: 20,
            RequiredPermissionCategories: ["Roles"]),
        new NavDefinition(
            GroupId: "admin",
            GroupLabel: "Administration",
            GroupOrder: 20,
            Id: "permissions",
            Label: "Permissions",
            Path: "/admin/permissions",
            ItemOrder: 30,
            RequiredPermissionCategories: ["Permissions"]),

        new NavDefinition(
            GroupId: "content",
            GroupLabel: "Content",
            GroupOrder: 30,
            Id: "documents",
            Label: "Documents",
            Path: "/admin/documents",
            ItemOrder: 10,
            RequiredRoles: ["Admin"]),
        new NavDefinition(
            GroupId: "content",
            GroupLabel: "Content",
            GroupOrder: 30,
            Id: "problems",
            Label: "Problems",
            Path: "/admin/problems",
            ItemOrder: 20,
            RequiredRoles: ["Admin"]),
        new NavDefinition(
            GroupId: "content",
            GroupLabel: "Content",
            GroupOrder: 30,
            Id: "quiz-questions",
            Label: "Quiz Questions",
            Path: "/admin/quiz-questions",
            ItemOrder: 30,
            RequiredRoles: ["Admin"]),
    ];
}

