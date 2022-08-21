using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;
using Utilities.GlobalViewModels.CRM;

namespace Utilities.Mappers.Profiles
{
    public class Offers_OffersVmProfile : Profile
    {
        public Offers_OffersVmProfile()
        {
            CreateMap<OffersVm, Offers>()
                .ReverseMap()
                .ForMember(a => a.Image, o => o.MapFrom(s =>(MapperConfig.source == RecordSource.Mobile ? ConfigurationManager.AppSettings["OfferMobileImages"].ToString()+s.MobileImage.Replace(" ", "%20") :
                                  MapperConfig.source == RecordSource.Web ? ConfigurationManager.AppSettings["OfferWebImages"].ToString()+s.WebImage.Replace(" ", "%20") : null)))
                .ForMember(a=>a.OfferId,o=>o.MapFrom(s=>s.Id))
                .ForMember(a=>a.OfferSector,o=>o.MapFrom(s=>(OfferSector)s.OfferSector.Value))
                .ForMember(a=>a.PricingId,o=>o.MapFrom(s=> (OfferSector)s.OfferSector.Value==OfferSector.Hourly?s.SelectedHourlyPricing : (OfferSector)s.OfferSector.Value == OfferSector.Individual?s.IndividualPricing:s.FlexiblePricing))
                ;
        }
    }
}
