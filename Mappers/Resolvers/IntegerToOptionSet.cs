using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappers.Resolver
{
    class FromIntegerToOptionSet : IMemberValueResolver<object, Entity, int?, OptionSetValue>
    {
        public OptionSetValue Resolve(object source, Entity destination, int? sourceMember, OptionSetValue destMember, ResolutionContext context)
            {

                if (sourceMember.HasValue)
                {
                    return new OptionSetValue(sourceMember.Value);
                }
            return null;
           }
    
 
    }
    

}
