namespace Eng_Backend.DtoLayer.Roles;
public class RemovePermissionDto
{
    public Guid RoleId { get; set; }
    public Guid PermissionId { get; set; }
}