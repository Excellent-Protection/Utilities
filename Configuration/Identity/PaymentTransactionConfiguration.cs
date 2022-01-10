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
    class PaymentTransactionConfiguration : EntityTypeConfiguration<PaymentTransaction>
    {
        public PaymentTransactionConfiguration()
        {
            HasKey(a => a.Id);
            Property(a => a.CustomerEmail).HasMaxLength(250);
            Property(a => a.CustomerId).HasMaxLength(250);
            Property(a => a.PaymentBrand).HasMaxLength(50);
            Property(a => a.RequestedPaymentBrand).HasMaxLength(50);
            Property(a => a.CardHolder).HasMaxLength(100);
            Property(a => a.CardBinCountry).HasMaxLength(50);
            Property(a => a.ContractId).HasMaxLength(250);
            Property(a => a.EntityName).HasMaxLength(250);
            Property(a => a.TransactionTypeName).HasMaxLength(50);
            Property(a => a.MerchantTransactionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }))
                ;
            Property(a => a.PaymentStatus).HasMaxLength(20);
            Property(a => a.PaymentStatusName).HasMaxLength(250);
            Property(a => a.SourceOfFundsIssuer).HasMaxLength(250);
            Property(a => a.TransactionAuthorizeCode).HasMaxLength(250);
            Property(a => a.CustomerIPCountry).HasMaxLength(250);
            Property(a => a.CreatedBy).HasMaxLength(250);
            Property(a => a.ModifiedBy).HasMaxLength(250);
            Property(a => a.Name).HasMaxLength(250);
            Property(a => a.DeletedBy).HasMaxLength(250);

        }
    }

}
