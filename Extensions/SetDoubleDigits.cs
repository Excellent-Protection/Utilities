using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.GlobalManagers.CRM;
using Utilities.Helpers;

namespace Utilities.Extensions
{
   public static class SetDoubleDigits 
    {
        //ExcSettingsManager _excSettingMngr;
        //public SetDoubleDigits(RequestUtility requestUtility) : base(requestUtility)
        //{
        //    _excSettingMngr = new ExcSettingsManager(RequestUtility);
        //}

    public static decimal SetDecimalDegits(decimal value)
        {
            //ExcSettingsManager _excSettingMngr= new ExcSettingsManager();
            //var res = int.Parse(_excSettingMngr.GetSettingByName("NumberofDigit").Value);

            return Math.Round(value, 4);

            //var padding = new StringBuilder();

            //for (int i = 0; i < res; i++)
            //{
            //    padding.Append("0");
            //}
            //return string.Format("{0:0." + padding + "}", value);
        }


        public static double SetDoubleDegits(double value)
        {
            //ExcSettingsManager _excSettingMngr= new ExcSettingsManager();
            //var res = int.Parse(_excSettingMngr.GetSettingByName("NumberofDigit").Value);

            return Math.Round(value, 4);

            //var padding = new StringBuilder();

            //for (int i = 0; i < res; i++)
            //{
            //    padding.Append("0");
            //}
            //return string.Format("{0:0." + padding + "}", value);
        }
    }
}
