using AutoMapper;
using Microsoft.Xrm.Sdk;
using Models.CRM.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.Extensions;
using Utilities.GlobalViewModels.CRM;

namespace Utilities.Mappers.Profiles
{
   public class Collection_CollectionVmProfiles :Profile
    {
        public Collection_CollectionVmProfiles()
        {
            CreateMap<CollectionVm, Collections>()
                //from Collection vm to coleection 
                .ForMember(a => a.CollectionId, opt => opt.MapFrom(s => !String.IsNullOrEmpty(s.CollectionId) ? new Guid(s.CollectionId) : (Guid?)null))
                .ForMember(a => a.PointNotesAr, opt => opt.MapFrom(s => s.PointNotes ?? s.PointNotesEN))
                .ForMember(a => a.PointNotesEn, opt => opt.MapFrom(s => s.PointNotesEN ?? s.PointNotes))

                //entityreference
                .ForMember(a => a.IndividualProcedure, opt => opt.MapFrom(s => !String.IsNullOrEmpty(s.IndividualProcedureId) ? new EntityReference(CrmEntityNamesMapping.IndividualContractProcedure, new Guid(s.IndividualProcedureId)) : null))
                .ForMember(a => a.HourlyContract, opt => opt.MapFrom(s => !String.IsNullOrEmpty(s.HourlyContractId) ? new EntityReference(CrmEntityNamesMapping.ServiceContractPerHour, new Guid(s.HourlyContractId)) : null))
                .ForMember(a => a.IndividualContractRequest, opt => opt.MapFrom(s => !String.IsNullOrEmpty(s.IndividualContractRequestId) ? new EntityReference(CrmEntityNamesMapping.IndividualContractRequest, new Guid(s.IndividualContractRequestId)) : null))
                .ForMember(a => a.Contact, opt => opt.MapFrom(s => !String.IsNullOrEmpty(s.ContactId) ? new EntityReference(CrmEntityNamesMapping.Contact, new Guid(s.ContactId)) : null))

               //optionset
               .ForMember(a => a.PaymentMethod, opt => opt.MapFrom(s => s.PaymentMethod.HasValue ? new OptionSetValue(s.PaymentMethod.Value) : null))
               .ForMember(a => a.PaymentType, opt => opt.MapFrom(s => s.PaymentType.HasValue ? new OptionSetValue(s.PaymentType.Value) : null))
               .ForMember(a => a.IsWalletBalance, opt => opt.MapFrom(s => s.IsWalletBalance.HasValue ? new OptionSetValue(s.IsWalletBalance.Value) : null))

               //ignore
               .IgnoreAllPropertiesWithAnInaccessibleSetter()

               .ReverseMap()//from collection to collectionvm

               .ForMember(a => a.CollectionId, opt => opt.MapFrom(s => s.CollectionId != null ? s.CollectionId.ToString() : null))

                //from entityreference
                .ForMember(a => a.IndividualProcedureId, opt => opt.MapFrom(s => s.IndividualProcedure != null ? s.IndividualProcedure.Id.ToString() : null))
                .ForMember(a => a.HourlyContractId, opt => opt.MapFrom(s => s.HourlyContract != null ? s.HourlyContract.Id.ToString() : null))
                .ForMember(a => a.IndividualContractRequestId, opt => opt.MapFrom(s => s.IndividualContractRequest != null ? s.IndividualContractRequest.Id.ToString() : null))
                .ForMember(a => a.PaymentType, opt => opt.MapFrom(s => s.PaymentType != null ? s.PaymentType.Value : (int?)null))
                .ForMember(a => a.PaymentMethod, opt => opt.MapFrom(s => s.PaymentMethod != null ? s.PaymentMethod.Value : (int?)null))
                .ForMember(a => a.CreatedOnString,
                    opt => opt.MapFrom(s =>
                        s.CreatedOn.HasValue
                                ? s.CreatedOn.Value.AddHours(3).ChangeDateTimeFormat(false)
                                : null))
                .ForMember(s => s.PointNotes,
                    opt => opt.MapFrom(a => 1<2? a.PointNotesEn ?? a.PointNotesAr : a.PointNotesAr ?? a.PointNotesEn))
                ;
        }
    }
}
