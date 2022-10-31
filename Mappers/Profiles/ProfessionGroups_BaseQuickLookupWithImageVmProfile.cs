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
                  .ForMember(a => a.Value, opt => opt.ResolveUsing(new ApplyLanguage(), a => new MappingTranslation(MapperConfig.lang, a.NameAr, a.Attributes.Contains("new_displayenglishname") ? a.Attributes["new_displayenglishname"].ToString() : (a.Name!=null?a.Name:null))))

                .ForMember(a=>a.Image , o=>o.MapFrom(s=>s.ImageUrl!=null ?ConfigurationManager.AppSettings["ProfessionGroupsImagesFolder"].ToString() +s.ImageUrl  :""))
                .ForMember(a=>a.Description, opt => opt.ResolveUsing(new ApplyLanguage(), a => new MappingTranslation(MapperConfig.lang, a.Description, a.Attributes.Contains("new_descriptionenglish") ? a.Attributes["new_descriptionenglish"].ToString() : null)))
                ;
        }
    }
}
