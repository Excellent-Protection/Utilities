using Models.Labor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    public class SettingConfiguration : EntityTypeConfiguration<Setting>
    {
        public SettingConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(250).HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            Property(a => a.OwnerId).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.Description).HasMaxLength(250);

        }
    }
}
