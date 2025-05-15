// <copyright file="Logger.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>
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
