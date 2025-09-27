using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(
            IIdObjectFactory<Order> orderFactory,
            IOrderRepository orderRepository)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateAsync(Guid id, Guid productid , DateTime? DateOfPurchase, int? Quantity, decimal? TotalPrice)
        {
            var order = _orderFactory.Create(id);
            order.DateOfPurchase = DateOfPurchase.Value;
            order.Quantity = Quantity.Value;
            order.TotalPrice = TotalPrice.Value;
            order.ProductId = productid;
            await _orderRepository.SaveAsync(order);
            return order;
        }
    }
}
