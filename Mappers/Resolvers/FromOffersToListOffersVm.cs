//using AutoMapper;
//using Microsoft.Xrm.Sdk;
//using Models.CRM;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Utilities.Defaults;
//using Utilities.Enums;
//using Utilities.GlobalRepositories.CRM;
//using Utilities.GlobalViewModels.CRM;
//using Utilities.GlobalViewModels.Custom;
//using Westwind.Globalization;

//namespace Utilities.Mappers.Resolvers
//{
//    public class FromOffersToListOffersVm : IValueResolver<Entity, Slider, string>
//    {
//        public string Resolve(Entity source, Slider destination, string destMember, ResolutionContext context)
//        {
//            if (source != null)
//            {
//                if (source.Attributes.Contains("Offers.new_webimage") && MapperConfig.source == RecordSource.Web)
//                    destination..Image = source[i].Attributes["Offers.new_webimage"].ToString();
//                if (source[i].Attributes.Contains("Offers.new_mobileimage") && MapperConfig.source == RecordSource.Mobile)
//                    destination[i].Image = source[i].Attributes["Offers.new_mobileimage"].ToString();
//                if (source[i].Attributes.Contains("Offers.new_offersectortype") && MapperConfig.source == RecordSource.Mobile)
//                    destination[i].OfferSector = (OfferSector)((AliasedValue)source[i].Attributes["Offers.new_offersectortype"]).Value;
//                if (source[i].Attributes.Contains("Offers.new_selectedhourlypricing") && destination[i].OfferSector == OfferSector.Hourly)
//                    destination[i].PricingId = source[i].Attributes["Offers.new_selectedhourlypricing"].ToString();
//                if (source[i].Attributes.Contains("Offers.new_individualpricing") && destination[i].OfferSector == OfferSector.Individual)
//                    destination[i].PricingId = source[i].Attributes["Offers.new_individualpricing"].ToString();
//                if (source[i].Attributes.Contains("Offers.new_flexiblepricing") && destination[i].OfferSector == OfferSector.Flexible)
//                    destination[i].PricingId = source[i].Attributes["Offers.new_flexiblepricing"].ToString();

//            }
//            return null;

//        }
//    }
//}

