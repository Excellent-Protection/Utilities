using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    public class UrlShortenerConfiguration : EntityTypeConfiguration<UrlShortener>
    {
        public UrlShortenerConfiguration()
        {
     
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            Property(a => a.OwnerId).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            //Property(a => a.RegisteredEmailAddress).HasMaxLength(250);
            //Property(a => a.RegisteredEmailPass).HasMaxLength(250);
            //Property(a => a.BitlyUserName).HasMaxLength(250);
            //Property(a => a.BitlyPass).HasMaxLength(250);
            //Property(a => a.AccessToken).HasMaxLength(250);

        }
    }
}
