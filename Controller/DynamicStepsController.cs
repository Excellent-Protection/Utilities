using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.GlobalManagers.Labor;
using Utilities.GlobalViewModels.Labor;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Steps")]
    public class DynamicStepsController : BaseApiController
    {


        public DynamicStepsController()
        {

        }


        [HttpGet]
        [Route("DynamicSteps")]
        public HttpResponseMessage GetDynamicSteps(int serviceType)
        {
            using (DynamicStepsManager _mngr = new DynamicStepsManager())
            {
                var result = _mngr.GetDynamicSteps(serviceType);
                return Response<List<StepDetailsVm>>(result);
            }
        }

        [HttpGet]
        [Route("FirstStep")]
        public HttpResponseMessage GetFirstStep(int serviceType)
        {
            using (DynamicStepsManager _mngr = new DynamicStepsManager())
            {
                var result = _mngr.GetFirstStep(serviceType);
                return Response<StepDetailsVm>(result);
            }
        }

        [HttpGet]
        [Route("StepDetailsByActionName")]
        public HttpResponseMessage GetStepDetailsByActionName(ServiceType serviceType, string actionName)
        {
            using (DynamicStepsManager _mngr = new DynamicStepsManager())
            {
                var result = _mngr.GetStepDetailsByActionNameAndServiceType(serviceType, actionName);
                return Response<StepDetailsVm>(result);
            }
        }

    
    }
}
