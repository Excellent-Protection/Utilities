using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.GlobalManagers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
namespace Utilities.Controller
{

    [RoutePrefix("{lang}/api/General")]
    public class GeneralController : BaseApiController
    {
        [HttpGet]
        [Route("FirstVisitExpireAfter")]
        public HttpResponseMessage FirstVisitExpireAfter()
        {
            using (GeneralManager _mngr = new GeneralManager(RequestUtility))
            {
                var result = _mngr.GetFirstVisitExpiryDate();
                return Response<string>(result);
            }
        }
        [HttpGet]
        [Route("GetSocialMediaLinks")]
        public HttpResponseMessage GetSocialMediaLinks()
        {
            using (GeneralManager _mngr = new GeneralManager(RequestUtility))
            {
                var result = _mngr.GetSocialMediaLinks();
                return Response<Dictionary<string, string>>(result);
            }
        }


        [HttpGet]
        [Route("ShowOtherRequest")]
        public HttpResponseMessage ShowOtherRequest()
        {
            using (GeneralManager _mngr = new GeneralManager(RequestUtility))
            {
                var result = _mngr.ShowOtherRequest();
                return Response<string>(new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = result.ToString()});
            }  
        }


    }
}


