
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Web;
using Utilities.GlobalManagers.Labor;
using Utilities.Enums;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Http.Filters;

namespace Utilities.Filters
{
   public class StepFilter : AuthorizationFilterAttribute
    {
        ServiceType _serviceType;
        public StepFilter(ServiceType serviceType)
        {
            _serviceType = serviceType;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //var serviceType = int.Parse(HttpContext.Current.Request.Params["serviceType"]);
            //var actionName = HttpContext.Current.Request.Params["actionName"];

            var actionName = actionContext.ActionDescriptor.ActionName;
            using (DynamicStepsManager _mgr = new DynamicStepsManager())
            {
                var step = _mgr.GetStepDetailsByActionNameAndServiceType(_serviceType, actionName);
                if (step.Data == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    return;

                }
                else
                {
                    if (step.Data.IsAuthorized)
                    {
                        bool isAuth = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
                        if (!isAuth)
                            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                    }
                }

            }

        }
    

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

    }

        //public override void OnAuthorization(HttpActionContext actionContext)
        //{
        //   // var serviceType = int.Parse(HttpContext.Current.Request.Params["serviceType"]);
        //    //  var actionName = HttpContext.Current.Request.Params["actionName"];
        //    var actionName = actionContext.ActionDescriptor.ActionName;
        //    using (DynamicStepsManager _mgr = new DynamicStepsManager())
        //    {
        //        var step = _mgr.GetStepDetailsByActionNameAndServiceType(_serviceType, actionName);
        //        if (step.Data == null)
        //        {
        //            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
        //            return;

        //        }
        //        else
        //        {
        //            if (step.Data.IsAuthorized)
        //            {
        //                bool  isAuth = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        //                if(isAuth)
        //                HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
        //                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        //                //return;
        //            }
        //        }

        //    }

        //    base.OnAuthorization(actionContext);
        //}

    }
    
    
}
