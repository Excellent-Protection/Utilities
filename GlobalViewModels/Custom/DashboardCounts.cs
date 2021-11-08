using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Custom
{

    public class ContractsCounts
    {
        public int RunningContracts { get; set; }
        public int AlmostOverContracts { get; set; }
        public int CanceledContracts { get; set; }
        public int CommingVisits { get; set; }
    }

    public class TicketsCounts
    {
        public int AllTickets { get; set; }
        public int OpeningTickets { get; set; }
        public int ClosedTickets { get; set; }
    }
  public  class DashboardCounts
    {
        public ContractsCounts ContractsCounts { get; set; }
        public TicketsCounts TicketsCounts { get; set; }
    }
}
