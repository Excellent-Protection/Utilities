using AutoMapper;
using Models.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalViewModels.CRM;
using Utilities.Mappers.Resolvers;

namespace Utilities.Mappers.Profiles
{
   public class ContactProcedureLogger_ContactProcedureLoggerVmProfile :Profile
    {
        public ContactProcedureLogger_ContactProcedureLoggerVmProfile()
        {
            CreateMap<ContactProcedureLogger, ContactProcedureLoggerVm>()
            .ForMember(a => a.PostpondDate, o => o.MapFrom(s => s.PostpondDate.Value.AddHours(3)))
           .ForMember(a => a.Contract, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.HourlyContractId))
           .ForMember(a => a.Visit, o => o.ResolveUsing(new EntityReferenceIdToStringResolver(), s => s.Visit))
          .ForMember(a => a.Proceduretype, opt => opt.MapFrom(s => s.Proceduretype != null ? s.Proceduretype.Value : (int?)null))

            ;
        }
    }
}
