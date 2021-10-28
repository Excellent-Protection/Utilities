using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.Mappers.Resolvers;

namespace Utilities.Mappers.Profiles
{
   public class Nationality_BaseQuickLookupVmProfile :Profile
    {
        public Nationality_BaseQuickLookupVmProfile()
        {
            CreateMap<BaseQuickLookupVm, Country>()
           .IgnoreAllPropertiesWithAnInaccessibleSetter()
           .ReverseMap()
           .ForMember(a => a.Key, opt => opt.MapFrom(s => s.Id != null ? s.Id.ToString() : null))
           .ForMember(a => a.Value, opt => opt.ResolveUsing(new ApplyLanguage(), a => new MappingTranslation(MapperConfig.lang, a.Name, a.EnglishName)))

           ;
        }

    }
}
