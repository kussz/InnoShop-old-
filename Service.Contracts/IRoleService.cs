using UMS.Shared.DataTransferObjects;

namespace UMS.Service.Contracts;

public interface IRoleService
{
    Task<IEnumerable<RoleDTO>> GetAllRolesAsync(bool trackChanges);
    Task<RoleDTO> GetRoleAsync(Guid id, bool trackChanges);
    Task<RoleDTO> CreateRoleAsync(RoleForPostDTO role);
    Task DeleteRoleAsync(Guid id, bool trackChanges);
    Task UpdateRoleAsync(Guid id, RoleForUpdateDTO role, bool trackChanges);
}
