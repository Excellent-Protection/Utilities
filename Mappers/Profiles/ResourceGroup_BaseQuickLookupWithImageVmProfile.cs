using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Custome;

namespace Utilities.Mappers.Profiles
{
   public class ResourceGroup_BaseQuickLookupWithImageVmProfile :Profile
    {
        public ResourceGroup_BaseQuickLookupWithImageVmProfile()
        {
            CreateMap<BaseQuickLookupWithImageVm, ResourceGroup>()
               .ReverseMap()
                .ForMember(a => a.Key, o => o.MapFrom(s => s.Id))
                .ForMember(a => a.Value, o => o.MapFrom(s => s.EnglishName))
                .ForMember(a => a.Image, o => o.MapFrom(s => s.ImageUrl != null ? ConfigurationManager.AppSettings["ResourceGroupsImagesFolder"].ToString() + s.ImageUrl : ""))

;
        }
    }
}
