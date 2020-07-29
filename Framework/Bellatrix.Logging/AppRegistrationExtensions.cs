// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Application;
using Bellatrix.Logging;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Bellatrix
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseLogger(this BaseApp baseApp)
        {
            var loggingSettings = ConfigurationService.Instance.Root.GetSection("logging")?.Get<LoggingSettings>();

            if (loggingSettings != null && loggingSettings.IsEnabled)
            {
                var loggerConfiguration = new LoggerConfiguration();
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

                Log.Logger = loggerConfiguration.CreateLogger();

                baseApp.RegisterInstance(Log.Logger);
                baseApp.RegisterType<IBellaLogger, BellaLogger>();
            }

            return baseApp;
        }
    }
}
