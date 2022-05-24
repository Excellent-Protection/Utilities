using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNet.Identity;
using Utilities;
using Utilities.Helpers;
using Utilities.GlobalManagers.Labor;
using Utilities.GlobalViewModels.Labor;

namespace RecService.Api.Filters
{
    public class SignAuthorizationFilter
        : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Contains("playerId"))
            {
                string deviceId = actionContext.Request.Headers.GetValues("playerId").FirstOrDefault();
                if (!String.IsNullOrEmpty(deviceId)&&deviceId.ToLower()!="null" && HttpContext.Current.User != null)
                {
                    var token = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == "authorization");
                    string userId = HttpContext.Current.User.Identity.GetUserId();
                    AddDevice(userId, deviceId);
                }
            }

            if (actionContext.Request.Method != HttpMethod.Get) return;

            //Get Request Authontication Header
            var signAuthDict = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == "signauth");
            var timexDict = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == "timex");

            if (signAuthDict.Value != null && timexDict.Value != null && signAuthDict.Value.Count() != 0 && timexDict.Value.Count() != 0)
            {
                var signAuth = signAuthDict.Value.FirstOrDefault();
                var timex = timexDict.Value.FirstOrDefault();

                if (signAuth != null && timex != null)
                {
                    string s;
                    string url = actionContext.Request.RequestUri.AbsoluteUri;
                    url = Uri.UnescapeDataString(url);
                    if (url.Contains("?_="))
                    {
                        url = url.Replace("?_=", ">");
                        url = url.Split('>')[0].ToString();

                    }
                    if (url.Contains("&_="))
                    {
                        url = url.Replace("&_=", ">");
                        url = url.Split('>')[0].ToString();

                    }


                    //url = Uri.UnescapeDataString(url);
                    s = url;
                    string res = SignatureGenerator.GetSignature(s, long.Parse(timex));

                    var RawCredentials = signAuth;
                    var encoding = Encoding.GetEncoding("iso-8859-1");

                    var usernameandpasswordencoded = RawCredentials.Split('#')[0];
                    var signature = RawCredentials.Split('#')[1];
                    var credentials = encoding.GetString(Convert.FromBase64String(usernameandpasswordencoded));
                    var Split = credentials.Split(':');

                    //APIKEY=UGFzc05BU0FQSUBOYXNBUElVc2VyMTIzQFBhc3M6TmFzQVBJVXNlcjEyM0B1c2Vy#
                    var username = Split[1].ToString();
                    var password = Split[0].ToString();
                    if (username == "NasAPIUser123@user" && password == "PassNASAPI@NasAPIUser123@Pass" && signature == res)
                        return;
                }

            }
            HandelOnAuthorized(actionContext);
        }

        void HandelOnAuthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

        }
        private void AddDevice(string userId, string deviceId)
        {
            try
            {
                using (DeviceManager _DeviceMngr = new DeviceManager(new RequestUtility()))
                {
                    if (!_DeviceMngr.CheckDeviceExist(deviceId))
                    {
                        if (!string.IsNullOrEmpty(userId))
                        {
                            _DeviceMngr.AddUserDevice(userId, deviceId);
                        }
                        else
                        {
                            _DeviceMngr.AddDevice(new DeviceVm() { DeviceId = deviceId, IsOnline = false });
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(userId))
                        {
                            var device = _DeviceMngr.GetByDeviceIdAndUserId(deviceId,userId);
                            if (device==null)
                            {
                                device = _DeviceMngr.GetAnonymousByDeviceId(deviceId);
                                if (device != null)
                                {
                                    device.UserId = userId;
                                    _DeviceMngr.UpdateDevice(device);
                                }
                                else
                                {
                                    _DeviceMngr.AddUserDevice(userId, deviceId);
                                }
                            }

                        }
                        else
                        {
                            var device = _DeviceMngr.GetAnonymousByDeviceId(deviceId);
                            if (device != null)
                            {
                                device.UserId = userId;
                                _DeviceMngr.UpdateDevice(device);
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name,("userId",userId),("deviceId",deviceId));
            }
        }
    }
}