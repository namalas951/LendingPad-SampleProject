using BusinessEntities;
using Common;
using Data.Repositories.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class UpdateOrderService : IUpdateOrderService
    {
        IOrderRepository _orderRepository;
        public UpdateOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> UpdateAsync(Guid orderid, Guid productid, DateTime? DateOfPurchase, int? Quantity, decimal? TotalPrice)
        {
           return await _orderRepository.UpdateOrderInCacheAsync(orderid, DateOfPurchase, Quantity, TotalPrice);
        }
    }
}
