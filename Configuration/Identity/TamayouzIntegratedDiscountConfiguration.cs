using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    class TamayouzIntegratedDiscountConfiguration : EntityTypeConfiguration<TamayouzIntegratedDiscount>
    {
        public TamayouzIntegratedDiscountConfiguration()
        {
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            Property(a => a.RequestDate).HasMaxLength(250);
            Property(a => a.ResponseCode).HasMaxLength(100);
            Property(a => a.BranchId).HasMaxLength(250);
            Property(a => a.SurName).HasMaxLength(100);
            Property(a => a.BranchName).HasMaxLength(100);
            Property(a => a.GlobalId).HasMaxLength(250);
            Property(a => a.ContractId).HasMaxLength(250);
            Property(a => a.ApiVersion).HasMaxLength(100);
        }
    }
}
