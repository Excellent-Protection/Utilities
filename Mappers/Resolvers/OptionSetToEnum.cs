using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utilities.Mappers.Resolver
{
    class FromOptionSetToEnum <T>: IMemberValueResolver<Entity, object, OptionSetValue, T> where T:Enum
    {
        public T Resolve(Entity source, object destination, OptionSetValue sourceMember, T destMember, ResolutionContext context)
        {
           if (sourceMember!= null)
            {
                T res = (T)Enum.Parse(typeof(T), sourceMember.Value.ToString());
                return res;
            }
 
            return default(T);
        }
    }
}
