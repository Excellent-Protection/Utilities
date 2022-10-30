using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.CRM
{
    public class ContactProcedureLoggerVm
    {
        public DateTime? PostpondDate { get; set; }
        public string Contract { get; set; }
        public string Visit { get; set; }
        public string CustomerId { get; set; }
        public DateTime? VisitDate { get; set; }
        public int? Proceduretype { get; set; }

    }


}
