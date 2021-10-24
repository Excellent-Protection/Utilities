using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.CRM
{
   public class CollectionVm
    {
        public string CollectionId { get; set; }
        public string Name { get; set; }
        public string PointNotes { get; set; }
        public string PointNotesEN { get; set; }
        public decimal Amount { get; set; }
        public decimal? WalletAmount { get; set; }
        public string IndividualProcedureId { get; set; }
        public string HourlyContractId { get; set; }
        public string IndividualContractRequestId { get; set; }
        public int? PaymentMethod { get; set; }
        public int? PaymentType { get; set; }
        public int? IsWalletBalance { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string ContactId { get; set; }
        public string EntityId { get; set; }
        public string EntityName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? Points { get; set; }
        public string CreatedOnString { get; set; }
    }
}
