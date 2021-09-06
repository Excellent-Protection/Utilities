using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappers.Resolver
{
    class FromOptionSetToInt : IMemberValueResolver<Entity, object, OptionSetValue, int?>
    {
        public int? Resolve(Entity source, object destination, OptionSetValue sourceMember, int? destMember, ResolutionContext context)
        {
            if (sourceMember != null)
            {
                return sourceMember.Value;
            }
            return null;
        }
    }
}
