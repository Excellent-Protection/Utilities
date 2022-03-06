using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.GlobalManagers;
using Utilities.GlobalViewModels;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/UrlShortenerConverter")]
    public class UrlShortenerConverterController : BaseApiController
    {
        [Route("GetShortenerUrl")]
        [HttpGet]
        public HttpResponseMessage GetShortenerUrl(string longUrl)
        {
            try
            {
                using (var _mngr = new UManager(RequestUtility))
                {
                    var ShotUrl = _mngr.GenerateShotUrl(longUrl);
                    return Response(new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok ,Data=ShotUrl});
                }
            }
            catch { }

            return null;
        }


        [HttpGet]
        [Route("ProjectTimeSheetURLS")]
        public IHttpActionResult ProjectTimeSheetURLS(string id, string UID)
        {
            var _UrlShortenerManager = new UManager(RequestUtility);
            var res = _UrlShortenerManager.SetProjectTimeSheetURLS(id, UID);
            return Ok(new { KAMURL = res.Item1, CustomerURL = res.Item2 });
        }


    }
}
