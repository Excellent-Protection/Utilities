using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.Extensions;
using Utilities.GlobalManagers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/Slider")]

    public class SliderController : BaseApiController
    {
        [HttpGet]
        [Route("GetSliderItems")]
        public HttpResponseMessage GetSliderItems(int?type=null)
        {
            using (SliderManager _mngr=new SliderManager(RequestUtility))
            {
                var result = _mngr.GetSliderItems(type);
                return Response<List<SliderVm>>(result);
            }
        }
        [HttpGet]
        [Route("GetOffersBySliderItem")]
        public HttpResponseMessage GetOffersBySliderItem(string SliderItemId)
        {
            using (SliderManager _mngr = new SliderManager(RequestUtility))
            {
                var result = _mngr.GetOffersBySliderItem(SliderItemId);
                return Response<List<OffersVm>>(result);
            }
        }
    }
}