using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.DataAccess.CRM;
using Utilities.Defaults;
using Utilities.GlobalRepositories.CRM;
using Utilities.GlobalViewModels;
using Utilities.GlobalViewModels.Custom;
using Utilities.GlobalViewModels.CRM;
using Utilities.Helpers;
using Utilities.Mappers;

namespace Utilities.GlobalManagers.CRM
{
   public class ContactManager :BaseManager , IDisposable
    {
        ContactRepository _repo;
        public ContactManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _repo = new ContactRepository(RequestUtility);
        }

        public void Dispose()
        {
        }

        public  bool IsProfileCompleted(string contactId)
        {
            try
            {
                using (var _globalRep = new GlobalCrmRepository())
                {
                    bool isCompleted = _globalRep.GetEmptyFieldsNamesForRecord(CrmEntityNamesMapping.Contact, "contactid", contactId, new[] { "new_gender", "new_idnumer", "new_contactcity", "new_contactnationality" }).Count() == 0;
                    return isCompleted ;
                }
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ContactId", contactId));

                return false;
            }
        }

        public ResponseVm<ContactDetailsVm> GetContactDetails(string contactId)
        {
            try
            {
                var contactDetails = _repo.GetContactDetails(contactId).Toclass<ContactDetailsVm>();
                return new ResponseVm<ContactDetailsVm> { Status = HttpStatusCodeEnum.Ok, Data = contactDetails };

            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ContactId", contactId));

                return new ResponseVm<ContactDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurred" };
            }
        }



        public  bool CompleteProfile(ContactDetailsVm contact)
        {
            try
            {
                var contactModel = contact.ToCrmEntity<Contact, ContactDetailsVm>();
                var _service = CRMService.Get;
                _service.Update(contactModel);
                return true;
            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("Contact", contact));

                return false;
            }
        }


        public bool IsIdentiefierExist(string id, string IdNo)
        {
            try
            {
                return _repo.IsIdentiefierExist(id, IdNo);
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return false;
        }


        public ContactVm GetContactNameAndNationality(string contactId)
        {
            try
            {
                return _repo.GetContactNameAndNationality(contactId).Toclass<ContactVm>();

            }
            catch(Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }



    }
}
