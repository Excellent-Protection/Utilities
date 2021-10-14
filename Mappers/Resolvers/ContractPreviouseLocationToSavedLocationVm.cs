using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custome;

namespace Utilities.Mappers.Resolvers
{
  public  class ContractPreviouseLocationToSavedLocationVm : IValueResolver<ContactPreviousLocation, SavedLocationVm, string>
    {
        public string Resolve(ContactPreviousLocation source, SavedLocationVm destination, string destMember, ResolutionContext context)
        {
            if (source != null)
            {
                string description="";
                if (source.Attributes.Contains("new_city.new_englsihname"))
                    description +=  (source.Attributes.Contains("new_city.new_englsihname") ? ((AliasedValue)source.Attributes["new_city.new_englsihname"]).Value.ToString() : source.City.Name  ) +" - ";
               if(source.Attributes.ContainsKey("new_district.new_englishname"))
                    description +=(source.Attributes.Contains("new_district.new_englishname") ? ((AliasedValue)source.Attributes["new_district.new_englishname"]).Value.ToString() : source.District.Name)+" - ";
                if (source.HouseType != null)
                    description +=( source.HouseType.Value == 0 ? "villa" : "Partment" )+" - ";
                if (source.HouseNumber != null)
                    description +=( string.Format("Number:{0}", source.HouseNumber) ) +" - ";
                if (source.FloorNumber!=null)
                    description +=  string.Format("Floor No:{0}", source.FloorNumber.Value);
                            return description;
                 
            }
            return null;

        }
    }
}
