using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custome;
using Utilities.Helpers;

namespace Utilities.Controller
{

    [RoutePrefix("{lang}/api/SavedContactLocation")]
    public class SavedContactLocationController : BaseApiController
    {

        public SavedContactLocationController()
        {

        }


        [HttpGet]
        [Route("ContactSavedAddress")]
        public HttpResponseMessage GetContactPreviousLocations(string contactId)
        {
            using (ContactLocationManager _mngr = new ContactLocationManager(RequestUtility))
            {
                var result = _mngr.GetAllPrevLocationsByContactId(contactId);
                return Response<ContactMainSubPreviouseLocationsVm>(result);
            }
        }

        [HttpGet]
        [Route("ContactSavedAddressByType")]
        public HttpResponseMessage GetContactPreviousLocationsByType(string contactId , int type)
        {
            using (ContactLocationManager _mngr = new ContactLocationManager(RequestUtility))
            {
                var result = _mngr.GetContactPreviousLocationByType(contactId, type);
                return Response<List< SavedLocationVm>>(result);
            }
        }

  






    }
}
