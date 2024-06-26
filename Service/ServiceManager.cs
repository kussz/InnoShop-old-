using AutoMapper;
using UMS.Contracts;
using UMS.Service.Contracts;

namespace UMS.Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IRoleService> _roleService;
    private readonly Lazy<IUserService> _userService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
    logger, IMapper mapper)
    {
        _roleService = new Lazy<IRoleService>(() => new
        RoleService(repositoryManager, logger, mapper));
        _userService = new Lazy<IUserService>(() => new
        UserService(repositoryManager, logger, mapper));
    }
    public IRoleService RoleService => _roleService.Value;
    public IUserService UserService => _userService.Value;
}
