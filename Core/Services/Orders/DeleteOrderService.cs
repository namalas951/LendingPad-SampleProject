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
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public  async Task DeleteAllAsync()
        {
           await  _orderRepository.DeleteAllAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            await _orderRepository.DeleteAsync(order);  
        }
    }
}
