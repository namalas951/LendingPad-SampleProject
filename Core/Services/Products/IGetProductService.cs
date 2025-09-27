using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Task<Product> GetProductAsync(Guid id);
    }
}
