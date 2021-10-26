using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Labor;

namespace Utilities.GlobalViewModels.Custom
{
  public  class ServiceStepResponseVm
    {
        public string StepId { get; set; }
        public StepDetailsVm StepDetailsVm { get; set; }
    }
}
