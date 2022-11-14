using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    class PaymentMethodConfiguration : EntityTypeConfiguration<paymentMethod>
    {
        public PaymentMethodConfiguration()
        {
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            Property(a => a.EntityId).HasMaxLength(250);
            Property(a => a.Authorization).HasMaxLength(250);
            Property(a => a.CheckOutUrl).HasMaxLength(250);
            Property(a => a.QueryUrl).HasMaxLength(250);
            Property(a => a.BrandName).HasMaxLength(50);
            Property(a => a.ImageUrl).HasMaxLength(400);
        }
    }
}
