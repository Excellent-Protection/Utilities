
//using Microsoft.Crm.Sdk.Messages;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
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

        private static IOrganizationService _serviceInstance = GetService();
        private static object _lockObject = new object();
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

        private static IOrganizationService GetService()
        {
            string ServerURL = ConfigurationManager.AppSettings["serverUrl"];
            string Organization = ConfigurationManager.AppSettings["organization"];
            string UserName = ConfigurationManager.AppSettings["username"];
            string Password = ConfigurationManager.AppSettings["password"];
            string DomainName = ConfigurationManager.AppSettings["domain"];
            string HostName = ConfigurationManager.AppSettings["HostName"];
            Password = Password.DecryptText("Ahmed");
            HttpContext context = HttpContext.Current;
            String strCookieName = "passcookiesforax1";

            if (checkIfCookieExist(strCookieName, context))
            {
                HttpCookie cookie = context.Request.Cookies[strCookieName];

                String strCookieValue = cookie.Value.ToString();
                var values = strCookieValue.Replace("===", ";").Split(';');

                UserName = values[0];
                Password = values[1];
                Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
            }
            NetworkCredential clntCredentials = new System.Net.NetworkCredential(UserName, Password, DomainName);
            Uri orgUri = new Uri(ServerURL + "/" + Organization + "/XRMServices/2011/Organization.svc");

            var client = new CrmServiceClient(clntCredentials, Microsoft.Xrm.Tooling.Connector.AuthenticationType.IFD, HostName, "5555", Organization, false, false, null);
            //OrganizationServiceProxy orgService = new OrganizationServiceProxy(orgUri, null, clntCredentials, null);

            return (IOrganizationService)client.OrganizationServiceProxy;

        }
      
        public static IOrganizationService Service

        {
            get
            {
                try

                {

                    if (_serviceInstance == null)

                    {

                        lock (_lockObject)

                        {

                            if (_serviceInstance == null)

                                _serviceInstance = GetService();

                        }

                    }

                    return _serviceInstance;

                }

                catch (Exception ex)

                {

                    if (ex.Message.Contains("disposed"))
                    {
                        try
                        {
                            //  if (_serviceInstance == null)

                            {

                                lock (_lockObject)

                                {

                                    if (_serviceInstance == null)

                                        _serviceInstance = GetService();

                                }

                            }

                            return _serviceInstance;

                        }
                        catch (Exception)
                        {

                            return null;
                        }
                    }
                    return null;

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
        public static Guid LoginSystemUserId
        {
            get
            {
                WhoAmIRequest systemUserRequest = new WhoAmIRequest();
                WhoAmIResponse systemUserResponse = (WhoAmIResponse)Service.Execute(systemUserRequest);
                return systemUserResponse.UserId;
            }
        }
    }
}
