using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.Helpers;

namespace Utilities.GlobalRepositories.CRM
{
   public class EmployeeRepository : BaseCrmEntityRepository
    {

        public EmployeeRepository(RequestUtility requestUtility) :
            base(requestUtility, CrmEntityNamesMapping.Employee, "new_employeeid", "new_name", "new_namearabic")
        {

        }

        public int GetAvailableEmployeesCount(string nationalityId, string professionId, string housingid)
        {
            try
            {
                string SQL = "";
                //English Language
                string status = (int)EmployeeStatus.BackLabor + "," + (int)EmployeeStatus.InHousing + "," + (int)EmployeeStatus.New + "," + (int)EmployeeStatus.Remepolize;
                SQL = String.Format(@"select count(*) count from new_employee employee
						   left join new_profession as prof
						 on employee.new_professionId=prof.new_professionId 
                         left join mw_lodginorderdetails as lodgin  
						 on lodgin.mw_lodginorderdetailsId=
						 (
         SELECT  TOP 1 mw_lodginorderdetailsId 
         FROM    mw_lodginorderdetails
         WHERE   mw_lodginorderdetails.mw_empid = employee.new_EmployeeId
		 order by  mw_lodginorderdetails.mw_arrivedate
         )
						 left join new_Country as country
						 on country.new_CountryId=employee.new_nationalityId
						 , new_candidate candidate
                           where 
                           employee.new_CandidateIld = candidate.new_candidateId
                           and employee.new_nationalityId = @nationalityId
                           and employee.new_professionId = @professionId
                           and employee.statecode = 0
                           and employee.new_employeetype = 3
                           and  employee.statuscode in({0})
                           and employee.new_indivcontract is null
and (employee.new_availablefor in (1,3) ) 
andHousingCondition ", status);

                if (!string.IsNullOrEmpty(housingid))
                {
                    SQL = SQL.Replace("andHousingCondition", string.Format("and employee.new_housingbuilding='{0}'", housingid));

                }
                else
                {
                    SQL = SQL.Replace("andHousingCondition", "");
                }
                var paramts = new List<Tuple<string, SqlDbType, object>>() {
          new Tuple<string, SqlDbType, object>("@nationalityId", SqlDbType.UniqueIdentifier, new Guid(nationalityId)),
            new Tuple<string, SqlDbType, object>("@professionId", SqlDbType.UniqueIdentifier, new Guid(professionId)),
        };

                DataTable dt = CRMAccessDB.SelectQ(SQL, paramts).Tables[0];
                var count = dt.Rows[0]["count"] != DBNull.Value ? int.Parse(dt.Rows[0]["count"].ToString()) : 0;

                return count;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

            return 0;
        }
    }
}
