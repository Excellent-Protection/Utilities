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
            var service = CRMService.Service;
            var city = service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_forindividual")).ToEntity<City>();
            var isAvilable = city.IsForIndv.HasValue ? city.IsForIndv.Value : false;
            return isAvilable;

        }



        public List<City> GetActiveCities(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId.ToString()), new ColumnSet("new_displaycities")).ToEntity<Service>();
            var displayCities = service.DisplayCities.Value;   //1 all ,2 only Service Cities ,3 Available For Hourly
            var result=new List<City>();

            var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
            if (displayCities == 2)
            {
                CityQuery.AddLink(CrmEntityNamesMapping.ServiceCity, "new_cityid", "new_city");
            }
            else if(displayCities == 1|| displayCities == 3) 
            {
            
                CityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active city
                //CityQuery.ColumnSet = new ColumnSet(true);
              
                var OrFilter = new FilterExpression(LogicalOperator.Or);
                OrFilter.AddCondition("new_availablefor", ConditionOperator.In, 1, 3);   //1 show for all   ,3 mobile and web
                OrFilter.AddCondition("new_availablefor", ConditionOperator.Null);
              
                if (displayCities == 3)  //new_isdalal
                {           
                    var AndFilter = new FilterExpression(LogicalOperator.And);
                    AndFilter.AddCondition("new_isdalal", ConditionOperator.Equal,true);
                    CityQuery.Criteria.AddFilter(AndFilter);
                }
          
                CityQuery.Criteria.AddFilter(OrFilter);
            }
            CityQuery.ColumnSet = new ColumnSet("new_citiesid", "new_name");
            result = _service.RetrieveMultiple(CityQuery).Entities.Select(a => a.ToEntity<City>()).Distinct().ToList();

            return result;

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
            var _service = CRMService.Service;
            return _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<District>()).ToList();
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
