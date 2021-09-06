using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Mappers.Resolver
{
    class FromIntToEnum<T> : IMemberValueResolver<object, object, int?, T> where T : Enum
    {
        public T Resolve(object source, object destination, int? sourceMember, T destMember, ResolutionContext context)
        {
            if (sourceMember.HasValue)
            {
                T res = (T)Enum.Parse(typeof(T), sourceMember.Value.ToString());
                return res;
            }

            return default(T);
        }
    }
}

