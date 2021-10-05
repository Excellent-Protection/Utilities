using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Labor
{
    public  class StepDetailsVm 
    {
        public Guid? StepHeaderId { get; set; }
        public int? StepOrder { get; set; }
        public string DBResourceName { get; set; }
        public string DBResourceFieldName { get; set; }
        public bool? IsAvailable { get; set; }
        public bool IsAuthorized { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }
        public string IconClass { get; set; }
        public string PreviousStepAction { get; set; }
        public string NextStepAction { get; set; }
        public string StepKeyword { get; set; }
        public string Description { get; set; }
        //public bool IsActiveStep { get; set; }
        public bool? IsVisible { get; set; }

    }
}
