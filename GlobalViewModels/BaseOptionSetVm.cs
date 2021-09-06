using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels
{
    public class BaseOptionSetVM 
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public BaseOptionSetVM()
        {

        }

        public BaseOptionSetVM(int key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
