using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers.CRM;
using Utilities.Helpers;

namespace Utilities.Controller
{

    [RoutePrefix("{lang}/api/ExcSetting")]
    public class ExcSettingController : BaseApiController
    {

        [HttpGet]
        [Route("GetSettingsByGroupId")]
        public HttpResponseMessage GetSettingsByGroupId(string SettingGroupId)
        {
            using (ExcSettingManager _mngr = new ExcSettingManager(RequestUtility))
            {
                var result = _mngr.GetSettingsByGroupId(SettingGroupId);
                return Response<Dictionary<string, string>>(result);
            }
        }
    }
}
