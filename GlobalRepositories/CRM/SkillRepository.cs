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
using Westwind.Globalization;

namespace Utilities.GlobalRepositories.CRM
{
   public class SkillRepository
    {

        public List<Skill> EmployeeSkills(List<string> employeesId)
        {
       
                QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Skill);
                query.Criteria.AddCondition("new_sector", ConditionOperator.Equal, 2);
                query.AddLink(CrmEntityNamesMapping.EmployeeSkills, "new_skillid", "new_skillid", JoinOperator.Inner);
                query.LinkEntities[0].EntityAlias = CrmEntityNamesMapping.EmployeeSkills;
                query.LinkEntities[0].LinkCriteria.AddCondition("new_employee", ConditionOperator.In, employeesId.ToArray());
                query.LinkEntities[0].Columns = new ColumnSet("new_employee");
                query.ColumnSet = new ColumnSet(true);
                var _service = CRMService.Service;
                var data = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Skill>()).ToList();
                return data;

        }
        public List<Skill> GetSkillsForFilter(string professionGroupId)
        {
            QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Skill);
            query.ColumnSet = new ColumnSet("new_name", "new_skillarabicname", "new_icon");
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("new_professiongroupidid", ConditionOperator.Equal, professionGroupId);

            var _service = CRMService.Service;
            var data = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Skill>()).ToList();
                return data;


        }
    }
}
