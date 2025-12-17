using System.Security.Claims;
using Eng_Backend.BusinessLayer.Exceptions;
using Eng_Backend.BusinessLayer.Interfaces;
using Eng_Backend.DtoLayer.Common;
using Eng_Backend.DtoLayer.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eng_BackendAPI.Controllers;

public class UploadDocumentRequest
{
    public IFormFile File { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? AssignedRoles { get; set; }
}

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DocumentController : ControllerBase
{
    private readonly IDocumentService _documentService;
    private readonly IS3Service _s3Service;

    public DocumentController(IDocumentService documentService, IS3Service s3Service)
    {
        _documentService = documentService;
        _s3Service = s3Service;
    }

    // Admin: Get all documents
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllDocuments()
    {
        var documents = await _documentService.GetAllDocumentsAsync();
        return Ok(ApiResponse<List<DocumentListDto>>.SuccessResponse(documents, "Documents retrieved successfully"));
    }

    // User: Get documents accessible to the current user based on their roles
    [HttpGet("my-documents")]
    public async Task<IActionResult> GetMyDocuments()
    {
        var userId = GetCurrentUserId();
        var documents = await _documentService.GetDocumentsForUserAsync(userId);
        return Ok(ApiResponse<List<DocumentListDto>>.SuccessResponse(documents, "Documents retrieved successfully"));
    }

    [HttpGet("{documentId:guid}")]
    public async Task<IActionResult> GetDocumentById(Guid documentId)
    {
        var document = await _documentService.GetDocumentByIdAsync(documentId);
        return Ok(ApiResponse<DocumentDto>.SuccessResponse(document, "Document retrieved successfully"));
    }

    // Upload file and create document with S3 integration
    [HttpPost("upload")]
    [Authorize(Roles = "Admin")]
    [RequestSizeLimit(50 * 1024 * 1024)] // 50MB limit
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadDocument([FromForm] UploadDocumentRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            throw new BadRequestException("File is required");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new BadRequestException("Title is required");

        // Upload to S3
        using var stream = request.File.OpenReadStream();
        var uploadResult = await _s3Service.UploadFileAsync(stream, request.File.FileName, request.File.ContentType);

        if (!uploadResult.Success)
            throw new InternalServerException(uploadResult.ErrorMessage ?? "Failed to upload file");

        // Parse assigned roles
        List<string>? roles = null;
        if (!string.IsNullOrWhiteSpace(request.AssignedRoles))
        {
            roles = request.AssignedRoles.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Trim())
                .ToList();
        }

        // Create document record
        var createDto = new CreateDocumentDto
        {
            Title = request.Title,
            Description = request.Description,
            FileUrl = uploadResult.FileUrl,
            FileKey = uploadResult.FileKey,
            FileName = request.File.FileName,
            ContentType = request.File.ContentType,
            FileSizeBytes = request.File.Length,
            AssignedRoles = roles
        };

        var userId = GetCurrentUserId();
        var document = await _documentService.CreateDocumentAsync(createDto, userId);

        return Ok(ApiResponse<DocumentDto>.SuccessResponse(document, "Document uploaded successfully", 201));
    }

    [HttpPut("{documentId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDocument(Guid documentId, UpdateDocumentDto dto)
    {
        var document = await _documentService.UpdateDocumentAsync(documentId, dto);
        return Ok(ApiResponse<DocumentDto>.SuccessResponse(document, "Document updated successfully"));
    }

    [HttpPatch("{documentId:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ToggleDocumentStatus(Guid documentId, ToggleDocumentStatusDto dto)
    {
        await _documentService.SetDocumentActiveStatusAsync(documentId, dto.IsActive);
        var status = dto.IsActive ? "activated" : "deactivated";
        return Ok(ApiResponse.SuccessResponse($"Document {status} successfully"));
    }

    [HttpPost("{documentId:guid}/roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AssignRoles(Guid documentId, AssignDocumentRolesDto dto)
    {
        await _documentService.AssignRolesToDocumentAsync(documentId, dto.Roles);
        return Ok(ApiResponse.SuccessResponse("Roles assigned to document successfully"));
    }

    [HttpDelete("{documentId:guid}/roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRoles(Guid documentId, AssignDocumentRolesDto dto)
    {
        await _documentService.RemoveRolesFromDocumentAsync(documentId, dto.Roles);
        return Ok(ApiResponse.SuccessResponse("Roles removed from document successfully"));
    }

    [HttpDelete("{documentId:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDocument(Guid documentId)
    {
        // Get document to retrieve file key for S3 deletion
        var document = await _documentService.GetDocumentByIdAsync(documentId);

        // Delete from S3
        if (!string.IsNullOrEmpty(document.FileKey))
        {
            await _s3Service.DeleteFileAsync(document.FileKey);
        }

        // Delete from database
        await _documentService.DeleteDocumentAsync(documentId);
        return Ok(ApiResponse.SuccessResponse("Document deleted successfully"));
    }

    private Guid GetCurrentUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedAccessException("User ID not found in token");
        return userId;
    }
}
