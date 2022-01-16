using Microsoft.Xrm.Sdk.Query;
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
    public class ProfessionGroupsRepository :BaseCrmEntityRepository
    {
        public ProfessionGroupsRepository(RequestUtility requestUtility) :
            base(requestUtility, CrmEntityNamesMapping.ProfessionGroup, "new_professiongroupid", "new_name", "new_namearabic")
        {

        }

        public List<ProfessionGroups> GetProfessionGroups()
        {

            var _service = CRMService.Service;
            var query = new QueryExpression(CrmEntityNamesMapping.ProfessionGroup);
            query.ColumnSet = new ColumnSet(true);
            var peofessionGroups = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<ProfessionGroups>()).ToList();
            return peofessionGroups;

        }

        public List<ProfessionGroups> GetProfessionGroupsFromPackages()
        {
            var _service = CRMService.Service;
            var querypricing = new QueryExpression(CrmEntityNamesMapping.IndividualPricing);
            querypricing.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);
            querypricing.Criteria.AddCondition("new_displaypricing", ConditionOperator.In, (int)DisplayPricingFor.Mobile, (int)DisplayPricingFor.WebAndMobile, (int)DisplayPricingFor.All);
            querypricing.ColumnSet = new ColumnSet(true);
            var Pricing = _service.RetrieveMultiple(querypricing).Entities.Select(a => a.ToEntity<IndividualPricing>()).ToList();
            var professionsIds = Pricing.Where(a => a.ProfessionGroup != null).Select(a => a.ProfessionGroup.Id.ToString()).Distinct().ToList();
            var professionQuery = new QueryExpression(CrmEntityNamesMapping.ProfessionGroup);
            FilterExpression filter = new FilterExpression();
            filter.AddCondition("new_professiongroupid", ConditionOperator.In, professionsIds.ToArray());

            professionQuery.Criteria.AddFilter(filter);
            professionQuery.ColumnSet = new ColumnSet(true);
            var professions = _service.RetrieveMultiple(professionQuery).Entities.Select(a => a.ToEntity<ProfessionGroups>()).ToList();
            return professions;
        }


        public ProfessionGroups GetProfessionGender(string professionGroupId)
        {
            var _service = CRMService.Service;
            var professionData = _service.Retrieve(CrmEntityNamesMapping.ProfessionGroup, new Guid(professionGroupId), new ColumnSet("new_gender")).ToEntity<ProfessionGroups>();
            return professionData;

        }

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
