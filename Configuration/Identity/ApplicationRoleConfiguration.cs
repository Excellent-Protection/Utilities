using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    class ApplicationRoleConfiguration: EntityTypeConfiguration<ApplicationRole>
    {
        public ApplicationRoleConfiguration()
        {
            HasMany(a => a.ApplicationPages).WithMany(a => a.ApplicationRoles)
              .Map(a => a.MapLeftKey("ApplicationRoleId").MapRightKey("ApplicationPageId")
              .ToTable("ApplicationRolePage"));
            HasKey(a => a.Id);
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
        }
    }
}
