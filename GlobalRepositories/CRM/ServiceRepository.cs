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
using Utilities.Helpers;

namespace HourlySectorLib.Repositories
{
   public class ServiceRepository : BaseCrmEntityRepository
    {
        public ServiceRepository(RequestUtility requestUtility):base(requestUtility)
        {
                
        }


        public Service GetServiceShifts(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_serviceshifts")).ToEntity<Service>();
            return service;
        }

        public Service GetServiceResourceGroups(string serviceId)
        {
            var _service = CRMService.Service;
            RelationshipQueryCollection relatedEntityCollection = new RelationshipQueryCollection();
            //resourceGroupRelation
            relatedEntityCollection.Add(new Relationship(CrmRelationsNameMapping.Service_ResourceGroupOneToMany),
                new QueryExpression()
                {
                    EntityName = CrmEntityNamesMapping.ResourceGroup,
                    ColumnSet = new ColumnSet(true)
                });
            RetrieveRequest request = new RetrieveRequest()
            {
                RelatedEntitiesQuery = relatedEntityCollection,
                Target = new EntityReference
                {
                    Id = new Guid(serviceId),
                    LogicalName = CrmEntityNamesMapping.Service
                }
            };
            RetrieveResponse response = ((RetrieveResponse)_service.Execute(request));
            var Service = response.Entity.ToEntity<Service>();
            return Service;

        }

        public IEnumerable<Service> GetHourlyServices(string projectId)
        {
            var _service = CRMService.Service;
            var query = new QueryExpression(CrmEntityNamesMapping.Service);
            query.Criteria.AddCondition("new_projectid", ConditionOperator.Equal, projectId);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, (int)CrmEntityState.Active);
            query.ColumnSet = new ColumnSet("new_serviceid", "new_servicenamearabic", "new_serviceenglishname", "new_arabicdescription", "new_englishdescription", "new_iconimage", "new_backimage", "new_servicenote", "new_servicenotear");
            return _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Service>());

        }
        public int GetServiceDisplayCitiesOption(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_displaycities")).ToEntity<Service>();
            return (service != null && service.DisplayCities != null) ? service.DisplayCities.Value : -1;
        }
        public Service GetMaxMinEmployeeNumber(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_maxlabornumber", "new_minlabornumber")).ToEntity<Service>();
            return service;
        }

        public Service GetServiceHoursNumbers(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_servicehours")).ToEntity<Service>();
            return service;
        }


        public Service GetServiceShift(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_serviceshifts")).ToEntity<Service>();
            return service;
        }


        public int? GetUnPaidContractStatus(string serviceId)
        {
            var service = CRMService.Service;
            var Service = service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_contractrestrict")).ToEntity<Service>();
            return Service.ContractRestrictUnpaid?.Value;

        }
        public Service GetCalendarDays(string serviceId)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet("new_servicecalendar")).ToEntity<Service>();
            return service;

        }
        public Service GetServiceTerms(string serviceId, string servicetermsField)
        {
            var _service = CRMService.Service;
            var service = _service.Retrieve(CrmEntityNamesMapping.Service, new Guid(serviceId), new ColumnSet(servicetermsField)).ToEntity<Service>();
            return service;
        }
    }
}
