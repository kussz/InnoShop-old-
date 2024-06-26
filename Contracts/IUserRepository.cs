using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Entities.Models;

namespace UMS.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(Guid roleId, bool trackChanges);
        Task<User> GetUserAsync(Guid roleId,Guid id, bool trackChanges);
        void CreateUser(Role role, User user);
        void DeleteUser(User user);
    }
}
