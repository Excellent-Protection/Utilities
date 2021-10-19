using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels
{
    public class MailToSendVM
    {
        public string subject { get; set; }
        public string body { get; set; }
        public string ToEmails { get; set; }
        public string CCEmail { get; set; }
    }
}
