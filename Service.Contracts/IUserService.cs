using UMS.Entities.Models;
using UMS.Shared.DataTransferObjects;

namespace UMS.Service.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync(Guid roleId, bool trackChanges);
        Task<UserDTO> GetUserAsync(Guid roleId, Guid id, bool trackChanges);
        Task<UserDTO> CreateUserAsync(UserForPostDTO user, Guid roleId, bool trackChanges);
        Task DeleteUserAsync(Guid roleId, Guid id, bool trackChanges);
        Task UpdateUserAsync(Guid roleId, Guid id, UserForUpdateDTO user, bool compTrackChanges, bool empTrackChanges);
    }
}
