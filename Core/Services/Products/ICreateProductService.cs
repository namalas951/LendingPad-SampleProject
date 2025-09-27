using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
        Task<Product> CreateAsync(Guid id, string name, decimal? price = null, CategoryTypes? Category = null, int? stockQuantity = null);
    }
}
