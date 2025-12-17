using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DAL.DbContext;
using Eng_Backend.DAL.Entities;
using Eng_Backend.DtoLayer.Documents;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eng_Backend.BusinessLayer.Managers;

public class DocumentService : IDocumentService
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public DocumentService(
        AppDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<DocumentListDto>> GetAllDocumentsAsync()
    {
        var documents = await _context.Documents
            .Include(d => d.DocumentRoles)
                .ThenInclude(dr => dr.Role)
            .Where(d => d.IsActive)
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();

        return documents.Select(d => new DocumentListDto
        {
            Id = d.Id,
            Title = d.Title,
            Description = d.Description,
            FileName = d.FileName,
            IsActive = d.IsActive,
            CreatedAt = d.CreatedAt,
            AssignedRoles = d.DocumentRoles.Select(dr => dr.Role.Name ?? string.Empty).ToList()
        }).ToList();
    }

    public async Task<List<DocumentListDto>> GetDocumentsForUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new NotFoundException($"User with ID '{userId}' not found");

        var userRoles = await _userManager.GetRolesAsync(user);

        var documents = await _context.Documents
            .Include(d => d.DocumentRoles)
                .ThenInclude(dr => dr.Role)
            .Where(d => d.IsActive && d.DocumentRoles.Any(dr => userRoles.Contains(dr.Role.Name!)))
            .OrderByDescending(d => d.CreatedAt)
            .ToListAsync();

        return documents.Select(d => new DocumentListDto
        {
            Id = d.Id,
            Title = d.Title,
            Description = d.Description,
            FileName = d.FileName,
            IsActive = d.IsActive,
            CreatedAt = d.CreatedAt,
            AssignedRoles = d.DocumentRoles.Select(dr => dr.Role.Name ?? string.Empty).ToList()
        }).ToList();
    }

    public async Task<DocumentDto> GetDocumentByIdAsync(Guid documentId)
    {
        var document = await _context.Documents
            .Include(d => d.CreatedByUser)
            .Include(d => d.DocumentRoles)
                .ThenInclude(dr => dr.Role)
            .FirstOrDefaultAsync(d => d.Id == documentId);

        if (document == null)
            throw new NotFoundException($"Document with ID '{documentId}' not found");

        return new DocumentDto
        {
            Id = document.Id,
            Title = document.Title,
            Description = document.Description,
            FileUrl = document.FileUrl,
            FileKey = document.FileKey,
            FileName = document.FileName,
            ContentType = document.ContentType,
            FileSizeBytes = document.FileSizeBytes,
            IsActive = document.IsActive,
            CreatedAt = document.CreatedAt,
            CreatedByUserId = document.CreatedByUserId,
            CreatedByUserName = document.CreatedByUser.FullName,
            AssignedRoles = document.DocumentRoles.Select(dr => dr.Role.Name ?? string.Empty).ToList()
        };
    }

    public async Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto dto, Guid createdByUserId)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            throw new BadRequestException("Title is required");

        if (string.IsNullOrWhiteSpace(dto.FileUrl))
            throw new BadRequestException("File URL is required");

        if (string.IsNullOrWhiteSpace(dto.FileName))
            throw new BadRequestException("File name is required");

        var user = await _userManager.FindByIdAsync(createdByUserId.ToString());
        if (user == null)
            throw new NotFoundException($"User with ID '{createdByUserId}' not found");

        var document = new Document
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Description = dto.Description,
            FileUrl = dto.FileUrl,
            FileKey = dto.FileKey,
            FileName = dto.FileName,
            ContentType = dto.ContentType,
            FileSizeBytes = dto.FileSizeBytes,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedByUserId = createdByUserId
        };

        _context.Documents.Add(document);

        // Assign roles if provided
        if (dto.AssignedRoles != null && dto.AssignedRoles.Any())
        {
            foreach (var roleName in dto.AssignedRoles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                    throw new BadRequestException($"Role '{roleName}' does not exist");

                _context.DocumentRoles.Add(new DocumentRole
                {
                    DocumentId = document.Id,
                    RoleId = role.Id,
                    AssignedAt = DateTime.UtcNow
                });
            }
        }

        await _context.SaveChangesAsync();

        return await GetDocumentByIdAsync(document.Id);
    }

    public async Task<DocumentDto> UpdateDocumentAsync(Guid documentId, UpdateDocumentDto dto)
    {
        var document = await _context.Documents.FindAsync(documentId);
        if (document == null)
            throw new NotFoundException($"Document with ID '{documentId}' not found");

        if (dto.Title != null)
            document.Title = dto.Title;

        if (dto.Description != null)
            document.Description = dto.Description;

        if (dto.FileUrl != null)
            document.FileUrl = dto.FileUrl;

        if (dto.FileName != null)
            document.FileName = dto.FileName;

        if (dto.IsActive.HasValue)
            document.IsActive = dto.IsActive.Value;

        document.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return await GetDocumentByIdAsync(documentId);
    }

    public async Task SetDocumentActiveStatusAsync(Guid documentId, bool isActive)
    {
        var document = await _context.Documents.FindAsync(documentId);
        if (document == null)
            throw new NotFoundException($"Document with ID '{documentId}' not found");

        document.IsActive = isActive;
        document.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
    }

    public async Task AssignRolesToDocumentAsync(Guid documentId, List<string> roles)
    {
        var document = await _context.Documents
            .Include(d => d.DocumentRoles)
            .FirstOrDefaultAsync(d => d.Id == documentId);

        if (document == null)
            throw new NotFoundException($"Document with ID '{documentId}' not found");

        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                throw new BadRequestException($"Role '{roleName}' does not exist");

            // Check if already assigned
            if (document.DocumentRoles.Any(dr => dr.RoleId == role.Id))
                continue;

            _context.DocumentRoles.Add(new DocumentRole
            {
                DocumentId = documentId,
                RoleId = role.Id,
                AssignedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync();
    }

    public async Task RemoveRolesFromDocumentAsync(Guid documentId, List<string> roles)
    {
        var document = await _context.Documents
            .Include(d => d.DocumentRoles)
                .ThenInclude(dr => dr.Role)
            .FirstOrDefaultAsync(d => d.Id == documentId);

        if (document == null)
            throw new NotFoundException($"Document with ID '{documentId}' not found");

        foreach (var roleName in roles)
        {
            var documentRole = document.DocumentRoles
                .FirstOrDefault(dr => dr.Role.Name == roleName);

            if (documentRole != null)
            {
                _context.DocumentRoles.Remove(documentRole);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteDocumentAsync(Guid documentId)
    {
        var document = await _context.Documents.FindAsync(documentId);
        if (document == null)
            throw new NotFoundException($"Document with ID '{documentId}' not found");

        _context.Documents.Remove(document);
        await _context.SaveChangesAsync();
    }
}
