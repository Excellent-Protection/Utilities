using Models.Labor;
using Models.Labor.DynamicSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.Labor;
using Utilities.Enums;
using Utilities.GlobalRepositories.Labor;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Labor;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.Labor
{
  public  class StepManager: IDisposable
    {
        StepRepository _repo;

            public StepManager()
        {
            _repo = new StepRepository();
        }
        public void Dispose()
        {
        }

        public StepDataVm GetById(string id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                var result = unitOfWork.Repository<StepData>().Single(new Guid(id));
                return result.Toclass<StepDataVm, StepData>();
            }
        }

        public int Update(StepDataVm entity)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
            {
                unitOfWork.Repository<StepData>().Update(entity.ToIEntityBase<StepData, StepDataVm>());
                int result = unitOfWork.SaveChanges();
                return result;
            }
        }

        public StepDataVm Add(StepDataVm entity)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    var result = unitOfWork.Repository<StepData>().Add(entity.ToIEntityBase<StepData, StepDataVm>());
                    unitOfWork.SaveChanges();
                    return result.Toclass<StepDataVm, StepData>();
                }
            }
            catch(Exception ex)
            {
                return null;
            }
        }




    }
}
