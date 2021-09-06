using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utilities.Mappers.Resolver
{
    class EntityReferanceToStringName : IMemberValueResolver<Entity, object, EntityReference, string>
    {


        public string Resolve(Entity source, object destination, EntityReference sourceMember, string destMember, ResolutionContext context)
        {
            if (sourceMember==null)
            {
                return sourceMember.Name;

            }
            
            return null;
        }
    }
    
}
