using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels;
using Utilities.Helpers;

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
            return new ResponseVm<List<BaseOptionSetVM>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurred" };
        }
        public void Dispose()
        {
        }
    }
}
