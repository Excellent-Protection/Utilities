using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappers.Resolvers
{
    class StringToEntityReferanceMapper : IMemberValueResolver<object, Entity, string, EntityReference>
    {
        public EntityReference Resolve(object source, Entity destination, string sourceMember, EntityReference destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return new EntityReference(destination.LogicalName, new Guid(sourceMember));
            }
            return null;
        }

    }
}
