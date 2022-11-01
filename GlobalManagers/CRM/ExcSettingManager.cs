using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
    public class ExcSettingManager : BaseManager, IDisposable
    {

        ExcSettingRepository _repo;
        internal RequestUtility _requestUtility;

        public ExcSettingManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _requestUtility = RequestUtility;
            _repo = new ExcSettingRepository();
        }


        public void Dispose(){}

        public ResponseVm<Dictionary<string,string>> GetSettingsByGroupId(string GroupId)
        {
            try
            {
                 var settingIds = _repo.GetSettingIdsByGroupId(GroupId).ToList();
                var res = new ExcSettingsManager(RequestUtility).GetSettingList(settingIds);


                return new ResponseVm<Dictionary<string, string>> { Status = HttpStatusCodeEnum.Ok, Data = res };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<Dictionary<string, string>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };

            }
        }
    }

 
 

}
