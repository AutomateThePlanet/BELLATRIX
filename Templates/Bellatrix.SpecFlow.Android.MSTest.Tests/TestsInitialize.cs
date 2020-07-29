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
using System.Linq;
using Bellatrix.Mobile.Android;
using Bellatrix.Mobile.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.Tests
{
    [Binding]
    [TestClass]
    public class TestsInitialize : AndroidHooks
    {
        [BeforeTestRun(Order = 1)]
        public static void PreBeforeTestRun()
        {
            var app = new Bellatrix.Mobile.AndroidApp();

            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseAppBehavior();
            app.UseLogExecutionBehavior();
            app.UseAndroidControlLocalOverridesCleanBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseScreenshotsOnFail();
            app.UseLogger();
            app.UseElementsBddLogging();
            app.UseEnsureExtensionsBddLogging();
            app.UseLayoutAssertionExtensionsBddLogging();
            app.StartAppiumLocalService();
            app.Initialize();

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
            var app = ServicesCollection.Current.Resolve<Bellatrix.Mobile.AndroidApp>();
            app?.Dispose();
            app?.StopAppiumLocalService();
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
