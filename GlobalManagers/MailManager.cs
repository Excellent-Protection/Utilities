using Microsoft.Xrm.Sdk.Query;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.GlobalViewModels;
using Utilities.Helpers;

namespace Utilities.GlobalManagers
{
    public class MailManager
    {
        public static bool SendEmail(string pTo,
             string ccEmails,
             string pSubject,
             string pBody,
             bool isBodyHTML,
             string pAttachmentPath)
        {
            try
            {
                string CurrentCSEmail = "e.shahin@excp.sa";
                string CurrentCSPassword = "pass.123";
                string exchangeDomain = "smtp.office365.com";
                string SMTPServer = "smtp.office365.com";

                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential = new NetworkCredential(CurrentCSEmail, CurrentCSPassword, exchangeDomain);
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                message.IsBodyHtml = false;
                MailAddress fromAddress = new MailAddress(CurrentCSEmail, "CRM Email");

                // setup up the host, increase the timeout to 5 minutes
                smtpClient.Host = SMTPServer;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;
                smtpClient.Timeout = (60 * 5 * 1000);
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                message.From = fromAddress;
                message.Subject = pSubject + " - " + DateTime.Now.Date.ToString().Split(' ')[0];
                message.IsBodyHtml = true;
                message.Body = pBody.Replace("\r\n", "<br>");
                string[] to = pTo.Split(';', ',');
                foreach (string mailto in to)
                {
                    if (mailto == "") continue;
                    string mailto02 = mailto.Trim(';').Trim(',');
                    if (!string.IsNullOrEmpty(mailto02))
                        message.To.Add(mailto02);
                }
                string[] ccs = ccEmails.Split(';', ',');
                for (int i = 0; i < ccs.Length; i++)
                {
                    if (!string.IsNullOrEmpty(ccs[i]))
                        message.CC.Add(ccs[i]);
                }
                if (!string.IsNullOrEmpty(pAttachmentPath))
                {
                    message.Attachments.Add(new Attachment(pAttachmentPath));
                }
                smtpClient.Send(message);


            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return true;

        }

        private static MailToSendVM GetEmailText(string Emailcode, string EntityID = null, Dictionary<string, string> Item = null, string pTo = null)
        {

            var Code = new QueryExpression(CrmEntityNamesMapping.EmailTemplate);
            Code.ColumnSet = new ColumnSet("new_mailtemplate", "new_subject", "new_toemails", "new_ccemail", "new_sql");
            Code.Criteria.AddCondition("new_code", ConditionOperator.Equal, Emailcode);
            var Email = CRMService.Service.RetrieveMultiple(Code).Entities.FirstOrDefault();
            string ToEmails = pTo;
            string CCEmail = "";
            string toDynamic = "";
            string ccdynamic = "";

            if (Email.Contains("new_toemails"))
            {
                ToEmails = ";" + Email["new_toemails"].ToString();
            }
            if (Email.Contains("new_dynamicto"))
            {
                toDynamic = Email["new_dynamicto"].ToString();
            }
            //new_ccdynamic
            if (Email.Contains("new_ccdynamic"))
            {
                ccdynamic = Email["new_ccdynamic"].ToString();
            }
            if (Email.Contains("new_ccemail"))
            {
                CCEmail = Email["new_ccemail"].ToString();
            }
            string subject = string.Empty;
            string body = string.Empty;
            if (Email.Contains("new_mailtemplate"))
            {
                body = Email["new_mailtemplate"].ToString();
            }
            if (Email.Contains("new_subject"))
            {
                subject = Email["new_subject"].ToString();
            }
            if (Email.Contains("new_sql"))
            {
                string sql = Email["new_sql"].ToString();
                sql = sql.Replace("@id", EntityID);
                DataTable dt = CRMAccessDB.SelectQ(sql).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        subject = subject.Replace("@" + dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());
                        body = body.Replace("@" + dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());
                        toDynamic = ToEmails.Replace("@" + dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());
                        ccdynamic = ccdynamic.Replace("@" + dt.Columns[i].ColumnName, dt.Rows[0][i].ToString());

                    }

                }
            }

            foreach (KeyValuePair<string, string> Keys in Item)
            {

                subject = subject.Replace("@" + Keys.Key, Keys.Value);
                body = body.Replace("@" + Keys.Key, Keys.Value);
                toDynamic = ToEmails.Replace("@" + Keys.Key, Keys.Value);
                ccdynamic = CCEmail.Replace("@" + Keys.Key, Keys.Value);

            }

            return new MailToSendVM()
            {
                body = body,
                subject = subject,
                ToEmails = ToEmails + ";" + toDynamic,
                CCEmail = CCEmail + ";" + ccdynamic
            };
        }


        public static bool SendEmails(string Emailcode, Dictionary<string, string> KeyToReplace = null, string EntityID = null, string pTo = null, string ccEmails = null, string pSubject = null, string pAttachmentPath = null, bool isBodyHTML = false)
        {
            var Msg = GetEmailText(Emailcode, EntityID, KeyToReplace, pTo);
            return SendEmail(Msg.ToEmails, Msg.CCEmail, Msg.subject, Msg.body, isBodyHTML, pAttachmentPath);
        }

        //  MailSender.SendEmail02(ToEmails+";"+toDynamic, CCEmail+";"+ccdynamic, subject, body, true, "");

    }





}
