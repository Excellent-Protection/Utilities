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
using Utilities.GlobalViewModels;
using Utilities.Helpers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
   public class ProfessionManager :BaseManager , IDisposable
    {
      
    internal RequestUtility _requestUtility;

    public ProfessionManager(RequestUtility requestUtility) : base(requestUtility)
    {
     
        _requestUtility = RequestUtility;
    }

        public void Dispose()
        {
        }


        public ResponseVm<List<BaseQuickLookupVm>> GetAvailableProfessionForIndvSector()
        {
            try
            {
                QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Profession)
                {
                    Distinct = true
                };
                query.Criteria.AddCondition("new_isvalidforsales", ConditionOperator.Equal, true);
                query.ColumnSet = new ColumnSet("new_professionid", "new_name", "new_professionenglish");
                //Is available for web and mobile => available=true


                var _service = CRMService.Service;
                var result = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Profession>()).OrderBy(p => p.AppearanceOrder).Select(t => new BaseQuickLookupVm() { Key = t.Id.ToString(), Value = RequestUtility.Language == UserLanguage.Arabic ? (t.Name != null ? t.Name : t.EnglishName) : (t.EnglishName != null ? t.EnglishName : t.Name) }).ToList();

                return new ResponseVm<List<BaseQuickLookupVm>> {Status=HttpStatusCodeEnum.Ok , Data= result};

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError,Message=DbRes.T("AnErrorOccurred","Shared") };
        }
    }
}
