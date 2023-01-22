using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.GlobalManagers.Labor;
using Utilities.GlobalViewModels.Custom;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Labor;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Steps")]
    public class DynamicStepsController : BaseApiController
    {
        private readonly StepManager _stepMangr;

        public DynamicStepsController()
        {
            _stepMangr = new StepManager();
        }


        [HttpGet]
        [Route("DynamicSteps")]
        public HttpResponseMessage GetDynamicSteps(int serviceType)
        {
            using (DynamicStepsManager _mngr = new DynamicStepsManager(RequestUtility))
            {
                var result = _mngr.GetDynamicSteps(serviceType);
                return Response<List<StepDetailsVm>>(result);
            }
        }

        [HttpGet]
        [Route("FirstStep")]
        public HttpResponseMessage GetFirstStep(int serviceType, string contractId = null)
        {
            using (DynamicStepsManager _mngr = new DynamicStepsManager(RequestUtility))
            {
                var firstStep = _mngr.GetFirstStep(serviceType);
                object data = null;
                if (!string.IsNullOrEmpty(contractId))
                {
                    data = new
                    {
                        ContractId = contractId,
                    };
                }

                string jsonData = data != null? System.Web.Helpers.Json.Encode(data).ToString() : null;

                var userSteps = new List<string>() { firstStep.Data.Action };
                var previousAction = System.Web.Helpers.Json.Encode(userSteps).ToString();

                string stepId = _stepMangr.Add(new StepDataVm() { Id = new Guid(), Data = jsonData, PreviousAction = previousAction ?? "", ControllerName = this.ControllerContext.ControllerDescriptor.ControllerName }).Id.ToString();

                return Response(new ResponseVm<ServiceStepResponseVm>
                {
                    Status = HttpStatusCodeEnum.Ok,
                    Data = new ServiceStepResponseVm
                    {
                        StepId = stepId,
                        StepDetailsVm = firstStep.Data,
                    }
                });
            }
        }

        [HttpGet]
        [Route("StepDetailsByActionName")]
        public HttpResponseMessage GetStepDetailsByActionName(ServiceType serviceType, string actionName)
        {
            using (DynamicStepsManager _mngr = new DynamicStepsManager(RequestUtility))
            {
                var result = _mngr.GetStepDetailsByActionNameAndServiceType(serviceType, actionName);
                return Response<StepDetailsVm>(result);
            }
        }

        [HttpGet]
        [Route("NextActionDetailsByActionName")]
        public HttpResponseMessage GetNextActionDetailsByActionName(ServiceType serviceType, string actionName)
        {
            using (DynamicStepsManager _mngr = new DynamicStepsManager(RequestUtility))
            {
                var result = _mngr.GetNextStepDetailsByCurrentActionName(actionName, serviceType);
                return Response<StepDetailsVm>(result);
            }
        }


    }
}
