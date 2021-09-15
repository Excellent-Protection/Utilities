using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels
{
  public  class BaseQuickLookupVm
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string AdditionalInformation { get; set; }
        public string IconURl { get; set; }
    }
}
