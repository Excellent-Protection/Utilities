using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.Helpers;

namespace Utilities.GlobalRepositories.CRM
{
    public class HourlyContractRepository : BaseCrmEntityRepository
    {

        public HourlyContractRepository(RequestUtility requestUtility) : base(requestUtility, CrmEntityNamesMapping.Contact, "contactid", "lastname", "new_englishname")
        {

        }


        public bool? HaveContactContracts(string contactId, string serviceId)
        {
            try
            {
                var query = new QueryExpression(HourlyContract.EntityLogicalName);

                query.Criteria.AddCondition("new_hindivclintname", ConditionOperator.Equal, new Guid(contactId));
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, (int)CrmEntityState.Active);

                if (serviceId != null)
                {
                    query.Criteria.AddCondition("new_service", ConditionOperator.Equal, new Guid(serviceId));
                    query.Criteria.AddCondition("new_ispaid", ConditionOperator.Equal, false);

                }

                var service = CRMService.Service;
                var t=service.RetrieveMultiple(query).Entities.Count;
                return service.RetrieveMultiple(query).Entities.Count > 0
                        ? true : false;

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }

        }
    }
}
