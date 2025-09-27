
using Core.Services.Orders;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models.Products;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [RoutePrefix("Orders")]
    public class OrderController : BaseApiController
    {

        private readonly ICreateOrderService _createOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;

        public OrderController(
            ICreateOrderService createOrderService,
            IUpdateOrderService updateOrderService,
            IDeleteOrderService deleteOrderService,
            IGetOrderService getOrderService)
        {
            _createOrderService = createOrderService;
            _updateOrderService = updateOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
        }


        [HttpPost]
        [Route("{orderId:guid}/create")]
        public async Task<HttpResponseMessage> CreateOrder(Guid orderId, [FromBody] OrderModel order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestForModelState();
            }
            try
            {
                var existingOrder = await _getOrderService.GetOrderAsync(orderId);
                if (existingOrder != null)
                {
                    return Conflict("Order already exits");
                }
                var user = await _createOrderService.CreateAsync(orderId, order.ProductId, order.DateOfPurchase, order.Quantity,order.TotalPrice);
                return Found();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{orderId:guid}")]
        public async Task<HttpResponseMessage> GetOrderById(Guid orderId)
        {
            try
            {
                var order = await _getOrderService.GetOrderAsync(orderId);
                if (order == null)
                {
                    return DoesNotExist();
                }
                return Found(order);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{orderId:guid}/update")]
        public async Task<HttpResponseMessage> UpdateOrderAsync(Guid orderId, [FromBody] OrderModel order) {

            var existingOrder = await _getOrderService.GetOrderAsync(orderId);
            if (existingOrder == null)
            {
                return DoesNotExist();
            }
            await _updateOrderService.UpdateAsync(orderId, order.ProductId, order.DateOfPurchase, order.Quantity, order.TotalPrice);
            return Found(existingOrder);

        }

        [HttpDelete]
        [Route("{orderId:guid}/delete")]
        public async Task<HttpResponseMessage> DeleteOrder(Guid orderId)
        {
            var existingOrder = await _getOrderService.GetOrderAsync(orderId);
            if (existingOrder == null)
            {
                return DoesNotExist();
            }
            await _deleteOrderService.DeleteAsync(existingOrder);
            return Found();
        }

        [HttpDelete]
        [Route("deleteAll")]
        public async Task<HttpResponseMessage> DeleteAllOrders()
        {
            try
            {
               await _deleteOrderService.DeleteAllAsync();
               
                return Found();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }

      

}