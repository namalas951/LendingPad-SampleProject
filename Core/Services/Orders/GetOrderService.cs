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
    public class GetOrderService : IGetOrderService
    {
         private readonly IOrderRepository _orderRepository;
        public GetOrderService(IOrderRepository orderRepository) {

            _orderRepository = orderRepository;


        }
        public async Task<Order> GetOrderAsync(Guid id)
        {
            return await  _orderRepository.GetAsync(id);
        }
    }
}
