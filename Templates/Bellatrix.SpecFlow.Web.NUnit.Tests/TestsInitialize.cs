// <copyright file="TestsInitialize.cs" company="Automate The Planet Ltd.">
// Copyright 2019 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
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
using Bellatrix.Web.SpecFlow;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.Tests
{
    [Binding]
    [TestFixture]
    public class TestsInitialize : WebHooks
    {
        [BeforeTestRun(Order = 1)]
        public static void PreBeforeTestRun()
        {
            App.UseUnityContainer();
            App.UseNUnitSettings();
            App.UseLogger();
            App.UseBrowserBehavior();
            App.UseLogExecutionBehavior();
            App.UseControlLocalOverridesCleanBehavior();
            App.UseFFmpegVideoRecorder();
            App.UseFullPageScreenshotsOnFail();
            App.AssemblyInitialize();

            InitializeAssemblyTestExecutionBehaviorObservers(TestExecutionProvider);
            InitializeTestExecutionBehaviorObservers(TestExecutionProvider);
            TestExecutionProvider.PreAssemblyInitialize();
        }

        [BeforeTestRun(Order = 100)]
        public static void PostBeforeTestRun()
        {
            TestExecutionProvider.PostAssemblyInitialize();
        }

        [AfterTestRun(Order = 1)]
        public static void PreAfterTestRun()
        {
            TestExecutionProvider.PreAssemblyCleanup();
            App.AssemblyCleanup();
            App.Dispose();
        }

        [AfterTestRun(Order = 100)]
        public static void PostAfterTestRun()
        {
            TestExecutionProvider.PreAssemblyCleanup();
        }

        [BeforeFeature(Order = 1)]
        public static void PreBeforeFeatureArrange(FeatureContext featureContext)
        {
            if (ThrownException?.Value != null)
            {
                ThrownException.Value = null;
            }

            try
            {
                TestExecutionProvider.PreBeforeFeatureArrange(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeScenarioFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 100)]
        public static void PostBeforeFeatureArrange(FeatureContext featureContext)
        {
            if (ThrownException?.Value != null)
            {
                ThrownException.Value = null;
            }

            try
            {
                TestExecutionProvider.PostBeforeFeatureArrange(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeScenarioFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 101)]
        public static void PreBeforeFeatureAct(FeatureContext featureContext)
        {
            if (ThrownException?.Value != null)
            {
                ThrownException.Value = null;
            }

            try
            {
                TestExecutionProvider.PreBeforeFeatureAct(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeScenarioFailed(ex);
                throw;
            }
        }

        [BeforeFeature(Order = 200)]
        public static void PostBeforeFeatureAct(FeatureContext featureContext)
        {
            if (ThrownException?.Value != null)
            {
                ThrownException.Value = null;
            }

            try
            {
                TestExecutionProvider.PostBeforeFeatureAct(featureContext.FeatureInfo.Title, featureContext.FeatureInfo.Tags.ToList());
            }
            catch (Exception ex)
            {
                TestExecutionProvider.BeforeScenarioFailed(ex);
                throw;
            }
        }
    }
}
