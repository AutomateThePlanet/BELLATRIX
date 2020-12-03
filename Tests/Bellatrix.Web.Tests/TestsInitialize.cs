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
using Bellatrix.Infrastructure;
using Bellatrix.MachineAutomation;
using Bellatrix.Trace;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests
{
    [TestClass]
    public class TestsInitialize : WebTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            var app = new App();

            app.UseMsTestSettings();
            app.UseControlDataHandlers();
            app.UseExceptionLogger();
            app.UseBrowserBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseFullPageScreenshotsOnFail();
            app.UseElementsBddLogging();
            app.UseHighlightElements();
            app.UseValidateExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();

            ////app.UseTestResultsAnalysisTraining();
            ////app.UseLoadTesting();
            app.Initialize();
            ////App.UseAllure();

            ////App.UseDynamicTestCases();
            ////App.UseAssertExtensionsDynamicTestCases();
            ////App.UseValidateExtensionsDynamicTestCases();
            ////App.UseLayoutAssertionExtensionsDynamicTestCases();

            ////App.UseQTestDynamicTestCases();
            ////App.UseAzureDevOpsDynamicTestCases();

            app.UseBugReporting();
            ////app.UseAssertExtensionsBugReporting();
            app.UseValidateExtensionsBugReporting();
            app.UseLayoutAssertionExtensionsBugReporting();
            ////App.UseAzureDevOpsBugReporting();
            ////App.UseJiraBugReporting();
            ////app.UseBotBugReporting();
            ////SoftwareAutomationService.InstallRequiredSoftware();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            var app = ServicesCollection.Current.Resolve<App>();
            app?.Dispose();
        }
    }
}
