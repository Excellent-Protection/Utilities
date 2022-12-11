using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
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
using Utilities.Helpers;
using Models.CRM.Individual_Contract;

namespace Utilities.GlobalRepositories.CRM
{
    public class CityRepository: BaseCrmEntityRepository
    {
        public CityRepository(RequestUtility RequestUtility) : base(RequestUtility)
        {
        }

        public bool CheckDistrictAvilabilityForService( string districtId, string serviceId=null)
        {
            var _service = CRMService.Service;
            if (serviceId != null)
            {
                var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_displaydistricts")).ToEntity<Service>();
                var displayDistrict = service.DisplayDistrict.Value;   //1 all ,2 only District service   

                if (displayDistrict == (int)DisplayDistrictForService.All)
                    return true;
            }


            var query = new QueryExpression(CrmEntityNamesMapping.District);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("new_districtid", ConditionOperator.Equal, districtId);
            query.AddLink(CrmEntityNamesMapping.ServiceDistrict, "new_districtid", "new_district");
            if (serviceId != null)
            {
                var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_displaydistricts")).ToEntity<Service>();
                var displayDistrict = service.DisplayDistrict.Value;   //1 all ,2 only District service   

                if (displayDistrict == (int)DisplayDistrictForService.All)
                    return true;
                query.LinkEntities[0].LinkCriteria.AddCondition("new_service", ConditionOperator.Equal, serviceId);

            }


            var res= _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<District>()).Distinct().ToList();
            return res.Count > 0;
        }


        public bool CheckCityAvilabilityForService(string cityId, ServiceType serviceType, string serviceId=null)
        {

            switch (serviceType)
            {
                case ServiceType.Individual:
                    return CheckCityAvailabilityForIndvService(cityId);
                case ServiceType.Hourly:
                    return CheckCityAvailabilityForHourlyService(cityId, serviceId); // will implement with hourly service steps 
                default:
                    return false;
            }

        }

        public bool CheckCityAvailabilityForHourlyService(string cityId, string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_displaycities")).ToEntity<Service>();
                if (service.DisplayCities.Value == (int)DisplayCitiesForService.onlyServiceCities)
                {
                    var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
                    //is available for hourly true
                    CityQuery.Criteria.AddCondition("new_isdalal", ConditionOperator.Equal,true);
                    CityQuery.AddLink(CrmEntityNamesMapping.ServiceCity, "new_cityid", "new_city");
                    CityQuery.LinkEntities[0].LinkCriteria.AddCondition("new_service", ConditionOperator.Equal, serviceId);
                    CityQuery.LinkEntities[0].LinkCriteria.AddCondition("new_city", ConditionOperator.Equal, cityId);
                    var result = _service.RetrieveMultiple(CityQuery).Entities.Select(a => a.ToEntity<City>()).Distinct().ToList();
                    return result.Count > 0;
                }
                else if(service.DisplayCities.Value == (int)DisplayCitiesForService.All)
                {
                    var city = _service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_isdalal")).ToEntity<City>();
                    var IsForHourly = city.IsForHourly.HasValue ? city.IsForHourly.Value : false;
                    return IsForHourly;
            }
            else
            {
                return true;
            }
        }

        public bool CheckCityAvailabilityForIndvService(string cityId)
        {
            var service = CRMService.Service;
            var city = service.Retrieve(CrmEntityNamesMapping.City, new Guid(cityId), new ColumnSet("new_forindividual")).ToEntity<City>();
            var isAvilable = city.IsForIndv.HasValue ? city.IsForIndv.Value : false;
            return isAvilable;

        }



