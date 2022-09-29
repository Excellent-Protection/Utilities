using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalViewModels.Custom;
using Utilities.Mappers.Resolvers;

namespace Utilities.Mappers.Profiles
{
    public class EditContactVm_ContactProfile : Profile
    {
        public EditContactVm_ContactProfile()
        {
            CreateMap<EditContactVm, Contact>()
                     .ForMember(a => a.CityId, o => o.MapFrom(s => s.CityId != null ? new EntityReference(CrmEntityNamesMapping.City, new Guid(s.CityId)) : null))
                     .ForMember(a => a.NationalityId, o => o.MapFrom(s => s.NationalityId != null ? new EntityReference(CrmEntityNamesMapping.Nationality, new Guid(s.NationalityId)) : null))
                     .ForMember(a => a.GenderId, o => o.MapFrom(s => s.Gender.HasValue ? new OptionSetValue(s.Gender.Value) : null))
                     .ForMember(a => a.IdentificationNo, o => o.MapFrom(s => s.IdNumber))
                     .ForMember(a => a.OtherMobilePhone, o => o.MapFrom(s => s.OtherMobilePhone))
                     .ForMember(a => a.MobilePhone, o => o.MapFrom(s => s.MobilePhone))
                     .ForMember(a => a.FullName, o => o.MapFrom(s => s.FullName))
                     .ForMember(a => a.Email, o => o.MapFrom(s => s.Email))
                     .ReverseMap()
                   .ForMember(a => a.IdNumber, o => o.MapFrom(s => s.IdentificationNo))
                   .ForMember(a => a.Gender, o => o.MapFrom(s => s.GenderId))
                   .ForMember(a => a.OtherMobilePhone, o => o.MapFrom(s => s.OtherMobilePhone))
                   .ForMember(a => a.MobilePhone, o => o.MapFrom(s => s.MobilePhone))
                   .ForMember(a => a.FullName, o => o.MapFrom(s => s.FullName))
                   .ForMember(a => a.Email, o => o.MapFrom(s => s.Email))
                   .ForMember(a => a.NationalityId, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.NationalityId))
                   .ForMember(a => a.CityId, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.CityId))

                     ;
        }
    }
}