using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace Utilities.GlobalManagers
{
  public  class MailManager
    {
        public static bool SendEmail(string pTo,
             string ccEmails,
             string pSubject,
             string pBody,
             bool isBodyHTML,
             string pAttachmentPath)
        {
            try { 
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
                for (int i = 0; i<ccs.Length; i++)
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
    }
}
