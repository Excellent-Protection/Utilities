﻿using Models.CRM;
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
    public class ContactManager : BaseManager, IDisposable
    {
        ContactRepository _repo;
        public ContactManager(RequestUtility requestUtility) : base(requestUtility)
        {
            _repo = new ContactRepository(RequestUtility);
        }

        public void Dispose()
        {
        }


        public ContactVm RegisterContactInPortal(ContactVm contact)
        {
            var crmEntity = _repo.GetContactByPhone(contact.MobilePhone).Toclass<ContactVm>();
            if (crmEntity == null)
                crmEntity = AddNewContactToCrm(contact);
            else
                UpdateCrmEntityFromRegisteration(contact, crmEntity.Id.ToString());

            if (crmEntity == null || crmEntity.Id == null)
                return null;

            contact.Id = crmEntity.Id;
            return contact;
        }

        public ContactVm AddNewContactToCrm(ContactVm contactVm)
        {
            try
            {
                var _service = CRMService.Service;
                var nameSegments = contactVm.FullName.Trim().Split(' ');
                var name = contactVm.FullName.Trim();
                contactVm.FName = string.Join(" ", nameSegments.Take(nameSegments.Length - 1));
                contactVm.LastName = nameSegments[nameSegments.Length - 1];
                contactVm.FullName = name;
                //contact.PlatformSource =new OptionSetValue ((int)RequestUtility.Source);
                contactVm.Id = Guid.NewGuid();
                var contact = contactVm.ToCrmEntity<Contact, ContactVm>();
                contactVm.Id = _service.Create(contact);
                return contactVm;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            return null;
        }
        public ContactVm UpdateCrmEntityFromRegisteration(ContactVm newContact, string oldContactId)
        {
            try
            {

                newContact.Id = new Guid(oldContactId);
                var service = CRMService.Service;
                service.Update(newContact.ToCrmEntity<Contact, ContactVm>());
                return newContact;

            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("newContact", newContact), ("oldContactId", oldContactId));
                return null;
            }
        }

        public bool IsProfileCompleted(string contactId)
        {
            try
            {
                using (var _globalRep = new GlobalCrmRepository())
                {
                    bool isCompleted = _globalRep.GetEmptyFieldsNamesForRecord(CrmEntityNamesMapping.Contact, "contactid", contactId, new[] { "new_gender", "new_idnumer", "new_contactcity", "new_contactnationality" }).Count() == 0;
                    return isCompleted;
                }
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("ContactId", contactId));

                return new ResponseVm<ContactDetailsVm> { Status = HttpStatusCodeEnum.IneternalServerError, Message = "An Error Occurred" };
            }
        }



        public bool CompleteProfile(ContactDetailsVm contact)
        {
            try
            {
                var contactModel = contact.ToCrmEntity<Contact, ContactDetailsVm>();
                var _service = CRMService.Service;
                _service.Update(contactModel);
                return true;
            }
            catch (Exception ex)
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


        public bool IsSaudi(string contactId)
        {
            try
            {
                var nationalityId = _repo.GetContactNationality(contactId).NationalityId.Id.ToString();
                return nationalityId == DefaultValues.SaudiNationalityId ? true : false;
            }
            catch (Exception ex)
            {

                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return false;
        }




        public ContactVm GetContactByPhone(string mobilePhone)
        {
            try
            {
                var contact = _repo.GetContactByPhone(mobilePhone).Toclass<ContactVm>();
                return contact;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name, ("MobilePhone", mobilePhone));
            }
            return null;
        }

        public bool isSaudi(string contactId)
        {
            try
            {
                var nationalityId = _repo.GetContactNationality(contactId).NationalityId.Id.ToString();
                return nationalityId == DefaultValues.SaudiNationalityId ? true : false;
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);

            }
            return false;
        }

    }

}


