using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        Task<Order> UpdateAsync(Guid orderid, Guid productid,DateTime? DateOfPurchase, int? Quantity, decimal? TotalPrice);
    }
}
