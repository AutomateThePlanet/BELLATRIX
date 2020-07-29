// <copyright file="BrowserStackBrowserConfiguration.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile;
using Bellatrix.Mobile.Configuration;
using Bellatrix.Mobile.TestExecutionExtensions;
using OpenQA.Selenium.Appium;

namespace Bellatrix.SpecFlow.Mobile.TestExecutionExtensions
{
    public class BrowserStackAppConfiguration : AppConfiguration, IAppiumOptionsFactory
    {
        public bool Debug { get; set; }

        public string Build { get; set; }

        public string BrowserVersion { get; set; }

        public string OperatingSystem { get; set; }

        public string OSVersion { get; set; }

        public bool CaptureVideo { get; set; }

        public bool CaptureNetworkLogs { get; set; }

        public BrowserStackConsoleLogType ConsoleLogType { get; set; }

        public string ScreenResolution { get; set; }

        public virtual void InitializeAppiumOptions(string classFullName)
        {
            AppiumOptions = AddAdditionalCapability(classFullName, new AppiumOptions());

            AppiumOptions.AddAdditionalCapability("browserstack.debug", Debug);

            if (!string.IsNullOrEmpty(Build))
            {
                AppiumOptions.AddAdditionalCapability("build", Build);
            }

            AppiumOptions.AddAdditionalCapability("browser", BrowserName);
            AppiumOptions.AddAdditionalCapability("os", OperatingSystem);
            AppiumOptions.AddAdditionalCapability("os_version", OSVersion);
            AppiumOptions.AddAdditionalCapability("browser_version", BrowserVersion);
            AppiumOptions.AddAdditionalCapability("resolution", ScreenResolution);
            AppiumOptions.AddAdditionalCapability("browserstack.video", CaptureVideo);
            AppiumOptions.AddAdditionalCapability("browserstack.networkLogs", CaptureNetworkLogs);
            string consoleLogTypeText = Enum.GetName(typeof(BrowserStackConsoleLogType), ConsoleLogType).ToLower();
            AppiumOptions.AddAdditionalCapability("browserstack.console", consoleLogTypeText);

            var browserStackCredentialsResolver = new BrowserStackCredentialsResolver();
            var credentials = browserStackCredentialsResolver.GetCredentials();
            AppiumOptions.AddAdditionalCapability("browserstack.user", credentials.Item1);
            AppiumOptions.AddAdditionalCapability("browserstack.key", credentials.Item2);
        }
    }
}