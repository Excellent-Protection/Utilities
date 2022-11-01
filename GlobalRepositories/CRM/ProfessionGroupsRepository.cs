using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using Models.CRM.Individual_Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalRepositories.CRM
{
    public class ProfessionGroupsRepository : BaseCrmEntityRepository
    {
        public ProfessionGroupsRepository(RequestUtility requestUtility) :
            base(requestUtility, CrmEntityNamesMapping.ProfessionGroup, "new_professiongroupid", "new_name", "new_namearabic")
        {

        }

        public List<BaseQuickLookupWithImageVm> GetProfessionGroups(ServiceType? serviceType = null)
        {
            var _service = CRMService.Service;
            var professionsIds = GetProfessionIdsListFromIndivPricing();
            var query = new QueryExpression(CrmEntityNamesMapping.ProfessionGroup);
            query.ColumnSet = new ColumnSet(true);
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            if (serviceType == ServiceType.Individual)
                query.Criteria.AddCondition("new_forindivdual", ConditionOperator.Equal, true);
            var professionGroups = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<ProfessionGroups>()).ToModelListData<BaseQuickLookupWithImageVm, ProfessionGroups>().ToList();
            professionGroups.ForEach(p => p.HasPackage = professionsIds.Contains(p.Key.ToString()) ? true : false);
            return professionGroups;

        }

        public List<BaseQuickLookupWithImageVm> GetProfessionGroupsFromPackages(ServiceType? serviceType = null)
        {
            var _service = CRMService.Service;
            var professionsIds=GetProfessionIdsListFromIndivPricing();
            if (professionsIds.Count ==0)
                return new List<BaseQuickLookupWithImageVm> { };
            var professionQuery = new QueryExpression(CrmEntityNamesMapping.ProfessionGroup);
            FilterExpression filter = new FilterExpression();
            filter.AddCondition("new_professiongroupid", ConditionOperator.In, professionsIds.ToArray());
            if (serviceType == ServiceType.Individual)
                filter.AddCondition("new_forindivdual", ConditionOperator.Equal, true);
            professionQuery.Criteria.AddFilter(filter);
            professionQuery.ColumnSet = new ColumnSet(true);
            var professions = _service.RetrieveMultiple(professionQuery).Entities.Select(a => a.ToEntity<ProfessionGroups>()).ToModelListData<BaseQuickLookupWithImageVm, ProfessionGroups>().ToList();
            professions.ForEach(p => p.HasPackage = true);
            return professions;
        }
        public List<string> GetProfessionIdsListFromIndivPricing()
        {
            var _service = CRMService.Service;
            var querypricing = new QueryExpression(CrmEntityNamesMapping.IndividualPricing);
            querypricing.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);

            //querypricing.Criteria.AddCondition("new_displaypricing", ConditionOperator.In, (int)DisplayPricingFor.Mobile, (int)DisplayPricingFor.WebAndMobile, (int)DisplayPricingFor.All);
            querypricing.ColumnSet = new ColumnSet(true);
            FilterExpression filter2 = new FilterExpression(LogicalOperator.Or);
            switch (RequestUtility.Source)
            {
                case RecordSource.CRMPortal:
                    {
                        filter2.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.CRMNewPortal.ToString() + "%");
                        break;
                    }
                case RecordSource.Mobile:
                    {
                        filter2.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.Mobile.ToString() + "%");
                        filter2.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.WebAndMobile.ToString() + "%");

                        break;
                    }
                case RecordSource.Web:
                default:
                    {
                        filter2.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.Web.ToString() + "%");
                        filter2.AddCondition("new_displaypricingfor", ConditionOperator.Like, "%" + DisplayPricingFor.WebAndMobile.ToString() + "%");
                        break;
                    }
            }
            querypricing.Criteria.AddFilter(filter2);
            var Pricing = _service.RetrieveMultiple(querypricing).Entities.Select(a => a.ToEntity<IndividualPricing>()).ToList();
            var professionsIds = Pricing.Where(a => a.ProfessionGroup != null).Select(a => a.ProfessionGroup.Id.ToString()).Distinct().ToList();
            return professionsIds;
        }


    public ProfessionGroups GetProfessionGender(string professionGroupId)
        {
            var _service = CRMService.Service;
            var professionData = _service.Retrieve(CrmEntityNamesMapping.ProfessionGroup, new Guid(professionGroupId), new ColumnSet("new_gender")).ToEntity<ProfessionGroups>();
            return professionData;

        }

        //public List<string> GetProfessionsId(string ProfGroupId)
        //{

        //    var _service = CRMService.Service;

        //    var query = new QueryExpression(CrmEntityNamesMapping.Profession);
        //    query.AddLink(CrmEntityNamesMapping.Profession_ProfessionGroup, "new_professionid", "new_professionid");
        //    query.LinkEntities[0].LinkCriteria.AddCondition("new_professiongroupid", ConditionOperator.Equal, ProfGroupId);
        //    var res= _service.RetrieveMultiple(query).Entities.Select(a => a.Id.ToString()).ToList();
        //    return res;

        //}



        public List<string> GetProfessionsId(string ProfGroupId)
        {


            var _service = CRMService.Service;

            var query = new QueryExpression(CrmEntityNamesMapping.Profession);
            query.Criteria.AddCondition("new_professiongroup", ConditionOperator.Equal, ProfGroupId);
            var res=  _service.RetrieveMultiple(query).Entities.Select(a => a.Id.ToString()).ToList();
            if (res.Count == 0)
                res.Add(_service.Retrieve(CrmEntityNamesMapping.ProfessionGroup, new Guid(ProfGroupId), new ColumnSet("new_defaultprofession"))
                    .ToEntity<ProfessionGroups>().Defaultprofession.Id.ToString());
         
            return res;

         

      
          

        }

        public string GetRequiredAttchmentsByProfessionGroup(string profGroupId)
        {
            var _service = CRMService.Service;
            var prof = _service.Retrieve(CrmEntityNamesMapping.ProfessionGroup, new Guid(profGroupId), new ColumnSet("new_requiredattachments")).ToEntity<ProfessionGroups>();
            return prof.RequiredAttachments;

        }
    }
}
