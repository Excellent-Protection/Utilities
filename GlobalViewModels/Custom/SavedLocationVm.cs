﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.GlobalViewModels.Custom
{
   public class SavedLocationVm 
    {

        public string Id { get; set; }
        public string DisplayValue {
            get; set;
                }
        public int? HouseType { get; set; }
        public string ApartmentNumber { get; set; }
        public string HouseNumber { get; set; }
        public string  CityId { get; set; }
        public int? Type { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string DistrictId { get; set; }
        public int? FloorNo { get; set; }

        public bool AvailableForHourly { get; set; }
        public bool AvailableForIndividual { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string AddressNotes { get; set; }

    }
}
