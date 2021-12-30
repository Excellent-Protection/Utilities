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
    public class CityManager : BaseManager , IDisposable
    {
        CityRepository _repo;
        internal RequestUtility _requestUtility;
        public CityManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _requestUtility = RequestUtility;
            _repo = new CityRepository();
        }

        


        public ResponseVm<string> CheckCityAvilabilityForService(string cityId, ServiceType serviceType, string serviceId)
        {
            try
            {
                var result = _repo.CheckCityAvilabilityForService(cityId, serviceType, serviceId);
                if (result)
                {
                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok };
                }
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = DbRes.T("CityNotAvilableForService", "Shared") };

            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("cityId", cityId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }


        public ResponseVm<string> CheckDistrictAvilabilityForService(string serviceId, string districtId)
        {
            try
            {
                var result = _repo.CheckDistrictAvilabilityForService(serviceId,districtId);
                if (result)
                {
                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok };
                }
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ambiguous, Message = DbRes.T("districtNotAvilableForService", "Shared") };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("districtId", districtId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }



        public ResponseVm<List<BaseQuickLookupVm>> GetActiveCities(string serviceId = "")
        {
            try
            {
                var cities = new List<BaseQuickLookupVm>();
                if (serviceId == "")
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

        public ResponseVm<List<BaseQuickLookupVm>> GetCityDistricts(string cityId, string serviceId)
        {
            try
            {
                var districts = _repo.GetCityDistricts(cityId, serviceId).ToModelListData<BaseQuickLookupVm>().ToList();
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.Ok, Data = districts };

            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };

            }
        }
    public ResponseVm<string>  GetDistrictPolygon (string districtId)
        {
            try
            {
                var polygon = _repo.GetDistrictPolygon(districtId);
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = polygon };
            }
            catch(Exception ex)
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
            catch(Exception ex)
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

        public void Dispose()
        {
        }
    }

}
