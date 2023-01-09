using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using Models.CRM.Individual_Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
    public class DashboardManager : BaseManager, IDisposable
    {
        public DashboardManager(RequestUtility requestUtility) : base(requestUtility)
        {

        }

        public ResponseVm<DashboardCounts> DashboardCounts(string contactId)
        {
            try
            {
                DashboardCounts obj = new DashboardCounts();
                obj.ContractsCounts = ContactContractsCount(contactId);
                obj.TicketsCounts = CustomerTicketsCounts(contactId);
                return new ResponseVm<DashboardCounts> { Status = HttpStatusCodeEnum.Ok, Data = obj };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return new ResponseVm<DashboardCounts> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred", "Shared") };
        }


        public ContractsCounts ContactContractsCount(string contactId)
        {
            try
            {
                var _service = CRMService.Service;
                ContractsCounts obj = new ContractsCounts();
                // contact individual contracts 
                QueryExpression indvQuery = new QueryExpression(CrmEntityNamesMapping.IndividualContract);
                indvQuery.Criteria.AddCondition("new_contact", ConditionOperator.Equal, contactId);
                indvQuery.ColumnSet = new ColumnSet("statuscode", "new_serviceenddate");
                var indvContracts = _service.RetrieveMultiple(indvQuery).Entities.Select(a => a.ToEntity<IndividualContract>()).ToList();
                // contact hourly contracts 
                QueryExpression hourlyQuery = new QueryExpression(CrmEntityNamesMapping.ServiceContractPerHour);
                hourlyQuery.Criteria.AddCondition("new_hindivclintname", ConditionOperator.Equal, contactId);
                hourlyQuery.ColumnSet = new ColumnSet("new_totalnotfinished", "statuscode", "new_employeenumber");
                var hourlyContracts = _service.RetrieveMultiple(hourlyQuery).Entities.Select(a => a.ToEntity<HourlyContract>()).ToList();

                obj.RunningContracts =
                    indvContracts.Where(a => a.StatusCode.Value == (int)IndividualContractStatus.ActiveLaborDelivered).ToList().Count()
                    + hourlyContracts.Where(a => a.StatusCode.Value == (int)ServiceContractPerHourStatus.ConfirmedByFinance).ToList().Count();
                obj.CanceledContracts =
                    indvContracts.Where(a => a.StatusCode.Value == (int)IndividualContractStatus.Cancelled).ToList().Count()
                + hourlyContracts.Where(a => a.StatusCode.Value == (int)ServiceContractPerHourStatus.Canceled).ToList().Count();

                ExcSettingsManager excSettingManager = new ExcSettingsManager(RequestUtility);
                var defaultDays = excSettingManager.GetSettingValueByName(DefaultValues.DaysBeforeEndContractToShowRenewBtnSettingName, DefaultValues.DaysBeforeEndContractToShowRenewBtn);

                obj.AlmostOverContracts = indvContracts.Where(a => a.StatusCode.Value == (int)IndividualContractStatus.ActiveLaborDelivered && a.ServiceEndDate.HasValue && (DateTime.Now - a.ServiceEndDate.Value).TotalDays <= defaultDays).ToList().Count()
                   + hourlyContracts.Where(a => a.StatusCode.Value == (int)ServiceContractPerHourStatus.ConfirmedByFinance && a.RemainingVisit == 1).ToList().Count();

                var confirmedContracts = hourlyContracts.Where(a => a.StatusCode.Value == (int)ServiceContractPerHourStatus.ConfirmedByFinance);
              
                obj.CommingVisits = confirmedContracts.
                    Select(a => new
                    {
                        remain = a.TotalNotFinishedVisits != null ? a.TotalNotFinishedVisits.Value / (a.NoOfWorker == null ? 1 : a.NoOfWorker.Value) : 0
                    }).
                    Select(a => a.remain).Sum();
                return obj;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }




        public TicketsCounts CustomerTicketsCounts(string contactId)
        {
            try
            {
                var _service = CRMService.Service;
                TicketsCounts obj = new TicketsCounts();
                QueryExpression query = new QueryExpression(CrmEntityNamesMapping.CustomerTicket);
                query.Criteria.AddCondition("new_contact", ConditionOperator.Equal, contactId);
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                query.ColumnSet = new ColumnSet("statuscode");
                var tickets = _service.RetrieveMultiple(query).Entities.Select(e => e.ToEntity<CustomerTicket>()).ToList();
                //obj.ClosedTickets = tickets.Where(a => a.Status.Value == (int)TicketStatus.ClosedByClient || a.Status.Value == (int)TicketStatus.Rejected ).Count();
                obj.ClosedTickets = tickets.Where(a => a.Status.Value == (int)CustomerTicketStatus.Servicehadstopped || a.Status.Value == (int)CustomerTicketStatus.Servicehaddonesuccessfully).Count();
                obj.AllTickets = tickets.Count();
                //obj.OpeningTickets = tickets.Where(a => a.Status.Value != (int)TicketStatus.Cancelled || a.Status.Value != (int)TicketStatus.Rejected || a.Status.Value != (int)TicketStatus.ClosedByClient).ToList().Count();
                obj.OpeningTickets = tickets.Where(a => a.Status.Value != (int)CustomerTicketStatus.Servicehadstopped && a.Status.Value != (int)CustomerTicketStatus.Servicehaddonesuccessfully).ToList().Count();
                return obj;
            }
            catch (Exception ex)
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
