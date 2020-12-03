// <copyright file="Telemetry.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Linq;
using Bellatrix.Infrastructure;
using BellatrixInfrastructure;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace Bellatrix.Trace
{
    public class Telemetry : ITelemetry
    {
        private static ITelemetry _instance;
        private readonly string _msTestClassAttributeName = "Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute";
        private readonly string _msTestTestAttributeName = "Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute";
        private readonly string _nunitTestFixtureAttributeName = "NUnit.Framework.TestFixtureAttribute";
        private readonly string _nunitTestAttributeName = "NUnit.Framework.TestAttribute";

        private Telemetry()
        {
        }

        public static ITelemetry Instance => _instance ?? (_instance = new Telemetry());

        public TelemetryClient TelemetryClient
        {
            get
            {
                bool isDebug = IsDebugRun();
                var client = TelemetryService.GetTelemetryClient(isDebug);

                return client;
            }
        }

        public void TrackExceptionAndFlush(Exception ex)
        {
            bool isDebug = IsDebugRun();
            if (!isDebug)
            {
                TelemetryService.TrackExceptionAndFlush(ex, isDebug);
            }
        }

        public void TrackEvent(EventTelemetry eventTelemetry)
        {
            bool isDebug = IsDebugRun();
            if (!isDebug)
            {
                TelemetryService.TrackEvent(eventTelemetry, isDebug);
            }
        }

        public void TrackTestExecution(string projectTrackInfo)
        {
            bool isDebug = IsDebugRun();
            try
            {
                if (!isDebug)
                {
                    projectTrackInfo = $"{projectTrackInfo}  Framework";

                    var assemblyFacade = new AssemblyFacade();
                    var callingAssemblies = assemblyFacade.GetAssembliesCallChain();
                    var testProjectAssembly = callingAssemblies[3];

                    if (callingAssemblies.Any(x => x.FullName.Contains("SpecFlow")))
                    {
                        projectTrackInfo = $"{projectTrackInfo} SpecFlow";
                    }

                    if (testProjectAssembly.FullName.Contains("GettingStarted"))
                    {
                        projectTrackInfo = $"{projectTrackInfo} GettingStarted";
                    }

                    var testInfoProvider = new TestInfoProvider();
                    EventTelemetry eventTelemetry = default;
                    int mstestTestsCount = testInfoProvider.CountBellatrixTests(testProjectAssembly, _msTestClassAttributeName, _msTestTestAttributeName);
                    int nunitTestsCount = testInfoProvider.CountBellatrixTests(testProjectAssembly, _nunitTestFixtureAttributeName, _nunitTestAttributeName);
                    if (mstestTestsCount > 0)
                    {
                        eventTelemetry = new EventTelemetry();
                        eventTelemetry.Name = "MSTest Test Run";
                        eventTelemetry.Metrics.Add(projectTrackInfo, mstestTestsCount);
                    }

                    if (nunitTestsCount > 0)
                    {
                        eventTelemetry = new EventTelemetry();
                        eventTelemetry.Name = "NUnit Test Run";
                        eventTelemetry.Metrics.Add(projectTrackInfo, nunitTestsCount);
                    }

                    TelemetryService.TrackEvent(eventTelemetry, isDebug);
                }
            }
            catch (Exception ex)
            {
                TelemetryService.TrackException(ex, isDebug);
            }
        }

        public void TrackEvent(string eventName)
        {
            bool isDebug = IsDebugRun();
            if (!isDebug)
            {
                var eventTelemetry = new EventTelemetry { Name = eventName };
                TelemetryService.TrackEvent(eventTelemetry, isDebug);
            }
        }

        public void Flush()
        {
            bool isDebug = IsDebugRun();
            TelemetryService.Flush(isDebug);
        }

        public void TrackException(Exception ex)
        {
            bool isDebug = IsDebugRun();
            if (!isDebug)
            {
                TelemetryService.TrackException(ex, isDebug);
            }
        }

        private bool IsDebugRun()
        {
#if DEBUG
            bool isDebug = true;
#else
            bool isDebug = false;
#endif

            return isDebug;
        }
    }
}