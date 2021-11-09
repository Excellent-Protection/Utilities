
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

namespace Utilities.GlobalManagers.CRM
{
    public class SmsManager : IDisposable
    {
        public void Dispose()
        {

        }


        private static bool CreateSms(string Message, string MobileNumber, string Regarding = null, string EntityName = null)
        {

            try
            {
                var SMS = new CrmSms();
                SMS.MobileNumbers = MobileNumber;
                SMS.MessageText = Message;
                if (!string.IsNullOrEmpty(Regarding) && !string.IsNullOrEmpty(EntityName))
                {
                    SMS.Regarding = new EntityReference(EntityName, new Guid(Regarding));
                }
                CRMService.Service.Create(SMS);
                return true;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("Message", Message), ("MobileNumber", MobileNumber), ("Regarding", Regarding), ("EntityName", EntityName));
                return false;
            }

        }

        /// <summary>
        /// select template from sms text entity from crm
        /// </summary>
        /// <param name="Smscode"></param>
        /// <param name="EntityID"> @id param in query if exist</param>
        /// <returns></returns>
        private static string GetSmsText(string Smscode, string EntityID = null, Dictionary<string, string> Item = null)
        {

            var Code = new QueryExpression(CrmEntityNamesMapping.SmsText);
            Code.ColumnSet = new ColumnSet("new_smsbody", "new_sqlquery");
            Code.Criteria.AddCondition("new_code", ConditionOperator.Equal, Smscode);
            var Sms = CRMService.Service.RetrieveMultiple(Code).Entities.FirstOrDefault();
            if (Sms != null)
            {
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
                    foreach (KeyValuePair<string, string> Keys in Item)
                    {
                        SmsBody = SmsBody.Replace("@" + Keys.Key, Keys.Value);
                    }


                }
                return SmsBody;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// get Sms Text from crm replace keywords then create new sms record
        /// </summary>
        /// <param name="Smscode"></param>
        /// <param name="MobileNumber"></param>
        /// <param name="RegardingEntityId"></param>
        /// <param name="RegardingEntityName"></param>
        /// <param name="EntityID"></param>
        /// <param name="KeyToReplace"></param>
        /// <returns></returns>
        public static bool SendSms(string Smscode, string MobileNumber, Dictionary<string, string> KeyToReplace = null, string RegardingEntityId = null, string RegardingEntityName = null, string EntityID = null)
        {
            var Msg = GetSmsText(Smscode, EntityID, KeyToReplace);
            return CreateSms(Msg, MobileNumber, RegardingEntityId, RegardingEntityName);
        }
    }
}
