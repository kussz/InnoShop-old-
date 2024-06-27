using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using PMS.Contracts;
using UMS.Contracts;
using UMS.Entities.Models;
using UMS.Service.Contracts;

namespace UMS.Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IAuthService> _authService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
    logger, IMapper mapper,UserManager<User> userManager,IConfiguration configuration)
    {
        _authService = new Lazy<IAuthService>(() => new
        AuthService(configuration, logger, mapper,userManager));
    }
    public IAuthService AuthService => _authService.Value;
}
