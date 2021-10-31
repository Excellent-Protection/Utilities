using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalViewModels.CRM;
using Utilities.Mappers.Resolver;

namespace Utilities.Mappers.Profiles
{
 public  class ContactPreviousLocation_ContactLocationVmProfile :Profile
    {
        public ContactPreviousLocation_ContactLocationVmProfile()
        {
            CreateMap<ContactLocationVm, ContactPreviousLocation>()
          .ForMember(a => a.City, o => o.MapFrom(s => s.CityId != null ? new EntityReference(CrmEntityNamesMapping.City, new Guid(s.CityId)) : null))
          .ForMember(a => a.Contact, o => o.MapFrom(s => s.ContactId != null ? new EntityReference(CrmEntityNamesMapping.Contact, new Guid(s.ContactId)) : null))
          .ForMember(a => a.District, o => o.MapFrom(s => s.DistrictId != null ? new EntityReference(CrmEntityNamesMapping.District, new Guid(s.DistrictId)) : null))
                  .ForMember(a => a.HouseNumber, o => o.MapFrom(s => s.HouseNo))
                  .ForMember(a => a.ApartmentNumber, o => o.MapFrom(s => s.ApartmentNo))
                  .ForMember(a => a.FloorNumber, o => o.MapFrom(s => s.FloorNo.HasValue ? new OptionSetValue(s.FloorNo.Value) : null))

                  .ForMember(a => a.HouseType, o => o.MapFrom(s => s.HouseType.HasValue ? new OptionSetValue(s.HouseType.Value)  :null))
                  .ForMember(a => a.Type, o => o.MapFrom(s => s.Type.HasValue ? new OptionSetValue(s.Type.Value)  :null))
                

          .IgnoreAllPropertiesWithAnInaccessibleSetter()
          .ReverseMap()
          .ForMember(a => a.CityId, o => o.ResolveUsing(new EntityReferanceToStringId(), s => s.City))
          .ForMember(a => a.DistrictId, o => o.ResolveUsing(new EntityReferanceToStringId(), s => s.District))
          .ForMember(a => a.ContactId, o => o.ResolveUsing(new EntityReferanceToStringId(), s => s.Contact))
          .ForMember(a => a.FloorNo, o => o.ResolveUsing(new FromOptionSetToInt(), s => s.FloorNumber))
          .ForMember(a => a.HouseType, o => o.ResolveUsing(new FromOptionSetToInt(), s => s.HouseType))
          .ForMember(a => a.HouseNo, o => o.MapFrom(a=>a.HouseNumber))
              .ForMember(a => a.ApartmentNo, o => o.MapFrom(s => s.ApartmentNumber))
          ;
        }
    }
}
