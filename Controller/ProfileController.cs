using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Profile")]

    public class ProfileController : BaseApiController
    {

        [HttpGet]
        [Route("DashboardData")]
        public HttpResponseMessage DashboardData(string contactId)
        {
            using (DashboardManager _mngr = new DashboardManager())
            {
                var result = _mngr.DashboardCounts(contactId);
                return Response<DashboardCounts>(result);

            }
        }

    }
}
