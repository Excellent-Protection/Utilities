using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using Models.CRM.Finance;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalViewModels.CRM;

namespace Utilities.Mappers.Profiles
{
    public class Slider_SliderVmProfile:Profile
    {
        public Slider_SliderVmProfile()
        {
            CreateMap<SliderVm, Slider>()
                .ReverseMap()
                .ForMember(a => a.Image, o => o.MapFrom(s => (MapperConfig.source == RecordSource.Mobile ? ConfigurationManager.AppSettings["SliderMobileImages"].ToString() + s.MobileImage.Replace(" ", "%20") :
                                                              MapperConfig.source == RecordSource.Web ? ConfigurationManager.AppSettings["SliderWebImages"].ToString() + s.WebImage.Replace(" ", "%20") : null)))
                .ForMember(a => a.Url, o => o.MapFrom(s => (MapperConfig.source == RecordSource.Mobile ? s.MobileUrl : MapperConfig.source == RecordSource.Web ? s.WebUrl : null)))
                .ForMember(a => a.SliderItemId, o => o.MapFrom(s => s.Id))
                .ForMember(a => a.SliderType, o => o.MapFrom(s => (SliderType)s.Type.Value))
                .ForMember(a => a.Description, o => o.MapFrom(s => s.Description))
                .ForMember(a => a.Name, o => o.MapFrom(s => s.Name));
                 //list
                //.ForMember(a => a.Offers, opt => opt.MapFrom(s => s.RelatedEntities.Contains(new Relationship()
                 //{ SchemaName = CrmRelationsNameMapping.SliderItem_Offers }) ? s.RelatedEntities[new Relationship()
                 //{ SchemaName = CrmRelationsNameMapping.SliderItem_Offers }].Entities.Select(o => o.ToEntity<Offers>()).ToModelListData<OffersVm>() : null));
        }
    }
}
