using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;

namespace Utilities.GlobalRepositories.CRM
{
    public class ExcSettingRepository
    {
        public List<string> GetSettingIdsByGroupId(string GroupId)
        {
            var _service = CRMService.Service;

            var settingQuery = new QueryExpression(CrmEntityNamesMapping.ExcSettings);
            settingQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);

            settingQuery.AddLink(CrmEntityNamesMapping.ExcSettingGroups, "new_settinggroup", "new_excsettinggroupid");
            settingQuery.LinkEntities[0].LinkCriteria.AddCondition("new_excsettinggroupid", ConditionOperator.Equal, GroupId);
            settingQuery.ColumnSet = new ColumnSet("new_excsettingsid");

            var result = _service.RetrieveMultiple(settingQuery).Entities.Select(a => a.ToEntity<ExcSettings>().Id.ToString()).ToList();
            return result;
        }
    }
}
