using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels;

namespace Utilities.Mappers.Profiles
{
   public class City_BaseQuickLookupVmProfile :Profile
    {

        public City_BaseQuickLookupVmProfile()
        {
            CreateMap<BaseQuickLookupVm, City>()

           .IgnoreAllPropertiesWithAnInaccessibleSetter()
           .ReverseMap()
           .ForMember(a => a.Key, opt => opt.MapFrom(s => s.Id != null ? s.Id.ToString() : null))
           .ForMember(a => a.Value, opt => opt.MapFrom(s => s.EnglishName))
     
           ;
        }
    }
}
