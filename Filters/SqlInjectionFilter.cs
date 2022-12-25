using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.IO;
using System.Text;
using System.Collections;
using Utilities.Enums;
using Utilities.GlobalManagers.Labor;
using Utilities.GlobalViewModels;
using Utilities;
using Westwind.Globalization;

namespace Utilities.Filters
{
    public class SqlInjectionFilter : ActionFilterAttribute
    {
        List<string> reservedqueries = new List<String>() {
                "delete ", "update ", "select ","create ","truncate ","drop ","alter ", "insert "
            };


        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (IsSqlInjectionV02(actionContext) || IsSqlInjectionInQueryString(actionContext))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            if (actionContext.Request.Headers.Contains("source") && (RecordSource)int.Parse(actionContext.Request.Headers.GetValues("source").First()) == RecordSource.Plugin)
            {
                base.OnActionExecuting(actionContext);
                return;
            }


            string controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            if (!IsUpdatedForMobile(actionContext) && controllerName != "UrlShortenerConverter")
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Ambiguous, new ResponseVm<string>()
                {
                    Status = HttpStatusCodeEnum.Ambiguous,
                    Code = "306",
                    Message = DbRes.T("VersionUpdate", "Shared")
                });
            }
            if (IsSqlInjectionV02(actionContext) || IsSqlInjectionInQueryString(actionContext))
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            else
                base.OnActionExecuting(actionContext);

        }

        private bool IsUpdatedForMobile(HttpActionContext actionContext)
        {

            // if (actionContext.Request.Method != HttpMethod.Get) return true;
            var settingsMgr = new SettingsManager();

            var re = actionContext.Request;
            var headers = re.Headers;
            string Source = "";
            Version MobileVersion =null;
            string Platform = "";           
            if (headers.Contains("source"))
            {
                Source = headers.GetValues("source").First();
            }
            if (Source == "2" || Source == "3")
                return true;
            if (headers.Contains("version"))
            {
                MobileVersion = new Version(headers.GetValues("version").First());
            }
            if (String.IsNullOrEmpty(Source) || MobileVersion == null)
            {
                return false;
            }
            if (headers.Contains("platform"))
            {
                Platform = headers.GetValues("platform").First();
            }
            

            if (Source == "1")
            {
                string CurrentAndroidVersion = settingsMgr["AndroidVersion"].Value;
                string CurrentIOSVersion = settingsMgr["IOSVersion"].Value;
                if (Platform.ToLower() == "android")
                {
                    var DbVersion = new Version(CurrentAndroidVersion);
                    return DbVersion.CompareTo(MobileVersion) <= 0;
                }
                if (Platform.ToLower() == "ios")
                {

                    var DbVersion = new Version(CurrentIOSVersion);
                    return DbVersion.CompareTo(MobileVersion) <= 0;
                }
            }
            return true;
        }

        protected bool IsSqlInjection(HttpActionContext actionContext)
        {
            //actionContext.

            return (IsSqlInjectionInQueryString(actionContext) || IsSqlInjectionInPostedData(actionContext));

        }

        protected bool IsSqlInjectionInQueryString(HttpActionContext actionContext)
        {
            var url = actionContext.RequestContext.Url.ToString();
            var queryStringStartIndex = url.IndexOf('?');
            if (queryStringStartIndex == -1 || queryStringStartIndex == url.Length - 1)
            {
                return false;
            }
            var output = url.Substring(queryStringStartIndex + 1);

            if (reservedqueries.Any(r => output.Contains(string.Format("{0} ", r)) || output.Contains(string.Format("{0}%20", r))))
            {
                return true;
            }
            return false;
        }

        protected bool IsSqlInjectionInPostedData(HttpActionContext actionContext)
        {
            return true;

            //var requestMethod = actionContext.Request.Method;
            //if (requestMethod != HttpMethod.Post && requestMethod != HttpMethod.Put)
            //    return false;

            //using (var reader = new StreamReader(actionContext.Request.Content.))
            //{
            //    string json = reader.ReadToEnd();
            //}
        }




        public bool IsSqlInjectionV02(HttpActionContext actionContext)
        {

            if (actionContext.ActionArguments.Count > 0)
            {
                foreach (var item in actionContext.ActionArguments)
                {
                    //Type type = item.Value.GetType();
                    string data = Serialize(item);

                    foreach (var x in reservedqueries)
                    {
                        if (data.ToLower().Contains(x))
                            return true;
                    }
                }
            }
            return false;
        }

        public static string Serialize(object obj)
        {
            ///// To parse base class object  
            var json = ParsePreDefinedClassObject(obj);

            ///// Null means it is not a base class object  
            if (!string.IsNullOrEmpty(json))
            {
                return json;
            }

            //// For parsing user defined class object  
            //// To get all properties of object  
            //// and then store object properties and their value in dictionary container  
            var objectDataContainer = obj.GetType().GetProperties().ToDictionary(i => i.Name, i => i.GetValue(obj));

            StringBuilder jsonfile = new StringBuilder();
            jsonfile.Append("{");
            foreach (var data in objectDataContainer)
            {
                jsonfile.Append($"\"{data.Key}\":{Serialize(data.Value)},");
            }

            //// To remove last comma  
            jsonfile.Remove(jsonfile.Length - 1, 1);
            jsonfile.Append("}");
            return jsonfile.ToString();
        }

        /// <summary>  
        /// To Serialize C# Pre defined classes  
        /// </summary>  
        /// <param name="obj">object for serialization</param>  
        /// <returns>json string of object</returns>  
        private static string ParsePreDefinedClassObject(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            if (IsJsonValueType(obj))
            {
                return obj.ToString().ToLower();
            }
            else if (IsJsonStringType(obj))
            {
                return $"\"{obj.ToString()}\"";
            }
            else if (obj is IDictionary)
            {
                return SearlizeDictionaryObject((IDictionary)obj);
            }
            else if (obj is IList || obj is Array)
            {
                return SearlizeListObject((IEnumerable)obj);
            }

            return null;
        }

        /// <summary>  
        /// To Serialize Dictionary type object  
        /// </summary>  
        /// <param name="obj">object for serialization</param>  
        /// <returns>json string of object</returns>  
        private static string SearlizeDictionaryObject(IDictionary dict)
        {
            StringBuilder jsonfile = new StringBuilder();
            jsonfile.Append("{");
            var keysAsJson = new List<string>();
            var valuesAsJson = new List<string>();
            foreach (var item in (IEnumerable)dict.Keys)
            {
                keysAsJson.Add(Serialize(item));
            }
            foreach (var item in (IEnumerable)dict.Values)
            {
                valuesAsJson.Add(Serialize(item));
            }
            for (int i = 0; i < dict.Count; i++)
            {
                ////To check whether data is under double quotes or not  
                keysAsJson[i] = keysAsJson[i].Contains("\"") ? keysAsJson[i] : $"\"{keysAsJson[i]}\"";

                jsonfile.Append($"{keysAsJson[i]}:{valuesAsJson[i]},");
            }
            jsonfile.Remove(jsonfile.Length - 1, 1);
            jsonfile.Append("}");
            return jsonfile.ToString();
        }

        /// <summary>  
        /// To Serialize Enumerable (IList,Array..etc) type object  
        /// </summary>  
        /// <param name="obj">object for serialization</param>  
        /// <returns>json string of object</returns>  
        private static string SearlizeListObject(IEnumerable obj)
        {
            StringBuilder jsonfile = new StringBuilder();
            jsonfile.Append("[");
            foreach (var item in obj)
            {
                jsonfile.Append($"{Serialize(item)},");
            }
            jsonfile.Remove(jsonfile.Length - 1, 1);
            jsonfile.Append("]");
            return jsonfile.ToString();
        }

        private static bool IsJsonStringType(object obj)
        {
            return obj is string || obj is DateTime;
        }

        private static bool IsJsonValueType(object obj)
        {
            return obj.GetType().IsPrimitive;
        }

    }



}