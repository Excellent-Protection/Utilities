using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/ResourceGroup")]
    public class ResourceGroupController :BaseApiController
    {

        public ResourceGroupController()
        {

        }

        [HttpGet]
        [Route("GetResourceGroupsByService")]
        public HttpResponseMessage GetResourceGroupsByService(string serviceId)
        {
            using (ResourceGroupManager _mngr = new ResourceGroupManager(RequestUtility))
            {
                var res = _mngr.GetResourceGroupsByService(serviceId);
                return Response<List<BaseQuickLookupWithImageVm>>(res);
            }
        }
        [HttpGet]
        [Route("AvailableResourceGroups")]
        public HttpResponseMessage GetAvailableResourceGroup(string professionGroupId=null  , ServiceType? serviceType = null , string serviceId=null)
        {

            using (ResourceGroupManager _Mngr = new ResourceGroupManager(RequestUtility))
            {
                var result = _Mngr.GetResourceGroup(professionGroupId, serviceType);
                return Response<List<BaseQuickLookupWithImageVm>>(result);

            }
        }



    }
}
