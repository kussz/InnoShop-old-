using UMS.Contracts;

namespace UMS.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IRoleRepository> _roleRepository;
    private readonly Lazy<IUserRepository> _userRepository;
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _roleRepository = new Lazy<IRoleRepository>(() => new
        RoleRepository(repositoryContext));
        _userRepository = new Lazy<IUserRepository>(() => new
        UserRepository(repositoryContext));
    }
    public IRoleRepository Role => _roleRepository.Value;
    public IUserRepository User => _userRepository.Value;
    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}
