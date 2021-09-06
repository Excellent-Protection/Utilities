using Microsoft.AspNet.Identity.EntityFramework;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities.DataAccess.Labor
{ 
    public class LaborDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public LaborDbContext()
            : base("EsadLaborDbContext")
        {
        }

        static LaborDbContext()
        {
            Database.SetInitializer<LaborDbContext>(null);
           
        }

        public static LaborDbContext Create()
        {
            return new LaborDbContext();
        }


        //MOdels
        #region Models
        public DbSet<Setting> Settings { get; set; }
        public DbSet<DynamicTemplate> DynamicTemplates { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<ReceiptVoucher> ReceiptVouchers { get; set; }
        public DbSet<Models.Labor.DbSqlQuery> DbSqlQueries { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<UrlShortener> UrlShorteners { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<StepData> StepDatas { get; set; }
        public DbSet<paymentMethod> paymentMethod { get; set; }
        public DbSet<TamayouzIntegratedDiscount> TamayouzIntegratedDiscount { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new ApplicationPageConfiguration());
            //modelBuilder.Configurations.Add(new ApplicationRoleConfiguration());
            //modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            //modelBuilder.Configurations.Add(new DeviceConfiguration());
            //modelBuilder.Configurations.Add(new SettingConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IEntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
                ? HttpContext.Current.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IEntityBase)entity.Entity).CreatedOn = DateTime.Now;
                    ((IEntityBase)entity.Entity).CreatedBy = currentUsername;
                }

                if (entity.State == EntityState.Deleted)
                {
                    ((IEntityBase)entity.Entity).DeletedOn = DateTime.Now;
                    ((IEntityBase)entity.Entity).DeletedBy = currentUsername;
                }

                ((IEntityBase)entity.Entity).ModifiedOn = DateTime.Now;
                ((IEntityBase)entity.Entity).ModifiedBy = currentUsername;
            }
        }
    }
}
