using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;

namespace Utilities.Helpers
{
    public class BaseApiController : ApiController
    {
        public RequestUtility RequestUtility { get; set; }
        public int PriceFormate { get; set; }
        public BaseApiController()
        {
            RequestUtility = new RequestUtility();
          //  PriceFormate  =int.Parse(new ExcSettingsManager(this.RequestUtility)["PriceFormate"].ToString());

        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }
        protected HttpResponseMessage Response<T>(HttpStatusCodeEnum statusCode, T result)
            where T : class
        {
            return Request.CreateResponse((HttpStatusCode)statusCode, result);
        }
        protected HttpResponseMessage Response(HttpStatusCodeEnum statusCode,bool result)
           
        {
            return Request.CreateResponse((HttpStatusCode)statusCode, result);
        }
        protected HttpResponseMessage Response(HttpStatusCodeEnum statusCode)
        {       
            return Request.CreateResponse(statusCode);
        }
        protected HttpResponseMessage NotFoundResponse(string Message)
        {
            var response = new ResponseVm<object> { Status = HttpStatusCodeEnum.NotFound, Message = Message };
            return Request.CreateResponse(HttpStatusCode.NotFound,response);
        }
        protected HttpResponseMessage AmbigiousResponse(string Message)
        {
            var response = new ResponseVm<object> { Status = HttpStatusCodeEnum.Ambiguous, Message = Message };

            return Request.CreateResponse(HttpStatusCode.Ambiguous, response);
        }
        protected HttpResponseMessage InternalServerErrorResponse(string Message)
        {
            var response = new ResponseVm<object> { Status = HttpStatusCodeEnum.IneternalServerError, Message = Message };
            return Request.CreateResponse(HttpStatusCode.InternalServerError, response);
        }
        protected HttpResponseMessage OKResponse<T>(T Data)
        {
            var response = new ResponseVm<object>() { Data = Data, Status = HttpStatusCodeEnum.Ok };
            return Request.CreateResponse(HttpStatusCode.OK,response);
        }

        protected HttpResponseMessage Response<T>(ResponseVm<T> result)
             where T : class
        {
            return Request.CreateResponse((HttpStatusCode)result.Status, result);
        }
    }
}
