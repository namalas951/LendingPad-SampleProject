using BusinessEntities;
using Common;
using Core.Factories;
using Core.Services.Users;
using Data.Repositories.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService: ICreateProductService
    {
        private readonly IIdObjectFactory<Product> _userFactory;
        private readonly IProductRepository _userRepository;
        private readonly IUpdateProductService _updateProductService;
        private readonly IGetProductService _getProductService;


        public CreateProductService(IIdObjectFactory<Product> userFactory, IProductRepository userRepository, IUpdateProductService updateProductService, IGetProductService getProductService)
        {
            _userFactory = userFactory;
            _userRepository = userRepository;
            _updateProductService = updateProductService;
            _getProductService = getProductService;
        }
        public async Task<Product> CreateAsync(Guid id, string name, decimal? price = null, CategoryTypes? Category = null, int ?stockQuantity = null)
        {
            var product =  _userFactory.Create(id);
            product.SetCategory(Category.Value);
            product.SetName(name);
            product.SetPrice(price.Value);
            product.SetStockQuantity(stockQuantity.Value);
             await _userRepository.SaveAsync(product);
            return product;

        }





    }
}
