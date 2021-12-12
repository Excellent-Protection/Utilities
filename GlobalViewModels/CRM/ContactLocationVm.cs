using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.CRM
{
    public class ContactLocationVm
    {
        public string ContactId { get; set; }
        public string LocationId { get; set; }
        public string HouseNo { get; set; }
        public int? HouseType { get; set; }

        public int? FloorNo { get; set; }
        public string ApartmentNo { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AddressNotes { get; set; }
        public int? Type { get; set; }

    }
}
