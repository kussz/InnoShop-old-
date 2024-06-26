using UMS.Entities.Models;
using UMS.Shared.DataTransferObjects;
using UMS.Shared.RequestFeatures;

namespace UMS.Service.Contracts
{
    public interface IUserService
    {
        Task<(IEnumerable<UserDTO> users, MetaData metaData)> GetUsersAsync(Guid roleId, UserParameters userParams, bool trackChanges);
        Task<UserDTO> GetUserAsync(Guid roleId, Guid id, bool trackChanges);
        Task<UserDTO> CreateUserAsync(UserForPostDTO user, Guid roleId, bool trackChanges);
        Task DeleteUserAsync(Guid roleId, Guid id, bool trackChanges);
        Task UpdateUserAsync(Guid roleId, Guid id, UserForUpdateDTO user, bool compTrackChanges, bool empTrackChanges);
    }
}
