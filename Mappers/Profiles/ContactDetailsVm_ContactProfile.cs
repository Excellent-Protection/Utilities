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
                     .ForMember(a => a.IdentificationNo, o => o.MapFrom(s => s.IdNumber));

        }
    }
}
