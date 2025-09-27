using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        public HttpResponseMessage Found(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, obj);
        }

        public HttpResponseMessage Found()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DoesNotExist()
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.NotFound);
        }

        public HttpResponseMessage Conflict(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.Conflict, obj);
        }

        public HttpResponseMessage InternalServerError(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.InternalServerError, obj);
        }

        public HttpResponseMessage BadRequestForModelState()
        {
            return Request.CreateErrorResponse(
                System.Net.HttpStatusCode.BadRequest, ModelState);
        }

        public HttpResponseMessage BadRequest(object obj)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.BadRequest, obj);
        }



    }
}