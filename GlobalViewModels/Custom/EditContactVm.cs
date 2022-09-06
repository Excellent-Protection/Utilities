﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Custom
{
    public class EditContactVm
    {
        public string ContactId { get; set; }
        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public string CityId { get; set; }
        public string NationalityId { get; set; }
        public int? Gender { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string OtherMobilePhone { get; set; }
    }
}
