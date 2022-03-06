using Models.Labor;
using Models.Labor;
using Models.Labor.DynamicSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.Labor;
using Utilities.GlobalViewModels.Labor;
using Utilities.Mappers;

namespace Utilities.GlobalRepositories.Labor
{
 public   class StepRepository
    {

        public StepDataVm GetStepDataById(string stepId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var result = unitOfWork.Repository<StepData>().Single(new Guid(stepId));
                return result.Toclass<StepDataVm, StepData>();
            }
        }


        public StepsDetails GetIndividualServiceFirstStep()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Find(a => a.IsAvailable == true && a.StepsHeader.ServiceType.ToString() == "IndividualService", a => a.StepsHeader)
                .OrderBy(a => a.StepOrder).FirstOrDefault();

            }
        }

        public StepsDetails GetIndivStepDetailsByActionName(string ActionName)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Find(a => a.Action == ActionName && a.StepsHeader.ServiceType.ToString() == "IndividualService", a => a.StepsHeader).FirstOrDefault();
            }
        }

        public StepsDetails GetStepDetailsByKeyword(string StepKeyword)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                return unitOfWork.Repository<StepsDetails>().Single(a => a.StepKeyword == StepKeyword);

            }
        }
    }
}
