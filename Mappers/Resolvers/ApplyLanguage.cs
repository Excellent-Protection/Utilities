using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;

namespace Utilities.Mappers.Resolvers
{
    public class ApplyLanguage : IValueResolver<object,object, string>
    {
        public string _lang { get; set; }
        public string _arabicValue { get; set; }
        public string _englishValue { get; set; }
        public ApplyLanguage()
        {

        }
        public ApplyLanguage(string Lang, string ArabicValue, string EnglishValue)
        {
            _lang = Lang;
            _arabicValue = ArabicValue;
            _englishValue = EnglishValue;
        }
        public string Resolve(object source, object destination, string destMember, ResolutionContext context)
        {
            switch (_lang)
            {
                case DefaultValues.RouteLang_ar:
                    {
                        return _arabicValue ?? _englishValue;
                    }
                case DefaultValues.RouteLang_en:
                    {
                        return _englishValue ?? _arabicValue;
                    }

            }
            return DefaultValues.DefaultLanguageRoute == DefaultValues.RouteLang_ar ? (_arabicValue ?? _englishValue) : (_englishValue ?? _arabicValue);
        }

     }
}
