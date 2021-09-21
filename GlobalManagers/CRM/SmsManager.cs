
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;
using Models.CRM;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Recruitment.DataAccess;

namespace Recruitment.Manager
{
    public class SmsManager : IDisposable 
    {
        public void Dispose()
        {

        }
   
        public bool Send(string Message, string MobileNumber )
        {
            try
            {
                //Snd To SMS 

                SettingsManager _settingsManager = new SettingsManager();
                bool IsTestMode = bool.Parse(_settingsManager["IsTestMode"].Value);
                if (!IsTestMode)
                {
                    string AppId = _settingsManager["SmsAppId"].Value;

                    string TagName = _settingsManager["TagName"].Value;
                    string fullUrl = "http://basic.unifonic.com/wrapper/sendSMS.php?to=" + MobileNumber + "&msg=" + Message + "&sender=" + TagName + "&appsid=" + AppId;
                    string request = fullUrl;

                    WebRequest webReq = WebRequest.Create(request);
                    webReq.Method = "GET";
                    byte[] byteArray = Encoding.UTF8.GetBytes(request);
                    webReq.ContentType = "application/x-www-form-urlencoded";
                    WebResponse webResponse = webReq.GetResponse();
                    var responseString = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                    webResponse.Close();
                }
                return true;
               

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("Message", Message), ("MobileNumber", MobileNumber));
                return false;
            }
            
        }

        public void CreateSms(string Message, string MobileNumber , string Regarding=null, string EntityName=null) {

            try
            {
                SettingsManager _settingsManager = new SettingsManager();
                bool IsTestMode = bool.Parse(_settingsManager["IsTestMode"].Value);
                if (!IsTestMode)
                {
                    var SMS = new CrmSms();
                    SMS.MobileNumbers = MobileNumber;
                    SMS.MessageText = Message;
                    if (!string.IsNullOrEmpty(Regarding) && !string.IsNullOrEmpty(EntityName))
                    {
                        SMS.Regarding = new EntityReference(EntityName, new Guid(Regarding));
                    }
                CRMService.Get.Create(SMS);
                }
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("Message", Message), ("MobileNumber", MobileNumber),("Regarding", Regarding),("EntityName", EntityName));
            }
                
        }

        /// <summary>
        /// select template from sms text entity from crm
        /// </summary>
        /// <param name="Smscode"></param>
        /// <param name="EntityID"> @id param in query if exist</param>
        /// <returns></returns>
        public static string GetSmsText(string Smscode,string EntityID=null,Dictionary<string,string> Item=null)
        {

            var Code = new QueryExpression(CrmEntityNamesMapping.SmsText);
            Code.ColumnSet = new ColumnSet("new_smsbody", "new_sqlquery");
            Code.Criteria.AddCondition("new_code", ConditionOperator.Equal, Smscode);
            var Sms= CRMService.Get.RetrieveMultiple(Code).Entities.FirstOrDefault();
            if (Sms != null) {
                string SmsBody = Sms["new_smsbody"].ToString();
                if (Sms.Attributes.Contains("new_sqlquery") && !string.IsNullOrEmpty(Sms["new_sqlquery"].ToString()))
                {
                    var query = Sms["new_sqlquery"].ToString().Replace("@id", EntityID);
                    var QueryResult = CRMAccessDB.SelectQ(query).Tables[0];
                    if (QueryResult.Rows.Count > 0)
                    {
                        for (int i = 0; i < QueryResult.Columns.Count; i++)
                        {
                            SmsBody = SmsBody.Replace("@" + QueryResult.Columns[i].ColumnName, QueryResult.Rows[0][i].ToString());
                        }
                    }
                }
                if (Sms.Attributes.Contains("new_smsbody") && !string.IsNullOrEmpty(Sms["new_smsbody"].ToString()))
                {
                    foreach ( KeyValuePair<string,string>  Keys in Item) {
                        SmsBody = SmsBody.Replace("@" + Keys.Key, Keys.Value);
                    }

      
                }
                return SmsBody;
            }
            else {
                return null;
            }
        }

    }
}
