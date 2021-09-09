using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.Labor;
using Utilities.Defaults;

namespace AuthonticationLib.Repositories
{
    public class ApplicationUserRepository : UserManager<ApplicationUser, string>
    {
        public ApplicationUserRepository(IUserStore<ApplicationUser, string> store)
            : base(store)
        {
        }

        public static ApplicationUserRepository Create(IdentityFactoryOptions<ApplicationUserRepository> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserRepository(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context.Get<LaborDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,
            };
            // Configure validation logic for passwords
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromMinutes(DefaultValues.TokenLifespan)
                    };
            }
            return manager;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await this.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public KeyValuePair<int, List<ApplicationUser>> SearchAllPaging(string keyword, int pageSize = 10, int pageNumber = 1)
        {
            int start = ((pageNumber - 1) * pageSize);
            if (start < 0) start = 0;

            var users = Users;

            if (string.IsNullOrEmpty(keyword) == false)
            {
                keyword = keyword.ToLower();
                users = users.Where(p => p.UserName.ToLower().Contains(keyword) || p.Email.ToLower().Contains(keyword));
            }
            var filterdItems = users.ToList();
            return new KeyValuePair<int, List<ApplicationUser>>(filterdItems.Count(), pageSize == 0 ? filterdItems : filterdItems.Skip(start).Take(pageSize).ToList());
        }

        public async Task<IdentityResult> Activate(string id)
        {
            using (LaborDbContext context = new LaborDbContext())
            {
                var currentItem = this.FindById(id);
                currentItem.IsDeactivated = false;
                return await UpdateAsync(currentItem);
            }
        }
    }
}
