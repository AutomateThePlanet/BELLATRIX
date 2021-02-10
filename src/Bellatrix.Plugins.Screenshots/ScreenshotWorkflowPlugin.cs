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
using System.Reflection;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using Bellatrix.TestExecutionExtensions.Screenshots.Plugins;
using Bellatrix.TestWorkflowPlugins;
using Serilog;

namespace Bellatrix.TestExecutionExtensions.Screenshots
{
    public class ScreenshotWorkflowPlugin : TestWorkflowPlugin
    {
        private readonly IScreenshotEngine _screenshotEngine;
        private readonly IScreenshotOutputProvider _screenshotOutputProvider;
        private readonly IScreenshotPluginProvider _screenshotPluginProvider;
        private bool _shouldTakeScreenshot;

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

        protected override void PreTestCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
            GetTestScreenshotOnFailMode(e.TestMethodMemberInfo, e.TestOutcome);

            try
            {
                if (_shouldTakeScreenshot)
                {
                    var screenshotSaveDir = _screenshotOutputProvider.GetOutputFolder();
                    var screenshotFileName = _screenshotOutputProvider.GetUniqueFileName(e.TestFullName);
                    string image = _screenshotEngine.TakeScreenshot(e.Container);
                    string imagePath = Path.Combine(screenshotSaveDir, screenshotFileName);
                    File.WriteAllBytes(imagePath, Convert.FromBase64String(image));
                    _screenshotPluginProvider.ScreenshotGenerated(e, imagePath);
                }
            }
            catch (Exception ex)
            {
                // Ignore since it is failing often because of bugs in Remote driver for Chrome
            }

            base.PreTestCleanup(sender, e);
        }

        private void GetTestScreenshotOnFailMode(MemberInfo memberInfo, TestOutcome testOutcome)
        {
            bool classScreenshotOnFail = GetTakeScreenshotOnFailModeByType(memberInfo.DeclaringType);
            bool? methodScreenshotOnFail = GetTakeScreenshotOnFailModeByMethodInfo(memberInfo);
            bool isEnabled = ConfigurationService.GetSection<ScreenshotsSettings>().IsEnabled;
            if (isEnabled && testOutcome != TestOutcome.Passed)
            {
                if (classScreenshotOnFail)
                {
                    _shouldTakeScreenshot = true;
                }

                if (methodScreenshotOnFail != null)
                {
                    _shouldTakeScreenshot = (bool)methodScreenshotOnFail;
                }
            }
            else
            {
                _shouldTakeScreenshot = false;
            }
        }

        private bool GetTakeScreenshotOnFailModeByType(Type currentType)
        {
            var takeScreenshotOnFailClassAttribute = currentType.GetCustomAttribute<ScreenshotOnFailAttribute>(true);
            if (takeScreenshotOnFailClassAttribute != null)
            {
                return takeScreenshotOnFailClassAttribute.Enabled;
            }

            return false;
        }

        private bool? GetTakeScreenshotOnFailModeByMethodInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException();
            }

            var takeScreenshotOnFailClassAttribute = memberInfo.GetCustomAttribute<ScreenshotOnFailAttribute>(true);

            return takeScreenshotOnFailClassAttribute?.Enabled;
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