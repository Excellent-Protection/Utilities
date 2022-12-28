using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Models.Labor;
using Utilities.GlobalViewModels.Labor;

namespace Utilities.Mappers.Profiles
{
    public class Device_DeviceVMProfile : Profile
    {
        public Device_DeviceVMProfile()
        {
            CreateMap<DeviceVm, Device>().ReverseMap()
                .ForMember(x => x.DeviceId, opt => opt.MapFrom(y => y.DeviceId != null ? y.DeviceId.ToString() : null))
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id != null ? y.Id.ToString() : null))
                .ForMember(x => x.UserId, opt => opt.MapFrom(y => y.UserId != null ? y.UserId.ToString() : null))
                .ForMember(x => x.IsOnline, opt => opt.MapFrom(y => y.IsOnline));
        }
    }
}
