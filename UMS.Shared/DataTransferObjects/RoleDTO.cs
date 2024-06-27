namespace UMS.Shared.DataTransferObjects
{
    public record RoleDTO(Guid Id, string Name, bool ManipulationAccess, bool PostAccess);
}
