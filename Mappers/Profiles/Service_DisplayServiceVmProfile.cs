using AutoMapper;
using HourlySectorLib.ViewModels.Custom;
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
    public class Service_DisplayServiceVmProfile : Profile
    {

        public Service_DisplayServiceVmProfile()
        {
            CreateMap<DisplayServiceVm, Service>()

           .IgnoreAllPropertiesWithAnInaccessibleSetter()
           .ReverseMap()
           .ForMember(a => a.Id, opt => opt.MapFrom(s => s.Id != null ? s.Id.ToString() : null))
           .ForMember(a => a.IconUrl, opt => opt.MapFrom(s => s.IconImageUrl != null ? ConfigurationManager.AppSettings["ServiceImages"].ToString() + s.IconImageUrl : null))
           .ForMember(a => a.Name, opt => opt.ResolveUsing(new ApplyLanguage(), src => new MappingTranslation(MapperConfig.lang, src.ArabicName, src.EnglishName)))
           .ForMember(a => a.Description, opt => opt.ResolveUsing(new ApplyLanguage(), src => new MappingTranslation(MapperConfig.lang, src.ArabicDescription, src.EnglishDescription)))
           .ForMember(a => a.ServiceNote, opt => opt.ResolveUsing(new ApplyLanguage(), src => new MappingTranslation(MapperConfig.lang, src.ServiceNoteAr, src.ServiceNoteEn)))
          
             

           ;
        }
    }
}
