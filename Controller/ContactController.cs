using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalManagers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalManagers.Labor;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custome;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Contact")]
    public class ContactController : BaseApiController
    {



        //[HttpGet]
        //[Route("IsProfileCompleted")]
        //public HttpResponseMessage IsProfileCompleted (string contactId)
        //{
        //    using (ContactManager _mngr= new ContactManager(RequestUtility))
        //    {
        //        var isCompleted = _mngr.IsProfileCompleted(contactId);
        //        return Response<bool>(isCompleted);
        //    }

        //}


        [HttpGet]
        [Route("ContactDetails")]
        public HttpResponseMessage ContactDetails(string contactId)
        {
            using (ContactManager _mngr = new ContactManager(RequestUtility))
            {
                var result = _mngr.GetContactDetails(contactId);
                return Response<ContactDetailsVm>(result);
            }

        }


        [HttpGet]
        [Route("ContactGender")]
        public HttpResponseMessage GetContactGender()
        {
            using (GlobalManager _mngr = new GlobalManager(RequestUtility))
            {
                var result = _mngr.GetOptionSetList("new_gender", CrmEntityNamesMapping.Contact);
                return Response<List<BaseOptionSetVM>>(result);
            }
        }

        [HttpPost]
        [Route("CompleteProfile")]
        public HttpResponseMessage CompleteProfile( ContactDetailsVm contact, ServiceType  serviceType ,string stepId , int stepType)
        {
            using (ContactManager _mngr = new ContactManager(RequestUtility))
            {
                var CurrentActionName = this.ActionContext.ActionDescriptor.ActionName;
                DynamicStepsManager _dynamicStepMngr = new DynamicStepsManager();
                var currentStepDetails = _dynamicStepMngr.GetStepDetailsByActionNameAndServiceType(serviceType, CurrentActionName);
                var nextAction = currentStepDetails.Data.NextStepAction;
                if (stepType == (int)StepTypeEnum.Previous)
                {
                    var prevStepDetails = _dynamicStepMngr.GetStepDetailsByActionNameAndServiceType(serviceType, currentStepDetails.Data?.PreviousStepAction);
                    return Response(new ResponseVm<ServiceStepResponseVm> { Status = HttpStatusCodeEnum.Ok, Data = new ServiceStepResponseVm { StepId = stepId, StepDetailsVm = prevStepDetails.Data } });
                }
                var validateIdNo = _mngr.IsIdentiefierExist(contact.ContactId, contact.IdNumber);
                if(validateIdNo)
                    return Response(new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message="Id Number  Exists Before" });

                var result = _mngr.CompleteProfile(contact);
                if (result)
                {
                    var nextStepDetails = _dynamicStepMngr.GetStepDetailsByActionNameAndServiceType(serviceType, currentStepDetails.Data?.NextStepAction);
                    return Response(new ResponseVm<ServiceStepResponseVm> { Status = HttpStatusCodeEnum.Ok, Data = new ServiceStepResponseVm { StepId = stepId, StepDetailsVm = nextStepDetails.Data } });
                
                }
                return Response(new ResponseVm<ServiceStepResponseVm> { Status = HttpStatusCodeEnum.Ambiguous, Data = new ServiceStepResponseVm { StepId = stepId, StepDetailsVm = currentStepDetails.Data } });

            }

        }

    }
}
