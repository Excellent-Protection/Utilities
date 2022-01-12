using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Utilities.Enums;
using Utilities.GlobalManagers;
using Utilities.GlobalManagers.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;

namespace Utilities.Controller
{
    [RoutePrefix("{lang}/api/City")]

    public class CityController: BaseApiController
    {
        [HttpGet]
        [Route("CheckCityAvailabilityForService")]
        public HttpResponseMessage CheckCityAvailabilityForService(string cityId , ServiceType serviceType ,  string hourlyServiceId)
        {
            using (CityManager _mngr= new CityManager(RequestUtility))
            {
              var result=  _mngr.CheckCityAvilabilityForService(cityId,serviceType , hourlyServiceId);
                return Response<string>(result);
            }
        }

        [HttpGet]
        [Route("CheckDistrictAvailabilityForService")]
        public HttpResponseMessage CheckDistrictAvailabilityForService(string ServiceId,string districtId)
        {
            using (CityManager _mngr = new CityManager(RequestUtility))
            {
                var result = _mngr.CheckDistrictAvilabilityForService(ServiceId, districtId);
                return Response<string>(result);
            }
        }


        [HttpGet]
        [Route("ActiveCities")]
        public HttpResponseMessage GetActiveCities(string serviceId=null)
        {
            using (CityManager _mngr = new CityManager(RequestUtility))
            {
                var result = _mngr.GetActiveCities(serviceId);
                return Response<List<BaseQuickLookupVm>>(result);
            }
        }


        //[HttpGet]
        //[Route("IndividualActiveCities")]
        //public HttpResponseMessage GetIndividualActiveCities()
        //{
        //    using (CityManager _mngr = new CityManager(RequestUtility))
        //    {
        //        var result = _mngr.GetActiveCities();
        //        return Response<List<BaseQuickLookupVm>>(result);
        //    }
        //}


        [HttpGet]
        [Route("CityDistricts")]
        public HttpResponseMessage GetCityDistricts(string cityId, string serviceId=null)
        {
            using (CityManager _mngr = new CityManager(RequestUtility))
            {
                var result = _mngr.GetCityDistricts(cityId, serviceId);
                return Response<List<BaseQuickLookupVm>>(result);
            }
        }


        [HttpGet]
        [Route("GetPolygonPath")]
        public HttpResponseMessage GetPolygonPath(string districtId)
        {
            using (CityManager _mngr = new CityManager(RequestUtility))
            {
                var result = _mngr.GetDistrictPolygon(districtId);
                return Response<string>(result);
            }

        }

    }
}
