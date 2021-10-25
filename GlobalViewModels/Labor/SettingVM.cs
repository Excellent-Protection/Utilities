using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Labor
{
    public class SettingVM 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte DataType { get; set; }
        public string Value { get; set; }
        public bool IsEditable { get; set; }
    }
}
