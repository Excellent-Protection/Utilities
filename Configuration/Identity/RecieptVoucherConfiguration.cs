using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    class RecieptVoucherConfiguration : EntityTypeConfiguration<ReceiptVoucher>
    {
        public RecieptVoucherConfiguration()
        {
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            Property(a => a.CustomerId).HasMaxLength(250);
            Property(a => a.ContractId).HasMaxLength(250);
            Property(a => a.ContractNumber).HasMaxLength(50);
            Property(a => a.PaymentCode).HasMaxLength(50);
        }
    }
}
