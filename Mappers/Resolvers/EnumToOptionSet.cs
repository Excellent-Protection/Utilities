using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Utilities.Mappers.Resolvers
{
    class FromEnumToOptionSet<T>  :  IMemberValueResolver<object, Entity, T, OptionSetValue>
    {
       public OptionSetValue Resolve(object source, Entity destination, T sourceMember, OptionSetValue destMember, ResolutionContext context)

        {
             if (sourceMember!=null)
            {
               var enumName = Enum.GetName(typeof(T), sourceMember);
               if (enumName != null)
                {
                    int enumValue = (int)Enum.Parse(typeof(T), enumName);
                    return new OptionSetValue(enumValue);
                }
             }
           return null;
        }
    }

}
