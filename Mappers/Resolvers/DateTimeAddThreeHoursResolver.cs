using AutoMapper;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Extensions;

namespace Utilities.Mappers.Resolvers
{
    public class DateTimeAddThreeHoursResolver : IMemberValueResolver<Entity, object, DateTime?, DateTime?>
    {

        public DateTime? Resolve(Entity source, object destination, DateTime? sourceMember, DateTime? destinationMember, ResolutionContext context)
        {
            return sourceMember.Add3Hours();
        }

    }
}
