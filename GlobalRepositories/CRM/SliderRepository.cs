using Microsoft.Xrm.Sdk.Query;
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
            query.ColumnSet = new ColumnSet("new_slideritemid", "new_description", "new_mobileimage", "new_mobileurl", "new_name", "new_type", "new_availablefor", "new_webimage", "new_weburl");
            //query.AddLink(CrmEntityNamesMapping.Offers, "new_slideritemid", "new_slideritem", JoinOperator.LeftOuter);
            //query.LinkEntities[0].EntityAlias = CrmEntityNamesMapping.Offers;
            //query.LinkEntities[0].Columns = new ColumnSet("new_webimage", "new_mobileimage", "new_offersectortype", "new_selectedhourlypricing", "new_individualpricing", "new_flexiblepricing");
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
            var result = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Slider>()).ToModelListData<SliderVm>().ToList();
            return result;

        }
        public List<OffersVm> GetOffersBySliderItem(string SliderItem)
        {
            var _service = CRMService.Service;
            var query = new QueryExpression(CrmEntityNamesMapping.Offers);
            query.ColumnSet = new ColumnSet("new_webimage", "new_mobileimage", "new_offersectortype", "new_selectedhourlypricing", "new_individualpricing", "new_flexiblepricing");
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
            if (SliderItem != null)
            {
                query.Criteria.AddCondition("new_slideritem", ConditionOperator.Equal, new Guid(SliderItem));
            }
            query.Criteria.AddCondition("new_available", ConditionOperator.Equal, true);
            query.Criteria.AddCondition("new_datefrom", ConditionOperator.OnOrBefore, DateTime.Now.Date);
            query.Criteria.AddCondition("new_dateto", ConditionOperator.OnOrAfter, DateTime.Now.Date);
            query.AddOrder("new_order", OrderType.Ascending);
            var result2 = _service.RetrieveMultiple(query).Entities;
            var result = result2.Select(a => a.ToEntity<Offers>()).ToModelListData<OffersVm>().ToList();
            return result;
        }
    }
}
