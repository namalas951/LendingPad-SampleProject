using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public interface ICreateOrderService
    {
        Task<Order> CreateAsync(Guid id, Guid productid, DateTime? DateOfPurchase, int? Quantity, decimal? TotalPrice);
    }
}
