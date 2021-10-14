using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalRepositories.CRM;
using Utilities.Helpers;

namespace Utilities.GlobalManagers.CRM
{
  public  class EmployeeManager : BaseManager , IDisposable
    {
        EmployeeRepository repository;
        public EmployeeManager(RequestUtility requestUtility) : base(requestUtility)
        {
            repository = new EmployeeRepository(requestUtility);
        }

        public void Dispose()
        {
        }

        public int GetAvailableEmployeesCount(string nationalityId, string professionId, string housingid = null)
        {
            try
            {
                return repository.GetAvailableEmployeesCount(nationalityId, professionId, housingid);
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return 0;
        }
    }
}
