using Models.Labor.DynamicSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;
using Utilities.GlobalRepositories.Labor;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Labor;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.Labor
{
    public class DynamicStepsManager : IDisposable
    {
        DynamicStepsRepository _repo;
        public DynamicStepsManager()
        {
            _repo = new DynamicStepsRepository();
        }
        public void Dispose()
        {
        }

        public ResponseVm<List<StepDetailsVm>> GetDynamicSteps(int serviceType)
        {
            switch (serviceType)
            {
                case (int)ServiceType.Individual:
                    return GetIndividualServiceSteps();

                case (int)ServiceType.Hourly:
                    return null;
                default:
                    return null;
            }

        }
        public ResponseVm<StepDetailsVm> GetFirstStep(int serviceType)
        {
            switch (serviceType)
            {
                case (int)ServiceType.Individual:
                    return GetIndividualServiceFirstStep();

                case (int)ServiceType.Hourly:
                    return null;
                default:
                    return null;
            }

        }


        public ResponseVm<StepDetailsVm> GetStepDetailsByActionNameAndServiceType(ServiceType serviceType, string actionName)
        {

            switch (serviceType)
            {
                case ServiceType.Individual:
                    return GetIndivStepDetailsByActionName(actionName);

                case ServiceType.Hourly:
                    return null;
                case ServiceType.Renew:
                   return GetRenewStepDetailsByActionName(actionName);
                default:
                    return null;
            }

        }

        #region DynamicSteps


        #region Individual Steps 
        public ResponseVm<List<StepDetailsVm>> GetIndividualServiceSteps()
        {
            try
            {
                var steps = _repo.GetIndividualServiceSteps().ToclassList<StepDetailsVm, StepsDetails>().ToList();
                return new ResponseVm<List<StepDetailsVm>> { Status = HttpStatusCodeEnum.Ok, Data = steps };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<StepDetailsVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "Error in Get Dynamic Steps " };

            }
        }

        public ResponseVm<StepDetailsVm> GetIndividualServiceFirstStep()
        {
            try
            {
                var result = _repo.GetIndividualServiceFirstStep().Toclass<StepDetailsVm, StepsDetails>();
                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurred" };

            }
        }

        public ResponseVm<StepDetailsVm> GetIndivStepDetailsByActionName(string actionName)
        {
            try
            {
                var result = _repo.GetIndivStepDetailsByActionName(actionName ).Toclass<StepDetailsVm, StepsDetails>();
                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurrred" };
            }
        }
        public ResponseVm<StepDetailsVm> GetRenewStepDetailsByActionName(string actionName)
        {
            try
            {
                var result = _repo.GetRenewStepDetailsByActionName(actionName).Toclass<StepDetailsVm, StepsDetails>();
                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurrred" };
            }
        }
        public bool CheckIfStepIsStratigy(string actionName)
        {
            try
            {
                var stepType = _repo.GetIndivStepDetailsTypeByActionName(actionName);
                if (stepType == (int)DynamicStepType.Stratigy)
                    return true;
                else
                 return   false;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return false;
            }
        }

        #endregion
        #endregion
    }
}
