using PMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMS.Entities.Models;
using PMS.Shared.RequestFeatures;

namespace PMS.Contracts;

public interface IProductRepository
{
    Task<PagedList<Product>> GetProductsAsync(Guid userId, ProductParameters userParams, bool trackChanges);
    Task<Product> GetProductAsync(Guid userId, Guid id, bool trackChanges);
    void CreateProduct(Guid userId, Product product);
    void DeleteProduct(Product product);
}
