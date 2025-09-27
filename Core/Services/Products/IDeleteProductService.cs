using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Products
{
    public interface IDeleteProductService
    {
        Task DeleteAsync(Product product);
        Task DeleteAllAsync();
    }
}
