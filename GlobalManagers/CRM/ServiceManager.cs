﻿using HourlySectorLib.Repositories;
using HourlySectorLib.ViewModels;
using HourlySectorLib.ViewModels.Custom;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalManagers;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

namespace HourlySectorLib.Managers
{
    public class ServiceManager : BaseManager, IDisposable
    {
        ServiceRepository _repo;
        public ServiceManager(RequestUtility requestUtility):base(requestUtility)
        {
            _repo = new ServiceRepository(RequestUtility);
        }

       


        public ResponseVm<List<BaseQuickLookupVm>> GetServiceResourceGroups(string serviceId)
        {
            try
            {
                var service = _repo.GetServiceResourceGroups(serviceId);
                var resourceGroups = service.RelatedEntities.Contains(new Relationship(CrmRelationsNameMapping.Service_ResourceGroupOneToMany)) ? service.RelatedEntities[new Relationship(CrmRelationsNameMapping.Service_ResourceGroupOneToMany)].Entities.Select(z => z.ToEntity<ResourceGroup>()).Where(d => d.statecode == 0).ToModelListData<BaseQuickLookupVm>() : null;
                return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.Ok, Data = resourceGroups.ToList() };
            }
            catch(Exception ex)
            {

            }
            return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred", "Shared") };

        }

        public ResponseVm<List<DisplayServiceVm>> GetServicesForService(ServiceType serviceType)
        {
            IEnumerable<Service> services = null;
            switch(serviceType)
            {
                case ServiceType.Hourly:
                    services = _repo.GetHourlyServices(DefaultValues.HourlyServiceProjectId);
                    break;
            }
            var servicesList = services.ToModelListData<DisplayServiceVm>().ToList();
            return new ResponseVm<List<DisplayServiceVm>> { Status = HttpStatusCodeEnum.Ok, Data = servicesList };
        }
        public int GetServiceDisplayCitiesOption(string serviceId)
        {
            var result = _repo.GetServiceDisplayCitiesOption(serviceId);
            return result;
        }

        public  List<int?> GetNumOfWorkers(string serviceId)
        {
            try
            {
                var serviceLaborNumbers = _repo.GetMaxMinEmployeeNumber(serviceId);
                return new List<int?> { serviceLaborNumbers.MinLaborNumber , serviceLaborNumbers.MaxLaborNumber};
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;

        }

        public List<string> GetServiceHours(string serviceId)
        {
            try
            {
                var service= _repo.GetServiceHoursNumbers(serviceId);
                var serviceHours = service != null ? service.ServiceHours.Split(',').ToList() : new List<string>();
                return serviceHours;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;

        }
        public List<string> GetServiceShifts(string serviceId)
        {
            try
            {
                var service = _repo.GetServiceShifts(serviceId);
                var serviceShifts = service != null ? service.ServiceShifts.Split(',').ToList() : new List<string>();
                using (GlobalManager _mngr = new GlobalManager(RequestUtility))
                {
                    return serviceShifts;
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }


        public string GetUnPaidContractStatus(string serviceId)
        {
            try
            {
                var UnPaidContractStatus = _repo.GetUnPaidContractStatus(serviceId);
                return UnPaidContractStatus.ToString() ;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }


        }

        public void Dispose()
        {
        }
    }
}