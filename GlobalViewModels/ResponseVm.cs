using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels
{ 
    public  class ResponseVm<T>  where T :   class
    {
        public HttpStatusCodeEnum Status { get; set; }
        public T Data { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

    }
}
