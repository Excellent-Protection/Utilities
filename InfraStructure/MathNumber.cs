using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.InfraStructure
{
    public class MathNumber
    {

        public static string RoundDeciaml(string Value)
        {

            return Math.Round(Decimal.Parse(Value), 2, MidpointRounding.AwayFromZero).ToString().Replace(".00", "");

        }
        public static string RoundDeciamlToInt(string Value)
        {

            return Math.Round(Decimal.Parse(Value), 0, MidpointRounding.AwayFromZero).ToString().Replace(".00", "");

        }

    }
}
