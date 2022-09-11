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
  public class ContactProcedureLoggerRepository : BaseCrmEntityRepository
    {
        public ContactProcedureLoggerRepository(RequestUtility RequestUtility) : base(RequestUtility)
        {

        }

        public ContactProcedureLogger GetContactLoggerPostpondDate(string contactLoggerId)
        {
            var service = CRMService.Service;
            var logger = service.Retrieve(CrmEntityNamesMapping.ContactProcedureLogger, new Guid(contactLoggerId), new ColumnSet("new_postponddate"));
            return logger.ToEntity<ContactProcedureLogger>();
        }
        public ContactProcedureLogger GetById(string requestId)
        {
            var _service = CRMService.Service;
            var request = _service.Retrieve(CrmEntityNamesMapping.ContactProcedureLogger, new Guid(requestId), new ColumnSet(true)).ToEntity<ContactProcedureLogger>();
            return request;
        }
    }
}
