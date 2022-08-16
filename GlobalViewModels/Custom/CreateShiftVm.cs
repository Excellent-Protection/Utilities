using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Custom
{
   public class CreateShiftVm
    {
        public string ContractId { get; set; }
        public string VisitId { get; set; }
        public DateTime ShiftFrom { get; set; }
        public DateTime ShiftTo { get; set; }
        public bool Shift { get; set; }
        public string ShiftName { get; set; }
    }
}
