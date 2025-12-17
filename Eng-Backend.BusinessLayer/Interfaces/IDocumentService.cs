using Eng_Backend.DtoLayer.Documents;

namespace Eng_Backend.BusinessLayer.Interfaces;

public interface IDocumentService
{
    Task<List<DocumentListDto>> GetAllDocumentsAsync();
    Task<List<DocumentListDto>> GetDocumentsForUserAsync(Guid userId);
    Task<DocumentDto> GetDocumentByIdAsync(Guid documentId);
    Task<DocumentDto> CreateDocumentAsync(CreateDocumentDto dto, Guid createdByUserId);
    Task<DocumentDto> UpdateDocumentAsync(Guid documentId, UpdateDocumentDto dto);
    Task SetDocumentActiveStatusAsync(Guid documentId, bool isActive);
    Task AssignRolesToDocumentAsync(Guid documentId, List<string> roles);
    Task RemoveRolesFromDocumentAsync(Guid documentId, List<string> roles);
    Task DeleteDocumentAsync(Guid documentId);
}
