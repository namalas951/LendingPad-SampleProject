using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> UpdateOrderInCacheAsync(Guid id, DateTime? DateOfPurchase, int? Quantity, decimal? TotalPrice);
        Task DeleteAllAsync();
    }
}
