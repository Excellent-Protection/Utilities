using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalManagers.CRM;
using Utilities.Helpers;

namespace Utilities.Extensions
{
    class SetDoubleDigits :BaseManager
    {
        ExcSettingsManager _excSettingMngr;
        public SetDoubleDigits(RequestUtility requestUtility) : base(requestUtility)
        {
            _excSettingMngr = new ExcSettingsManager(RequestUtility);
        }

    public string setDigit(double value)
        {
            var res = int.Parse(_excSettingMngr.GetSettingByName("NumberofDigit").Value);

            var padding = new StringBuilder();

            for (int i = 0; i < res; i++)
            {
                padding.Append("0");
            }
            return string.Format("{0:0." + padding + "}", value);
        }
    }
}
