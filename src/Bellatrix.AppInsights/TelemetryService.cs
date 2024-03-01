// <copyright file="TelemetryService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Bellatrix.KeyVault;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Bellatrix;

public static class TelemetryService
{
    static TelemetryService()
    {
        var config = new TelemetryConfiguration();
        config.ConnectionString = SecretsResolver.GetSecret(() => ConfigurationService.GetSection<AppInsightsSettings>().InstrumentationKey);
        TelemetryClient = new TelemetryClient(config);

        // Enable filter by product version.
        TelemetryClient.Context.Component.Version = typeof(TelemetryService).Assembly.GetName().Version.ToString();
        TelemetryClient.Context.GlobalProperties.Add("Client OS", System.Runtime.InteropServices.RuntimeInformation.OSDescription);
        TelemetryClient.Context.Component.Version = typeof(TelemetryService).Assembly.GetName().Version.ToString();
        TelemetryClient.Context.Device.OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
    }

    private static TelemetryClient TelemetryClient { get; set; }

    public static void TrackExceptionAndFlush(Exception ex)
    {
        if (ConfigurationService.GetSection<AppInsightsSettings>().IsEnabled)
        {
            TelemetryClient.TrackException(ex);
            TelemetryClient.Flush();

            // Wait for the flush really to happen.
            System.Threading.Thread.Sleep(5000);
        }
    }

    public static void Flush()
    {
        if (ConfigurationService.GetSection<AppInsightsSettings>().IsEnabled)
        {
            TelemetryClient.Flush();

            // Wait for the flush really to happen.
            System.Threading.Thread.Sleep(5000);
        }
    }

    public static void TrackException(Exception ex)
    {
        if (ConfigurationService.GetSection<AppInsightsSettings>().IsEnabled)
        {
            TelemetryClient.TrackException(ex);
        }
    }

    public static void TrackEvent(EventTelemetry eventTelemetry)
    {
        if (ConfigurationService.GetSection<AppInsightsSettings>().IsEnabled)
        {
            TelemetryClient.TrackEvent(eventTelemetry);
        }
    }

    public static void TrackEvent(string name)
    {
        if (ConfigurationService.GetSection<AppInsightsSettings>().IsEnabled)
        {
            var eventTelemetry = new EventTelemetry(name);
            TelemetryClient.TrackEvent(eventTelemetry);
        }
    }
}
