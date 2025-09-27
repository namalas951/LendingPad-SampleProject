using BusinessEntities;
using Common;
using Core.Services.Users;
using Data.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _userRepository;
       

        public GetProductService(IProductRepository productRepository, IUpdateProductService updateProductService = null)
        {

            _userRepository = productRepository;
           
        }
        public async Task<Product> GetProductAsync(Guid id)
        {
          var product=   await _userRepository.GetAsync(id);

            return product;
        }
    }
}
