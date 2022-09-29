using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utilities.Helpers
{
    public class LogUpdateShift
    {
        public static void Log(string MethodName, params (string Name, object Value)[] parameters)
        {
            try
            {
                string message = "";
                message = "------------------Error----------------------";
                message += Environment.NewLine;
                message += string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Location: {0}", HttpContext.Current.Request.Url.LocalPath);
                message += Environment.NewLine;
                message += string.Format("Method: {0}", MethodName);
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;

                if (parameters.Length > 0)
                {
                    message += "---------------Parameters--------------------";
                    message += Environment.NewLine;
                    for (var i = 0; i < parameters.Length; i++)
                    {
                        message += string.Format("{1}: {0}", JsonConvert.SerializeObject(parameters[i].Value), parameters[i].Name);
                        message += Environment.NewLine;
                    }
                    message += "-----------------------------------------------------------";
                    message += Environment.NewLine;
                }
                var path = HttpContext.Current.Server.MapPath("~/UpdateShiftLog");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = path + "/UpdateShiftLog-" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                LogError.Error(ex, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
    }
}
