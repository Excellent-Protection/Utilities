using AutoMapper;
using Models.Labor;
using Models.Labor.DynamicSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;
using Utilities.GlobalViewModels.Labor;

namespace Utilities.Mappers.Profiles
{
   public class StepDetails_StepDetailsVmProfile :Profile
    {
        public StepDetails_StepDetailsVmProfile()
        {
            CreateMap<StepsDetails, StepDetailsVm>()

        .IgnoreAllPropertiesWithAnInaccessibleSetter()
        .ForMember(a => a.StepType, opt => opt.MapFrom(s => (DynamicStepType)s.StepType ));
       
        }
    }
}
