using AuthonticationLib.Repositories;
using AuthonticationLib.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities.DataAccess.Labor;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.Labor.Identity
{
 public   class ApplicationUserManager : BaseManager

    {
        private LaborDbContext _ctx;
        private ApplicationUserRepository _repository;

        public ApplicationUserManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _ctx = new LaborDbContext();
            _repository = new ApplicationUserRepository(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(_ctx));
            _repository.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser> { });

        }

        ApplicationUserRepository Repository
        {
            get
            {
                return _repository ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserRepository>();
            }
            set
            {
                _repository = value;
            }
        }



        //public async Task<ApplicationUserVm> FindById(string id)
        //{
        //    var user = await Repository.FindByIdAsync(id);
        //    return user.ToApplicationUserVModel<ApplicationUser, ApplicationUserVm>();
        //}
        //public ApplicationUserVm FindByPhone(string userMobile)
        //{
        //    var user = Repository.Users.FirstOrDefault(t => t.PhoneNumber == userMobile);
        //    return user.ToApplicationUserVModel<ApplicationUser, ApplicationUserVm>();
        //}
        //public ApplicationUserVm FindByName(string userMobile)
        //{
        //    var user = new EsadLaborDbContext().Users.FirstOrDefault(a => a.PhoneNumber == userMobile);
        //    //var user = Repository.FindByName(userMobile);
        //    return user.ToApplicationUserVModel<ApplicationUser, ApplicationUserVm>();
        //}
        //public ApplicationUserVm FindByUserName(string username)
        //{
        //    var user = new EsadLaborDbContext().Users.FirstOrDefault(a => a.UserName == username);
        //    return user.ToApplicationUserVModel<ApplicationUser, ApplicationUserVm>();
        //}

        //public string GenerateTwoFactorTokenAndSendSMS(string UserId, string MobileNumber)
        //{
        //    var code = Repository.GenerateTwoFactorTokenAsync(UserId, "PhoneCode").Result;
        //    SetTwoFactorAuthCookie(UserId);

        //    var user = FindById(UserId);
        //    //Send CodeTo Email
        //    if (!string.IsNullOrEmpty(user.Result.Email))
        //        MailManager.SendEmail(user.Result.Email, "", "رمز تسجيل الدخول لخدمة حاضر", " من فضلك استخدم الكود الاّتى لإستكمال تسجيل حسابك في خدمة حاضر" + code, false, null);

        //    var IsSent = new SmsManager().Send(" من فضلك استخدم الكود الاّتى لإستكمال تسجيل الدخول  " + code, MobileNumber);

        //    return code;

        //}

        //public Task<bool> VerifyTwoFactorToken(string UserId, string code)
        //{
        //    var token = Repository.VerifyTwoFactorTokenAsync(UserId, "PhoneCode", code);
        //    return token;
        //}
        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.Current.GetOwinContext().Authentication;
        //    }
        //}

        //public void SetTwoFactorAuthCookie(string userId)
        //{
        //    ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
        //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
        //    AuthenticationManager.SignIn(identity);
        //}

        //public async Task<ApplicationUser> FindAsync(string Username, string Password)
        //{
        //    var user = await Repository.FindAsync(Username, Password);
        //    return user;
        //}


        public async Task<ApplicationUserVm> FindById(string id)
        {
            var user = await Repository.FindByIdAsync(id);
            return user.ToApplicationUserVModel<ApplicationUser, ApplicationUserVm>();
        }
        public async Task<ApplicationUser> FindAsync(string Username, string Password)
        {
            var user = await Repository.FindAsync(Username, Password);
            return user;
        }
        public async Task<string> GetCrmUserId(string currentUserId)
        {
            try
            {
                var user = await FindById(currentUserId);
                if (user == null)
                    return null;

                return user.CrmUserId;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }

        }

        public ResponseVm<UserProfileDataVm> GetUserProfileData(string userId)
        {
            try
            {
                var user = _repository.Users.FirstOrDefault(t => t.Id == userId).ToApplicationUserVModel<ApplicationUser, UserProfileDataVm>();
                return new ResponseVm<UserProfileDataVm> { Status = HttpStatusCodeEnum.Ok, Data = user };
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return new ResponseVm<UserProfileDataVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred", "Shared") };
        }

    }
}

