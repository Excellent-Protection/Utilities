using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Helpers;

namespace Utilities.GlobalRepositories.CRM
{
  public  class NationalityRepository : BaseCrmEntityRepository
    {

        public NationalityRepository(RequestUtility requestUtility) :
            base(requestUtility, CrmEntityNamesMapping.Nationality, "new_countryid", "new_name", "new_namearabic")
        {

        }


        public List<Country> GetActiveNationalities()
        {
            var query = new QueryExpression(CrmEntityNamesMapping.Nationality) { Distinct = true };
            query.ColumnSet = new ColumnSet("new_countryid", "new_name", "new_nameenglish");
            query.Criteria.AddCondition("new_isindv", ConditionOperator.Equal, true);
            var _service = CRMService.Service;
            var nationalites = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Country>()).ToList();
            return nationalites;
        }
    }
}
