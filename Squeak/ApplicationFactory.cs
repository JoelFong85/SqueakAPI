using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Squeak
{
    public class ApplicationFactory
    {
        private static ILog currentLogger;
        public static ILog CurrentLogger
        {
            get
            {
                if (currentLogger == null)
                {
                    //Generic instance created to pass to all classes to use. Otherwise, within each class, can use a reflection classes to log the app name.
                    ////currentLogger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    currentLogger = LogManager.GetLogger("JoelFongAPI");
                }
                return currentLogger;
            }
        }

        public static bool LogWithObject(string title, object logObject)
        {
            try
            {
                string jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(logObject);
                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(jsonObject);
                string[] parameterName = dict.Keys.ToArray();
                string[] parameterValue = dict.Values.ToArray();

                string logTxt = $@"{title} | Model - ";

                for (int i = 0; i < dict.Count; i++)
                {
                    logTxt += $@"{parameterName[i]} : {parameterValue[i]} | ";
                }

                CurrentLogger.Info(logTxt);

                return true;
            }
            catch (Exception ex)
            {
                CurrentLogger.Error($@"Logging with object failed: {ex.Message}");
                return false;
            }
        }

    }
}