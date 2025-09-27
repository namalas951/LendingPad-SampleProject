using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Filters;
using Raven.Abstractions.Exceptions;
using Raven.Client;

namespace WebApi.App_Start
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ContextInitializeAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            var container = GlobalConfiguration.Configuration.DependencyResolver;
            var method = actionExecutedContext.Request.Method;
            if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
            {
                var session = (IAsyncDocumentSession)container.GetService(typeof(IAsyncDocumentSession));
                try
                {
                    await session.SaveChangesAsync();
                }
                catch (ConcurrencyException ex)
                {
                    // Handle concurrency error
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                        System.Net.HttpStatusCode.Conflict, "Concurrency error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Handle all other errors
                    actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(
                        System.Net.HttpStatusCode.InternalServerError, ex);
                }
             
            }
        }
    }
}