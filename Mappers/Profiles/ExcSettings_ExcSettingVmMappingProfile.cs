using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;
using Utilities.GlobalViewModels.CRM;

namespace Utilities.Mappers.Profiles
{
    public class ExcSettings_ExcSettingVmMappingProfile:Profile
    {
        public ExcSettings_ExcSettingVmMappingProfile()
        {
            CreateMap<ExcSettingsVm, ExcSettings>()
                //map from EscSettingsVm to ExcSettings
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ReverseMap()    //map from EscSettings to ExcSettingsVm

                //entity refrence
                .ForMember(a=>a.ServiceId,opt=>opt.MapFrom(s=>s.Service!=null?s.Service.Id.ToString():null))


                //option Set
                .ForMember(a=>a.Type,opt=>opt.MapFrom(s=>s.Type!=null?(ExcSettingsType)s.Type.Value:(ExcSettingsType?)null))
                ;
        }
    }
}
