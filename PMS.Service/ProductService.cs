using AutoMapper;
using PMS.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Contracts;
using PMS.Entities.Exceptions;
using UMS.Contracts;
using PMS.Shared.DataTransferObjects;
using PMS.Shared.RequestFeatures;
using PMS.Entities.Models;
using UMS.Shared.DataTransferObjects;

namespace PMS.Service
{
    public class ProductService : IProductService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public ProductService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<(IEnumerable<ProductDTO> users, MetaData metaData)> GetProductsAsync(Guid roleId, ProductParameters productParams, bool trackChanges)
        {
            var usersWithMetaData = await _repository.Product.GetProductsAsync(roleId, productParams, trackChanges);

            var usersDTO = _mapper.Map<IEnumerable<ProductDTO>>(usersWithMetaData);
            return (users: usersDTO, metaData: usersWithMetaData.MetaData);
        }
        public async Task<ProductDTO> GetProductAsync(Guid userId, Guid productId, bool trackChanges)
        {
            //await CheckIfRoleExists(roleId, trackChanges);
            var product = await GetProductAndCheckIfItExists(userId, productId, trackChanges);
            return _mapper.Map<ProductDTO>(product);
        }
        public async Task<ProductDTO> CreateProductAsync(Guid userId, ProductForPostDTO product)
        {
            var productEntity = _mapper.Map<Product>(product);
            _repository.Product.CreateProduct(userId, productEntity);
            await _repository.SaveAsync();
            var productToReturn = _mapper.Map<ProductDTO>(productEntity);
            return productToReturn;
        }
        public async Task DeleteProductAsync(Guid userId, Guid productId, bool trackChanges)
        {
            var product = await GetProductAndCheckIfItExists(userId, productId, trackChanges);
            _repository.Product.DeleteProduct(product);
            await _repository.SaveAsync();
        }
        public async Task UpdateProductAsync(Guid userId, Guid id, ProductForUpdateDTO product, bool trackChanges)
        {
            var productEntity = await GetProductAndCheckIfItExists(userId, id, trackChanges);
            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();
        }

        private async Task<Product> GetProductAndCheckIfItExists
        (Guid userId, Guid id, bool trackChanges)
        {
            var product = await _repository.Product.GetProductAsync(userId, id,
            trackChanges);
            if (product is null)
                throw new ProductNotFoundException(id);
            return product;
        }
    }
}
