// <copyright file="BrowserStackAttribute.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Reflection;
using Bellatrix.Mobile.Enums;
using Bellatrix.Mobile.TestExecutionExtensions;
using Bellatrix.Mobile.TestExecutionExtensions.Attributes;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class BrowserStackAttribute : AppAttribute, IAppiumOptionsFactory
    {
        protected BrowserStackAttribute(
            string appPath,
            string platformVersion,
            string deviceName,
            Lifecycle behavior = Lifecycle.NotSet,
            bool captureVideo = false,
            bool captureNetworkLogs = false,
            BrowserStackConsoleLogType consoleLogType = BrowserStackConsoleLogType.Disable,
            bool debug = false,
            string build = null)
            : base(appPath, platformVersion, deviceName, behavior)
        {
            Debug = debug;
            Build = build;
            CaptureVideo = captureVideo;
            CaptureNetworkLogs = captureNetworkLogs;
            ConsoleLogType = consoleLogType;
            AppConfiguration.ExecutionType = ExecutionType.BrowserStack;
        }

        public bool Debug { get; }

        public string Build { get; }

        public bool CaptureVideo { get; }

        public bool CaptureNetworkLogs { get; }

        public BrowserStackConsoleLogType ConsoleLogType { get; }

        public AppiumOptions CreateAppiumOptions(MemberInfo memberInfo, Type testClassType)
        {
            var appiumOptions = new AppiumOptions();
            AddAdditionalCapabilities(testClassType, appiumOptions);

            appiumOptions.AddAdditionalCapability("browserstack.debug", Debug);

            if (!string.IsNullOrEmpty(Build))
            {
                appiumOptions.AddAdditionalCapability("build", Build);
            }

            appiumOptions.AddAdditionalCapability("device", AppConfiguration.DeviceName);
            appiumOptions.AddAdditionalCapability("os_version", AppConfiguration.PlatformVersion);
            appiumOptions.AddAdditionalCapability("app", AppConfiguration.AppPath);
            appiumOptions.AddAdditionalCapability("browserstack.video", CaptureVideo);
            appiumOptions.AddAdditionalCapability("browserstack.networkLogs", CaptureNetworkLogs);
            string consoleLogTypeText = Enum.GetName(typeof(BrowserStackConsoleLogType), ConsoleLogType)?.ToLower();
            appiumOptions.AddAdditionalCapability("browserstack.console", consoleLogTypeText);

            var browserStackCredentialsResolver = new BrowserStackCredentialsResolver();
            var credentials = browserStackCredentialsResolver.GetCredentials();
            appiumOptions.AddAdditionalCapability("browserstack.user", credentials.Item1);
            appiumOptions.AddAdditionalCapability("browserstack.key", credentials.Item2);

            return appiumOptions;
        }
    }
}