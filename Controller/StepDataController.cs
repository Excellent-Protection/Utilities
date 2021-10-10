using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers.Labor;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Labor;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/StepData")]
    public  class StepDataController :BaseApiController
    {
        [HttpGet]
        [Route("StepDataById")]
        public HttpResponseMessage GetIndividualStepDataById(string stepId)
        {
            using (StepManager _mngr = new StepManager())
            {
                var result = _mngr.GetDataById(stepId);
             
                return Response<StepDataVm>(result);
            }
        }
             
    }
}
