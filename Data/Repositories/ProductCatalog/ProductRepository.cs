using BusinessEntities;
using Common;
using Data.Repositories.Products;
using Microsoft.Extensions.Caching.Memory;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.ProductCatalog
{
    [AutoRegister]
    public class ProductRepository : InMemoryRepository<Product>, IProductRepository
    {
        private readonly ICustomCache _cacheSession;
        public ProductRepository(ICustomCache cache) : base(cache)
        {
            _cacheSession = cache;
        }

        public async Task<Product> UpdateProductInCacheAsync(Guid productId, string name = null, decimal? price = null, int? stockQuantity = null, CategoryTypes? categoryTypes = null)
        {
            string cacheKey = "Product";
            var items = _cacheSession.GetOrCreate(cacheKey, entry => new List<Product>());
            var product = items.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                if (!string.IsNullOrEmpty(name))
                    product.SetName(name);
                if (price.HasValue)
                    product.SetPrice(price.Value);
                if (categoryTypes.HasValue)
                    product.SetCategory(categoryTypes.Value);
                if (stockQuantity.HasValue)
                    product.SetStockQuantity(stockQuantity.Value);

                // Save the updated list back to cache
                _cacheSession.Set(cacheKey, items);
                return await Task.FromResult(product);
            }

            // Product not found
            return await Task.FromResult<Product>(null);
        }

        public async Task DeleteAllAsync()
        {
            string cacheKey = "Product";
            await Task.Run(() => _cacheSession.Remove(cacheKey));
        }
    }
}
