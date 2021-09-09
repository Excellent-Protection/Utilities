using AuthonticationLib.Repositories;
using Microsoft.AspNet.Identity.EntityFramework;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.DataAccess.Labor.Identity
{
    public class AuthRepository : IDisposable
    {
        private LaborDbContext _ctx;

        private ApplicationUserRepository _userManager;

        public AuthRepository()
        {
            _ctx = new LaborDbContext();
            _userManager = new ApplicationUserRepository(new UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(_ctx));
        }

        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
