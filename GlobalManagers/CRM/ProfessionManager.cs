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

        public List<BaseQuickLookupVm> GetAvailableProfession()
        {
            try
            {
                QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Profession)
                {
                    Distinct = true
                };
                query.AddLink(CrmEntityNamesMapping.IndividualPricing, "new_professionid", "new_profession");
                query.Criteria.AddCondition("new_isvalidforsales", ConditionOperator.Equal, true);
                query.ColumnSet = new ColumnSet("new_professionid", "new_name", "new_professionenglish", "new_appearanceorder", "new_forgender");
                //Is available for web and mobile => available=true
                query.LinkEntities[0].LinkCriteria.AddCondition("new_displaypricing", ConditionOperator.In, (int)DisplayPricingFor.Mobile, (int)DisplayPricingFor.WebAndMobile, (int)DisplayPricingFor.All);


                var _service = CRMService.Get;
                var result = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Profession>()).OrderBy(p => p.AppearanceOrder).Select(t => new BaseQuickLookupVm() { Key = t.Id.ToString(), Value = RequestUtility.Language == UserLanguage.Arabic ? (t.Name != null ? t.Name : t.EnglishName) : (t.EnglishName != null ? t.EnglishName : t.Name), AdditionalInformation = t.ForGender != null ? t.ForGender.Value.ToString() : null }).ToList();

                return new List<BaseQuickLookupVm>(result);

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }
    }
}
