using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
  public  class SkillManager : BaseManager, IDisposable
    {
        internal RequestUtility _requestUtility;
        SkillRepository _repo;
        public SkillManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _requestUtility = RequestUtility;
            _repo = new SkillRepository();
        }
        public void Dispose()
        {
            _repo = new SkillRepository();
        }
        public List<SkillsVm> EmployeeSkills(List<string> employeesId)
        {
            try
            {
                var data = _repo.EmployeeSkills(employeesId).ToModelListData<SkillsVm>().ToList();
                return data;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return null;
        }
     


        public ResponseVm<List<BaseQuickLookupVm>> GetSkillsForFilter(string professionGroupId)
        {
            try
            {
                
                var data =_repo.GetSkillsForFilter(professionGroupId).ToModelListData<BaseQuickLookupVm>().ToList();
               return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.Ok, Data = data };


            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);


            }
            return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred", "Shared") };
        }
    }
}
