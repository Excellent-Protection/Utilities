using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.CRM;

namespace Utilities.Mappers.Profiles
{
    class SettingsCRM_SettingCRMVmMappingProfile : Profile
    {
        public SettingsCRM_SettingCRMVmMappingProfile()
        {

            CreateMap<SettingCRMVm, SettingCRM>()
                 .ForMember(a => a.Key, opt => opt.MapFrom(s => s.Key))
                .ForMember(a => a.Value, opt => opt.MapFrom(s => s.Value))
                .ForMember(a => a.Id, opt => opt.Ignore())
                .ForMember(a => a.SettingId, opt => opt.Ignore())
                .ForMember(a => a.IsEditable, opt => opt.Ignore())
                .ForMember(a => a.Description, opt => opt.Ignore())
                .ForMember(a => a.DataType, opt => opt.Ignore())
                .ForMember(a => a.LogicalName, opt => opt.Ignore())
                .ForMember(a => a.Attributes, opt => opt.Ignore())
                .ForMember(a => a.EntityState, opt => opt.Ignore())
                .ForMember(a => a.FormattedValues, opt => opt.Ignore())
                .ForMember(a => a.RelatedEntities, opt => opt.Ignore())
                .ForMember(a => a.RowVersion, opt => opt.Ignore())
                .ForMember(a => a.KeyAttributes, opt => opt.Ignore());

        }
    }
}