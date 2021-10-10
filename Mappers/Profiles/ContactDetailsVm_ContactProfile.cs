using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalViewModels.Custome;
using Utilities.Mappers.Resolver;

namespace Utilities.Mappers.Profiles
{
    public class ContactDetailsVm_ContactProfile : Profile
    {
        public ContactDetailsVm_ContactProfile()
        {
            CreateMap<ContactDetailsVm, Contact>()
                     .ForMember(a => a.CityId, o => o.MapFrom(s => s.CityId != null ? new EntityReference(CrmEntityNamesMapping.City, new Guid(s.CityId)) : null))
                     .ForMember(a => a.NationalityId, o => o.MapFrom(s => s.NationalityId != null ? new EntityReference(CrmEntityNamesMapping.Nationality, new Guid(s.NationalityId)) : null))
                     .ForMember(a => a.GenderId, o => o.MapFrom(s => s.Gender.HasValue ? new OptionSetValue(s.Gender.Value) : null))
                     .ForMember(a => a.IdentificationNo, o => o.MapFrom(s => s.IdNumber))
                     .ForMember(a => a.JobTitle, o => o.MapFrom(s => s.JobTitle))
                     .ForMember(a => a.Email, o => o.MapFrom(s => s.Email))
                     .ReverseMap()
                   .ForMember(a => a.IdNumber, o => o.MapFrom(s => s.IdentificationNo))
                   .ForMember(a => a.Gender, o => o.MapFrom(s => s.GenderId))
                               .ForMember(a => a.JobTitle, o => o.MapFrom(s => s.JobTitle))
                     .ForMember(a => a.Email, o => o.MapFrom(s => s.Email))
                   .ForMember(a => a.NationalityId, o => o.ResolveUsing(new EntityReferanceToStringId(), s => s.NationalityId))
                   .ForMember(a => a.CityId, o => o.ResolveUsing(new EntityReferanceToStringId(), s => s.CityId))
                   
                     ;

        }
    }
}
