using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Defaults;
using Utilities.GlobalManagers;
using Utilities.GlobalViewModels;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/ContactAddress")]
    public class ContactAddressController : BaseApiController
    {
        [HttpGet]
        [Route("HousingTypes")]
        public HttpResponseMessage GetHousingTypes()
        {
            using (GlobalManager _mngr = new GlobalManager(RequestUtility))
            {
                var result = _mngr.GetOptionSetList("new_housetype" , CrmEntityNamesMapping.IndividualContractRequest);
                return Response<List<BaseOptionSetVM>>(result);
            }
        }
        [HttpGet]
        [Route("HousingFloors")]
        public HttpResponseMessage GetHousingFloors()
        {
            using (GlobalManager _mngr = new GlobalManager(RequestUtility))
            {
                var result = _mngr.GetOptionSetList("new_floorno", CrmEntityNamesMapping.IndividualContractRequest);
                return Response<List<BaseOptionSetVM>>(result);
            }
        }

    
    }
}
