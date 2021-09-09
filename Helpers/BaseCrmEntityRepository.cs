using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Helpers
{

    public class BaseCrmEntityRepository
    {
        public RequestUtility RequestUtility { get; set; }
        public string CrmEntityName { get; set; }
        public string CrmGuidFieldName { get; set; }
        public string CrmDisplayFieldName { get; set; }
        public string CrmDisplayFieldName_Arabic { get; set; }
        public string StatusFieldName { get; set; }

        public BaseCrmEntityRepository(RequestUtility RequestUtility)
            : this(RequestUtility, null)
        {

        }
        public BaseCrmEntityRepository(RequestUtility RequestUtility, string crmEntityName)
            : this(RequestUtility, crmEntityName, null)
        {
        }
        public BaseCrmEntityRepository(RequestUtility RequestUtility, string crmEntityName, string crmGuidFieldName)
            : this(RequestUtility, crmEntityName, crmGuidFieldName, null)
        {

        }
        public BaseCrmEntityRepository(RequestUtility RequestUtility, string crmEntityName, string crmGuidFieldName, string crmDisplayFieldName)
            : this(RequestUtility, crmEntityName, crmGuidFieldName, crmDisplayFieldName, null)
        {
        }
        public BaseCrmEntityRepository(RequestUtility RequestUtility, string crmEntityName, string crmGuidFieldName, string crmDisplayFieldName, string crmDisplayFieldName_Arabic)
            : this(RequestUtility, crmEntityName, crmGuidFieldName, crmDisplayFieldName, crmDisplayFieldName_Arabic, "statuscode") { }


        public BaseCrmEntityRepository(RequestUtility RequestUtility, string crmEntityName, string crmGuidFieldName, string crmDisplayFieldName, string crmDisplayFieldName_Arabic, string StatusFieldName)

        {
            this.CrmEntityName = crmEntityName;
            this.CrmGuidFieldName = crmGuidFieldName;
            this.CrmDisplayFieldName = crmDisplayFieldName;
            this.CrmDisplayFieldName_Arabic = crmDisplayFieldName_Arabic;
            this.StatusFieldName = StatusFieldName;
            this.RequestUtility = RequestUtility;
        }

        //public virtual Entity GetCrmEntity(string id)
        //{
        //    using (var globalmgr = new GlobalCrmRepository())
        //        return globalmgr.GetCrmEntity(CrmEntityName, id);
        //}

        //public virtual Entity GetCrmEntity(string id, string[] columns)
        //{
        //    using (var globalmgr = new GlobalCrmRepository())
        //        return globalmgr.GetCrmEntity(CrmEntityName, id, columns);
        //}

        //public virtual IEnumerable<BaseQuickLookupVM> GetAllForLookup(bool withOppositeDisplayLangIfNull = true, string filterWhereCondition = "")
        //{
        //    UserLanguage lang = RequestUtility.Language.Value;
        //    using (var globalmgr = new GlobalCrmRepository())
        //    {
        //        string displayField = string.Empty, oppositeDisplayField = string.Empty;
        //        switch (lang)
        //        {
        //            case UserLanguage.Arabic:
        //                displayField = CrmDisplayFieldName_Arabic;
        //                oppositeDisplayField = (withOppositeDisplayLangIfNull ? CrmDisplayFieldName : null);
        //                break;
        //            default:
        //                displayField = CrmDisplayFieldName;
        //                oppositeDisplayField = (withOppositeDisplayLangIfNull ? CrmDisplayFieldName_Arabic : null);
        //                break;
        //        }
        //        return globalmgr.GetQuickLookup(CrmEntityName, CrmGuidFieldName, displayField, oppositeDisplayField, filterWhereCondition);
        //    }
        //}

        //public virtual IEnumerable<string> GetRequiredFields()
        //{
        //    using (GlobalCrmRepository crmGlobalMgr = new GlobalCrmRepository())
        //    {
        //        return crmGlobalMgr.GetRequiredFieldsNamesForEntity(CrmEntityName).ToList();
        //    }
        //}

        //public virtual IEnumerable<BaseOptionSetVM> GetEntityStatusOptions(UserLanguage lang)
        //{
        //    using (var globalCrmManager = new GlobalCrmRepository())
        //    {
        //        return globalCrmManager.GetOptionSetLookup(CrmEntityName, StatusFieldName, lang);
        //    }
        //}




    }
}
