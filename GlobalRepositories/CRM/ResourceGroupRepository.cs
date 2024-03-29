﻿using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using Models.CRM.Individual_Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.Helpers;

namespace Utilities.GlobalRepositories.CRM
{
 public   class ResourceGroupRepository: BaseCrmEntityRepository
    {

        public ResourceGroupRepository(RequestUtility requestUtility) :
            base(requestUtility, CrmEntityNamesMapping.ResourceGroup, "new_resourcegroupid", "new_name", "new_namearabic")
        {


        }

        public List<ResourceGroup> GetResourceGroupsFromIndividualPackages(string professionGroupId)
        {
            var _service = CRMService.Service;
            var querypricing = new QueryExpression(CrmEntityNamesMapping.IndividualPricing);
            querypricing.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            querypricing.Criteria.AddCondition("new_professiongroup", ConditionOperator.Equal, professionGroupId);
//          querypricing.Criteria.AddCondition("new_displaypricing", ConditionOperator.In, (int)DisplayPricingFor.Mobile, (int)DisplayPricingFor.WebAndMobile, (int)DisplayPricingFor.All);

            FilterExpression filter = new FilterExpression(LogicalOperator.Or);
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
            querypricing.Criteria.AddFilter(filter);
            querypricing.Criteria.AddCondition("new_availablefornewstring", ConditionOperator.Equal, AvailableForNew.Yes.ToString());
            querypricing.ColumnSet = new ColumnSet(true);
            var Pricing = _service.RetrieveMultiple(querypricing).Entities.Select(a => a.ToEntity<IndividualPricing>()).ToList();
            var resourceGroupIds = Pricing.Where (a=>a.ResourceGroup!=null).Select(a => a.ResourceGroup.Id.ToString()).Distinct().ToList();
            var resourceGroupQuery = new QueryExpression(CrmEntityNamesMapping.ResourceGroup);
            resourceGroupQuery.Criteria.AddCondition("new_resourcegroupid", ConditionOperator.In, resourceGroupIds.ToArray());
            resourceGroupQuery.ColumnSet = new ColumnSet(true);
            var resourceGroups = _service.RetrieveMultiple(resourceGroupQuery).Entities.Select(a => a.ToEntity<ResourceGroup>()).ToList();
            return resourceGroups;
        }


        public List<ResourceGroup> GetResourceGroups()
        {

            var _service = CRMService.Service;
            var query = new QueryExpression(CrmEntityNamesMapping.ResourceGroup);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            //query.Criteria.AddCondition("new_professiongroup", ConditionOperator.Equal, professionGroupId);
            var resourceGroups = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<ResourceGroup>()).ToList();
            return resourceGroups;

        }


        public List<ResourceGroup> GetResourceGroupsByService(string serivceId)
        {

            var _service = CRMService.Service;
            var query = new QueryExpression(CrmEntityNamesMapping.ResourceGroup);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            query.Criteria.AddCondition("new_service", ConditionOperator.Equal, new Guid(serivceId));

            var resourceGroups = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<ResourceGroup>()).ToList();
            return resourceGroups;

        }

        public List<string> GetNationalitiesId(string resourceGropId)
        {
            try
            {
                var _service = CRMService.Service;
                var query = new QueryExpression(CrmEntityNamesMapping.Nationality);
                query.AddLink(CrmEntityNamesMapping.ResourceGroupNationality, "new_countryid" ,"new_nationality");
                query.LinkEntities[0].LinkCriteria.AddCondition("new_resourcegroup", ConditionOperator.Equal, resourceGropId);
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                return _service.RetrieveMultiple(query).Entities.Select(a => a.Id.ToString()).ToList();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("resourceGropId", resourceGropId));
                return null;
            }
        }
        public List<ResourceGroup> GetResourceGroupsFromIndividualPackagesByCity(string professionGroupId, string cityId)
        {
            var _service = CRMService.Service;
            var querypricing = new QueryExpression(CrmEntityNamesMapping.IndividualPricing);
            if (!string.IsNullOrEmpty(cityId))
            {
                LinkEntity linkEntity1 = new LinkEntity(CrmEntityNamesMapping.IndividualPricing, CrmRelationsNameMapping.IndividualContractPricing_City, "new_indvpriceid", "new_indvpriceid", JoinOperator.Inner);
                LinkEntity linkEntity2 = new LinkEntity(CrmRelationsNameMapping.IndividualContractPricing_City, CrmEntityNamesMapping.City, "new_cityid", "new_cityid", JoinOperator.Inner);
                linkEntity2.Columns = new Microsoft.Xrm.Sdk.Query.ColumnSet(true);
                linkEntity2.EntityAlias = "city";
                linkEntity2.LinkCriteria.AddCondition("new_cityid", ConditionOperator.Equal, cityId); //to filter by cities

                linkEntity1.LinkEntities.Add(linkEntity2);
                querypricing.LinkEntities.Add(linkEntity1);
            }
            querypricing.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            querypricing.Criteria.AddCondition("new_professiongroup", ConditionOperator.Equal, professionGroupId);
            //          querypricing.Criteria.AddCondition("new_displaypricing", ConditionOperator.In, (int)DisplayPricingFor.Mobile, (int)DisplayPricingFor.WebAndMobile, (int)DisplayPricingFor.All);

            FilterExpression filter = new FilterExpression(LogicalOperator.Or);
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
            querypricing.Criteria.AddFilter(filter);
            querypricing.Criteria.AddCondition("new_availablefornewstring", ConditionOperator.Equal, AvailableForNew.Yes.ToString());
            querypricing.ColumnSet = new ColumnSet(true);
            var Pricing = _service.RetrieveMultiple(querypricing).Entities.Select(a => a.ToEntity<IndividualPricing>()).ToList();
            var resourceGroupIds = Pricing.Where(a => a.ResourceGroup != null).Select(a => a.ResourceGroup.Id.ToString()).Distinct().ToList();
            var resourceGroupQuery = new QueryExpression(CrmEntityNamesMapping.ResourceGroup);
            resourceGroupQuery.Criteria.AddCondition("new_resourcegroupid", ConditionOperator.In, resourceGroupIds.ToArray());
            resourceGroupQuery.ColumnSet = new ColumnSet(true);
            var resourceGroups = _service.RetrieveMultiple(resourceGroupQuery).Entities.Select(a => a.ToEntity<ResourceGroup>()).ToList();
            return resourceGroups;
        }

    }
}
