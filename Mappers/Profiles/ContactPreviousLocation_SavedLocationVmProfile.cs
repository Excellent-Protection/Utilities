using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Custom;
using Utilities.Mappers.Resolvers;

namespace Utilities.Mappers.Profiles
{
   public class ContactPreviousLocation_SavedLocationVmProfile:Profile
    {
        public ContactPreviousLocation_SavedLocationVmProfile()
        {
            CreateMap<ContactPreviousLocation, SavedLocationVm>()
             .ForMember(a => a.Id, o => o.MapFrom(s => s.Id.ToString()))
             .ForMember(a => a.DisplayValue, o => o.ResolveUsing(new ContractPreviouseLocationToSavedLocationVm()))
            .ForMember(a => a.CityId, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.City))
            .ForMember(a => a.Type, o => o.ResolveUsing(new FromOptionSetToInt(), s => s.Type))
            .ForMember(a => a.CityName, o => o.ResolveUsing(new EntityReferenceNameToStringResolver(), s => s.City))
            .ForMember(a => a.CityName, s => s.ResolveUsing(new ApplyLanguage(), src => new MappingTranslation(MapperConfig.lang, src.Attributes.Contains("new_city.new_name") ? ((AliasedValue)src.Attributes["new_city.new_name"]).Value.ToString() : null, src.Attributes.Contains("new_city.new_englsihname") ? ((AliasedValue)src.Attributes["new_city.new_englsihname"]).Value.ToString() : null)))
            .ForMember(a => a.DistrictName, s => s.ResolveUsing(new ApplyLanguage(), src => new MappingTranslation(MapperConfig.lang, src.Attributes.Contains("new_district.new_name") ? ((AliasedValue)src.Attributes["new_district.new_name"]).Value.ToString() : null, src.Attributes.Contains("new_district.new_englishname") ? ((AliasedValue)src.Attributes["new_district.new_englishname"]).Value.ToString() : null)))
            .ForMember(a => a.DistrictId, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.District))

            .ForMember(a => a.FloorNo, o => o.ResolveUsing(new FromOptionSetToInt(), s=>s.FloorNumber))
            .ForMember(a => a.Latitude, o => o.MapFrom(s =>!string.IsNullOrEmpty( s.Latitude)? s.Latitude : null))
            .ForMember(a => a.AddressNotes, o => o.MapFrom(s =>!string.IsNullOrEmpty( s.AddressNotes)? s.AddressNotes : null))
            .ForMember(a => a.Longitude, o => o.MapFrom(s =>!string.IsNullOrEmpty( s.Longitude) ? s.Longitude :null))
            .ForMember(a => a.ApartmentNumber, o => o.MapFrom(s => s.ApartmentNumber))
            .ForMember(a => a.HouseNumber, o => o.MapFrom(s => s.HouseNumber))
            .ForMember(a => a.HouseType, o => o.ResolveUsing(new FromOptionSetToInt(), s => s.HouseType))
            .ForMember(a => a.Type, o => o.ResolveUsing(new FromOptionSetToInt(), s => s.Type))
            .ForMember(a => a.AvailableForIndividual, o => o.MapFrom(s => s.Attributes.Contains("new_city.new_forindividual") ? ((AliasedValue)s.Attributes["new_city.new_forindividual"]).Value : s.City.Name))
            //.ForMember(a => a.AvailableForHourly, o => o.MapFrom(s => s.Attributes.Contains("new_city.new_isdalal") ? ((AliasedValue)s.Attributes["new_city.new_isdalal"]).Value : s.City.Name))
            ;
        }
    }
}
