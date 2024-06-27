using PMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using PMS.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Data;
using PMS.Shared.RequestFeatures;
using UMS.Entities.Models;

namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        public async Task<PagedList<Product>> GetProductsAsync(Guid userId, ProductParameters prodParam, bool trackChanges)
        {

            var products = await FindByCondition(a => a.UserId == userId, trackChanges)
        .OrderBy(c => c.Name)
            .ToListAsync();
            return PagedList<Product>.ToPagedList(products, prodParam.PageNumber, prodParam.PageSize);
        }
        public async Task<Product> GetProductAsync(Guid userId, Guid productId, bool trackChanges) =>
             await FindByCondition(c => c.Id.Equals(productId)&&c.UserId.Equals(userId), trackChanges)
             .SingleOrDefaultAsync()!;
        public void CreateProduct(Guid userId, Product product)
        {
            product.UserId = userId;
            product.CreationDate = DateTime.Now;
            Create(product);
        }
        public void DeleteProduct(Product role) => Delete(role);
    }
}
