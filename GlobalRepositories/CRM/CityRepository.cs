using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalViewModels;

namespace Utilities.GlobalRepositories.CRM
{
    public class CityRepository
    {


        public bool CheckCityAvilabilityForService(string cityId,ServiceType serviceType,string serviceId)
        {

            switch (serviceType)
            {
                case ServiceType.Individual:
                    return CheckCityAvailabilityForIndvService(cityId);
                case ServiceType.Hourly:
                    return false; // will implement with hourly service steps 
                default:
                    return false;
            }

        }



        public bool CheckCityAvailabilityForIndvService(string cityId )
        {
            var service = CRMService.Get;
            var city = service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_forindividual")).ToEntity<City>();
            var isAvilable = city.IsForIndv.HasValue ? city.IsForIndv.Value : false;
            return isAvilable;

        }



        public List<City> GetActiveCities()
        {
            var _service = CRMService.Get;
            var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
            CityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active city
            CityQuery.ColumnSet = new ColumnSet(true);
            var OrFilter = new FilterExpression(LogicalOperator.Or);
            OrFilter.AddCondition("new_availablefor", ConditionOperator.In, 1, 3);
            OrFilter.AddCondition("new_availablefor", ConditionOperator.Null);
            CityQuery.Criteria.AddFilter(OrFilter);
            var result = _service.RetrieveMultiple(CityQuery).Entities.Select(a => a.ToEntity<City>());
            return result.ToList();

        }
        public List<District> GetCityDistricts(string cityId)
        {
            var query = new QueryExpression(CrmEntityNamesMapping.District);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("new_cityid", ConditionOperator.Equal, cityId);
            query.ColumnSet = new ColumnSet("new_englishname", "new_name", "new_districtid");
            query.AddOrder("new_name", OrderType.Ascending);

            //if (RequestUtility.Language == UserLanguage.Arabic)
            //{
            //    query.AddOrder("new_name", OrderType.Ascending);
            //}
            //else
            //{
            //    query.AddOrder("new_englishname", OrderType.Ascending);
            //}
            var _service = CRMService.Get;
            return _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<District>()).ToList();
        }
        public string GetDistrictPolygon(string districtId)
        {

                var _service = CRMService.Get;
                var PolygonResult = _service.Retrieve(CrmEntityNamesMapping.District, new Guid(districtId), new ColumnSet("new_polygonpath")).ToEntity<District>().PolygonPath;
                return PolygonResult;
  
         
        }

        public City GetCityDeliveryCost(string cityId)
        {
            var _service = CRMService.Get;
            var city = _service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_individualcontractdeliverycost")).ToEntity<City>();
            return city;
        } 
        public City GetEmployeeSelectMthodsByCity(string cityId)
        {
            var _service = CRMService.Get;
            var city = _service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_recieveworkertype")).ToEntity<City>();
            return city;
        }
    }
}
