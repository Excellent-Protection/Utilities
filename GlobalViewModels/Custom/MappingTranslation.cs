using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.GlobalViewModels.Custom
{
    public class MappingTranslation
    {
        public MappingTranslation(string Lang, string ArabicValue, string EnglishValue)
        {
            _lang = Lang;
            _arabicValue = ArabicValue;
            _englishValue = EnglishValue;
        }
        public string _lang { get; set; }
        public string _arabicValue { get; set; }
        public string _englishValue { get; set; }
    }
}
