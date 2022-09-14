using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.CRM
{
    public class SettingsCRMManager : BaseManager, IDisposable
    {
        internal RequestUtility _requestUtility;

        public SettingsCRMManager(RequestUtility requestUtility) : base(requestUtility)
        {
                _requestUtility = RequestUtility;
        }

        public IEnumerable<SettingCRMVm> this[List<string> keys]
        {
            get
            {
                var filter = new FilterExpression();
                filter.FilterOperator = LogicalOperator.Or;

                List<ConditionExpression> Conditions = new List<ConditionExpression>();
                keys.ForEach(a =>
                           filter.AddCondition(new ConditionExpression("new_name", ConditionOperator.Equal, a))

               );
                var _service = CRMService.Service;
                QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Settings);
                query.ColumnSet = new ColumnSet("new_value", "new_name");


                query.Criteria.AddFilter(filter);
                var Value = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<SettingCRM>());
                return Value.ToModelListData<SettingCRMVm>();

            }
        }



        public void Dispose()
        {

        }
    }
}
