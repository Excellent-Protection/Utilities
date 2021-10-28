using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utilities.Mappers.Resolvers
{
    public class EntityReferenceNameToStringResolver : IMemberValueResolver<Entity, object, EntityReference, string>
    {
        public string Resolve(Entity source, object destination, EntityReference sourceMember, string destMember, ResolutionContext context)
        {
            return sourceMember != null ? sourceMember.Name : null;
        }
    }

}
