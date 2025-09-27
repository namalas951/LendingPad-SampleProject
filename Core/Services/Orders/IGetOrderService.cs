using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Task<Order> GetOrderAsync(Guid id);
    }
}
