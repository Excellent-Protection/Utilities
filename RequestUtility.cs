using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities
{
    public class RequestUtility
    {
        public UserLanguage? Language { get; set; }
        public RecordSource? Source { get; set; }
        public string RouteLanguage { get; set; }
        public MobilePhoneSource? PhoneSource { get; set; }
    }
}
