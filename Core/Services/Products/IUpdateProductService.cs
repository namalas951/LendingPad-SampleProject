using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        Task<Product> UpdateAsync(Guid productid, string name, decimal? price = null, CategoryTypes? category = null, int? stockQuantity = null);
   
    }
}
