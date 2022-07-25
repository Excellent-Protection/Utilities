
//using Microsoft.Crm.Sdk.Messages;
using HourlySectorLib.ViewModels.Custom;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
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
using Utilities.Helpers;

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

        public static IOrganizationService Service
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
                    CRMPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(CRMPassword));
                }
                //else

                  //  CRMPassword = CRMPassword.DecryptText("Ahmed");

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
        public static Guid LoginSystemUserId
        {
            get
            {
                WhoAmIRequest systemUserRequest = new WhoAmIRequest();
                WhoAmIResponse systemUserResponse = (WhoAmIResponse)Service.Execute(systemUserRequest);
                return systemUserResponse.UserId;
            }
        }
        public static void DeleteBulkEntities<T>(List<T> ListOfEntities) where T : Entity
        {
            ExecuteMultipleRequest multipleRequest = new ExecuteMultipleRequest()
            {
                // Assign settings that define execution behavior: continue on error, return responses.
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = false,
                    ReturnResponses = true
                },
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection()
            };
            foreach (var entityRef in ListOfEntities)
            {
                DeleteRequest deleteRequest = new DeleteRequest()
                {
                    Target = new EntityReference(entityRef.LogicalName, entityRef.Id)
                };
                multipleRequest.Requests.Add(deleteRequest);
                if (multipleRequest.Requests.Count == 1000)
                {
                    ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)Service.Execute(multipleRequest);
                    multipleRequest = new ExecuteMultipleRequest()
                    {
                        // Assign settings that define execution behavior: continue on error, return responses.
                        Settings = new ExecuteMultipleSettings()
                        {
                            ContinueOnError = false,
                            ReturnResponses = true
                        },
                        // Create an empty organization request collection.
                        Requests = new OrganizationRequestCollection()
                    };
                }
            }
            if (multipleRequest.Requests.Count <= 1000)
            {
                ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)Service.Execute(multipleRequest);
            }
        }
        public static BulkEntitiesResult UpdateBulkEntiteies<T>(List<T> ListOfEntities) where T : Entity
        {
            List<UpdateRequest> updateRequests = new List<UpdateRequest>();
            var multipleRequest = new ExecuteMultipleRequest()
            {
                // Assign settings that define execution behavior: continue on error, return responses.
                Settings = new ExecuteMultipleSettings()
                {
                    ContinueOnError = true,
                    ReturnResponses = true
                },
                // Create an empty organization request collection.
                Requests = new OrganizationRequestCollection()
            };

            int counts = 0;

            for (int i = 0; i < ListOfEntities.Count; i++)
            {
                updateRequests.Add(new UpdateRequest() { Target = ListOfEntities[i] });
            }
            var lstlstEntity = SplitUpdateList(updateRequests, 1000);
            var BulkEntitiesResultreturn = new BulkEntitiesResult { UpdatedEntityId = new List<string>(), NotUpdatedEntityId = new List<string>() };
            foreach (var lstEntity in lstlstEntity)
            {

                multipleRequest.Requests.Clear();
                //times += "Count=" + counts + "Start:" + DateTime.Now.Minute + ":" + DateTime.Now.Second;

                multipleRequest.Requests.AddRange(lstEntity);

                ExecuteMultipleResponse multipleResponse = (ExecuteMultipleResponse)Service.Execute(multipleRequest);
                if (multipleResponse.IsFaulted)
                {
                    LogError.Error(new Exception(multipleResponse.Responses[0].Fault.Message, new Exception(multipleResponse.Responses[0].Fault.InnerFault.Message)), System.Reflection.MethodBase.GetCurrentMethod().Name, ("ListOfEntities", multipleResponse.Responses.Where(a => a.Fault != null)));
                    var faultedIndex = multipleResponse.Responses.Where(a => a.Response != null).Select(a => a.RequestIndex).ToList();
                    faultedIndex.ForEach(a =>
                    {
                        BulkEntitiesResultreturn.NotUpdatedEntityId.Add(lstEntity[a].RequestId.ToString());
                    });
                }
                else
                {
                    BulkEntitiesResultreturn.UpdatedEntityId.AddRange(lstEntity.Select(a => a.RequestId.ToString()));
                }

            }
            return BulkEntitiesResultreturn;
        }
        public static List<List<UpdateRequest>> SplitUpdateList(List<UpdateRequest> locations, int nSize = 30)
        {
            var list = new List<List<UpdateRequest>>();
            for (int i = 0; i < locations.Count; i += nSize)
            {
                list.Add(locations.GetRange(i, Math.Min(nSize, locations.Count - i)));
            }
            return list;
        }
    }
}
