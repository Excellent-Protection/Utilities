using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Defaults;
using Utilities.GlobalManagers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;
using Westwind.Globalization;

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

        [HttpPost]
        [Route("AddNewAddress")]
        public HttpResponseMessage AddNewAddress(ContactLocationVm contactLocationModel)
        {
            using (ContactLocationManager _locationMngr = new ContactLocationManager(RequestUtility))
            {
                var result = _locationMngr.AddLocation(contactLocationModel);
                if(result.Status==HttpStatusCodeEnum.Ok)
                {
                    return  Response(new ResponseVm<string> {Status= result.Status , Data= DbRes.T("LocationSavedSuccessfully", "Shared") });
                }
                return Response<string>(result);
            }
        }

    
    }
}
