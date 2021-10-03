using AutoMapper;
using Models.Labor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Labor;

namespace Utilities.Mappers.Profiles
{
  public  class StepData_StepDataVmProfile :Profile
    {
        public StepData_StepDataVmProfile()
        {
            CreateMap<StepDataVm, StepData>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                ;
        }
    }
}
