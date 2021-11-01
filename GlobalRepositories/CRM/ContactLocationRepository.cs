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
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;

namespace Utilities.GlobalRepositories.CRM
{
   public class ContactLocationRepository
    {

        public List< ContactPreviousLocation> GetContactPreviouseLocation(string ContactId)
        {
          
                QueryExpression PrevLocationQuery = new QueryExpression(CrmEntityNamesMapping.ContactPreviousLocation);
                PrevLocationQuery.Criteria.AddCondition("new_contact", ConditionOperator.Equal, ContactId);
                PrevLocationQuery.ColumnSet = new ColumnSet(true);
                PrevLocationQuery.AddLink(CrmEntityNamesMapping.City, "new_city", "new_cityid", JoinOperator.Inner);
                PrevLocationQuery.AddLink(CrmEntityNamesMapping.District, "new_district", "new_districtid", JoinOperator.Inner);
                PrevLocationQuery.LinkEntities[0].Columns = new ColumnSet("new_englsihname", "new_name", "new_forindividual" , "new_isdalal");
                PrevLocationQuery.LinkEntities[0].EntityAlias = CrmEntityNamesMapping.City;
                PrevLocationQuery.LinkEntities[1].Columns = new ColumnSet("new_englishname", "new_name");
                PrevLocationQuery.LinkEntities[1].EntityAlias = CrmEntityNamesMapping.District;
                PrevLocationQuery.Criteria.AddCondition("new_latitude", ConditionOperator.NotNull);
                PrevLocationQuery.Criteria.AddCondition("new_longitude", ConditionOperator.NotNull);
                var _service = CRMService.Service;
                var PrevLocResult = _service.RetrieveMultiple(PrevLocationQuery).Entities;

                var PrevLocList = PrevLocResult.Select(a => a.ToEntity<ContactPreviousLocation>()).ToList();

                return  PrevLocList ;


          
        }



        public List<ContactPreviousLocation> GetContactPreviouseLocationByType(string ContactId , int type)
        {
       
                QueryExpression PrevLocationQuery = new QueryExpression(CrmEntityNamesMapping.ContactPreviousLocation);
                PrevLocationQuery.Criteria.AddCondition("new_contact", ConditionOperator.Equal, ContactId);
                if (type!=0)
                {
                    PrevLocationQuery.Criteria.AddCondition("new_type", ConditionOperator.Equal, type);
                }
                PrevLocationQuery.ColumnSet = new ColumnSet(true);
                PrevLocationQuery.AddLink(CrmEntityNamesMapping.City, "new_city", "new_cityid", JoinOperator.Inner);
                PrevLocationQuery.AddLink(CrmEntityNamesMapping.District, "new_district", "new_districtid", JoinOperator.Inner);
                PrevLocationQuery.LinkEntities[0].Columns = new ColumnSet("new_englsihname", "new_forindividual", "new_isdalal");
                PrevLocationQuery.LinkEntities[0].EntityAlias = CrmEntityNamesMapping.City;
                PrevLocationQuery.LinkEntities[1].Columns = new ColumnSet("new_englishname");
                PrevLocationQuery.LinkEntities[1].EntityAlias = CrmEntityNamesMapping.District;
                PrevLocationQuery.Criteria.AddCondition("new_latitude", ConditionOperator.NotNull);
                PrevLocationQuery.Criteria.AddCondition("new_longitude", ConditionOperator.NotNull);
                var _service = CRMService.Service;
                var PrevLocResult = _service.RetrieveMultiple(PrevLocationQuery).Entities;

                var PrevLocList = PrevLocResult.Select(a => a.ToEntity<ContactPreviousLocation>()).ToList();

                return PrevLocList ;


       
        }


        public ContactPreviousLocation GetContactMainLocation(string contactId)
        {
            QueryExpression PrevLocationQuery = new QueryExpression(CrmEntityNamesMapping.ContactPreviousLocation);
            PrevLocationQuery.Criteria.AddCondition("new_contact", ConditionOperator.Equal, contactId);
            PrevLocationQuery.ColumnSet = new ColumnSet(true);
            PrevLocationQuery.Criteria.AddCondition("new_type", ConditionOperator.Equal, (int)ContactLocationType.Main);
            var _service = CRMService.Service;
            var mainLoaction = _service.RetrieveMultiple(PrevLocationQuery).Entities.Select(a => a.ToEntity<ContactPreviousLocation>()).FirstOrDefault();
            return mainLoaction;
        }
        public bool isAlreadyExist(ContactLocationVm location)
        {
            var _service = CRMService.Service;
            QueryExpression PrevLocationQuery = new QueryExpression(CrmEntityNamesMapping.ContactPreviousLocation);

            PrevLocationQuery.ColumnSet = new ColumnSet(true);

            PrevLocationQuery.Criteria.AddCondition("new_contact", ConditionOperator.Equal, location.ContactId);

            PrevLocationQuery.Criteria.AddCondition("new_city", ConditionOperator.Equal, location.CityId);
            PrevLocationQuery.Criteria.AddCondition("new_district", ConditionOperator.Equal, location.DistrictId);
            PrevLocationQuery.Criteria.AddCondition("new_latitude", ConditionOperator.Equal, location.Latitude);
            PrevLocationQuery.Criteria.AddCondition("new_longitude", ConditionOperator.Equal, location.Longitude);

            //PrevLocationQuery.Criteria.AddCondition("new_apartmentnumber", ConditionOperator.Equal, location.ApartmentNo);
            if (location.HouseType != null)
            {
                PrevLocationQuery.Criteria.AddCondition("new_housetype", ConditionOperator.Equal, location.HouseType.Value);
            }
            if (location.FloorNo != null)
            {
                PrevLocationQuery.Criteria.AddCondition("new_floornumber", ConditionOperator.Equal, location.FloorNo);
            }

            if (location.ApartmentNo != null)
            {
                PrevLocationQuery.Criteria.AddCondition("new_apartmentnumber", ConditionOperator.Equal, location.ApartmentNo.ToString());
            }
            if (location.HouseNo != null)
            {
                PrevLocationQuery.Criteria.AddCondition("new_housenumber", ConditionOperator.Equal, location.ApartmentNo.ToString());
            }
            // PrevLocationQuery.Criteria.AddCondition("new_type", ConditionOperator.Equal, location.Type.Value);
            var res= _service.RetrieveMultiple(PrevLocationQuery).Entities.Count() > 0;

            return res;

        }
        public ContactPreviousLocation GetLocationById(string locationId)
        {
            var _service = CRMService.Service;
            var location = _service.Retrieve(CrmEntityNamesMapping.ContactPreviousLocation, new Guid(locationId), new ColumnSet(true)).ToEntity<ContactPreviousLocation>();
            return location;
        }
        public ContactPreviousLocation GetContactId(string locationId)
        {
            var _service = CRMService.Service;
            var location = _service.Retrieve(CrmEntityNamesMapping.ContactPreviousLocation, new Guid(locationId), new ColumnSet("new_contact")).ToEntity<ContactPreviousLocation>();
            return location;
        }



    }
}
