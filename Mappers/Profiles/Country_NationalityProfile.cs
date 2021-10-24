using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Custom;

namespace Utilities.Mappers.Profiles
{
  public  class Country_NationalityProfile :Profile
    {
        public Country_NationalityProfile()
        {
            CreateMap<NationalityVm, Country>()
               //map from EscSettingsVm to ExcSettings
               .IgnoreAllPropertiesWithAnInaccessibleSetter()
               .ReverseMap()    //map from EscSettings to ExcSettingsVm

               //entity refrence
               .ForMember(a => a.Key, opt => opt.MapFrom(s => s.Id != null ? s.Id.ToString() : null))
               .ForMember(a => a.Value, opt => opt.MapFrom(s => s.Name != null ? s.Name : null))
             
               ;

        }






    }
}
