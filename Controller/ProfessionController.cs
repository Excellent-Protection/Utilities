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
    [RoutePrefix("{lang}/api/Profession")]
    public class ProfessionController :BaseApiController
    {

        [HttpGet]
        [Route("AvailableProfessionForIndvSector")]
        public HttpResponseMessage GetAvailableProfessionForIndvSector()
        {

            using (ProfessionManager _mngr = new ProfessionManager(RequestUtility))
            {

                var result = _mngr.GetAvailableProfessionForIndvSector();
                return Response<List<BaseQuickLookupVm>>(result);

            }
        }
   

    }
}
