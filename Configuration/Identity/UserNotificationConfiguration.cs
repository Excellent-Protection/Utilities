using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Configuration.Identity
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            Property(a => a.Text).HasMaxLength(250);
            Property(a => a.DeviceId).HasMaxLength(250);
            Property(a => a.CrmUserId).HasMaxLength(250);
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            Property(a => a.OwnerId).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
 

        }
    }
}
