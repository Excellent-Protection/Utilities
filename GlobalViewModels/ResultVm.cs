using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels
{
  public  class ResultVm
    {
       public HttpStatusCodeEnum Status { get; set; }
        public object Data { get; set; }

    }
}
