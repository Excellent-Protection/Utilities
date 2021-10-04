using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Helpers;

namespace Utilities.GlobalRepositories.CRM
{
    public class ContactRepository : BaseCrmEntityRepository
    {


        public ContactRepository(RequestUtility requestUtility) :
        base(requestUtility, CrmEntityNamesMapping.Contact, "contactid", "lastname", "new_englishname")
        {

        }

     
        public Contact GetContactDetails(string contactId)
        {
            var _service = CRMService.Get;
            var contact = _service.Retrieve(CrmEntityNamesMapping.Contact, new Guid(contactId), new Microsoft.Xrm.Sdk.Query.ColumnSet("new_gender", "new_idnumer", "new_contactcity", "new_contactnationality")).ToEntity<Contact>();
            return contact;
        }

        public bool IsIdentiefierExist(string id, string IdNo)
        {

                QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Contact);
                query.Criteria.AddCondition("new_idnumer", ConditionOperator.Equal, IdNo);
                query.Criteria.AddCondition("contactid", ConditionOperator.NotEqual, id);
                var _Service = CRMService.Get;
                var Result = _Service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Contact>());
                if (Result.Count() > 0)
                    return true;

            return false;

        }
    }
}
