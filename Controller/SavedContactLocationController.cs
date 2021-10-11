using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Defaults;
using Utilities.Enums;
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
        [Route("SetMainAddress")]
        public HttpResponseMessage SetMainAddress(string addressId)
        {
            using (ContactLocationManager _mngr = new ContactLocationManager(RequestUtility))
            {
                var result = _mngr.SetMainAddress(addressId);
                return Response<SavedLocationVm>(result);
            }
        }

        [HttpGet]
        [Route("ContactSavedAddress")]
        public HttpResponseMessage GetContactPreviousLocations(string contactId)
        {
            using (ContactLocationManager _mngr = new ContactLocationManager(RequestUtility))
            {
               // get setting check main addresses
               //var dealingWithMainAddressSetting = new ExcSettingsManager(RequestUtility)[DefaultValues.DealingWithMainAddressName];
               // var dealingWithMainAddress = DefaultValues.DealingWithMainAddress;
               // if (dealingWithMainAddressSetting != null)
               // {
               //     dealingWithMainAddress = bool.Parse(dealingWithMainAddressSetting.ToString());
               // }
               // if (dealingWithMainAddress)
               // {
               //     var mainLocation = _mngr.GetContactPreviousLocationByType(contactId, (int)ContactLocationType.Main);
               //     return Response<List<SavedLocationVm>>(mainLocation);
               // }

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
        [HttpGet]
        [Route("RemoveAddress")]
        public HttpResponseMessage RemoveAddress(string loctionId)
        {
            using (ContactLocationManager _mngr = new ContactLocationManager(RequestUtility))
            {
                var result = _mngr.RemoveAddress(loctionId);
                return Response<SavedLocationVm>(result);
            }
        }








    }
}
