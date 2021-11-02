﻿using Models.Labor.DynamicSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.Labor;
using Utilities.Enums;

namespace Utilities.GlobalRepositories.Labor
{
   public class DynamicStepsRepository 
    {
        public DynamicStepsRepository()
        {

        }
        public List<StepsDetails> GetIndividualServiceSteps()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var result = unitOfWork.Repository<StepsDetails>().Find(s => s.IsAvailable == true && s.IsVisible==true && s.StepsHeader.ServiceType ==(int) ServiceType.Individual, a => a.StepsHeader)
                    .OrderBy(a => a.StepOrder)
                    .ToList();

                return result;
            }
        }
             public List<StepsDetails> GetRenewSteps()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var result = unitOfWork.Repository<StepsDetails>().Find(s => s.IsAvailable == true && s.IsVisible==true && s.StepsHeader.ServiceType ==(int) ServiceType.Renew, a => a.StepsHeader)
                    .OrderBy(a => a.StepOrder)
                    .ToList();

                return result;
            }
        }


        public StepsDetails GetIndividualServiceFirstStep()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Find(a => a.IsAvailable == true && a.IsVisible==true && a.StepsHeader.ServiceType==(int) ServiceType.Individual, a => a.StepsHeader)
                .OrderBy(a => a.StepOrder).FirstOrDefault();

            }
        }

        public StepsDetails GetIndivStepDetailsByActionName(string ActionName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Find(a => a.Action == ActionName&& a.IsAvailable==true && a.StepsHeader.ServiceType ==  (int)ServiceType.Individual, a => a.StepsHeader).FirstOrDefault();
            }
        }
        public StepsDetails GetRenewStepDetailsByActionName(string ActionName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Find(a => a.Action == ActionName && a.IsAvailable == true && a.StepsHeader.ServiceType == (int)ServiceType.Renew, a => a.StepsHeader).FirstOrDefault();
            }
        }
        public StepsDetails GetRenewFirstStep()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Find(a => a.IsAvailable == true && a.IsVisible == true && a.StepsHeader.ServiceType == (int)ServiceType.Renew && string.IsNullOrEmpty(a.PreviousStepAction), a => a.StepsHeader)
                .OrderBy(a => a.StepOrder).FirstOrDefault();

            }
        }

        public StepsDetails GetLastStep(int serviceType)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Find(a => a.IsAvailable == true && a.IsVisible == true &&  string.IsNullOrEmpty(a.NextStepAction) && a.StepsHeader.ServiceType == serviceType , a => a.StepsHeader)
                .OrderBy(a => a.StepOrder).FirstOrDefault();

            }
        }
        public StepsDetails GetStepDetailsByKeyword(string StepKeyword)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Single(a => a.StepKeyword == StepKeyword);

            }
        }

        public int GetIndivStepDetailsTypeByActionName(string ActionName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
               
                return unitOfWork.Repository<StepsDetails>().Find(a => a.Action == ActionName && a.IsAvailable == true && a.StepsHeader.ServiceType == (int)ServiceType.Individual, a => a.StepsHeader).Select(a=>a.StepType).FirstOrDefault();
            }
        }


    }
}
