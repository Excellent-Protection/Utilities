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
            return Request.CreateResponse(HttpStatusCode.NotFound,Message);
        }
        protected HttpResponseMessage AmbigiousResponse(string Message)
        {       
            return Request.CreateResponse(HttpStatusCode.Ambiguous, Message);
        }
        protected HttpResponseMessage InternalServerErrorResponse(string Message)
        {       
            return Request.CreateResponse(HttpStatusCode.InternalServerError, Message);
        }
        protected HttpResponseMessage OKResponse<T>(T Data)
        {       
            return Request.CreateResponse(HttpStatusCode.OK,Data);
        }

        protected HttpResponseMessage Response<T>(ResponseVm<T> result)
             where T : class
        {
            return Request.CreateResponse((HttpStatusCode)result.Status, result);
        }
    }
}
