using Models.Labor;
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
using Westwind.Globalization;

namespace Utilities.GlobalManagers.Labor
{
    public class DynamicStepsManager : IDisposable
    {
        DynamicStepsRepository _repo;
        public DynamicStepsManager(RequestUtility requestUtility) 
        {
            _repo = new DynamicStepsRepository(requestUtility);
        }
        public void Dispose()
        {
        }

        public ResponseVm<List<StepDetailsVm>> GetDynamicSteps(int serviceType)
        {
            return GetServiceSteps(serviceType);
            switch (serviceType)
            {
                case (int)ServiceType.Individual:
                    return GetServiceSteps(serviceType);

                case (int)ServiceType.Hourly:
                    return GetServiceSteps(serviceType);
                default:
                    return GetRenewSteps();
            }

        }
        public ResponseVm<StepDetailsVm> GetFirstStep(int serviceType)
        {
            return GetServiceFirstStep(serviceType);
            switch (serviceType)
            {
                case (int)ServiceType.Individual:
                    return GetServiceFirstStep(serviceType);

                case (int)ServiceType.Hourly:
                    return GetServiceFirstStep(serviceType);
                case (int)ServiceType.Renew:
                    return GetRenewFirstStep();
                default:
                    return null;
            }

        }

          public ResponseVm<StepDetailsVm> GetLastStep(int serviceType)
        {
            try
            {
                var result= _repo.GetLastStep(serviceType).Toclass<StepDetailsVm, StepsDetails>();
                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred", "Shared") };
            }
        }


        public ResponseVm<StepDetailsVm> GetStepDetailsByActionNameAndServiceType(ServiceType serviceType, string actionName)
        {
            return GetStepDetailsByActionName(actionName, serviceType);
            switch (serviceType)
            {
                case ServiceType.Individual:
                    return GetStepDetailsByActionName(actionName, serviceType);

                case ServiceType.Hourly:
                    return GetStepDetailsByActionName(actionName, serviceType);
                case ServiceType.Renew:
                   return GetRenewStepDetailsByActionName(actionName);
                default:
                    return null;
            }

        }


        public ResponseVm<StepDetailsVm> GetNextStepDetailsByActionNameAndServiceType(ServiceType serviceType, string actionName)
        {

            switch (serviceType)
            {
                case ServiceType.Individual:
                    return GetStepDetailsByActionName(actionName,serviceType);

                case ServiceType.Hourly:
                    return GetStepDetailsByActionName(actionName, serviceType);
                case ServiceType.Renew:
                    return GetRenewStepDetailsByActionName(actionName);
                default:
                    return null;
            }

        }

        public ResponseVm<StepDetailsVm> GetServiceFirstStep(int serviceType)
        {
            try
            {
                var result = _repo.GetServiceFirstStep(serviceType).Toclass<StepDetailsVm, StepsDetails>();
                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurred" };

            }
        }


        #region DynamicSteps


        #region Individual Steps 
        public ResponseVm<List<StepDetailsVm>> GetServiceSteps(int serviceType)
        {
            try
            {
                var steps = _repo.GetServiceSteps(serviceType).ToclassList<StepDetailsVm, StepsDetails>().ToList();
                return new ResponseVm<List<StepDetailsVm>> { Status = HttpStatusCodeEnum.Ok, Data = steps };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<StepDetailsVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "Error in Get Dynamic Steps " };

            }
        }




        public ResponseVm<StepDetailsVm> GetRenewFirstStep()
        {
            try
            {
                var result = _repo.GetRenewFirstStep().Toclass<StepDetailsVm, StepsDetails>();
                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurred" };

            }
        }

        //public ResponseVm<StepDetailsVm> GetIndivStepDetailsByActionName(string actionName)
        //{
        //    try
        //    {
        //        var result = _repo.GetIndivStepDetailsByActionName(actionName ).Toclass<StepDetailsVm, StepsDetails>();
        //        return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
        //    }
        //    catch (Exception ex)
        //    {
        //        LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

        //        return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurrred" };
        //    }
        //}

        public ResponseVm<StepDetailsVm> GetStepDetailsByActionName(string actionName, ServiceType serviceType)
        {
            try
            {
                var result = _repo.GetStepDetailsByActionName(actionName, serviceType).Toclass<StepDetailsVm, StepsDetails>();
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


        public ResponseVm<StepDetailsVm> GetNextStepDetailsByCurrentActionName(string actionName , ServiceType serviceType)
        {
            try
            {
                var result = _repo.GetNextStepDetailsByCurrentActionName(actionName, serviceType).Toclass<StepDetailsVm, StepsDetails>();
                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<StepDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurrred" };
            }
        }

        #endregion
        #endregion


        public ResponseVm<List<StepDetailsVm>> GetRenewSteps()
        {
            try
            {
                var steps = _repo.GetRenewSteps().ToclassList<StepDetailsVm, StepsDetails>().ToList();
                return new ResponseVm<List<StepDetailsVm>> { Status = HttpStatusCodeEnum.Ok, Data = steps };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<StepDetailsVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "Error in Get Dynamic Steps " };

            }
        }
    }
}