        public List<City> GetALlActiveCities()
        {
            var _service = CRMService.Service;

            var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
            CityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active city

            var OrFilter = new FilterExpression(LogicalOperator.Or);
            OrFilter.AddCondition("new_availablefor", ConditionOperator.In, 1, 3);   //1 show for all   ,3 mobile and web
            OrFilter.AddCondition("new_availablefor", ConditionOperator.Null);

            CityQuery.Criteria.AddFilter(OrFilter);
            CityQuery.ColumnSet = new ColumnSet("new_citiesid", "new_name", "new_englsihname");

            var OrderFeild = RequestUtility.Language == UserLanguage.Arabic ? "new_name" : "new_englsihname";
            CityQuery.AddOrder(OrderFeild, OrderType.Ascending);
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
            CityQuery.ColumnSet = new ColumnSet("new_citiesid", "new_name", "new_englsihname");

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


            CityQuery.ColumnSet = new ColumnSet("new_citiesid", "new_name", "new_englsihname");

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

        public List<District> GetCityDistricts(string cityId, string serviceId=null)

        {

            var _service = CRMService.Service;
            var query = new QueryExpression(CrmEntityNamesMapping.District);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("new_cityid", ConditionOperator.Equal, cityId);




            query.ColumnSet = new ColumnSet("new_name","new_englishname", "new_districtid");
            if (serviceId != null)
            {
                var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId.ToString()), new ColumnSet("new_displaydistricts")).ToEntity<Service>();
                var displayDistrict = service.DisplayDistrict.Value;   //1 all ,2 only District service   
                if (displayDistrict == (int)DisplayDistrictForService.OnlyServiceDistricts)
                {
                    query.AddLink(CrmEntityNamesMapping.ServiceDistrict, "new_districtid", "new_district");
                    query.LinkEntities[0].LinkCriteria.AddCondition("new_service", ConditionOperator.Equal, serviceId);
                }
            }
                


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
        public List<BaseQuickLookupVm> GetAvailableCitiesForIndividual(string professionId,string pricingId=null)
        {
            var _service = CRMService.Service;

            var CityQuery = new QueryExpression(CrmEntityNamesMapping.City);
            CityQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active city

            var OrFilter = new FilterExpression(LogicalOperator.Or);
            OrFilter.AddCondition("new_availablefor", ConditionOperator.In, 1, 3);   //1 show for all   ,3 mobile and web
            OrFilter.AddCondition("new_availablefor", ConditionOperator.Null);

            CityQuery.Criteria.AddFilter(OrFilter);

            var AndFilter = new FilterExpression(LogicalOperator.And);
            AndFilter.AddCondition("new_forindividual", ConditionOperator.Equal, true);

            CityQuery.Criteria.AddFilter(AndFilter);

            CityQuery.ColumnSet = new ColumnSet("new_cityid", "new_name", "new_englsihname");
            CityQuery.Distinct = true;

            LinkEntity CityPricingLink = new LinkEntity(CrmEntityNamesMapping.City, CrmRelationsNameMapping.IndividualContractPricing_City, "new_cityid", "new_cityid", JoinOperator.Inner);
            if (!string.IsNullOrEmpty(professionId))
            {
                LinkEntity PricingProfessionLink = new LinkEntity(CrmRelationsNameMapping.IndividualContractPricing_City, CrmEntityNamesMapping.IndividualPricing, "new_indvpriceid", "new_indvpriceid", JoinOperator.Inner);
                PricingProfessionLink.LinkCriteria.AddCondition("new_professiongroup", ConditionOperator.Equal, professionId);
                CityPricingLink.LinkEntities.Add(PricingProfessionLink);
                if(!string.IsNullOrEmpty(pricingId))
                    PricingProfessionLink.LinkCriteria.AddCondition("new_indvpriceid", ConditionOperator.Equal, pricingId);
            }
            CityQuery.LinkEntities.Add(CityPricingLink);

            var result = _service.RetrieveMultiple(CityQuery).Entities.Select(a => a.ToEntity<City>()).Distinct().ToList();
            var cities = result.Select(a => new BaseQuickLookupVm()
            {
                Key = a.CityId.ToString(),
                Value = RequestUtility.Language == UserLanguage.Arabic ? a.ArabicName : a.EnglishName,
            }).ToList();
            return cities;

            //var service = CRMService.Service;
            //var PricingQuery = new QueryExpression(CrmEntityNamesMapping.IndividualPricing);
            //PricingQuery.Criteria.AddCondition("new_professiongroup", ConditionOperator.Equal, professionId);


        }
        public bool CheckCityIsInPricing(string CityId,string PricingId)
        {
            var query = new QueryExpression(CrmEntityNamesMapping.IndividualPricing);

            if (!string.IsNullOrEmpty(CityId))
            {
                LinkEntity linkEntity1 = new LinkEntity(CrmEntityNamesMapping.IndividualPricing, CrmRelationsNameMapping.IndividualContractPricing_City, "new_indvpriceid", "new_indvpriceid", JoinOperator.Inner);
                LinkEntity linkEntity2 = new LinkEntity(CrmRelationsNameMapping.IndividualContractPricing_City, CrmEntityNamesMapping.City, "new_cityid", "new_cityid", JoinOperator.Inner);
                linkEntity2.Columns = new Microsoft.Xrm.Sdk.Query.ColumnSet(true);
                linkEntity2.EntityAlias = "city";
                linkEntity2.LinkCriteria.AddCondition("new_cityid", ConditionOperator.Equal, CityId); //to filter by cities
                linkEntity1.LinkEntities.Add(linkEntity2);
                query.LinkEntities.Add(linkEntity1);
            }
            else
            {
                return false;
            }
            query.Criteria.AddCondition("new_indvpriceid",ConditionOperator.Equal, PricingId);
            //active pricing => Active=0
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            //Pricing Available For Renew
            query.Criteria.AddCondition("new_availablefornewstring", ConditionOperator.Equal, AvailableForRenew.Yes.ToString());
            //Is available for web and mobile => available=true
            FilterExpression filter = new FilterExpression(LogicalOperator.Or);
            //Is available for web and mobile => available=true
            switch (RequestUtility.Source)
            {
                case RecordSource.CRMPortal:
                    {
                        filter.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.CRMNewPortal.ToString() + "%");
                        break;
                    }
                case RecordSource.Mobile:
                    {
                        filter.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.Mobile.ToString() + "%");
                        filter.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.WebAndMobile.ToString() + "%");

                        break;
                    }
                case RecordSource.Web:
                default:
                    {
                        filter.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.Web.ToString() + "%");
                        filter.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.WebAndMobile.ToString() + "%");
                        break;
                    }
            }
            query.Criteria.AddFilter(filter);
            FilterExpression StartDateFilter = new FilterExpression(LogicalOperator.Or);
            FilterExpression EndDateFilter = new FilterExpression(LogicalOperator.Or);
            StartDateFilter.AddCondition("new_pricestartdate", ConditionOperator.Null);
            StartDateFilter.AddCondition("new_pricestartdate", ConditionOperator.OnOrBefore, DateTime.Now);
            EndDateFilter.AddCondition("new_priceenddate", ConditionOperator.Null);
            EndDateFilter.AddCondition("new_priceenddate", ConditionOperator.OnOrAfter, DateTime.Now);
            query.Criteria.AddFilter(StartDateFilter);
            query.Criteria.AddFilter(EndDateFilter);
            query.Distinct = true;
            query.ColumnSet = new ColumnSet("new_indvpriceid");
            var _service = CRMService.Service;
            var pricing=_service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<IndividualPricing>()).ToList();
            return pricing.Count > 0 ? true : false;
        }
    }
}
