using BusinessEntities;
using Common;
using Data.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    [AutoRegister]
    public class DeleteProductsService : IDeleteProductService
    {

        private readonly IProductRepository _productRepository;

        public DeleteProductsService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task DeleteAsync(Product product)
        {
            await _productRepository.DeleteAsync(product);
        }

        public async Task DeleteAllAsync()
        {
            await _productRepository.DeleteAllAsync();
        }
    }
}
