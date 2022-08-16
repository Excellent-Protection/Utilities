using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.CRM
{
   public class ContactProcedureLoggerManager: BaseManager , IDisposable
    {
        ContactProcedureLoggerRepository _repo;
        public ContactProcedureLoggerManager(RequestUtility requestUtility): base(requestUtility)
        {

            _repo = new ContactProcedureLoggerRepository(requestUtility);
        }
        public ContactProcedureLoggerVm GetProcedurePostpondDate(string procedureId)
        {
            try
            {
                var procedureContract = _repo.GetContactLoggerPostpondDate(procedureId).Toclass<ContactProcedureLoggerVm>();
                return procedureContract;
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return null;

        }
        public ContactProcedureLoggerVm GetById(string requestId)
        {
            try
            {
                var request = _repo.GetById(requestId).Toclass<ContactProcedureLoggerVm>();
                return request;
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return null;
        }
        public void Dispose()
        {
        }

    }
}
