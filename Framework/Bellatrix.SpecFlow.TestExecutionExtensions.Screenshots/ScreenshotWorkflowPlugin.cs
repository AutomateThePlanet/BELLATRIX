// <copyright file="ScreenshotWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Drawing.Imaging;
using System.IO;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using Bellatrix.Trace;
using Serilog;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.TestExecutionExtensions.Screenshots
{
    public class ScreenshotWorkflowPlugin : TestWorkflowPlugin
    {
        private readonly IScreenshotEngine _screenshotEngine;
        private readonly IScreenshotOutputProvider _screenshotOutputProvider;
        private readonly IScreenshotPluginProvider _screenshotPluginProvider;

        public ScreenshotWorkflowPlugin()
        {
        }

        public ScreenshotWorkflowPlugin(
            IScreenshotEngine screenshotEngine,
            IScreenshotOutputProvider screenshotOutputProvider,
            IScreenshotPluginProvider screenshotPluginProvider)
        {
            _screenshotEngine = screenshotEngine;
            _screenshotOutputProvider = screenshotOutputProvider;
            _screenshotPluginProvider = screenshotPluginProvider;
            InitializeScreenshotProviderObservers();
        }

        protected override void PreAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            GetTestScreenshotOnFailMode(e.TestOutcome);

            try
            {
                if (ScreenshotWorkflowPluginContext.ShouldTakeScreenshot)
                {
                    var screenshotSaveDir = _screenshotOutputProvider.GetOutputFolder();
                    var screenshotFileName = _screenshotOutputProvider.GetUniqueFileName(e.TestFullName);
                    var image = _screenshotEngine.TakeScreenshot(ServicesCollection.Current);
                    string imagePath = Path.Combine(screenshotSaveDir, screenshotFileName);
                    image.Save(imagePath, ImageFormat.Png);
                    _screenshotPluginProvider.ScreenshotGenerated(e, imagePath);
                }
            }
            catch (Exception ex)
            {
                // Ignore
            }
        }

        private void GetTestScreenshotOnFailMode(TestOutcome testOutcome)
        {
            bool isEnabled = ConfigurationService.Instance.GetScreenshotsSettings().IsEnabled;
            if (isEnabled && testOutcome != TestOutcome.Passed)
            {
                ScreenshotWorkflowPluginContext.ShouldTakeScreenshot = true;
            }
            else
            {
                ScreenshotWorkflowPluginContext.ShouldTakeScreenshot = false;
            }
        }

        private void InitializeScreenshotProviderObservers()
        {
            var observers = ServicesCollection.Current.ResolveAll<IScreenshotPlugin>();
            foreach (var observer in observers)
            {
                observer.SubscribeScreenshotPlugin(_screenshotPluginProvider);
            }
        }
    }
}