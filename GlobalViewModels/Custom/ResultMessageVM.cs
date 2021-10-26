using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Custom
{
    public class ResultMessageVM
    {
        public ResultMessageVM()
        {
            RedirectTimeout = 10;
            HasLayout = true;
        }
        public string Title { get; set; }
        public string Message { get; set; }
        public string UrlToRedirect { get; set; }
        public string UrlToAnonymousRedirect { get; set; }
        public int RedirectTimeout { get; set; }
        public bool IsWithAutoRedirect { get; set; }
        public string Footer { get; set; }
        public string HtmlContent { get; set; }
        public bool HasLayout { get; set; }
    }
}
