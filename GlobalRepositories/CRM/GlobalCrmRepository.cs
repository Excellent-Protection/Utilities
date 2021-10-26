using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;

namespace Utilities.GlobalRepositories.CRM
{
    public class GlobalCrmRepository : IDisposable
    {
        public void Dispose()
        {
        }

        public IEnumerable<string> GetEmptyFieldsNamesForRecord(string entityName, string searchColumn, string searchValue, string[] columns = null)
        {
            QueryExpression query = new QueryExpression(entityName);

            if (columns != null)
                query.ColumnSet = new ColumnSet(columns);
            else
                query.ColumnSet = new ColumnSet(true);

            query.Criteria.AddCondition(new ConditionExpression(searchColumn, ConditionOperator.Equal, searchValue));
            var record = CRMService.Service.RetrieveMultiple(query);
            if (record.Entities == null || record.Entities.Count == 0)
                throw new Exception("No record");



            var emptyData = record.Entities.FirstOrDefault().Attributes.Where(a => a.Value == null || string.IsNullOrEmpty(a.Value.ToString())).Select(a => a.Key.ToLower());
            if (columns != null)
                emptyData = emptyData.Union(columns.Except(record.Entities.FirstOrDefault().Attributes.Keys.Select(k => k.ToLower())));

            return emptyData;
        }

    }
}
