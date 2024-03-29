﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers
{
   public class ProfileManager :BaseManager , IDisposable
    {
        public ProfileManager(RequestUtility requestUtility):base(requestUtility)
        {

        }

        public ResponseVm<string> ContactUs(ContactUsVm model)
        {
            try
            {
                //get support email 

                string supportEmail = DefaultValues.SupportEmail;
                var supportEmailSetting = new ExcSettingsManager(RequestUtility)[DefaultValues.SupportEmailSettingName];
                if (supportEmailSetting != null)
                {
                    supportEmail = supportEmailSetting.ToString();
                }
                string CCEmail = "";
                string subject = "User Want To Communicate";
                string body = "<div style='direction:ltr;'>";
                body += "User Data" +"</br>"
                     + "Name:  " + model.Name+"</br>" +
                     "Phone:  " +model.PhoneNumber + "</br>" +
                      "Email:  "+ model.Email +"</br>"+
                       "Message:  " + model.MessageTitle +"</br>"+model.MessageDetails;
                body += "</div>";

                    if (!string.IsNullOrEmpty(supportEmail))

                        MailManager.SendEmail(supportEmail, CCEmail, subject, body, true, "");


                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = DbRes.T("DataSendSuccessfully", "Shared") };

            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Data = DbRes.T("AnErrorOccurred", "Shared") };


        }


        public ResponseVm<string> WhatsappContactUs()
        {
            try
            {
                string whatsappNumber = DefaultValues.whatsappNumber;
                var whatsappNumberSetting = new ExcSettingsManager(RequestUtility)[DefaultValues.whatsappNumberSettingName];
                if (whatsappNumberSetting != null)
                {
                    whatsappNumber = whatsappNumberSetting.ToString();
                }
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = whatsappNumber};

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Data = DbRes.T("AnErrorOccurred", "Shared") };

        }

        public void Dispose()
        {
        }
    }
}
