using BusinessEntities;
using Common;
using Data.Repositories.Orders;
using Data.Repositories.Products;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.OrderManagement
{
    [AutoRegister]
    public class OrderRepository : InMemoryRepository<Order>, IOrderRepository
    {
        private readonly ICustomCache _cacheSession;
        public OrderRepository(ICustomCache cache) : base(cache)
        {
            _cacheSession = cache;
        }

        public  async Task DeleteAllAsync()
        {
            string cacheKey = "Order";
            await Task.Run(() => _cacheSession.Remove(cacheKey));
        }

        public async Task<Order> UpdateOrderInCacheAsync(Guid id, DateTime? dateOfPurchase = null, int? quantity = null, decimal? totalPrice = null)
        {

            string cacheKey = "Order";
            var items = _cacheSession.GetOrCreate(cacheKey, entry => new List<Order>());
            var order = items.FirstOrDefault(o => o.Id == id);

            if (order != null)
            {
                if (dateOfPurchase.HasValue)
                    order.DateOfPurchase = dateOfPurchase.Value;
                if (quantity.HasValue)
                    order.Quantity = quantity;
                if (totalPrice.HasValue)
                    order.TotalPrice = totalPrice;

                _cacheSession.Set(cacheKey, items);
                return await Task.FromResult(order);
            }

            return await Task.FromResult<Order>(null);

        }


    }
}
