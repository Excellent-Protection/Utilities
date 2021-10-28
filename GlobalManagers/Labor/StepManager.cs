using Models.CRM.Individual_Contract;
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
            try
            {
                return _repo.GetStepDataById(id);
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return null;
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
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return null;
            }
        }

        public ResponseVm< StepDataVm> GetDataById(string id)
        {
            try
            {
                var result = _repo.GetStepDataById(id);
                return new ResponseVm<StepDataVm> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return new ResponseVm<StepDataVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message= "An Error"};

        }


      
    }
}
