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
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;
using Utilities.GlobalViewModels.Custome;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.CRM
{
    public class ContactLocationManager : BaseManager, IDisposable
    {
        internal RequestUtility _requestUtility;
        ContactLocationRepository _repo;
        public ContactLocationManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _requestUtility = RequestUtility;
            _repo = new ContactLocationRepository();
        }
        public void Dispose()
        {
            _repo =new ContactLocationRepository();
        }
        public ResponseVm<ContactMainSubPreviouseLocationsVm> GetAllPrevLocationsByContactId(string contactId)
        {
            try
            {

                var PrevLocResult = _repo.GetContactPreviouseLocation(contactId);
                if (PrevLocResult != null)
                {
                    var PrevLocList = PrevLocResult.Select(a => a.ToEntity<ContactPreviousLocation>()).ToModelListData<SavedLocationVm>().ToList();

                    ContactMainSubPreviouseLocationsVm result = new ContactMainSubPreviouseLocationsVm()
                    {
                        MainLocations = PrevLocList.Where(a => a.Type == (int)ContactLocationType.Main).ToList(),
                        SubLocation = PrevLocList.Where(a => a.Type == (int)ContactLocationType.Sub).ToList()
                    };
                    return new ResponseVm<ContactMainSubPreviouseLocationsVm>() { Status = HttpStatusCodeEnum.Ok, Data = result };

                }
                return new ResponseVm<ContactMainSubPreviouseLocationsVm>() { Status = HttpStatusCodeEnum.Ok, Data = null };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ContactId", contactId));
                return new ResponseVm<ContactMainSubPreviouseLocationsVm>(){ Status = HttpStatusCodeEnum.IneternalServerError, Message = "Error" };

            }
        }

        public ResponseVm<List<SavedLocationVm>>  GetContactPreviousLocationByType(string contactId , int type)
        {
            try
            {
                var result = _repo.GetContactPreviouseLocationByType(contactId , type).Select(a => a.ToEntity<ContactPreviousLocation>()).ToModelListData<SavedLocationVm>().ToList(); ;
             
                return new ResponseVm<List<SavedLocationVm>>() { Status = HttpStatusCodeEnum.Ok, Data = result };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ContactId", contactId) , ("Type" , type));
                return new ResponseVm<List<SavedLocationVm>>() { Status = HttpStatusCodeEnum.IneternalServerError, Message = "Error" };

            }
        }



        public ResponseVm<ContactLocationVm> AddLocation(ContactLocationVm LocationVm)
        {
            try
            {
                ContactPreviousLocation Location = LocationVm.ToCrmEntity<ContactPreviousLocation, ContactLocationVm>();
                string oldMainLocationId = "";
                if(LocationVm.Type==(int)ContactLocationType.Main)
                {
                    oldMainLocationId = _repo.GetContactMainLocation(LocationVm.ContactId)?.Id.ToString();
                }
                var _service = CRMService.Get;

                Location.Id = Guid.NewGuid();
                LocationVm.LocationId = _service.Create(Location).ToString();

                if (LocationVm.LocationId!=null && oldMainLocationId!="")
                {
                    UpdateContactLocationsToBeSub(LocationVm.ContactId, oldMainLocationId);
                }
                return new ResponseVm<ContactLocationVm> { Status= HttpStatusCodeEnum.Ok , Data=LocationVm};
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("LocationVm", LocationVm));
                return new ResponseVm<ContactLocationVm>
                {
                    Status = HttpStatusCodeEnum.IneternalServerError,
                    Message =  "An Error Occurred...!" 
                };
            }
        }

        public ContactLocationVm GetLocationById(string locationId)
        {
            try
            {
              var location=  _repo.GetLocationById(locationId).Toclass<ContactLocationVm>();
                return location;
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("locationId", locationId));
                return null;
            }
        }
        public void UpdateContactLocationsToBeSub(string contactId , string oldMainLocationId)
        {
            try
            {

                var oldMainLocation = _repo.GetLocationById(oldMainLocationId);
                oldMainLocation.Type = new Microsoft.Xrm.Sdk.OptionSetValue();
                var _service = CRMService.Get;
                _service.Update(oldMainLocation);
            
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("contactId", contactId));
            }
        }

    }
}