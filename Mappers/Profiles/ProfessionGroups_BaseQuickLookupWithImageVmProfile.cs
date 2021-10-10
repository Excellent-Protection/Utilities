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
   public class ProfessionGroups_BaseQuickLookupWithImageVmProfile :Profile
    {

        public ProfessionGroups_BaseQuickLookupWithImageVmProfile()
        {
            CreateMap<BaseQuickLookupWithImageVm, ProfessionGroups>()
      
               .ReverseMap()
                .ForMember(a => a.Key, o => o.MapFrom(s => s.Id))
                .ForMember(a => a.Value, o => o.MapFrom(s => s.Name))
                .ForMember(a=>a.Image , o=>o.MapFrom(s=>s.ImageUrl!=null ?ConfigurationManager.AppSettings["ProfessionGroupsImagesFolder"].ToString() +s.ImageUrl  :""))
                ;
        }
    }
}
