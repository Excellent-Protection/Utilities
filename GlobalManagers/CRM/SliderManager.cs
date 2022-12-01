using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
    public class SliderManager : BaseManager, IDisposable
    {
        internal RequestUtility _requestUtility;
        SliderRepository _rep;
        public SliderManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _requestUtility = RequestUtility;
            _rep = new SliderRepository(RequestUtility);
        }
        public ResponseVm<List<SliderVm>> GetSliderItems(int? type)
        {
            try
            {
                var SliderItems=_rep.GetSliderItems(type);
                using (OffersManager _OffersManager=new OffersManager(RequestUtility))
                {
                    SliderItems.ForEach(a => a.offers = _OffersManager.GetOffersBySliderItem(a.SliderItemId).Data);
                }

                return new ResponseVm<List<SliderVm>> { Status=HttpStatusCodeEnum.Ok,Data= SliderItems};
            }
            catch(Exception ex)
            {
                LogError.Error(ex,System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<SliderVm>>
                {
                    Status = HttpStatusCodeEnum.IneternalServerError,
                    Message = "An Error Occurred"
                };
            }
        }
        public void Dispose()
        {
        }
    }
}
