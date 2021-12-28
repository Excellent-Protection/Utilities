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
    [RoutePrefix("{lang}/api/ProfessionGroups")]

    public class ProfessionGroupsController : BaseApiController
    {


        [HttpGet]
        [Route("AvailableProfessions")]
        public HttpResponseMessage GetAvailableProfession(ServiceType? serviceType=null) 
        {

            using (ProfessionGroupsManager _Mngr = new ProfessionGroupsManager(RequestUtility))
            {
                var result = _Mngr.GetProfessionGroups(serviceType);
                return Response <List<BaseQuickLookupWithImageVm >> (result);

            }
        }

        [HttpGet]
        [Route("TestProfessions")]
        public HttpResponseMessage ProfessionTest(string profGroupId, ServiceType? serviceType = null)
        {

            using (ProfessionGroupsManager _Mngr = new ProfessionGroupsManager(RequestUtility))
            {
               
                var resultsfd = _Mngr.GetProfessionsId(profGroupId);
                var result = _Mngr.GetProfessionGroups(serviceType);

                return Response<List<BaseQuickLookupWithImageVm>>(result);

            }
        }

        //[HttpGet]
        //[Route("RequiredAttachments")]
        //public HttpResponseMessage GetRequiredAttachments(string profGroupId)
        //{
        //    using (ProfessionGroupsManager _mngr= new ProfessionGroupsManager(RequestUtility))
        //    {
        //        var result = _mngr.GetRequiredAttchmentsByProfessionGroup(profGroupId);
        //        return Response<string>(result);
        //    }

        //}

    }
}
