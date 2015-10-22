using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DingTalk.SDK.Utilitys
{
    public class LogUtility
    {
        private static readonly ILog log;
        private static readonly ILog logError;
        private static readonly ILog logInfo;

        static LogUtility()
        {
            log4net.Config.XmlConfigurator.Configure();
            log = log4net.LogManager.GetLogger("logdebug");
            logError = log4net.LogManager.GetLogger("logerror");
            logInfo = log4net.LogManager.GetLogger("loginfo");
        }
        public static void Debug(string content)
        {
            log.Debug(content);
        }
        public static void Info(string content)
        {
            logInfo.Info(content);
        }
        public static void Error(string content, Exception e)
        {
            logError.Error(content, e);
        }

        internal static void SendLog(string url, string returnText)
        {
            Debug("-----------------------\r\nRequest url:" + url + "\r\n" + returnText);
        }
    }
}
