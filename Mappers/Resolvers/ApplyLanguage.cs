using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Defaults;
using Utilities.GlobalViewModels.Custom;

namespace Utilities.Mappers.Resolvers
{
    public class ApplyLanguage : IMemberValueResolver<object,object, MappingTranslation, string>
    {
        public ApplyLanguage()
        {

        }

        public string Resolve(object source, object destination, MappingTranslation sourceMember, string destMember, ResolutionContext context)
        {
            switch (sourceMember._lang)
            {
                case DefaultValues.RouteLang_ar:
                    {
                        return sourceMember._arabicValue ?? sourceMember._englishValue;
                    }
                case DefaultValues.RouteLang_en:
                    {
                        return sourceMember._englishValue ?? sourceMember._arabicValue;
                    }
                default:
                    {
                        return DefaultValues.DefaultLanguageRoute == DefaultValues.RouteLang_ar ? (sourceMember._arabicValue ?? sourceMember._englishValue) : (sourceMember._englishValue ?? sourceMember._arabicValue);
                    }

            }
            
        }
    }
}
