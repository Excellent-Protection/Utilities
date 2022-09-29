using HourlySectorLib.Repositories;
using HourlySectorLib.ViewModels;
using HourlySectorLib.ViewModels.Custom;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.DataAccess.CRM;
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
        public ServiceManager(RequestUtility requestUtility) : base(requestUtility)
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
            catch (Exception ex)
            {

            }
            return new ResponseVm<List<BaseQuickLookupVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred", "Shared") };

        }

        public ResponseVm<List<DisplayServiceVm>> GetServicesForService(ServiceType serviceType)
        {
            IEnumerable<Service> services = null;
            switch (serviceType)
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

        public List<int?> GetNumOfWorkers(string serviceId)
        {
            try
            {
                var serviceLaborNumbers = _repo.GetMaxMinEmployeeNumber(serviceId);
                return new List<int?> { serviceLaborNumbers.MinLaborNumber, serviceLaborNumbers.MaxLaborNumber };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;

        }

        public List<string> GetServiceHours(string serviceId)
        {
            try
            {
                var service = _repo.GetServiceHoursNumbers(serviceId);
                var serviceHours = service != null ? service.ServiceHours.Split(',').ToList() : new List<string>();
                return serviceHours;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;

        }
        public List<VisitShift> GetServiceShifts(string serviceId)
        {
            try
            {
                var service = _repo.GetServiceShifts(serviceId);
                var serviceShifts = service != null ? service.ServiceShifts.Split(',').ToList() : new List<string>();
                using (GlobalManager _mngr = new GlobalManager(RequestUtility))
                {

                    var shifts = serviceShifts.Select(a => (VisitShift)Enum.Parse(typeof(VisitShift), a)).ToList();


                    return shifts;
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
                return UnPaidContractStatus.ToString();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return null;
            }


        }
        public ResponseVm<List<string>> GetCalendarDays(string serviceId)
        {
            List<string> days = null;
            try
            {
                var service = _repo.GetCalendarDays(serviceId).CalendarDays;
                if (!string.IsNullOrEmpty(service))
                {
                    days = service.Split(',').ToList();
                }

                return new ResponseVm<List<string>> { Status = HttpStatusCodeEnum.Ok, Data = days };
            }
            catch (Exception e)
            {
                return new ResponseVm<List<string>> { Status = HttpStatusCodeEnum.IneternalServerError };

            }
        }
        public ResponseVm<string> GetServiceTerms(string serviceId)
        {
            try
            {
                string servicetermsField = RequestUtility.Language == UserLanguage.Arabic ? "new_arabicterms" : "new_englishterms";

                var service = _repo.GetServiceTerms(serviceId, servicetermsField);
                if (service.Attributes.Contains(servicetermsField))
                {

                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = service.Attributes[servicetermsField].ToString() };
                }
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok, Data = "" };

            }
            catch (Exception e)
            {
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnErrorOccurred", "Shared") };
            }
        }
        public ResponseVm<ServiceVm> GetDetails(string id)
        {
            try
            {
                var columnSet = new ColumnSet("new_name",
                                   "new_serviceenglishname",
                                   "new_englishdescription",
                                   "new_servicenamearabic",
                                   "new_arabicdescription",
                                   "new_servicecalendar",
                                   "new_serviceshifts",
                                   "new_servicehours",
                                   "new_projectid",
                                   "new_gender",
                                   "new_displaycities",
                                   "new_displaydistricts", "new_minlabornumber", "new_maxlabornumber", "new_servicenotear", "new_servicenote");
                RelationshipQueryCollection relatedEntityCollection = new RelationshipQueryCollection();
                //resourceGroupRelation
                relatedEntityCollection.Add(new Relationship(CrmRelationsNameMapping.Service_ResourceGroupOneToMany),
                    new QueryExpression()
                    {
                        EntityName = CrmEntityNamesMapping.ResourceGroup,
                        ColumnSet = new ColumnSet(true)
                    });

                //ExcSettingsRelation
                var ExcSettingQuery = new QueryExpression()
                {
                    EntityName = CrmEntityNamesMapping.ExcSettings,
                    ColumnSet = new ColumnSet(true)
                };
                switch (RequestUtility.Source)
                {
                    case RecordSource.CRMPortal:
                        {
                            ExcSettingQuery.Criteria.AddCondition("new_applyto", ConditionOperator.In, (int)ApplyToOrDisplayFor.All, (int)ApplyToOrDisplayFor.CRMNewPortal);
                            break;
                        }
                    case RecordSource.Web:
                        {
                            ExcSettingQuery.Criteria.AddCondition("new_applyto", ConditionOperator.In, (int)ApplyToOrDisplayFor.All, (int)ApplyToOrDisplayFor.Web, (int)ApplyToOrDisplayFor.WebAndMobile);
                            break;
                        }
                    case RecordSource.Mobile:
                        {
                            ExcSettingQuery.Criteria.AddCondition("new_applyto", ConditionOperator.In, (int)ApplyToOrDisplayFor.All, (int)ApplyToOrDisplayFor.Mobile, (int)ApplyToOrDisplayFor.WebAndMobile);
                            break;
                        }
                }
                ExcSettingQuery.AddOrder("new_applyto", OrderType.Ascending);
                relatedEntityCollection.Add(new Relationship(CrmRelationsNameMapping.Service_ExcSettings),
                   ExcSettingQuery);

                RetrieveRequest request = new RetrieveRequest()
                {
                    RelatedEntitiesQuery = relatedEntityCollection,
                    ColumnSet = columnSet,
                    Target = new EntityReference
                    {
                        Id = new Guid(id),
                        LogicalName = CrmEntityNamesMapping.Service
                    }
                };

                var _service = CRMService.Service;
                RetrieveResponse response = ((RetrieveResponse)_service.Execute(request));
                var Service = response.Entity.ToEntity<Service>().Toclass<ServiceVm>();
                Service.ServiceShiftsLookup = Service.ServiceShifts.Select(a => new BaseOptionSetVM() { Key = (int)a, Value = DbRes.T("Shift" + a.ToString(), "HourlyResources") }).ToList();
                return new ResponseVm<ServiceVm> { Status = HttpStatusCodeEnum.Ok, Data = Service };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("id", id));
                return new ResponseVm<ServiceVm>
                {
                    Status = HttpStatusCodeEnum.IneternalServerError,
                    Message = DbRes.T("AnErrorOccurred", "Shared")
                };
            };
        }
    
        public void Dispose()
        {
        }
    }
}
