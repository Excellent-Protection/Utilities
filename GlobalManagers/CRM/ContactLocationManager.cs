using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.Enums;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.CRM;
using Utilities.GlobalViewModels.Custom;
using Utilities.Helpers;
using Utilities.Mappers;
using Westwind.Globalization;

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
                        MainLocations = PrevLocList.Where(a => a.Type == (int)ContactLocationType.Main).FirstOrDefault(),
                        SubLocation = PrevLocList.Where(a => a.Type == (int)ContactLocationType.Sub).ToList()
                    };
                    return new ResponseVm<ContactMainSubPreviouseLocationsVm>() { Status = HttpStatusCodeEnum.Ok, Data = result };

                }
                return new ResponseVm<ContactMainSubPreviouseLocationsVm>() { Status = HttpStatusCodeEnum.Ok, Data = null };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ContactId", contactId));
                return new ResponseVm<ContactMainSubPreviouseLocationsVm>(){ Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };

            }
        }

        public ResponseVm<List<SavedLocationVm>>  GetContactPreviousLocationByType(string contactId , int type)
        {
            try
            {
                var result = _repo.GetContactPreviouseLocationByType(contactId, type).Select(a => a.ToEntity<ContactPreviousLocation>()).ToModelListData<SavedLocationVm>();

                if (type== (int)ContactLocationType.Main)
                {
                    return new ResponseVm<List<SavedLocationVm>>() { Status = HttpStatusCodeEnum.Ok, Data = new List<SavedLocationVm>() {result.FirstOrDefault()} };
                }

                return new ResponseVm<List<SavedLocationVm>>() { Status = HttpStatusCodeEnum.Ok, Data = result.ToList() };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ContactId", contactId) , ("Type" , type));
                return new ResponseVm<List<SavedLocationVm>>() { Status = HttpStatusCodeEnum.IneternalServerError, Message = DbRes.T("AnerrorOccurred", "Shared") };

            }
        }



        public ResponseVm<ContactLocationVm> AddLocation(ContactLocationVm LocationVm)
        {
            try
            {
                ContactPreviousLocation Location = LocationVm.ToCrmEntity<ContactPreviousLocation, ContactLocationVm>();
               var isExist=   _repo.isAlreadyExist(Location);
                if (isExist)
                {
                    return new ResponseVm<ContactLocationVm> { Status = HttpStatusCodeEnum.Ambiguous, Data = LocationVm };

                }

                string oldMainLocationId = "";
                if(LocationVm.Type==(int)ContactLocationType.Main)
                {
                    oldMainLocationId = _repo.GetContactMainLocation(LocationVm.ContactId)?.Id.ToString();
                }
                var _service = CRMService.Service;

                Location.Id = Guid.NewGuid();
                LocationVm.LocationId = _service.Create(Location).ToString();

                if (LocationVm.LocationId!=null && oldMainLocationId!="" && oldMainLocationId!=null)
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
                    Message = DbRes.T("AnerrorOccurred", "Shared")
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

                //var oldMainLocation = _repo.GetLocationById(oldMainLocationId);
                //oldMainLocation.Type = new Microsoft.Xrm.Sdk.OptionSetValue((int)ContactLocationType.Sub);
                var _service = CRMService.Service;
                Entity address = new Entity (CrmEntityNamesMapping.ContactPreviousLocation);
                address.Id =new Guid( oldMainLocationId);
                address["new_type"]= new Microsoft.Xrm.Sdk.OptionSetValue((int)ContactLocationType.Sub);
                _service.Update(address);
            
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("contactId", contactId));
            }
        }
        public ResponseVm<string> RemoveAddress(string locationId)
        {
            try
            {
                var address = _repo.GetLocationById(locationId);
                if(address.Type.Value == (int)ContactLocationType.Main)
                {
                    return new ResponseVm<string> { Status = HttpStatusCodeEnum.NotAllowed,Message= DbRes.T("DeleteMainAddressNotAllowed", "IndividualResource") };
                 }
                var _service = CRMService.Service;
                _service.Delete(address.LogicalName,new Guid(locationId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.Ok  , Data=DbRes.T("LocationDeletedSuccessfully","Shared")};

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("locationId",locationId));
                return new ResponseVm<string> { Status = HttpStatusCodeEnum.IneternalServerError , Message= DbRes.T("AnerrorOccurred", "Shared") };

            }
        }
        public ResponseVm<SavedLocationVm> SetMainAddress(string locationId)
        {
            try
            {
                var loaction = _repo.GetContactId(locationId);
                var mainContactLocations=_repo.GetContactPreviouseLocationByType(loaction.Contact.Id.ToString(), (int)ContactLocationType.Main);
                var _service = CRMService.Service;

                using (TransactionScope transaction = new TransactionScope())
                {
                    Entity address = new Entity(CrmEntityNamesMapping.ContactPreviousLocation);
                    address.Id = new Guid(locationId);
                    address["new_type"] =new Microsoft.Xrm.Sdk.OptionSetValue((int)ContactLocationType.Main);
                    _service.Update(address);
                    if (mainContactLocations.Count()>0)
                    {
                        foreach (var item in mainContactLocations)
                        {
                            Entity mainAddress = new Entity(CrmEntityNamesMapping.ContactPreviousLocation);
                            mainAddress.Id = item.Id;
                            mainAddress["new_type"] = new Microsoft.Xrm.Sdk.OptionSetValue((int)ContactLocationType.Sub);
                            _service.Update(mainAddress);

                        }
                    }

                    transaction.Complete();
                };

                return new ResponseVm<SavedLocationVm> { Status = HttpStatusCodeEnum.Ok };

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("locationId", locationId));
                return new ResponseVm<SavedLocationVm> { Status = HttpStatusCodeEnum.IneternalServerError };

            }
        }



    }
}
