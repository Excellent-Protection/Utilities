using AuthonticationLib.ViewModels;
using AutoMapper;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthonticationLib.Mappers
{
  public  class ApplicationUser_ApplicationUserVmProfile :Profile
    {
        public ApplicationUser_ApplicationUserVmProfile()
        {
            CreateMap< ApplicationUserVm, ApplicationUser>();
        }
    }
}
