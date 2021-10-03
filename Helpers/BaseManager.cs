using ISVGeneralServices;
using ISVGeneralServices.Models;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Enums;
using Utilities.GlobalViewModels;

namespace Utilities.Helpers
{
    public abstract class BaseManager
    {
        public RequestUtility RequestUtility { get; set; }
        public BaseManager(RequestUtility requestUtility)
        {
            RequestUtility = requestUtility;
        }

        public int GenerateRandomNumber()
        {
            var rnd = new Random();
            return rnd.Next(10000, 99999);
        }
        public string GetEncodedCode(string input)
        {
            var encodedSignature = GenerateEncodedSignature.EncodedSignature.GetEncodedSignature(input);
            return encodedSignature;
        }
        public List<BaseOptionSetVM> GetOptionSet(string optionName, string entityName)
        {
            var attReq = new RetrieveAttributeRequest();
            attReq.EntityLogicalName = entityName;
            attReq.LogicalName = optionName;
            attReq.RetrieveAsIfPublished = true;
            var Service =CRMService.Get;
            var attResponse = (RetrieveAttributeResponse)Service.Execute(attReq);
            var attMetadata = ((EnumAttributeMetadata)attResponse.AttributeMetadata).OptionSet.Options;
            try
            {
                var lang = (int)RequestUtility.Language;
                var optionSet = attMetadata.Select(a => new BaseOptionSetVM()
                {
                    Key = a.Value.Value,
                    Value = a.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lang) != null ? a.Label.LocalizedLabels.FirstOrDefault(l => l.LanguageCode == lang).Label :
                a.Label.LocalizedLabels.FirstOrDefault().Label
                });
                return optionSet.ToList();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("optionName", optionName), ("entityName", entityName));
                return null;
            }

        }


     
        public VatGroup GetVatGroupForContract(DateTime contractDate, ContractServiceType contractServiceType)
        {
            var vatMgr = new VatManager();
            return vatMgr.GetVatForContract(new VatContractRequest() { ContractServiceType = contractServiceType, ContractDate = contractDate });
        }
    }
}
