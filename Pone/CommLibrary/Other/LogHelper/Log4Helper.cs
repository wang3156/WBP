using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
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
            _InfoLogging = LogManager.GetLogger("Info.Logging");
            _DebugLogging = LogManager.GetLogger("Debug.Logging");
            _WarnLogging = LogManager.GetLogger("Warn.Logging");
            _ErrorLogging = LogManager.GetLogger("Error.Logging");
            _DbCommandLogging = LogManager.GetLogger("DbCommandLogging.Logging");

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

    /// <summary>
    /// Log4帮助类,使用一个对象写数据
    /// </summary>
    public class Log4SingleHelper {
        private static readonly ILog logger;
        static Log4SingleHelper()
        {             
            var repository = LogManager.CreateRepository("NETCoreRepository");
            // .net core需要添加这个不然日志写到控制台的会报错. 
            //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            XmlConfigurator.Configure(repository, new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"config{Path.DirectorySeparatorChar}log4net.config")));
            logger = LogManager.GetLogger(repository.Name, "Ii");
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Info(message);
            else
                logger.Info(message, exception);
        }

        /// <summary>
        /// 告警日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Warn(message);
            else
                logger.Warn(message, exception);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Error(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Error(message);
            else
                logger.Error(message, exception);
        }
    }
}
