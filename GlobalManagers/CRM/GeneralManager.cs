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


namespace Utilities.GlobalManagers.CRM
{
    public class GeneralManager : BaseManager, IDisposable
    {
        public GeneralManager(RequestUtility requestUtility) : base(requestUtility)
        {

        }

        public ResponseVm<string> GetFirstVisitExpiryDate()
        {
            try
            {
                var _excsettingMngr = new ExcSettingsManager(RequestUtility);
                var FirstVisitExpiryAfter = _excsettingMngr.GetSettingByName(ExcSettingNames.FirstVisitExpiryAfter);
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = FirstVisitExpiryAfter.Value };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;

            }
        }
        public void Dispose()
        {
        }

    }
}
