using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.GlobalViewModels.CRM
{
    public class SliderVm
    {
        public string SliderItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public SliderType SliderType { get; set; }
        public string SliderTypeName { get;set; }

    }
}
