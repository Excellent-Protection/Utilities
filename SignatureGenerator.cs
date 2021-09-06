using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class SignatureGenerator
    {

        /*
                string url = Request.Url.AbsoluteUri.Replace("&Sign", "*").Split('*')[0];
                url = Uri.UnescapeDataString(url);
                s = url;
         
         */

        public static string GetSignature(string url, long key)
        {
            int x = url.Split('/').Count();
            int y = url.Split('?').Count();

            var portion1 = 2 * key;
            portion1 = portion1 + x;
            portion1 = portion1 + y;
            portion1 = portion1 + 8;

            var portion2 = key + x;
            portion2 = portion2 - y;
            portion2 = portion2 - 2;

            var portion3 = portion1 + portion2;
            portion3 = portion3 + 5;

            var portion4 = x + y;

            var sign = portion3 + portion4;

            //var sign = (((((((2 * key * x) + y) + 8) * (((key + x) - y) - 2)) * key) + 5) * (x + y));
            return sign.ToString();
        }


    }
}
