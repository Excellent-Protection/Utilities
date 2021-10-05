
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;
using System.Web;
using Utilities.GlobalManagers.Labor;

namespace Utilities.Filters
{
    class StepFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var serviceType = int.Parse(HttpContext.Current.Request.Params["serviceType"]);
            //  var actionName = HttpContext.Current.Request.Params["actionName"];
            var actionName = actionContext.ActionDescriptor.ActionName;
            using (DynamicStepsManager _mgr = new DynamicStepsManager())
            {
                var step = _mgr.GetStepDetailsByActionName(serviceType, actionName);
                if (step.Data == null)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.NotFound);
                    return;

                }
                else
                {
                    if (!step.Data.IsAuthorized)
                    {
                        HttpContext.Current.Response.AddHeader("AuthenticationStatus", "NotAuthorized");
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                        return;
                    }
                }

            }

            base.OnAuthorization(actionContext);
        }

    }
    
    
}
