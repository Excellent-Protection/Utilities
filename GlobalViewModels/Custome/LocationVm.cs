using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Custome;

namespace Utilities.GlobalViewModels.CRM
{
   public class LocationVm
    {
        public BaseQuickLookupVm City { get; set; }
        public DistrictQuickLookupVm District { get; set; }
        public BaseOptionSetVM FloorNumber { get; set; }
        public BaseOptionSetVM HouseType { get; set; }
        public BaseOptionSetVM ApartmentNumber { get; set; }


    }
}
