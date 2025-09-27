using BusinessEntities;
using Common;
using Data.Repositories.ProductCatalog;
using Data.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    [AutoRegister]
    public class UpdateProductService : IUpdateProductService
    {
        IProductRepository _productRepository;
        public UpdateProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> UpdateAsync(Guid productID, string name, decimal? price = null, CategoryTypes? category = null, int? stockQuantity = null)
        {
         
            return await _productRepository.UpdateProductInCacheAsync(productID, name, price, stockQuantity, category);

            
        }

       
    }
}
