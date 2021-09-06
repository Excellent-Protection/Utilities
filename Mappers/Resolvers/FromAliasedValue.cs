using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Utilities.Mappers.Resolver
{
    class FromAliasedValue<T>  : IMemberValueResolver<Entity, object, AttributeCollection, T> 
    {
        string entity;
        string property;
       public FromAliasedValue(string entity,string property)
        {
            this.entity = entity;
            this.property = property;
        }
        public T Resolve(Entity source, object destination, AttributeCollection sourceMember, T destMember, ResolutionContext context)
        {
            if (sourceMember != null&&sourceMember.Contains(entity+"."+property))
            {
                return (T)((AliasedValue)sourceMember["new_loyalitylevels.new_amountrangeperyearforhourlycontract"]).Value;
            }
            return default(T);
        }

        
    }
 
}
