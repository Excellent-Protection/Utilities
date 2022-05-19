using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Labor
{
    public class DeviceVm 
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public string UserId { get; set; }
        public bool IsOnline { get; set; }
        public string CrmUserId { get; set; }
    }
}
