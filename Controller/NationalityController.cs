using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Nationality")]

    public class NationalityController : BaseApiController
    {

        [HttpGet]
        [Route("ActiveNationalities")]
        public HttpResponseMessage GetActiveNationalities()
        {
            using (NationalityManager _mngr = new NationalityManager(RequestUtility))
            {
                var result = _mngr.GetActiveNationalities();
                return Response<List<BaseQuickLookupVm>>(result);

            }
        }
    }
}
