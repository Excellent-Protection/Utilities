using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using System.Web.Routing;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.Mappers;

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

            var controller = controllerContext.Controller as BaseApiController; 
            if (controller != null)
            {
                IEnumerable<IHttpRouteData> subroutes = (IEnumerable<IHttpRouteData>)controllerContext.RouteData.Values["MS_SubRoutes"];
                IHttpRouteData routeData = subroutes.FirstOrDefault();
                var langRouting = routeData.Values["lang"];
                switch (langRouting)
                {
                    case "en":
                        {
                            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
                            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                           controller.RequestUtility.Language = UserLanguage.English;
                            break;
                        }
                    case "ar":
                        {

                            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ar-SA");
                            Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-UK").DateTimeFormat;
                            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                            controller.RequestUtility.Language = UserLanguage.Arabic;
                            break;
                        }
                    default:
                        {
                            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(DefaultValues.DefaultLanguageRoute);
                            Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-UK").DateTimeFormat;
                            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                           controller.RequestUtility.Language = DefaultValues.Language;
                            break;
                        }
                }

              // MapperConfig.lang = controller.RequestUtility.RouteLanguage = langRouting ?? (DefaultValues.Language == UserLanguage.Arabic ? DefaultValues.RouteLang_ar : DefaultValues.RouteLang_en);

            }



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
