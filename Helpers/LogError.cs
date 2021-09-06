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
    public class LogError
    {
        public static void Error(Exception ex, string MethodName, params (string Name, object Value)[] parameters)
        {
            if (MethodName == null)
            {
                MethodName = new StackTrace(ex).GetFrame(0).GetMethod().Name;
            }
            string message = "";
            if (ex != null)
            {
                message = "------------------Error----------------------";
                message += Environment.NewLine;
                message += string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                if (ex.InnerException != null)
                {
                    message += string.Format("Inner Exception: {0}", ex.InnerException);
                    message += Environment.NewLine;
                }
                message += string.Format("Location: {0}", HttpContext.Current.Request.Url.LocalPath);
                message += Environment.NewLine;
                message += string.Format("Method: {0}", MethodName);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                message += string.Format("Target Site: {0}", ex.TargetSite);
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
            }
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
            var path = HttpContext.Current.Server.MapPath("~/ErrorLog");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

             path = path+"/ErrorLog-"+DateTime.Now.ToString("dd-MM-yyyy")+".txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
    }
}
