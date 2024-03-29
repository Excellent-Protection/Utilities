﻿using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalViewModels.CRM;
using Utilities.GlobalViewModels;
using Utilities.Helpers;
using Westwind.Globalization;
using Utilities.Mappers;
using Models.CRM;

namespace Utilities.GlobalRepositories.CRM
{
    public class SliderRepository : BaseCrmEntityRepository
    {
        public SliderRepository(RequestUtility requestUtility) :
            base(requestUtility)
        {
            
        }
        public List<SliderVm> GetSliderItems(int? type)
        {
            var _service = CRMService.Service;

            var query = new QueryExpression(CrmEntityNamesMapping.SliderItems);
            query.ColumnSet = new ColumnSet("new_slideritemid", "new_description", "new_mobileimage", "new_mobileurl", "new_name", "new_type", "new_availablefor", "new_webimage", "new_weburl", "new_externalurl");
            query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active SliderItems only
            switch (RequestUtility.Source)
            {
                case RecordSource.Mobile:
                    {
                        query.Criteria.AddCondition("new_availablefor", ConditionOperator.Like, "%" + AvailableForOffers.Mobile.ToString() + "%");
                        break;
                    }
                case RecordSource.Web:
                    {
                        query.Criteria.AddCondition("new_availablefor", ConditionOperator.Like, "%" + AvailableForOffers.Web.ToString() + "%");
                        break;
                    }
            }
            if (type != null)
            {
                query.Criteria.AddCondition("new_type", ConditionOperator.Equal, type);
            }
            query.AddOrder("createdon", OrderType.Descending);
            var result = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Slider>()).ToModelListData<SliderVm>().Distinct().ToList();
            return result;

        }
    }
}
