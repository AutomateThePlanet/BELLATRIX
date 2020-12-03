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
using Bellatrix.Web.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.Web.Tests
{
    [Binding]
    [TestClass]
    public class TestsInitialize : WebHooks
    {
        [BeforeTestRun(Order = 1)]
        public static void PreBeforeTestRun()
        {
            var app = new Bellatrix.Web.App();
            app.UseExceptionLogger();
            app.UseMsTestSettings();
            app.UseControlDataHandlers();
            app.UseBrowserBehavior();
            app.UseLogExecutionBehavior();
            app.UseFFmpegVideoRecorder();
            app.UseFullPageScreenshotsOnFail();

            InitializeTestExecutionBehaviorObservers(TestExecutionProvider);
        }

        [AfterTestRun(Order = 1)]
        public static void PreAfterTestRun()
        {
            var app = ServicesCollection.Current.Resolve<Bellatrix.Web.App>();
            app?.Dispose();
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
