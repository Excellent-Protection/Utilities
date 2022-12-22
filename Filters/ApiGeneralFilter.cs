using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalManagers.CRM;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.Filters
{

    public class ApiGeneralFilter : ActionFilterAttribute
    {


        public override void OnActionExecuting(HttpActionContext actionContext)
        {


            var controller = actionContext.ControllerContext.Controller as BaseApiController;
            if (controller != null)
            {
                var langRouting = actionContext.RequestContext.RouteData.Values["lang"].ToString().ToLower();
                switch (langRouting)
                {
                    case "en":
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(langRouting);
                        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                        controller.RequestUtility.Language = UserLanguage.English;
                        controller.RequestUtility.RouteLanguage = langRouting;
                        MapperConfig.lang = langRouting;
                        break;
                    case "ar":
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(langRouting);
                        Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-UK").DateTimeFormat;
                        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                        controller.RequestUtility.Language = UserLanguage.Arabic;
                        controller.RequestUtility.RouteLanguage = langRouting;
                        MapperConfig.lang = langRouting;

                        break;
                    default:
                        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(DefaultValues.DefaultLanguageRoute);
                        Thread.CurrentThread.CurrentCulture.DateTimeFormat = new System.Globalization.CultureInfo("en-UK").DateTimeFormat;
                        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                        controller.RequestUtility.Language = DefaultValues.Language;
                        controller.RequestUtility.RouteLanguage = DefaultValues.Language == UserLanguage.Arabic ? "ar" : "en";
                        MapperConfig.lang = langRouting;
                        break;
                }

                if (actionContext.Request.Headers.Contains("source"))
                {
                    var source = int.Parse(actionContext.Request.Headers.GetValues("source").First());
                    switch (source)
                    {
                        case (int)RecordSource.Mobile:
                            {
                                controller.RequestUtility.Source = RecordSource.Mobile;
                                var mobilesource = actionContext.Request.Headers.GetValues("platform").First();
                                controller.RequestUtility.PhoneSource = !string.IsNullOrEmpty(mobilesource) && mobilesource.ToLower() == "android" ? MobilePhoneSource.Android : MobilePhoneSource.Apple;
                                break;
                            }
                        case (int)RecordSource.Web:
                            controller.RequestUtility.Source = RecordSource.Web;
                            break;
                        case (int)RecordSource.Plugin:
                            controller.RequestUtility.Source = RecordSource.Plugin;
                            break;
                        default:
                            controller.RequestUtility.Source = DefaultValues.Source;
                            break;
                    }
                }
                else
                {
                    controller.RequestUtility.Source = DefaultValues.Source;
                    actionContext.Request.Headers.Add("source", ((int)DefaultValues.Source).ToString());
                }

            }
           
            

            base.OnActionExecuting(actionContext);
        }
    }
}