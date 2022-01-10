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
    public class DbSqlQueryConfiguration : EntityTypeConfiguration<DbSqlQuery>
    {
        public DbSqlQueryConfiguration()
        {
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.Description).HasMaxLength(250);
            Property(a => a.Query).HasMaxLength(250);
   

        }
    }
}
