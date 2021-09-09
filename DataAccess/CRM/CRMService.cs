using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities.Extensions;

namespace Utilities.DataAccess.CRM
{
    public class CRMService
    {
         static IOrganizationService GetCRMService(string ServerURL, string Organization, string UserName, string Password, string DomainName, string UserGuid)
        {
            // CRMService.OrganizationServiceClient client = new OrganizationServiceClient();
            //  return client;
            ClientCredentials credentials = new ClientCredentials();
            credentials.Windows.ClientCredential = new System.Net.NetworkCredential(UserName, Password, DomainName);

            Uri organizationUri = new Uri(ServerURL + "/" + Organization + "/XRMServices/2011/Organization.svc");
            Uri homeRealmUri = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            OrganizationServiceProxy orgService = new OrganizationServiceProxy(organizationUri, homeRealmUri, credentials, null);
            orgService.EnableProxyTypes();
            if (!string.IsNullOrEmpty(UserGuid))
            {
                //orgService.CallerId = new Guid(UserGuid);
            }
            IOrganizationService _service = (IOrganizationService)orgService;
            return _service;
        }
        private static IOrganizationService GetCRMService(string ServerURL, string Organization, string UserName, string Password, string DomainName)
        {
            return GetCRMService(ServerURL, Organization, UserName, Password, DomainName, "");
        }
        public static IOrganizationService Get
        {
            get
            {
                
                string CRMServerURL = ConfigurationManager.AppSettings["serverUrl"];
                string CRMOrganiza = ConfigurationManager.AppSettings["organization"];
                string CRMUserName = ConfigurationManager.AppSettings["username"];
                string CRMPassword = ConfigurationManager.AppSettings["password"];


                HttpContext context = HttpContext.Current;
                String strCookieName = "passcookiesforax1";
               
                if (checkIfCookieExist(strCookieName, context))
                {
                    HttpCookie cookie = context.Request.Cookies[strCookieName];

                    String strCookieValue = cookie.Value.ToString();
                    var values = strCookieValue.Replace("===", ";").Split(';');

                    CRMUserName = values[0];
                    CRMPassword = values[1];
                    //CRMPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(CRMPassword));
                }
                else
                    CRMPassword = CRMPassword.DecryptText("Ahmed");
                {
                    string CRMDomain = ConfigurationManager.AppSettings["domain"];

                    //context.Session[serviceSessionId] =

                    return GetCRMService(CRMServerURL, CRMOrganiza, CRMUserName, CRMPassword, CRMDomain);

                }
            }
        }

        static bool checkIfCookieExist(string strCookieName, HttpContext context)
        {

            HttpCookie cookie = context.Request.Cookies[strCookieName];

            return
                cookie != null &&
                cookie.Value.ToString().Replace("===", ";").Split(';').Length == 2
                ? true :
                false;
        }
    }
}
