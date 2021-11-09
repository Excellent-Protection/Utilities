using AutoMapper;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Custom;

namespace Utilities.Mappers.Profiles
{
 public   class ApplicationUser_UserProfileDataVmProfile :Profile
    {
        public ApplicationUser_UserProfileDataVmProfile()
        {
            CreateMap<UserProfileDataVm, ApplicationUser>();

        }
    }
}
