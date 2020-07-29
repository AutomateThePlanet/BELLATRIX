// <copyright file="CrossBrowserTestingBrowserConfiguration.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Configuration;
using Bellatrix.Mobile.TestExecutionExtensions;
using OpenQA.Selenium.Appium;

namespace Bellatrix.SpecFlow.Mobile.TestExecutionExtensions
{
    public class CrossBrowserTestingAppConfiguration : AppConfiguration, IAppiumOptionsFactory
    {
        public string Build { get; set; }

        public string BrowserVersion { get; set; }

        public string Platform { get; set; }

        public bool RecordVideo { get; set; }

        public bool RecordNetwork { get; set; }

        public string ScreenResolution { get; set; }

        public virtual void InitializeAppiumOptions(string classFullName)
        {
            AppiumOptions = AddAdditionalCapability(classFullName, new AppiumOptions());

            if (!string.IsNullOrEmpty(Build))
            {
                AppiumOptions.AddAdditionalCapability("build", Build);
            }

            AppiumOptions.AddAdditionalCapability("browserName", string.Empty);
            AppiumOptions.AddAdditionalCapability("platform", Platform);
            AppiumOptions.AddAdditionalCapability("version", BrowserVersion);
            AppiumOptions.AddAdditionalCapability("screen_resolution", ScreenResolution);
            AppiumOptions.AddAdditionalCapability("record_video", RecordVideo);
            AppiumOptions.AddAdditionalCapability("record_network", RecordNetwork);

            var crossBrowserTestingCredentialsResolver = new CrossBrowserTestingCredentialsResolver();
            var credentials = crossBrowserTestingCredentialsResolver.GetCredentials();
            AppiumOptions.AddAdditionalCapability("username", credentials.Item1);
            AppiumOptions.AddAdditionalCapability("password", credentials.Item2);

            AppiumOptions.AddAdditionalCapability("name", classFullName);
        }
    }
}