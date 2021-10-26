using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
  public  class NationalityManager : BaseManager , IDisposable
    {
        internal RequestUtility _requestUtility;
        NationalityRepository _repo;
        public NationalityManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _repo = new NationalityRepository(RequestUtility);
            _requestUtility = RequestUtility;
        }

        public void Dispose()
        {
        }

        public ResponseVm< List<BaseQuickLookupVm>> GetNationalitiesForIndvSales()
        {
            try
            {
                var query = new QueryExpression(CrmEntityNamesMapping.Nationality) { Distinct = true };
                query.ColumnSet = new ColumnSet("new_countryid", "new_name", "new_nameenglish");
                query.Criteria.AddCondition("new_isindv", ConditionOperator.Equal, true);
                var _service = CRMService.Service;
                var nationalites = _service.RetrieveMultiple(query).Entities.
                    Select(a => a.ToEntity<Country>()).Select(t => new BaseQuickLookupVm() { Key = t.Id.ToString(), Value = RequestUtility.Language == UserLanguage.Arabic ? (t.Name != null ? t.Name : t.EnglishName) : (t.EnglishName != null ? t.EnglishName : t.Name) }).ToList();
                return new ResponseVm<List<BaseQuickLookupVm>> { Status=HttpStatusCodeEnum.Ok , Data= nationalites};
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred" , "Shared")};

        }



        public ResponseVm<List<BaseQuickLookupVm>> GetActiveNationalities()
        {
            try
            {
                var nationalities = _repo.GetActiveNationalities().ToModelListData<BaseQuickLookupVm>().ToList();
                return new ResponseVm<List<BaseQuickLookupVm>>()
                {
                    Status = HttpStatusCodeEnum.Ok,
                    Data = nationalities
                };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<BaseQuickLookupVm>>()
                {
                    Status = HttpStatusCodeEnum.IneternalServerError,
                    Message = "An Error Occurred"
                };

            }
        }

    }
}
