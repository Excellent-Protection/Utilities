using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Index")]
    public class ShortenerUrlController : BaseApiController
    {
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage Index(string id)
        {
            try
            {
                using (var _mngr = new UManager(RequestUtility))
                {
                    var record = _mngr.GetLongUrl(id);
                    if (record != null)
                    {
                        ++record.NoOfVisits;
                        _mngr.Update(record);
                    }
                    var response = Request.CreateResponse(HttpStatusCode.Moved);
                    response.Headers.Location = new Uri(record.LongUrl);
                    return response;
                    //return Redirect(record.LongUrl);
                }
            }
            catch { }

            return null;
        }

    }
}
