using Bellatrix.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bellatrix
{
    public static class Logger
    {
        private static readonly ILogger s_logger;

        static Logger()
        {
            var loggingSettings = ConfigurationService.GetSection<LoggingSettings>();
            var loggerConfiguration = new LoggerConfiguration();
            if (loggingSettings != null && loggingSettings.IsEnabled)
            {
                if (loggingSettings.IsConsoleLoggingEnabled)
                {
                    loggerConfiguration.WriteTo.Console(outputTemplate: loggingSettings.OutputTemplate);
                }

                if (loggingSettings.IsDebugLoggingEnabled)
                {
                    loggerConfiguration.WriteTo.Debug(outputTemplate: loggingSettings.OutputTemplate);
                }

                if (loggingSettings.IsFileLoggingEnabled)
                {
                    loggerConfiguration.WriteTo.File("bellatrix-log.txt", rollingInterval: RollingInterval.Day, outputTemplate: loggingSettings.OutputTemplate);
                }
            }

            s_logger = loggerConfiguration.CreateLogger();
        }

        public static void LogInformation(string message, params object[] args)
        {
            s_logger.Information(message, args);
        }

        public static void LogError(string message, params object[] args)
        {
            s_logger.Error(message, args);
        }

        public static void LogWarning(string message, params object[] args)
        {
            s_logger.Warning(message, args);
        }
    }
}
