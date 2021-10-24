using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM;
using Models.CRM.Individual_Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalViewModels.CRM;

namespace Utilities.Mappers.Profiles
{
  public  class RecieptVoucherCrm_RecieptVoucherVmProfile :Profile
    {
        public RecieptVoucherCrm_RecieptVoucherVmProfile()
        {
          
                CreateMap<RecieptVoucherVm, ReceiptVoucherCRM>()

                     //from recieptvouchervm to recieptvoucher
                     .ForMember(a => a.ReceiptDate, opt => opt.MapFrom(s => s.ReceiptDate != null ? s.ReceiptDate.Value.Date : DateTime.Now.Date))
                     .ForMember(a => a.CheckNumber, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.CheckNumber) ? s.CheckNumber : null))
                     .ForMember(a => a.PaymentNote, opt => opt.MapFrom(s => s.PaymentNote != null ? s.PaymentNote : null))

                     //monry
                     .ForMember(a => a.VatAmount, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.VatAmount) ? new Money(decimal.Parse(s.VatAmount)) : null))
                     .ForMember(a => a.Amount, opt => opt.MapFrom(s => s.Amount.HasValue ? new Money(s.Amount.Value) : null))
                     .ForMember(a => a.FinalPrice, opt => opt.MapFrom(s => s.FinalPrice.HasValue ? new Money(s.FinalPrice.Value) : null))
                     //entityreference
                     .ForMember(a => a.ContractId, opt => opt.MapFrom(s => s.ContractId != null ? new EntityReference(CrmEntityNamesMapping.ServiceContractPerHour, new Guid(s.ContractId)) : null))
                     .ForMember(a => a.OwnerId, opt => opt.MapFrom(s => s.OwnerId != null ? new EntityReference(CrmEntityNamesMapping.SystemUser, new Guid(s.OwnerId)) : null))
                     .ForMember(a => a.FlexContractId, opt => opt.MapFrom(s => s.FlexContractId != null ? new EntityReference(FlexibleServicePerHour.EntityLogicalName, new Guid(s.FlexContractId)) : null))
                     .ForMember(a => a.IndividualContractRequestId, opt => opt.MapFrom(s => s.IndividualContractRequestId != null ? new EntityReference(IndividualContractRequest.EntityLogicalName, new Guid(s.IndividualContractRequestId)) : null))
                     .ForMember(a => a.IndividualContractId, opt => opt.MapFrom(s => s.IndividualContractId != null ? new EntityReference(CrmEntityNamesMapping.IndividualContract, new Guid(s.IndividualContractId)) : null))
                     .ForMember(a => a.ContactId, opt => opt.MapFrom(s => s.ContactId != null ? new EntityReference(Contact.EntityLogicalName, new Guid(s.ContactId)) : null))
                     .ForMember(a => a.IndividualContractProcedure, opt => opt.MapFrom(s => s.IndividualContractProcedureId != null ? new EntityReference(IndividualContractProcedure.EntityLogicalName, new Guid(s.IndividualContractProcedureId)) : null))
                     .ForMember(a => a.AccountNumber, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.AccountNumber) ? new EntityReference(CrmEntityNamesMapping.ExpenseAccount, new Guid(s.AccountNumber)) : null))
                     .ForMember(a => a.FinancialRequestId, opt => opt.MapFrom(s => s.FinancialRequestId != null ? new EntityReference(FinancialRequest.EntityLogicalName, new Guid(s.FinancialRequestId)) : null))
                    .ForMember(a => a.VatGroup, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.VatGroupId) ? new EntityReference(CrmEntityNamesMapping.VatGroup, new Guid(s.VatGroupId)) : null))

                     //optionset
                     .ForMember(a => a.Source, opt => opt.MapFrom(s => s.Source != null ? new OptionSetValue((int)s.Source) : null))
                     .ForMember(a => a.PaymentType, opt => opt.MapFrom(s => s.PaymentType != null ? new OptionSetValue((int)s.PaymentType) : null))
                     .ForMember(a => a.PaymentPosting, opt => opt.MapFrom(s => s.PaymentPosting != null ? new OptionSetValue((int)s.PaymentPosting) : null))
                     .ForMember(a => a.CollectionCreated, opt => opt.MapFrom(s => s.CollectionCreated.HasValue ? new OptionSetValue(s.CollectionCreated.Value) : null))
                    //ignore
                    .IgnoreAllPropertiesWithAnInaccessibleSetter()

                    .ReverseMap() //from recieptvoucher to recieptvouchervm
                     .ForMember(a => a.VoucherNumber, opt => opt.MapFrom(s => s.VoucherNumber))
                     .ForMember(a => a.CheckNumber, opt => opt.MapFrom(s => !string.IsNullOrEmpty(s.CheckNumber) ? s.CheckNumber : null))
                     .ForMember(a => a.PaymentNote, opt => opt.MapFrom(s => s.PaymentNote))

                     //from monry
                     .ForMember(a => a.Amount, opt => opt.MapFrom(s => s.Amount != null ? s.Amount.Value : (decimal?)null))
                     .ForMember(a => a.VatAmount, opt => opt.MapFrom(s => s.VatAmount != null ? s.VatAmount.Value.ToString() : null))
                     .ForMember(a => a.FinalPrice, opt => opt.MapFrom(s => s.FinalPrice != null ? s.FinalPrice.Value : (decimal?)null))

                     //from entityreference
                     .ForMember(a => a.ContactId, opt => opt.MapFrom(s => s.ContactId != null ? s.ContactId.Id.ToString() : null))
                     .ForMember(a => a.OwnerId, opt => opt.MapFrom(s => s.OwnerId != null ? s.OwnerId.Id.ToString() : null))
                     .ForMember(a => a.FlexContractId, opt => opt.MapFrom(s => s.FlexContractId != null ? s.FlexContractId.Id.ToString() : null))
                     .ForMember(a => a.IndividualContractProcedureId, opt => opt.MapFrom(s => s.IndividualContractProcedure != null ? s.IndividualContractProcedure.Id : (Guid?)null))
                     .ForMember(a => a.IndividualContractProcedureName, opt => opt.MapFrom(s => s.IndividualContractProcedure != null ? s.IndividualContractProcedure.Name : null))
                     .ForMember(a => a.ContractId, opt => opt.MapFrom(s => s.ContractId != null ? s.ContractId.Id.ToString() : null))
                     .ForMember(a => a.AccountNumber, opt => opt.MapFrom(s => s.AccountNumber != null ? s.AccountNumber.Id.ToString() : null))
                     .ForMember(a => a.VatGroupId, opt => opt.MapFrom(s => s.VatGroup != null ? s.VatGroup.Id.ToString() : null))
                     //from optionset
                     .ForMember(a => a.Source, opt => opt.MapFrom(s => s.Source.Value))
                     .ForMember(a => a.PaymentType, opt => opt.MapFrom(s => s.PaymentType != null ? s.PaymentType.Value : (int?)null))
                     .ForMember(a => a.PaymentPosting, opt => opt.MapFrom(s => s.PaymentPosting != null ? s.PaymentPosting.Value : (int?)null))
                     .ForMember(a => a.CollectionCreated, opt => opt.MapFrom(s => s.CollectionCreated != null ? s.CollectionCreated.Value : (int?)null))
                     ;

            
        }
    }
}
