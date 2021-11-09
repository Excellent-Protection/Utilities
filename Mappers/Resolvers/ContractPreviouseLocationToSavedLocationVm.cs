using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Westwind.Globalization;

namespace Utilities.Mappers.Resolvers
{
  public  class ContractPreviouseLocationToSavedLocationVm : IValueResolver<ContactPreviousLocation, SavedLocationVm, string>
    {
        public string Resolve(ContactPreviousLocation source, SavedLocationVm destination, string destMember, ResolutionContext context)
        {
            if (source != null)
            {

                string description="";
                if (source.Attributes.Contains("new_city.new_englsihname") || source.Attributes.Contains("new_city.new_name"))
                description +=new ApplyLanguage().Resolve(source , destination , new MappingTranslation(MapperConfig.lang, source.Attributes.Contains("new_city.new_name") ?((AliasedValue)source.Attributes["new_city.new_name"]).Value.ToString():null, source.Attributes.Contains("new_city.new_englsihname") ?((AliasedValue)source.Attributes["new_city.new_englsihname"]).Value.ToString():null), destMember, context) +" - ";
                if (source.Attributes.ContainsKey("new_district.new_englishname") || source.Attributes.Contains("new_district.new_name"))
                description += new ApplyLanguage().Resolve(source, destination, new MappingTranslation(MapperConfig.lang, source.Attributes.Contains("new_district.new_name") ? ((AliasedValue)source.Attributes["new_district.new_name"]).Value.ToString() : null, source.Attributes.Contains("new_district.new_englishname") ? ((AliasedValue)source.Attributes["new_district.new_englishname"]).Value.ToString() : null), destMember, context) + " - ";
                if (source.HouseType != null)
                    description +=( source.HouseType.Value == 0 ? DbRes.T("Villa","Shared") : DbRes.T("Apartment", "Shared"))+" - ";
                if (source.HouseType!=null && source.HouseType.Value == 0 && source.HouseNumber != null)
                    description +=DbRes.T("Number" ,"Shared") + ":" +source.HouseNumber ;
                if (source.HouseType!=null && source.HouseType.Value == 1 &&source.ApartmentNumber != null)
                    description += DbRes.T("Number", "Shared") + ":" + source.ApartmentNumber;
                if (source.FloorNumber != null)
                    description +=" - "+ DbRes.T("FloorNo", "Shared") + ":" + source.FloorNumber.Value;
                            return description;
            }
            return null;

        }
    }
}
