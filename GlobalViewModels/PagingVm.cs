using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels
{
    public class PagingVm<T> where T : class
    {
        public List<T> Model { get; set; }
        public int TotalCount { get; set; }
        public int TotalCountInPages { get; set; }
    }
}
