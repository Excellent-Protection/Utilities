using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Offers")]
    public class OffersController : BaseApiController
    {
        [HttpGet]
        [Route("GetOffers")]
        public HttpResponseMessage GetOffers(string offersector=null)
        {
            using (OffersManager _mngr = new OffersManager(RequestUtility))
            {
                var result = _mngr.GetOffers(offersector);
                return Response<List<OffersVm>>(result);
            }
        }
    }
}
