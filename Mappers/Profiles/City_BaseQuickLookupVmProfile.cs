using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.Mappers.Resolvers;

namespace Utilities.Mappers.Profiles
{
   public class City_BaseQuickLookupVmProfile :Profile
    {

        public City_BaseQuickLookupVmProfile()
        {
            CreateMap<BaseQuickLookupVm, City>()

           .IgnoreAllPropertiesWithAnInaccessibleSetter()
           .ReverseMap()
           .ForMember(a => a.Key, opt => opt.MapFrom(s =>s.Id != null ? s.Id.ToString() : null))
           .ForMember(a => a.Value, opt => opt.ResolveUsing(new ApplyLanguage(),src=>new MappingTranslation(MapperConfig.lang,src.ArabicName,src.EnglishName)))


            ;
        }
    }
}
