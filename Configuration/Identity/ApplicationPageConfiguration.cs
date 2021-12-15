using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    public class ApplicationPageConfiguration: EntityTypeConfiguration<ApplicationPage>
    {
        public ApplicationPageConfiguration()
        {
            HasKey(a => a.Id);
            HasMany(a => a.ChildernPages).WithMany(a => a.ParentPages)
                .Map(a=>a.MapLeftKey("ApplicationPage_ApplicationPageId")
                .MapRightKey("ApplicationPage_ApplicationPageId1")
                .ToTable("ApplicationPageParents"));
            Property(a => a.Id).HasColumnName("ApplicationPageId");
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);

        }
    }
}
