using Microsoft.Crm.Sdk.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
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

        public void Dispose()
        {
        }
    }
}
