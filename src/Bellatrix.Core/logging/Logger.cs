using System;
using System.Collections.Generic;
using Serilog;
using System.Threading;
using ReportPortal.Serilog;
using Bellatrix.Core.logging;

namespace Bellatrix;

public static class Logger
{
    // ThreadLocal to ensure each test has its own log buffer
    private static readonly ThreadLocal<Dictionary<string, List<string>>> s_logBuffer = new ThreadLocal<Dictionary<string, List<string>>>(() => new Dictionary<string, List<string>>());

    // ThreadLocal to ensure each thread has its own logger configuration
    private static readonly ThreadLocal<ILogger> s_logger = new ThreadLocal<ILogger>(InitializeLogger);

    public static ThreadLocal<string> CurrentTestFullName { get; set; } = new ThreadLocal<string>();

    static Logger()
    {
        s_logBuffer = new ThreadLocal<Dictionary<string, List<string>>>(() => new Dictionary<string, List<string>>());
        s_logger = new ThreadLocal<ILogger>(InitializeLogger);
    }

    private static ILogger InitializeLogger()
    {
        var loggerConfiguration = new LoggerConfiguration();
        var settings = ConfigurationService.GetSection<LoggingSettings>();

        if (settings.IsEnabled)
        {
            if (settings.IsConsoleLoggingEnabled)
            {
                loggerConfiguration.WriteTo.Console(outputTemplate: settings.OutputTemplate);
            }

            if (settings.IsDebugLoggingEnabled)
            {
                loggerConfiguration.WriteTo.Debug(outputTemplate: settings.OutputTemplate);
            }

            if (settings.IsFileLoggingEnabled)
            {
                loggerConfiguration.WriteTo.File("test-log.txt", rollingInterval: RollingInterval.Day, outputTemplate: settings.OutputTemplate);
            }

            if (settings.IsReportPortalLoggingEnabled)
            {
                loggerConfiguration.WriteTo.ReportPortal();
            }
        }

        return loggerConfiguration.CreateLogger();
    }

    // Method to accumulate logs in the buffer per test/thread
    private static void AccumulateLog(string logLevel, string message)
    {
        if (s_logBuffer.Value == null)
        {
            s_logBuffer.Value = new Dictionary<string, List<string>>();
        }

        if (!s_logBuffer.Value.ContainsKey(CurrentTestFullName.Value))
        {
            s_logBuffer.Value.Add(CurrentTestFullName.Value, new List<string>());
        }

        s_logBuffer.Value[CurrentTestFullName.Value].Add(message);
    }


    public static void LogInformation(string message, params object[] args)
    {
        AccumulateLog("INFO", string.Format(message, args));
    }

    public static void LogError(string message, params object[] args)
    {
        AccumulateLog("ERROR", string.Format(message, args));
    }

    public static void LogWarning(string message, params object[] args)
    {
        AccumulateLog("WARNING", string.Format(message, args));
    }

    // Lock object to ensure thread-safe execution of FlushLogs
    private static readonly object _flushLock = new object();

    public static string GetLogs()
    {
        if (s_logBuffer.Value.ContainsKey(CurrentTestFullName.Value))
        {
            return string.Join(Environment.NewLine, s_logBuffer.Value[CurrentTestFullName.Value]);
        }

        return "⚠️ No log entries captured for this test.";
    }

    // Method to print the accumulated logs at the end of the test
    public static void FlushLogs()
    {
        lock (_flushLock) // Ensure that only one thread can execute FlushLogs at a time
        {
            // Check if there's anything to flush for the current test
            if (s_logBuffer.Value.ContainsKey(CurrentTestFullName.Value))
            {
                // Flush the logs to the configured sinks
                foreach (var log in s_logBuffer.Value[CurrentTestFullName.Value])
                {
                    s_logger.Value.Information(log); // This could be switched to appropriate log levels
                }

                // Clear the log buffer after flushing
                s_logBuffer.Value[CurrentTestFullName.Value].Clear();
            }
        }
    }
}
