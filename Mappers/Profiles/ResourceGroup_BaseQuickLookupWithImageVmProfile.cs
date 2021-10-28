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
   public class ResourceGroup_BaseQuickLookupWithImageVmProfile :Profile
    {
        public ResourceGroup_BaseQuickLookupWithImageVmProfile()
        {
            CreateMap<BaseQuickLookupWithImageVm, ResourceGroup>()
               .ReverseMap()
                .ForMember(a => a.Key, o => o.MapFrom(s => s.Id))
                .ForMember(a => a.Value, opt => opt.ResolveUsing(new ApplyLanguage(), a => new MappingTranslation(MapperConfig.lang, a.ArabicName, a.EnglishName)))

                .ForMember(a => a.Image, o => o.MapFrom(s => s.ImageUrl != null ? ConfigurationManager.AppSettings["ResourceGroupsImagesFolder"].ToString() + s.ImageUrl : ""))

;
        }
    }
}
