﻿using Microsoft.Crm.Sdk.Messages;
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
                //var FirstVisitExpiryAfter = _excsettingMngr.GetSettingByName(ExcSettingNames.FirstVisitExpiryAfter);
                var FirstVisitExpiryAfter = _excsettingMngr.GetSettingValueByName(ExcSettingNames.FirstVisitExpiryAfter,DefaultValues.FirstVisitExpiryAfter);
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = FirstVisitExpiryAfter.ToString() };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;

            }
        }
        public ResponseVm<Dictionary<string, string>> GetSocialMediaLinks()
        {
            var _excsettingMngr = new ExcSettingsManager(RequestUtility);
            List<string> SocialMediaLinksList =new List<string>() { DefaultValues.FacebookLinkSettingName, DefaultValues.TwitterLinkSettingName, DefaultValues.InstagramLinkSettingName 
            ,DefaultValues.YouTubeLinkSettingName,DefaultValues.LinkedInLinkSettingName,DefaultValues.CompanyNumberSettingName,DefaultValues.WebsiteSettingName};
            var SocialMediaLinks = _excsettingMngr[SocialMediaLinksList];
            return new ResponseVm<Dictionary<string, string>> { Status = HttpStatusCodeEnum.Ok, Data = SocialMediaLinks };
        }


        public bool ShowOtherRequest()
        {
            try
            {
                var _excsettingMngr = new ExcSettingsManager(RequestUtility);
                var ShowOtherRequest = _excsettingMngr.GetSettingValueByName(ExcSettingNames.ShowOtherRequest,DefaultValues.ShowOtherRequest);
                
                return  ShowOtherRequest ;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;

            }
        }
        public void Dispose()
        {
        }

    }
}
