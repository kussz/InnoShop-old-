using Microsoft.EntityFrameworkCore;
using UMS.Contracts;
using UMS.Entities.Models;

namespace UMS.Repository;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<User>> GetUsersAsync(Guid roleId, bool trackChanges)
    => await FindByCondition(e => e.RoleID.Equals(roleId), trackChanges).OrderBy(e => e.Name).ToListAsync();
    public async Task<User> GetUserAsync(Guid roleId, Guid id, bool trackChanges)
        => await FindByCondition(e => e.RoleID.Equals(roleId) && e.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    public void CreateUser(Guid roleId, User user)
    {
        user.RoleID = roleId;
        Create(user);
    }
    public void DeleteUser(User user) => Delete(user);
    

}
