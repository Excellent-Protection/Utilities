using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.GlobalViewModels.CRM
{
    public class ExcSettingsVm
    {
        public string ExcSettingsId { get; set; }
        public string Name { get; set; }
        public string ServiceId { get; set; }
        public string Value { get; set; }
        public ExcSettingsType? Type { get; set; }
    }
}
