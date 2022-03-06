using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers
{
    public class GlobalManager : BaseManager, IDisposable
    {
        public GlobalManager(RequestUtility requestUtility) : base(requestUtility)
        {

        }

        public ResponseVm <List<BaseOptionSetVM>> GetOptionSetList(string optionName, string entityName)
        {
            var options = GetOptionSet(optionName, entityName);
            if (options != null)
                return new ResponseVm<List<BaseOptionSetVM>> { Status = HttpStatusCodeEnum.Ok, Data = options };
            return new ResponseVm<List<BaseOptionSetVM>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurrred", "Shared") };
        }
        public static Guid LoginSystemUserId()
        {
            WhoAmIRequest systemUserRequest = new WhoAmIRequest();
            WhoAmIResponse systemUserResponse = (WhoAmIResponse)CRMService.Service.Execute(systemUserRequest);
            return systemUserResponse.UserId;
        }

        public static EntityCollection GetEntitiesBy(string entityName, string SearchColumn, object searchValue)
        {
            return GetEntitiesBy(entityName, SearchColumn, searchValue, true);

        }
        public static EntityCollection GetEntitiesBy(string entityName, string SearchColumn, object searchValue, bool AllColumns)
        {
            
            QueryExpression query = new QueryExpression(entityName);
            query.ColumnSet = new ColumnSet(AllColumns);
            query.Criteria.AddCondition(new ConditionExpression(SearchColumn, ConditionOperator.Equal, searchValue));
            var _service = CRMService.Service;
            return _service.RetrieveMultiple(query);

        }


        public void Dispose()
        {
        }
    }
}
