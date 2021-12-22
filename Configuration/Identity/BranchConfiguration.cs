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
    public class BranchConfiguration : EntityTypeConfiguration<Branch>
    {
        public BranchConfiguration()
        {
            
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);
            Property(a => a.OwnerId).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.ArabicName).HasMaxLength(250);
            Property(a => a.ArabicAddress).HasMaxLength(400);
            Property(a => a.EnglishName).HasMaxLength(250);
            Property(a => a.EnglishAddress).HasMaxLength(400);
            Property(a => a.PhoneNumber).HasMaxLength(250);
            Property(a => a.MapUrl).HasMaxLength(250);
            Property(a => a.EmpedMapUrl).HasMaxLength(400);
            Property(a => a.FaceBookLink).HasMaxLength(250);
            Property(a => a.InstagramLink).HasMaxLength(250);
            Property(a => a.TwitterLink).HasMaxLength(250);
            Property(a => a.ArabicArea).HasMaxLength(250);
            Property(a => a.EnglishArea).HasMaxLength(250);
            Property(a => a.ArabicCity).HasMaxLength(250);
            Property(a => a.EnglishCity).HasMaxLength(250);
            Property(a => a.ArabicCountry).HasMaxLength(250);
            Property(a => a.EnglishCountry).HasMaxLength(250);
            Property(a => a.ImagePath).HasMaxLength(400);

        }
        
    }
}
