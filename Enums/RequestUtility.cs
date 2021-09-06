using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Enums
{
    public class RequestUtility
    {
        public UserLanguage? Language { get; set; }
        public RecordSource? Source { get; set; }
        public string RouteLanguage { get; set; }
        public MobilePhoneSource? PhoneSource { get; set; }

        public string ProjectId { get; set; }
        public string ProjectName { get; set; }

    }
}
