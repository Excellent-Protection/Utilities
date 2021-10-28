using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extensions
{
    public static class DateTimeExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.

        public static DateTime? Add3Hours(this DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.Add3Hours();
            }
            return (DateTime?)null;
        }

        public static DateTime Add3Hours(this DateTime date)
        {
            return date.Kind == DateTimeKind.Utc ? date.AddHours(3) : date;
        }
    }
}
