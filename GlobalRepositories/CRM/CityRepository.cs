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


        public bool CheckCityAvilabilityForService(string cityId, ServiceType serviceType, string serviceId)
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



        public bool CheckCityAvailabilityForIndvService(string cityId)
        {
            var service = CRMService.Service;
            var city = service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_forindividual")).ToEntity<City>();
            var isAvilable = city.IsForIndv.HasValue ? city.IsForIndv.Value : false;
            return isAvilable;

        }

        // GetHourlyCities .. service 


        // GetCitiesAvailableForHourly   .. Is Dala = true 
        // Get ALl Active Cities ,,, old 
        //Get ServiceCities .. join

        public List<City> GetALlActiveCities()
        {
            var _service = CRMService.Service;

            var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
            CityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active city
            CityQuery.ColumnSet = new ColumnSet(true);

            var OrFilter = new FilterExpression(LogicalOperator.Or);
            OrFilter.AddCondition("new_availablefor", ConditionOperator.In, 1, 3);   //1 show for all   ,3 mobile and web
            OrFilter.AddCondition("new_availablefor", ConditionOperator.Null);

            CityQuery.Criteria.AddFilter(OrFilter);

            var result = _service.RetrieveMultiple(CityQuery).Entities.Select(a => a.ToEntity<City>()).Distinct().ToList();
            return result;
        }

        public List<City> GetServiceCities(string serviceId = "")
        {
            var _service = CRMService.Service;
            var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
            CityQuery.AddLink(CrmEntityNamesMapping.ServiceCity, "new_cityid", "new_city");
            CityQuery.LinkEntities[0].LinkCriteria.AddCondition("new_service", ConditionOperator.Equal, serviceId);

            CityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active city


            var OrFilter = new FilterExpression(LogicalOperator.Or);
            OrFilter.AddCondition("new_availablefor", ConditionOperator.In, 1, 3);   //1 show for all   ,3 mobile and web
            OrFilter.AddCondition("new_availablefor", ConditionOperator.Null);

            CityQuery.Criteria.AddFilter(OrFilter);
            CityQuery.ColumnSet = new ColumnSet("new_citiesid", "new_name");

            var result = _service.RetrieveMultiple(CityQuery).Entities.Select(a => a.ToEntity<City>()).Distinct().ToList();
            return result;
        }


        public List<City> GetCitiesAvailableForHourly(string serviceId)
        {
            var _service = CRMService.Service;

            var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
            CityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active city

            var OrFilter = new FilterExpression(LogicalOperator.Or);
            OrFilter.AddCondition("new_availablefor", ConditionOperator.In, 1, 3);   //1 show for all   ,3 mobile and web
            OrFilter.AddCondition("new_availablefor", ConditionOperator.Null);

            CityQuery.Criteria.AddFilter(OrFilter);

            var AndFilter = new FilterExpression(LogicalOperator.And);
            AndFilter.AddCondition("new_isdalal", ConditionOperator.Equal, true);
            CityQuery.Criteria.AddFilter(AndFilter);


            CityQuery.ColumnSet = new ColumnSet("new_citiesid", "new_name");

            var result = _service.RetrieveMultiple(CityQuery).Entities.Select(a => a.ToEntity<City>()).Distinct().ToList();
            return result;

        }

        public List<City> GetHourlyCities(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId.ToString()), new ColumnSet("new_displaycities")).ToEntity<Service>();
            var displayCities = service.DisplayCities.Value;   //1 all ,2 only Service Cities ,3 Available For Hourly       

            var result = new List<City>();

            if (displayCities == (int)DisplayCitiesForService.AvailableForHourly)
                return GetCitiesAvailableForHourly(serviceId);

            else if (displayCities == (int)DisplayCitiesForService.All)
                return GetALlActiveCities();

            else if (displayCities == (int)DisplayCitiesForService.onlyServiceCities)
                return GetServiceCities(serviceId);

            return result;

        }
        public List<District> GetCityDistricts(string cityId, string serviceId)
        {

            var _service = CRMService.Service;

            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId.ToString()), new ColumnSet("new_displaydistricts")).ToEntity<Service>();
            var displayDistrict = service.DisplayDistrict.Value;   //1 all ,2 only District service   


            var query = new QueryExpression(CrmEntityNamesMapping.District);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("new_cityid", ConditionOperator.Equal, cityId);


            if (displayDistrict == (int)DisplayDistrictForService.OnlyServiceDistricts) {
                query.AddLink(CrmEntityNamesMapping.ServiceDistrict, "new_districtid", "new_district");
                query.LinkEntities[0].LinkCriteria.AddCondition("new_service", ConditionOperator.Equal, serviceId);
            }
                
            query.ColumnSet = new ColumnSet("new_englishname", "new_name", "new_districtid");

            return _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<District>()).Distinct().ToList();
        }
        public string GetDistrictPolygon(string districtId)
        {

            var _service = CRMService.Service;
            var PolygonResult = _service.Retrieve(CrmEntityNamesMapping.District, new Guid(districtId), new ColumnSet("new_polygonpath")).ToEntity<District>().PolygonPath;
            return PolygonResult;


        }

        public City GetCityDeliveryCost(string cityId)
        {
            var _service = CRMService.Service;
            var city = _service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_individualcontractdeliverycost")).ToEntity<City>();
            return city;
        }
        public City GetEmployeeSelectMthodsByCity(string cityId)
        {
            var _service = CRMService.Service;
            var city = _service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_recieveworkertype")).ToEntity<City>();
            return city;
        }
    }
}
