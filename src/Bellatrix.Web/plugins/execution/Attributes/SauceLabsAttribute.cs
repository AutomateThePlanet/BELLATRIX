// <copyright file="SauceLabsAttribute.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using System.Reflection;
using Bellatrix.Web.Enums;
using Bellatrix.Web.Services;
using Bellatrix.Web.Plugins.Browser;

namespace Bellatrix.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SauceLabsAttribute : BrowserAttribute, IDriverOptionsAttribute
    {
        public SauceLabsAttribute(
            BrowserType browser,
            string browserVersion,
            string platform,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool recordScreenshots = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : base(browser, behavior, shouldAutomaticallyScrollToVisible)
        {
            BrowserVersion = browserVersion;
            Platform = platform;
            RecordVideo = recordVideo;
            RecordScreenshots = recordScreenshots;
            ExecutionType = ExecutionType.SauceLabs;
        }

        public SauceLabsAttribute(
            BrowserType browser,
            string browserVersion,
            string platform,
            int width,
            int height,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool recordScreenshots = false,
            bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, width, height, behavior, shouldAutomaticallyScrollToVisible)
        {
            BrowserVersion = browserVersion;
            Platform = platform;
            RecordVideo = recordVideo;
            RecordScreenshots = recordScreenshots;
            ExecutionType = ExecutionType.SauceLabs;
            ScreenResolution = new Size(width, height).ConvertToString();
        }

        public SauceLabsAttribute(
            BrowserType browser,
            string browserVersion,
            string platform,
            MobileWindowSize mobileWindowSize,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool recordScreenshots = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : this(browser, browserVersion, platform, behavior, recordVideo, recordScreenshots, shouldAutomaticallyScrollToVisible)
            => ScreenResolution = WindowsSizeResolver.GetWindowSize(mobileWindowSize).ConvertToString();

        public SauceLabsAttribute(
            BrowserType browser,
            string browserVersion,
            string platform,
            TabletWindowSize tabletWindowSize,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool recordScreenshots = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : this(browser, browserVersion, platform, behavior, recordVideo, recordScreenshots, shouldAutomaticallyScrollToVisible)
            => ScreenResolution = WindowsSizeResolver.GetWindowSize(tabletWindowSize).ConvertToString();

        public SauceLabsAttribute(
            BrowserType browser,
            string browserVersion,
            string platform,
            DesktopWindowSize desktopWindowSize,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool recordScreenshots = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : this(browser, browserVersion, platform, behavior, recordVideo, recordScreenshots, shouldAutomaticallyScrollToVisible)
            => ScreenResolution = WindowsSizeResolver.GetWindowSize(desktopWindowSize).ConvertToString();

        public string BrowserVersion { get; }

        public string Platform { get; }

        public bool RecordVideo { get; }

        public bool RecordScreenshots { get; }

        public string ScreenResolution { get; set; }

        public dynamic CreateOptions(MemberInfo memberInfo, Type testClassType)
        {
            var driverOptions = GetDriverOptionsBasedOnBrowser(testClassType);
            AddAdditionalCapabilities(testClassType, driverOptions);

            string browserName = Enum.GetName(typeof(BrowserType), Browser);
            driverOptions.AddAdditionalCapability("platform", Platform);
            driverOptions.AddAdditionalCapability("version", BrowserVersion);
            driverOptions.AddAdditionalCapability("screenResolution", ScreenResolution);
            driverOptions.AddAdditionalCapability("recordVideo", RecordVideo);
            driverOptions.AddAdditionalCapability("recordScreenshots", RecordScreenshots);

            var sauceLabsCredentialsResolver = new SauceLabsCredentialsResolver();
            var credentials = sauceLabsCredentialsResolver.GetCredentials();
            driverOptions.AddAdditionalCapability("username", credentials.Item1);
            driverOptions.AddAdditionalCapability("accessKey", credentials.Item2);
            driverOptions.AddAdditionalCapability("name", testClassType.FullName);

            return driverOptions;
        }
    }
}