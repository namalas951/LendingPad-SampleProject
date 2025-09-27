using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> UpdateProductInCacheAsync(Guid productId, string name = null, decimal? price = null, int? stockQuantity = null, CategoryTypes? categoryTypes = null);

        Task DeleteAllAsync();
    }
}
