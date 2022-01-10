using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasMany(a => a.Devices).WithOptional(a => a.User).HasForeignKey(a => a.UserId);
            Property(a => a.CrmUserId).HasMaxLength(250);
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            
        }
    }
}
