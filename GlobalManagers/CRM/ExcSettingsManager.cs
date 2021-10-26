using LinqKit;
using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.CRM
{
    public class ExcSettingsManager : BaseManager
    {
        internal CrmContext _ctx;
        internal RequestUtility _requestUtility;

        public ExcSettingsManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _ctx = new CrmContext(CRMService.Service);
            _requestUtility = RequestUtility;
        }

      


        public object this[string Name]
        {
            get
            {
                var result = GetSettingByName(Name);
                if (result != null)
                {
                    var Parse = bool.TryParse(result.Value.ToLower(), out bool settingboolean);
                    if (Parse)
                    {
                        return settingboolean 
                        ;
                    }
                    Parse = decimal.TryParse(result.Value, out decimal settinginteger);
                    if (Parse)
                    {
                        return  settinginteger;
                    }
                    return  result.Value;
                }                
                else
                {
                    return null;
                }
            }
        }
        public ExcSettingsVm GetSettingByName(string key)
        {

            try
            {
                var Setting = _ctx.CreateQuery<ExcSettings>().FirstOrDefault(a => a.Name == key);
                return Setting.Toclass<ExcSettingsVm>();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("key", key));
                return null;
            }

        }
        public ExcSettingsVm GetSettingByNameAndService(string key, string serviceId)
        {
            try
            {
                var Setting = _ctx.CreateQuery<ExcSettings>().FirstOrDefault(a => a.Name == key && a.Service.Id == new Guid(serviceId));
                return Setting.Toclass<ExcSettingsVm>();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("key", key));
                return null;
            }

        }
        
        public ExcSettingsVm GetSettingByNameAndSource(string key, int applyTo)
        {
            try
            {
                var Setting = _ctx.CreateQuery<ExcSettings>().FirstOrDefault(a => a.Name == key && a.ApplyTo.Value== applyTo);
                return Setting.Toclass<ExcSettingsVm>();
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("key", key));
                return null;
            }

        }
        public Dictionary<string, string> this[List<string> keys]
        {
            get
            {
                try
                {

                    var predicateName = PredicateBuilder.New<ExcSettings>();
                    keys.ForEach(a =>
                    {

                        predicateName = predicateName.Or(p => p.Name == a);
                    });
                    Expression<Func<ExcSettings, bool>> KeysExpression = predicateName;
                    var predicateApplyTo = PredicateBuilder.False<ExcSettings>();
                    predicateApplyTo = predicateApplyTo.Or(p => p.ApplyTo.Value == (int)ApplyToOrDisplayFor.All);
                    switch (_requestUtility.Source)
                    {
                        case RecordSource.CRMPortal:
                            {
                                predicateApplyTo = predicateApplyTo.Or(p => p.ApplyTo.Value == (int)ApplyToOrDisplayFor.CRMNewPortal);
                                break;
                            }
                        case RecordSource.Web:
                            {
                                predicateApplyTo = predicateApplyTo.Or(p => p.ApplyTo.Value == (int)ApplyToOrDisplayFor.Web);
                                predicateApplyTo = predicateApplyTo.Or(p => p.ApplyTo.Value == (int)ApplyToOrDisplayFor.WebAndMobile);
                                break;
                            }
                        case RecordSource.Mobile:
                            {
                                predicateApplyTo = predicateApplyTo.Or(p => p.ApplyTo.Value == (int)ApplyToOrDisplayFor.Mobile);
                                predicateApplyTo = predicateApplyTo.Or(p => p.ApplyTo.Value == (int)ApplyToOrDisplayFor.WebAndMobile);
                                break;
                            }
                    }
                    var setting = _ctx.CreateQuery<ExcSettings>().AsExpandable().Where(KeysExpression.And(predicateApplyTo));
                    return setting.ToDictionary(a => a.Name, a => a.Value);
                }
                catch (Exception ex)
                {
                    LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
                return null;


            }
        }
        public Dictionary<string, Dictionary<string, string>> GetServicesSetting(List<string> serviceIds, List<string> Keys = null)
        {
            try
            {
                var _service = CRMService.Service;
                var filter = new FilterExpression(LogicalOperator.And)
                {
                    Conditions = { new ConditionExpression("new_service", ConditionOperator.In, serviceIds) }
                };
                if (Keys != null)
                {
                    filter.AddCondition("new_name", ConditionOperator.In, Keys);
                }
                var settings = _service.RetrieveMultiple(new QueryExpression(CrmEntityNamesMapping.ExcSettings)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = filter
                });


                return settings.Entities.Select(a => a.ToEntity<ExcSettings>()).ToModelListData<ExcSettingsVm>().GroupBy(a => a.ServiceId).ToDictionary(a => a.Key, a => a.ToDictionary(z => z.Name, z => z.Value));
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }

        public Dictionary<string, string> GetServicesSetting(string serviceId, List<string> Keys = null)
        {
            try
            {
                var _service = CRMService.Service;
                var filter = new FilterExpression(LogicalOperator.And)
                {
                    Conditions = { new ConditionExpression("new_service", ConditionOperator.Equal, serviceId) }
                };
                if (Keys != null)
                {
                    filter.AddCondition("new_name", ConditionOperator.In, Keys);
                }
                var settings = _service.RetrieveMultiple(new QueryExpression(CrmEntityNamesMapping.ExcSettings)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = filter
                });


                return settings.Entities.Select(a => a.ToEntity<ExcSettings>()).ToModelListData<ExcSettingsVm>().ToDictionary(a => a.Name, a => a.Value);
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }

        public Dictionary<string, string> GetServiceSetting(string serviceId, List<string> Keys = null)
        {
            try
            {
                var _service = CRMService.Service;
                var filter = new FilterExpression(LogicalOperator.And)
                {
                    Conditions = { new ConditionExpression("new_service", ConditionOperator.Equal, serviceId) }
                };
                if (Keys != null)
                {
                    filter.AddCondition("new_name", ConditionOperator.In, Keys.ToArray());
                }
                switch (RequestUtility.Source)
                {
                    case RecordSource.CRMPortal:
                        {
                            filter.AddCondition("new_applyto", ConditionOperator.In, (int)ApplyToOrDisplayFor.All, (int)ApplyToOrDisplayFor.CRMNewPortal);
                            break;
                        }
                    case RecordSource.Web:
                        {
                            filter.AddCondition("new_applyto", ConditionOperator.In, (int)ApplyToOrDisplayFor.All, (int)ApplyToOrDisplayFor.Web, (int)ApplyToOrDisplayFor.WebAndMobile);
                            break;
                        }
                    case RecordSource.Mobile:
                        {
                            filter.AddCondition("new_applyto", ConditionOperator.In, (int)ApplyToOrDisplayFor.All, (int)ApplyToOrDisplayFor.Mobile, (int)ApplyToOrDisplayFor.WebAndMobile);
                            break;
                        }
                }
                var settings = _service.RetrieveMultiple(new QueryExpression(CrmEntityNamesMapping.ExcSettings)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = filter
                });


                return settings.Entities.Select(a => a.ToEntity<ExcSettings>()).ToModelListData<ExcSettingsVm>().ToDictionary(a => a.Name, a => a.Value);
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }



    }
}
