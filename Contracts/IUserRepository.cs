using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Entities.Models;
using UMS.Shared.RequestFeatures;

namespace UMS.Contracts;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync(Guid roleId,UserParameters userParams, bool trackChanges);
    Task<User> GetUserAsync(Guid roleId,Guid id, bool trackChanges);
    void CreateUser(Guid roleId, User user);
    void DeleteUser(User user);
}
