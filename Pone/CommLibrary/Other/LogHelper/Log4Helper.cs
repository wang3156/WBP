using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommLibrary.Other.LogHelper
{
    /// <summary>
    /// 日志记录帮助类
    /// </summary>
    public class Log4Helper
    {
        static readonly ILog _InfoLogging;
        static readonly ILog _DebugLogging;
        static readonly ILog _WarnLogging;
        static readonly ILog _ErrorLogging;
        static readonly ILog _DbCommandLogging;
        static Log4Helper()
        {
            XmlConfigurator.Configure();
            _InfoLogging = LogManager.GetLogger("Info.Logging");
            _DebugLogging = LogManager.GetLogger("Debug.Logging");
            _WarnLogging = LogManager.GetLogger("Warn.Logging");
            _ErrorLogging = LogManager.GetLogger("Error.Logging");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            _InfoLogging.Info(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        ///  <param name="exception"></param>
        public static void Info(string message, Exception exception)
        {
            _InfoLogging.Info(message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(string message)
        {
            _DebugLogging.Debug(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        ///  <param name="exception"></param>
        public static void Debug(string message, Exception exception)
        {
            _DebugLogging.Debug(message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(string message)
        {
            _WarnLogging.Warn(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(string message, Exception exception)
        {
            _WarnLogging.Warn(message, exception);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>

        public static void Error(string message)
        {
            _ErrorLogging.Error(message);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        ///  <param name="exception"></param>
        public static void Error(string message, Exception exception)
        {
            _ErrorLogging.Error(message, exception);
        }

    }
}
