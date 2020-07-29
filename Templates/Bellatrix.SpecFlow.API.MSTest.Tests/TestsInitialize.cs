// <copyright file="TestsInitialize.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using Bellatrix.Api;
using Bellatrix.Api.SpecFlow;
using Bellatrix.API.SpecFlow;
using Bellatrix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.API.MSTest.Tests
{
    [Binding]
    [TestClass]
    public class TestsInitialize : APIHooks
    {
        private static Process _testApiProcess;

        [BeforeTestRun(Order = 1)]
        public static void PreBeforeTestRun()
        {
            var app = new App();
            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseLogger();
            app.UseExecutionTimeUnderExtensions();
            app.UseApiExtensionsBddLogging();
            app.UseAssertExtensionsBddLogging();
            app.UseLogExecution();
            app.UseRetryFailedRequests();
            app.Initialize();

            string workingDir = Path.Combine(ProcessProvider.GetEntryProcessApplicationPath(), "Demos", "TestAPI");
            _testApiProcess = ProcessProvider.StartProcess("dotnet", workingDir, " run", true);
            ProcessProvider.WaitPortToGetBusy(55215);

            InitializeTestExecutionBehaviorObservers(TestExecutionProvider);

            // Software machine automation module helps you to install the required software to the developer's machine
            // such as a specific version of the browsers, browser extensions, and any other required software.
            // You can configure it from BELLATRIX configuration file testFrameworkSettings.json
            //  "machineAutomationSettings": {
            //      "isEnabled": "true",
            //      "packagesToBeInstalled": [ "googlechrome", "firefox --version=65.0.2", "opera" ]
            //  }
            //
            // You need to specify the packages to be installed in the packagesToBeInstalled array. You can search for packages in the
            // public community repository- https://chocolatey.org/
            //
            // To use the service you need to start Visual Studio in Administrative Mode. The service supports currently only Windows.
            // In the future BELLATRIX releases we will support OSX and Linux as well.
            //
            // To use the machine automation setup- install Bellatrix.MachineAutomation NuGet package.
            // SoftwareAutomationService.InstallRequiredSoftware();
        }

        [AfterTestRun(Order = 1)]
        public static void PreAfterTestRun()
        {
            ProcessProvider.CloseProcess(_testApiProcess);
        }

        [BeforeFeature(Order = 1)]
        public static void PreBeforeFeatureArrange(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PreBeforeFeatureArrange(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 100)]
        public static void PostBeforeFeatureArrange(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PostBeforeFeatureArrange(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 101)]
        public static void PreBeforeFeatureAct(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PreBeforeFeatureAct(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 200)]
        public static void PostBeforeFeatureAct(FeatureContext featureContext)
        {
            try
            {
                TestExecutionProvider.PostBeforeFeatureAct(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeFeatureFailed(ex);
                throw;
            }
        }
    }
}
