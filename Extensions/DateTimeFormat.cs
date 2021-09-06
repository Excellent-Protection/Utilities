using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extensions
{
    public static class DateTimeFormat
    {
        public static string ChangeDateTimeFormat(this DateTime date, bool isEnglish)
        {
            //string format = "dddd, dd MMMM yyyy hh:mm tt";
            string format = "dddd, dd MMMM yyyy";

            return isEnglish == true
                    ? date.ToString(format, new CultureInfo("en-AU"))
                    : date.ToString(format, new CultureInfo("ar-AE"));
        }
    }
}
