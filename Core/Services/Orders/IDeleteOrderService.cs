using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public interface IDeleteOrderService
    {
        Task DeleteAsync(Order order);
        Task DeleteAllAsync();
    }
}
