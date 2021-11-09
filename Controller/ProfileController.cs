using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalManagers.Labor.Identity;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Profile")]

    public class ProfileController : BaseApiController
    {

        [HttpGet]
        [Route("DashboardData")]
        public HttpResponseMessage DashboardData(string contactId)
        {
            using (DashboardManager _mngr = new DashboardManager())
            {
                var result = _mngr.DashboardCounts(contactId);
                return Response<DashboardCounts>(result);

            }
        }
        [HttpGet]
        [Route("UserProfileData")]
        public HttpResponseMessage GetUserProfileData(string userId)
        {
            ApplicationUserManager _userMngr = new ApplicationUserManager(RequestUtility);
            var userData = _userMngr.GetUserProfileData(userId);
            return Response<UserProfileDataVm>(userData);
        }


        [HttpPost]
        [Route("ContactUs")]
        public HttpResponseMessage ContactUs(ContactUsVm model)
        {
            using (ProfileManager _mngr= new ProfileManager(RequestUtility))
            {
                var result = _mngr.ContactUs(model);
                return Response<string>(result);
            
            
            }
        }

    }
}
