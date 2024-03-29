﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
    public class CityManager : BaseManager, IDisposable
    {
        CityRepository _repo;
        internal RequestUtility _requestUtility;
        ExcSettingsManager _excSettingMngr;
        public CityManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _requestUtility = RequestUtility;
            _repo = new CityRepository(RequestUtility);
            _excSettingMngr = new ExcSettingsManager(RequestUtility);
        }


        public ResponseVm<string> CheckCityAndDistrictAvilabilityForService(string cityId, string districtId, ServiceType serviceType, string serviceId = null)
        {
            try
            {
                var CityAvilability = _repo.CheckCityAvilabilityForService(cityId, serviceType, serviceId);
                if (CityAvilability)
                {
                    var DistrictAvilability = _repo.CheckDistrictAvilabilityForService(districtId, serviceId);
                    if (DistrictAvilability)
                        return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok };
                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = DbRes.T("tellmewhenserviceavailablefordistrict", "HourlyResources"), Code = "300.1" };

                }
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = DbRes.T("tellmewhenserviceavailableforcity", "HourlyResources") ,Code="300.1"};

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("cityId", cityId ),("DistrictId",districtId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }




        public ResponseVm<string> CheckCityAvilabilityForService(string cityId, ServiceType serviceType, string serviceId=null)
        {
            try
            {
                var result = _repo.CheckCityAvilabilityForService(cityId, serviceType, serviceId);
                if (result)
                {
                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok };
                }
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = DbRes.T("CityNotAvilableForService", "Shared"), Code = "300.1" };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("cityId", cityId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }



        public ResponseVm<string> CheckDistrictAvilabilityForService( string districtId, string serviceId=null)


        {
            try
            {
                var result = _repo.CheckDistrictAvilabilityForService(districtId,serviceId);
                if (result)
                {
                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok };
                }
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = DbRes.T("districtNotAvilableForService", "Shared"), Code = "300.1" };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("districtId", districtId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }

        public ResponseVm<string> CheckAddressAvilabilityForService(string CityId, string districtId, string serviceId = null)
        {
            try
            {
                var message = "";
                var canCreateLead =_excSettingMngr.GetSettingValueByName("CanCreateLead",DefaultValues.CanCreateLead);
                var CityAvilability = CheckCityAvilabilityForService(CityId, serviceId != null ? ServiceType.Hourly : ServiceType.Individual, serviceId);

                if (CityAvilability.Status == HttpStatusCodeEnum.Ambiguous)
                {
                    message = serviceId == null ? DbRes.T("cancreateorderbyurl", "Shared") : canCreateLead ? DbRes.T("tellmewhenserviceisavailable", "Shared") : CityAvilability.Message;
                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = message, Code = canCreateLead ? "300.1" : "" };
                }


                if (CityAvilability.Status == HttpStatusCodeEnum.Ok)
                {
                    var DistrictAvilability = CheckDistrictAvilabilityForService(serviceId, districtId);

                    if (DistrictAvilability.Status == HttpStatusCodeEnum.Ambiguous)
                    {
                        message = serviceId == null ? DbRes.T("cancreateorderbyurl", "Shared") : canCreateLead ? DbRes.T("tellmewhenserviceisavailable", "Shared") : DistrictAvilability.Message;
                        return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = message, Code = canCreateLead ? "300.1" : "" };
                    }

                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok };
                }

                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("districtId", districtId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }



        public ResponseVm<List<BaseQuickLookupVm>> GetActiveCities(string serviceId = null)
        {
            try
            {
                var cities =new List<BaseQuickLookupVm>();
                if (serviceId == null)

                    cities = _repo.GetALlActiveCities().ToModelListData<BaseQuickLookupVm>().ToList();
                else
                    cities = _repo.GetHourlyCities(serviceId).ToModelListData<BaseQuickLookupVm>().ToList();
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.Ok, Data = cities };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };

            }
        }

        public ResponseVm<List<BaseQuickLookupVm>> GetCityDistricts(string cityId, string serviceId=null)
        {
            try
            {
                var districts = _repo.GetCityDistricts(cityId, serviceId).ToModelListData<BaseQuickLookupVm>().ToList();
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.Ok, Data = districts };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };

            }
        }
        public ResponseVm<string> GetDistrictPolygon(string districtId)
        {
            try
            {
                var polygon = _repo.GetDistrictPolygon(districtId);
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = polygon };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }

        public decimal GetCityDeliveryCost(string cityId)
        {
            try
            {
                var cost = _repo.GetCityDeliveryCost(cityId).IndividualContractDeliveryCost;
                return cost != null ? cost.Value : 0;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
        }


        public int? GetEmployeeSelectTypeByCity(string cityId)
        {
            try
            {
                var selectEmployeeType = _repo.GetEmployeeSelectMthodsByCity(cityId).RecieveWorkerType.Value;
                return selectEmployeeType;

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return 0;
            }
        }
        public ResponseVm<List<BaseQuickLookupVm>> GetAvailableCitiesForIndividual(string ProfessionId,string PricingId=null)
        {
            try
            {
                var result = _repo.GetAvailableCitiesForIndividual(ProfessionId,PricingId);
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.Ok, Data=result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }
        public bool CheckCityIsInPricing(string CityId,string PricingId)
        {
            try
            {
                return _repo.CheckCityIsInPricing(CityId, PricingId);
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }


        public void Dispose()
        {
        }
    }

}
