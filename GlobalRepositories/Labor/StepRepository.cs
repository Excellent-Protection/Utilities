using Models.Labor;
using Models.Labor.DynamicSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.Labor;

namespace Utilities.GlobalRepositories.Labor
{
 public   class StepRepository
    {

        public List<StepsDetails> GetIndividualServiceSteps()
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var result = unitOfWork.Repository<StepsDetails>().Find(s => s.IsAvailable == true && s.StepsHeader.ServiceType.ToString() == "IndividualService", a => a.StepsHeader)
                    .OrderBy(a => a.StepOrder)
                    .ToList();

                return result;
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
