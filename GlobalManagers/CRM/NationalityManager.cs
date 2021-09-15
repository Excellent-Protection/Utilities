﻿using Microsoft.Xrm.Sdk.Query;
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
using Utilities.Helpers;

namespace Utilities.GlobalManagers.CRM
{
  public  class NationalityManager : BaseManager , IDisposable
    {
        internal RequestUtility _requestUtility;

        public NationalityManager(RequestUtility requestUtility) : base(requestUtility)
        {

            _requestUtility = RequestUtility;
        }

        public void Dispose()
        {
        }

        public List<BaseQuickLookupVm> GetNationalitiesForIndvSales()
        {
            try
            {
                var query = new QueryExpression(CrmEntityNamesMapping.Nationality) { Distinct = true };
                query.ColumnSet = new ColumnSet("new_countryid", "new_name", "new_nameenglish");
                query.Criteria.AddCondition("new_isindv", ConditionOperator.Equal, true);
                var _service = CRMService.Get;
                var nationalites = _service.RetrieveMultiple(query).Entities.
                    Select(a => a.ToEntity<Country>()).Select(t => new BaseQuickLookupVm() { Key = t.Id.ToString(), Value = RequestUtility.Language == UserLanguage.Arabic ? (t.Name != null ? t.Name : t.EnglishName) : (t.EnglishName != null ? t.EnglishName : t.Name) }).ToList();
                return new List<BaseQuickLookupVm>(nationalites);
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }

    }
}
