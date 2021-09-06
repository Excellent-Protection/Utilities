using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Utilities.Helpers
{
    public class BaseApiController : ApiController
    {
        public RequestUtility RequestUtility { get; set; }

        public BaseApiController()
        {
            RequestUtility = new RequestUtility();
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }
        protected HttpResponseMessage Response<T>(HttpStatusCodeEnum statusCode,T result)
            where T : class
        {
            return Request.CreateResponse((HttpStatusCode)statusCode, result);
        }
        protected HttpResponseMessage Response(HttpStatusCodeEnum statusCode)
        {       
            return Request.CreateResponse(statusCode);
        }
    }
}
