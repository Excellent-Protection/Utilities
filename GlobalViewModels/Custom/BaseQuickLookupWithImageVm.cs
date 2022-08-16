using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Custom
{
   public class BaseQuickLookupWithImageVm  :BaseQuickLookupVm
    {
        public string Image { get; set; }
        public bool HasPackage { get; set; }
    }
}
