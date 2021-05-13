using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Bellatrix.AppInsights
{
    public static class TelemetryService
    {
        static TelemetryService()
        {
            TelemetryDebugClient =
                new TelemetryClient(new TelemetryConfiguration("asd"));
            TelemetryNotDebugClient =
                new TelemetryClient(new TelemetryConfiguration("asd"));

            // Enable filter by product version.
            TelemetryDebugClient.Context.Component.Version = typeof(TelemetryService).Assembly.GetName().Version.ToString();
            TelemetryDebugClient.Context.GlobalProperties.Add("Client OS", System.Runtime.InteropServices.RuntimeInformation.OSDescription);
            TelemetryNotDebugClient.Context.Component.Version = typeof(TelemetryService).Assembly.GetName().Version.ToString();
            TelemetryNotDebugClient.Context.Device.OperatingSystem = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        }

        private static TelemetryClient TelemetryDebugClient { get; set; }
        private static TelemetryClient TelemetryNotDebugClient { get; set; }

        public static TelemetryClient GetTelemetryClient(bool isDebug)
        {
            var client = isDebug ? TelemetryDebugClient : TelemetryNotDebugClient;

            return client;
        }

        public static void TrackExceptionAndFlush(Exception ex, bool isDebug)
        {
            var client = isDebug ? TelemetryDebugClient : TelemetryNotDebugClient;
            client.TrackException(ex);
            client.Flush();

            // Wait for the flush really to happen.
            System.Threading.Thread.Sleep(5000);
        }

        public static void Flush(bool isDebug)
        {
            var client = isDebug ? TelemetryDebugClient : TelemetryNotDebugClient;
            client.Flush();

            // Wait for the flush really to happen.
            System.Threading.Thread.Sleep(5000);
        }

        public static void TrackException(Exception ex, bool isDebug)
        {
            var client = isDebug ? TelemetryDebugClient : TelemetryNotDebugClient;
            client.TrackException(ex);
        }

        public static void TrackEvent(EventTelemetry eventTelemetry, bool isDebug)
        {
            var client = isDebug ? TelemetryDebugClient : TelemetryNotDebugClient;
            client.TrackEvent(eventTelemetry);
        }
    }
}
