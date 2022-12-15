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
                query.ColumnSet = new ColumnSet("new_name", "new_datefrom", "new_dateto", "new_webimage", "new_mobileimage", "new_offersectortype", "new_selectedhourlypricing", "new_individualpricing", "new_flexiblepricing", "new_individualdiscount", "new_slideritem","new_description");
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
                LinkEntity SelectedHourlyPricing = new LinkEntity("new_offers", "new_selectedhourlypricing", "new_selectedhourlypricing", "new_selectedhourlypricingid", JoinOperator.LeftOuter);
                SelectedHourlyPricing.LinkCriteria.AddCondition("new_isavaliable", ConditionOperator.Equal, true);
                SelectedHourlyPricing.LinkCriteria.AddCondition("statecode", ConditionOperator.Equal, 0); //active
                SelectedHourlyPricing.EntityAlias = CrmEntityNamesMapping.SelectedHourlyPricing;
                SelectedHourlyPricing.Columns = new ColumnSet("new_selectedhourlypricingid");
                LinkEntity IndvPricing = new LinkEntity("new_offers", "new_indvprice", "new_individualpricing", "new_indvpriceid", JoinOperator.LeftOuter);
                IndvPricing.LinkCriteria.AddCondition("statecode", ConditionOperator.Equal, 0);
                IndvPricing.LinkCriteria.AddCondition("new_availablefornewstring", ConditionOperator.Equal, AvailableForRenew.Yes.ToString());
                //Is available for web and mobile => available=true
                FilterExpression filter = new FilterExpression(LogicalOperator.Or);
                //Is available for web and mobile => available=true
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
                IndvPricing.LinkCriteria.AddFilter(filter);
                FilterExpression StartDateFilter = new FilterExpression(LogicalOperator.Or);
                FilterExpression EndDateFilter = new FilterExpression(LogicalOperator.Or);
                StartDateFilter.AddCondition("new_pricestartdate", ConditionOperator.Null);
                StartDateFilter.AddCondition("new_pricestartdate", ConditionOperator.LessThan, DateTime.Now);
                EndDateFilter.AddCondition("new_priceenddate", ConditionOperator.Null);
                EndDateFilter.AddCondition("new_priceenddate", ConditionOperator.GreaterThan, DateTime.Now);
                IndvPricing.LinkCriteria.AddFilter(StartDateFilter);
                IndvPricing.LinkCriteria.AddFilter(EndDateFilter);
                IndvPricing.EntityAlias = CrmEntityNamesMapping.IndividualPricing;
                IndvPricing.Columns = new ColumnSet("new_indvpriceid");
                query.LinkEntities.Add(IndvPricing);
                query.LinkEntities.Add(SelectedHourlyPricing);
                var result1 = _service.RetrieveMultiple(query).Entities;
                var result=result1.Select(a => a.ToEntity<Offers>()).ToModelListData<OffersVm>().ToList();
                return new ResponseVm<List<OffersVm>> { Status = HttpStatusCodeEnum.Ok, Data = result.Where(s=>s.PricingId!=null).ToList() };
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
                query.ColumnSet = new ColumnSet("new_name", "new_datefrom", "new_dateto", "new_webimage", "new_mobileimage", "new_offersectortype", "new_selectedhourlypricing", "new_individualpricing", "new_flexiblepricing","new_slideritem", "new_individualdiscount", "new_description");
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
