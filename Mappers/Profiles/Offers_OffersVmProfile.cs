using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Utilities.Enums;
using Utilities.GlobalViewModels.CRM;
using Utilities.Mappers.Resolvers;
using Microsoft.Xrm.Sdk;
using Utilities.GlobalViewModels;

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
                .ForMember(a=>a.PricingId,o=>o.MapFrom(s=> (OfferSector)s.OfferSector.Value==OfferSector.Hourly?s.SelectedHourlyPricing.Id : (OfferSector)s.OfferSector.Value == OfferSector.Individual?s.IndividualPricing.Id:s.FlexiblePricing.Id))
                .ForMember(a=>a.SliderItemName,o=>o.MapFrom(s=>s.SliderItem.Name))
                .ForMember(a=>a.IndividualDiscountCode,o=>o.MapFrom(s=>s.IndividualDiscount.Name))
                .ForMember(a=>a.OfferName,o=>o.MapFrom(s=>s.Name))
                .ForMember(a=>a.OfferDateFrom,o=>o.MapFrom(s=>s.DateFrom!=null?s.DateFrom:null))
                .ForMember(a=>a.OffersDateTo,o=>o.MapFrom(s=>s.DateTo!=null?s.DateTo:null))
                .ForMember(a => a.Description,o=>o.MapFrom(s=>s.Description))
                //entity refernce
                .ForMember(a => a.IndividualDiscountId, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.IndividualDiscount))
                .ForMember(a => a.SliderItemId, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.SliderItem))
                ;
        }
    }
}
