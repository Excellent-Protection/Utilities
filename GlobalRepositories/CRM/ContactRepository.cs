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
            var _service = CRMService.Service;
            var contact = _service.Retrieve(CrmEntityNamesMapping.Contact, new Guid(contactId), new Microsoft.Xrm.Sdk.Query.ColumnSet("new_gender", "new_idnumer", "new_contactcity", "new_contactnationality", "emailaddress1"  , "jobtitle")).ToEntity<Contact>();
            return contact;
        }

        public bool IsIdentiefierExist(string id, string IdNo)
        {

                QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Contact);
                query.Criteria.AddCondition("new_idnumer", ConditionOperator.Equal, IdNo);
                query.Criteria.AddCondition("contactid", ConditionOperator.NotEqual, id);
                var _Service = CRMService.Service;
                var Result = _Service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Contact>());
                if (Result.Count() > 0)
                    return true;

            return false;

        }

        public Contact GetContactNationality(string contactId)
        {

            var _service = CRMService.Service;
            return _service.Retrieve(CrmEntityNamesMapping.Contact, new Guid(contactId), new Microsoft.Xrm.Sdk.Query.ColumnSet("new_contactnationality", "fullname", "new_englishname")).ToEntity<Contact>();
        }
        public Contact GetContactByPhone(string mobilePhone)
        {
            QueryExpression query = new QueryExpression(CrmEntityNamesMapping.Contact);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition("mobilephone", ConditionOperator.Equal, mobilePhone);

            var _service = CRMService.Service;
            var contact = _service.RetrieveMultiple(query).Entities.Select(q => q.ToEntity<Contact>()).FirstOrDefault();

            return contact;
        }

       public Contact GetContactName (string contactId)
        {
            var _service = CRMService.Service;
            var contact = _service.Retrieve(CrmEntityNamesMapping.Contact, new Guid(contactId), new ColumnSet("fullname")).ToEntity<Contact>();
            return contact;
        }

        public bool? IsBlocked(string id)
        {          
            var _service = CRMService.Service;
            var contact = _service.Retrieve(CrmEntityNamesMapping.Contact, new Guid(id), new ColumnSet("new_blacklist")).ToEntity<Contact>();

            return contact == null ? false : contact.IsBlocked;

        }
    }
}
