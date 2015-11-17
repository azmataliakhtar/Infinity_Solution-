using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using log4net;

namespace INF.Web.UI.Logging.Log4Net
{
    public class Log4NetLogger : ILogger
    {

        private readonly ILog _logger;

        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(GetCallerType(1));
        }

        public bool IsDebugEnabled
        {
            get { return _logger.IsDebugEnabled; }
        }

        public void Info(string message)
        {
            _logger.Info(GetCallerMemberName(1) + " >> " + message);
        }

        public void Warn(string message)
        {
            _logger.Warn(GetCallerMemberName(1) + " >> " + message);
        }

        public void Debug(string message)
        {
            _logger.Debug(GetCallerMemberName(1) + " >> "+ message);
        }

        public void Error(string message)
        {
            _logger.Error(GetCallerMemberName(1) + " >> " + message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.Error(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(GetCallerMemberName(1) + " >> " + message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }

        private Type GetCallerType(int vSkipFrames)
        {
            var trace = new StackTrace(vSkipFrames + 1, false);
            var count = trace.FrameCount;

            for (var index = 0; index <= count; index++)
            {
                var frame = trace.GetFrame(index);
                var type = frame.GetMethod().DeclaringType;
                //if (type != typeof(BasePage))
                {
                    return type;
                }
            }
            return null;
        }

        private string GetCallerMemberName(int vSkipFrames)
        {
            try
            {
                var trace = new StackTrace(vSkipFrames + 1, false);
                var count = trace.FrameCount;

                for (var index = 0; index <= count; index++)
                {
                    var frame = trace.GetFrame(index);
                    return frame.GetMethod().Name;
                }
            }
            catch
            {
                return "[NotAvailable]";
            }
            
            return "[NotAvailable]";
        }
    }
}
