﻿using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels;

namespace Utilities.Mappers.Profiles
{
  public  class District_BaseQuickLookupVmProfile :Profile
    {
        public District_BaseQuickLookupVmProfile()
        {
            CreateMap<BaseQuickLookupVm, District>()

        .IgnoreAllPropertiesWithAnInaccessibleSetter()
        .ReverseMap()
        .ForMember(a => a.Key, opt => opt.MapFrom(s => s.Id != null ? s.Id.ToString() : null))
        .ForMember(a => a.Value, opt => opt.MapFrom(s => s.EnglishName))

        ;
        }
    }
}