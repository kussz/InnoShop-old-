using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using UMS.Contracts;
using PMS.Entities.Models;
using PMS.Service.Contracts;
using PMS.Contracts;

namespace PMS.Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IProductService> _productService;
    public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
    logger, IMapper mapper)
    {
        _productService = new Lazy<IProductService>(() => new
        ProductService(repositoryManager, logger, mapper));

    }
    public IProductService ProductService => _productService.Value;
}
