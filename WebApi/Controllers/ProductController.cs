using BusinessEntities;
using Core.Services.Products;
using Raven.Abstractions.Exceptions;
using System;
using System.Collections.Generic;
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
    [RoutePrefix("Products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;
        private readonly IDeleteProductService _deleteProductService;

        public ProductController(ICreateProductService createProductService, IGetProductService getProductService, IUpdateProductService updateProductService, IDeleteProductService deleteProductService)
        {

            _createProductService = createProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
            _deleteProductService = deleteProductService;
        }

        [HttpPost]
        [Route("{productId:guid}/create")]
        public async Task<HttpResponseMessage> CreateProduct(Guid productId, [FromBody] ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequestForModelState();
            }
            try
            {
                var existingProduct = await _getProductService.GetProductAsync(productId);
                if (existingProduct != null)
                {
                    return Conflict("Product already exits");
                }
                var user = await _createProductService.CreateAsync(productId, product.Name, product.Price, product.Category, product.StockQuantity);
                return Found();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("{productId:guid}")]
        public async Task<HttpResponseMessage> GetProductById(Guid productId)
        {
            try
            {
                var product = await _getProductService.GetProductAsync(productId);
                if (product == null)
                    return DoesNotExist();

                return Found(product);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("{productId:guid}/update")]
        public async Task<HttpResponseMessage> UpdateProductAsync(
        Guid productId,
        [FromBody] ProductModel product)
        {

            var existingProduct = await _getProductService.GetProductAsync(productId);
            if (existingProduct == null)
            {
                return DoesNotExist();
            }
            await _updateProductService.UpdateAsync(productId, product.Name, product.Price, product.Category, product.StockQuantity);
            return Found();

        }

        [HttpDelete]
        [Route("{productId:guid}/delete")]
        public async Task<HttpResponseMessage> DeleteProductAsync(Guid productId)
        {
            try
            {
                var existingProduct = await _getProductService.GetProductAsync(productId);
                if (existingProduct == null)
                {
                    return DoesNotExist();
                }

                await _deleteProductService.DeleteAsync(existingProduct);
                return Found();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}