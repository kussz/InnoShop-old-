using Microsoft.AspNetCore.Identity;
using UMS.Entities.Models;
using UMS.Shared.DataTransferObjects;

namespace UMS.Service.Contracts
{
    public interface IAuthService
    {
        //Task<(IEnumerable<UserDTO> users, MetaData metaData)> GetUsersAsync(Guid roleId, UserParameters userParams, bool trackChanges);
        //Task<UserDTO> GetUserAsync(Guid roleId, Guid id, bool trackChanges);
        Task<IdentityResult> RegisterUserAsync(UserForRegDTO user);
        Task<bool> ValidateUser(UserForAuthDTO userForAuth);
        Task<string> CreateToken();

        //Task DeleteUserAsync(Guid roleId, Guid id, bool trackChanges);
        //Task UpdateUserAsync(Guid roleId, Guid id, UserForUpdateDTO user, bool compTrackChanges, bool empTrackChanges);
    }
}
