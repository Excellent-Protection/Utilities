using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Enums;

namespace Utilities.Helpers
{
    public abstract class BaseManager
    {
        public RequestUtility RequestUtility { get; set; }
        public BaseManager(RequestUtility requestUtility)
        {
            RequestUtility = requestUtility;
        }

        public int GenerateRandomNumber()
        {
            var rnd = new Random();
            return rnd.Next(10000, 99999);
        }
        public string GetEncodedCode(string input)
        {
            var encodedSignature = GenerateEncodedSignature.EncodedSignature.GetEncodedSignature(input);
            return encodedSignature;
        }
    }
}
