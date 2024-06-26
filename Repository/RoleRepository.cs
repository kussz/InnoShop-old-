using UMS.Entities.Models;
using UMS.Contracts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;

namespace UMS.Repository;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }
    public async Task<IEnumerable<Role>> GetAllRolesAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync();
    public async Task<Role> GetRoleAsync(Guid roleId, bool trackChanges) =>
         await FindByCondition(c => c.Id.Equals(roleId), trackChanges)
         .SingleOrDefaultAsync()!;
    public void CreateRole(Role role) => Create(role);
    public void DeleteRole(Role role) => Delete(role);


}
