using HourlySectorLib.Managers;
using HourlySectorLib.ViewModels.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.GlobalViewModels;
using Utilities.Helpers;

namespace HourlySectorLib.Controller
{

    [RoutePrefix("{lang}/api/Service")]

    public class ServiceController : BaseApiController
    {
        [HttpGet]
        [Route("ServicesForService")]
        public HttpResponseMessage GetServicesForService(ServiceType serviceType)
        {
            using (ServiceManager _mngr = new ServiceManager(RequestUtility))
            {
                var result = _mngr.GetServicesForService(serviceType);
                return Response<List<DisplayServiceVm>>(result);
            }
        }
        [HttpGet]
        [Route("GetCalendarDays")]
        public HttpResponseMessage GetCalendarDays(string serviceId)
        {
            using (ServiceManager _mngr = new ServiceManager(RequestUtility))
            {
                var result = _mngr.GetCalendarDays(serviceId);
                return Response<List<string>>(result);
            }
        }



    }
}
