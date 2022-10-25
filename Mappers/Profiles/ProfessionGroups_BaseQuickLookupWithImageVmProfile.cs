using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Custom;
using Utilities.Mappers.Resolvers;

namespace Utilities.Mappers.Profiles
{
   public class ProfessionGroups_BaseQuickLookupWithImageVmProfile :Profile
    {

        public ProfessionGroups_BaseQuickLookupWithImageVmProfile()
        {
            CreateMap<BaseQuickLookupWithImageVm, ProfessionGroups>()
      
               .ReverseMap()
                .ForMember(a => a.Key, o => o.MapFrom(s => s.Id))
                  .ForMember(a => a.Value, opt => opt.ResolveUsing(new ApplyLanguage(), a => new MappingTranslation(MapperConfig.lang, a.NameAr, a.Name)))

                .ForMember(a=>a.Image , o=>o.MapFrom(s=>s.ImageUrl!=null ?ConfigurationManager.AppSettings["ProfessionGroupsImagesFolder"].ToString() +s.ImageUrl  :""))
                .ForMember(a=>a.Description,o=>o.MapFrom(s=>s.Description))
                ;
        }
    }
}
