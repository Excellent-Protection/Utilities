using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

namespace Utilities.GlobalManagers.CRM
{
    public class OffersManager : BaseManager, IDisposable
    {
        internal RequestUtility _requestUtility;
        public OffersManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _requestUtility = RequestUtility;
        }
        public ResponseVm<List<OffersVm>>  GetOffers(string offersector = null)
        {
            try
            {
                var _service = CRMService.Service;
                var query = new QueryExpression(CrmEntityNamesMapping.Offers);
                LinkEntity Slider = new LinkEntity( CrmEntityNamesMapping.Offers, CrmEntityNamesMapping.SliderItems, "new_slideritem", "new_slideritemid", JoinOperator.LeftOuter);
                Slider.Columns = new ColumnSet("new_description");
                Slider.EntityAlias=CrmEntityNamesMapping.SliderItems;
                query.LinkEntities.Add(Slider);
                query.ColumnSet = new ColumnSet("new_name", "new_datefrom", "new_dateto", "new_webimage", "new_mobileimage", "new_offersectortype", "new_selectedhourlypricing", "new_individualpricing", "new_flexiblepricing", "new_individualdiscount", "new_slideritem");
                query.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);//active offers only
                query.Criteria.AddCondition("new_available", ConditionOperator.Equal, true);
                query.Criteria.AddCondition("new_datefrom", ConditionOperator.OnOrBefore, DateTime.Now.Date);
                query.Criteria.AddCondition("new_dateto", ConditionOperator.OnOrAfter, DateTime.Now.Date);
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
                if (!string.IsNullOrEmpty(offersector))
                {
                    query.Criteria.AddCondition("new_offersectortype", ConditionOperator.Equal, int.Parse(offersector));
                }
                query.AddOrder("new_order", OrderType.Ascending);
                var result = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Offers>()).ToModelListData<OffersVm>().ToList();
                return new ResponseVm<List<OffersVm>> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<OffersVm>> { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };
            }
        }
        public ResponseVm<List<OffersVm>> GetOffersBySliderItem(string SliderItem)
        {
            try {
                var _service = CRMService.Service;
                var query = new QueryExpression(CrmEntityNamesMapping.Offers);
                query.ColumnSet = new ColumnSet("new_name", "new_datefrom", "new_dateto", "new_webimage", "new_mobileimage", "new_offersectortype", "new_selectedhourlypricing", "new_individualpricing", "new_flexiblepricing","new_slideritem", "new_individualdiscount");
                LinkEntity Slider = new LinkEntity(CrmEntityNamesMapping.Offers, CrmEntityNamesMapping.SliderItems, "new_slideritem", "new_slideritemid", JoinOperator.LeftOuter);
                Slider.Columns = new ColumnSet("new_description");
                Slider.EntityAlias = CrmEntityNamesMapping.SliderItems;
                query.LinkEntities.Add(Slider);
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
                if (!string.IsNullOrEmpty(SliderItem))
                {
                    query.Criteria.AddCondition("new_slideritem", ConditionOperator.Equal, new Guid(SliderItem));
                }
                query.Criteria.AddCondition("new_available", ConditionOperator.Equal, true);
                query.Criteria.AddCondition("new_datefrom", ConditionOperator.OnOrBefore, DateTime.Now.Date);
                query.Criteria.AddCondition("new_dateto", ConditionOperator.OnOrAfter, DateTime.Now.Date);
                query.AddOrder("new_order", OrderType.Ascending);
                var result = _service.RetrieveMultiple(query).Entities.Select(a => a.ToEntity<Offers>()).ToModelListData<OffersVm>().ToList();
                return new ResponseVm<List<OffersVm>> { Status = HttpStatusCodeEnum.Ok, Data = result };
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                return new ResponseVm<List<OffersVm>>
                {
                    Status = HttpStatusCodeEnum.IneternalServerError,
                    Message = "An Error Occurred"
                };
            }
        }
        public void Dispose()
        {
        }
    }
}
