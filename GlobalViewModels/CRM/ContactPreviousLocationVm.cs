using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.Custom;

namespace Utilities.GlobalViewModels.CRM
{
public  class ContactPreviousLocationVm
    {
        public string ContactPreviouslocationId { get; set; }
        public string ContactId { get; set; }
        public string ContactName { get; set; }
        public BaseQuickLookupVm City { get; set; }
        public DistrictQuickLookupVm District { get; set; }
        public BaseOptionSetVM FloorNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string HouseNumber { get; set; }
        public BaseOptionSetVM HouseType { get; set; }
        public string AddressNotes { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationDescription { get; set; }
       // public IEnumerable<DistrictAvailableDays> AvailableDays { get; set; }
    }
}
