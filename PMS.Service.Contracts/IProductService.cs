using PMS.Entities.Models;
using PMS.Shared.DataTransferObjects;
using PMS.Shared.RequestFeatures;

namespace PMS.Service.Contracts;

public interface IProductService
{
    Task<(IEnumerable<ProductDTO> users, MetaData metaData)> GetProductsAsync(Guid roleId, ProductParameters productParams, bool trackChanges);
    Task<ProductDTO> GetProductAsync(Guid userId, Guid id, bool trackChanges);
    Task<ProductDTO> CreateProductAsync(Guid userId, ProductForPostDTO product);
    Task DeleteProductAsync(Guid userId, Guid id, bool trackChanges);
    Task UpdateProductAsync(Guid userId, Guid id, ProductForUpdateDTO product, bool trackChanges);

}
