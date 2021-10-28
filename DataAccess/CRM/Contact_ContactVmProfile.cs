using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalManagers.CRM;

namespace Utilities.DataAccess.CRM
{
    public class Contact_ContactVmProfile : Profile
    {
        public Contact_ContactVmProfile()
        {


            CreateMap<ContactVm, Contact>()
                    //from contactvm to contact
                    .ForMember(a => a.ContactId, opt => opt.MapFrom(s => s.Id))
                     .ForMember(a => a.Id, opt => opt.MapFrom(s => s.Id))
                     .ForMember(a => a.IsBlocked, opt => opt.MapFrom(s => s.BlackList.HasValue ? s.BlackList.Value : (bool?)null))
                     .ForMember(a => a.new_blacklistreason, opt => opt.MapFrom(s => s.BlackListReason))
                      .ForMember(a => a.HadRenewDiscount, opt => opt.MapFrom(s => s.HadRenewDiscount.HasValue ? s.HadRenewDiscount.Value : (bool?)null))
                     .ForMember(a => a.longitude, opt => opt.MapFrom(s => s.longitude))
                     .ForMember(a => a.Latitude, opt => opt.MapFrom(s => s.Latitude))
                     .ForMember(a => a.ContactAddress, opt => opt.MapFrom(s => s.ContactAddress))
                     .ForMember(a => a.WorkPlace, opt => opt.MapFrom(s => s.WorkPlace))
                     .ForMember(a => a.WorkAddress, opt => opt.MapFrom(s => s.WorkAddress))
                     .ForMember(a => a.OtherMobilePhone, opt => opt.MapFrom(s => s.OtherMobilePhone))
                     .ForMember(a => a.FullName, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.FullName) ? s.FullName : s.FName + " " + s.LastName))
                    //ignore
                    .IgnoreAllPropertiesWithAnInaccessibleSetter()

                    // entityreference
                    .ForMember(a => a.CityId, opt => opt.MapFrom(s => s.CityId != null ? new EntityReference(Contact.EntityLogicalName, new Guid(s.CityId)) : null))
                     .ForMember(a => a.NationalityId, opt => opt.MapFrom(s => s.NationalityId != null ? new EntityReference(Contact.EntityLogicalName, new Guid(s.NationalityId)) : null))
                     .ForMember(a => a.RegionId, opt => opt.MapFrom(s => s.RegionId != null ? new EntityReference(Contact.EntityLogicalName, new Guid(s.RegionId)) : null))

                     //optionset
                     .ForMember(a => a.GenderId, opt => opt.MapFrom(s => s.GenderId != null ? new OptionSetValue(int.Parse(s.GenderId.ToString())) : null))
                     .ForMember(a => a.WorkSector, opt => opt.MapFrom(s => s.WorkSector != null ? new OptionSetValue(int.Parse(s.WorkSector.ToString())) : null))
                     .ForMember(a => a.PlatformSource, opt => opt.MapFrom(s => s.PlatformSource != null ? new OptionSetValue(int.Parse(s.PlatformSource.ToString())) : null))
                     .ForMember(a => a.new_blackliststatus, opt => opt.MapFrom(s => s.BlackListStatus != null ? new OptionSetValue(s.BlackListStatus.Value) : null))
                     .ForMember(a => a.CompanyKnownBy, opt => opt.MapFrom(s => s.CompanyKnownBy.HasValue ? new OptionSetValue(s.CompanyKnownBy.Value) : null))

                     .ReverseMap() //from contact to contactvm
                     .ForMember(a => a.Id, opt => opt.MapFrom(s => s.ContactId != new Guid() && s.ContactId != null ? s.ContactId : null))
                     .ForMember(a => a.WorkPlace, opt => opt.MapFrom(s => s.WorkPlace))
                     .ForMember(a => a.WorkAddress, opt => opt.MapFrom(s => s.WorkAddress))
                     .ForMember(a => a.OtherMobilePhone, opt => opt.MapFrom(s => s.OtherMobilePhone))
                     .ForMember(a => a.BlackList, opt => opt.MapFrom(s => s.IsBlocked.HasValue ? s.IsBlocked.Value : (bool?)null))
                     .ForMember(a => a.BlackListReason, opt => opt.MapFrom(s => s.new_blacklistreason))
                      .ForMember(a => a.HadRenewDiscount, opt => opt.MapFrom(s => s.HadRenewDiscount.HasValue ? s.HadRenewDiscount.Value : (bool?)null))
                     .ForMember(a => a.FullName, opt => opt.MapFrom(s => s.FullName))

                     //from entityreference
                     .ForMember(a => a.CityId, opt => opt.MapFrom(s => s.CityId.Id != new Guid() ? s.CityId.Id.ToString() : null))
                     .ForMember(a => a.CityName, opt => opt.MapFrom(s => s.CityId.Id != new Guid() ? s.CityId.Name : null))
                     .ForMember(a => a.NationalityId, opt => opt.MapFrom(s => s.NationalityId.Id != new Guid() ? s.NationalityId.Id.ToString() : null))
                     .ForMember(a => a.NationalityName, opt => opt.MapFrom(s => s.NationalityId != null ? s.NationalityId.Name : null))
                     .ForMember(a => a.RegionId, opt => opt.MapFrom(s => s.RegionId.Id != new Guid() ? s.RegionId.Id.ToString() : null))

                     //from optionset
                     .ForMember(a => a.GenderId, opt => opt.MapFrom(s => s.GenderId.Value))
                     .ForMember(a => a.WorkSector, opt => opt.MapFrom(s => s.WorkSector.Value))
                     .ForMember(a => a.PlatformSource, opt => opt.MapFrom(s => s.PlatformSource.Value))
                     .ForMember(a => a.BlackListStatus, opt => opt.MapFrom(s => s.new_blackliststatus != null ? s.new_blackliststatus.Value : (int?)null))
                      .ForMember(a => a.CompanyKnownBy, opt => opt.MapFrom(s => s.CompanyKnownBy != null ? s.CompanyKnownBy.Value : (int?)null))
                      ;



    }
    }

}