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
using Utilities.Helpers;

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

        public async Task<bool> GetUserByCrmUserIdAndUpdateReletedEntities(string id)
        {
            try
            {
                using (UnitOfWork _unitOfWork = new UnitOfWork(new DbFactory()))
                {
                    var currentItem = this.Users.FirstOrDefault(u => u.CrmUserId == id);
                    var devicesList = _unitOfWork.Repository<Device>().GetAll().Where(d => d.UserId == currentItem.Id).ToList();
                    if (devicesList.Count() > 0)
                    {
                        foreach (var item in devicesList)
                        {
                            item.IsDeleted = true;

                            _unitOfWork.Repository<Device>().Update(item); 
                        }
                    }
                

                    var notificationList = _unitOfWork.Repository<UserNotification>().GetAll().Where(ui => ui.CrmUserId == id).ToList();
                    if (notificationList.Count() > 0)
                    {
                        foreach (var item in notificationList)
                        {

                            item.IsDeleted = true;
                            _unitOfWork.Repository<UserNotification>().Update(item);

                        }
                    }

                
                    currentItem.IsDeleted = true;
                    currentItem.PhoneNumberConfirmed = false;
                    var result = await UpdateAsync(currentItem);
            
                    return true;
                }
            }
           
            catch (Exception ex)

            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }

            

        }



        public async Task<bool> ActivateOrDeactivateAsync(string id, string Status)
        {

            try
            {
                using (UnitOfWork _unitOdWork = new UnitOfWork(new DbFactory()))
                {
                    var currentItem = this.Users.FirstOrDefault(u => u.CrmUserId.ToLower() == id);
                    var devicesList = _unitOdWork.Repository<Device>().GetAll().Where(d => d.UserId == currentItem.Id).ToList();

                    //Reactivate user with devices and notifications
                    var notificationList = _unitOdWork.Repository<UserNotification>().GetAll().Where(ui => ui.CrmUserId == id).ToList();

                    if (currentItem !=null && currentItem.IsDeactivated == true && Status.ToLower() == "active")


                    {
                        //Reactivate related devices
                      
                        if (devicesList.Count() > 0)
                        {
                            foreach (var item in devicesList)
                            {
                                item.IsDeactivated = false;
                                _unitOdWork.Repository<Device>().Update(item);
                            }

                        }
                        //
                        //Reactivate related notifications


                        if (notificationList.Count() > 0)
                        {
                            foreach (var item in notificationList)
                            {

                                item.IsDeactivated = false;
                                _unitOdWork.Repository<UserNotification>().Update(item);

                            }
                        }

                       //
                       //Reactivate user
                        currentItem.IsDeactivated = false;
                        var result = await UpdateAsync(currentItem);
                     //

    
                        if (result.Succeeded)
                            return true;
                        return false;
                    }

                    //Deactivate user with devices and notifications

                    else 
                    {
                        if (currentItem != null && currentItem.IsDeactivated == false && Status.ToLower() == "inactive")
                        { 
                            //Deactivate related devices
                         
                        if (devicesList.Count() > 0)
                        {
                            foreach (var item in devicesList)
                            {
                                item.IsDeactivated = true;
                                _unitOdWork.Repository<Device>().Update(item);
                            }

                        }
                         //
                         //Deactivate related notifications
                        if (notificationList.Count() > 0)
                        {
                            foreach (var item in notificationList)
                            {

                                item.IsDeactivated = true;
                                _unitOdWork.Repository<UserNotification>().Update(item);

                            }
                        }
                        //
                        //Deactivate user
                        currentItem.IsDeactivated = true;
                        var result = await UpdateAsync(currentItem);
                        //
                        if (result.Succeeded)
                            return true;
                    }
                        return false;

                    
                       
                       
                      
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name + ex.InnerException == null ? ex.Message : ex.InnerException.Message);

                
                return false;
            }

        } 
         public  bool UpdateRelatedDevicesAndNotificationsIfExist(string userId, string oldCrmUserId , string newCrmUserId)
        { try
            {
                using (UnitOfWork _unitOfWork  = new UnitOfWork(new DbFactory()))
                {
                    var devicesList = _unitOfWork.Repository<Device>().GetAll().Where(d => d.UserId == userId).ToList();
                    if (devicesList.Count() > 0)
                    {
                        foreach (var item in devicesList)
                        {
                            item.IsDeleted = false;
                             _unitOfWork.Repository<Device>().Update(item);

                        }
                      
                    }
                    var notificationList = _unitOfWork.Repository<UserNotification>().GetAll().Where(ui => ui.CrmUserId == oldCrmUserId).ToList();
                    if (notificationList.Count() > 0)
                    {
                        foreach (var item in notificationList)
                        {

                            item.IsDeleted = false;
                            item.CrmUserId = newCrmUserId;
                            _unitOfWork.Repository<UserNotification>().Update(item);

                        }
                    }
                    _unitOfWork.SaveChanges();
                }
                return true; 
            } 
            catch(Exception ex )
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name + ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return false; 
            }
        }


    }
}
