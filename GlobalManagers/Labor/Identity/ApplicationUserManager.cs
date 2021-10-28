using AuthonticationLib.Repositories;
using AuthonticationLib.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.Labor.Identity
{
 public   class ApplicationUserManager : BaseManager
    {
        private ApplicationUserRepository _repository;

        public ApplicationUserManager(RequestUtility requestUtility) : base(requestUtility)
        {
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
    }
}

