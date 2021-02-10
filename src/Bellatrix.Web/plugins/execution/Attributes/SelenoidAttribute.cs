// <copyright file="SelenoidAttribute.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.TestExecutionExtensions.Browser;

namespace Bellatrix.Web
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class SelenoidAttribute : BrowserAttribute, IDriverOptionsAttribute
    {
        private const string DefaultScreenResolution = "1920x1080x24";

        public SelenoidAttribute(
            BrowserType browser,
            string browserVersion,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = true,
            bool enableVnc = true,
            bool saveSessionLogs = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : base(browser, behavior, shouldAutomaticallyScrollToVisible)
        {
            BrowserVersion = browserVersion;
            RecordVideo = recordVideo;
            ExecutionType = ExecutionType.Grid;
            EnableVnc = enableVnc;
            SaveSessionLogs = saveSessionLogs;
            ScreenResolution = DefaultScreenResolution;
        }

        public SelenoidAttribute(
            BrowserType browser,
            string browserVersion,
            int width,
            int height,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = true,
            bool enableVnc = true,
            bool saveSessionLogs = false,
            bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, width, height, behavior, shouldAutomaticallyScrollToVisible)
        {
            BrowserVersion = browserVersion;
            RecordVideo = recordVideo;
            ExecutionType = ExecutionType.Grid;
            EnableVnc = enableVnc;
            SaveSessionLogs = saveSessionLogs;
            ScreenResolution = (width <= 0 || height <= 0) ? DefaultScreenResolution : new Size(width, height).ConvertToStringWithColorDepth();
        }

        public SelenoidAttribute(
            BrowserType browser,
            string browserVersion,
            MobileWindowSize mobileWindowSize,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool enableVnc = false,
            bool saveSessionLogs = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : this(browser, browserVersion, behavior, recordVideo, enableVnc, saveSessionLogs, shouldAutomaticallyScrollToVisible)
            => ScreenResolution = WindowsSizeResolver.GetWindowSize(mobileWindowSize).ConvertToStringWithColorDepth();

        public SelenoidAttribute(
            BrowserType browser,
            string browserVersion,
            TabletWindowSize tabletWindowSize,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool enableVnc = false,
            bool saveSessionLogs = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : this(browser, browserVersion, behavior, recordVideo, enableVnc, saveSessionLogs, shouldAutomaticallyScrollToVisible)
            => ScreenResolution = WindowsSizeResolver.GetWindowSize(tabletWindowSize).ConvertToStringWithColorDepth();

        public SelenoidAttribute(
            BrowserType browser,
            string browserVersion,
            DesktopWindowSize desktopWindowSize,
            Lifecycle behavior = Lifecycle.NotSet,
            bool recordVideo = false,
            bool enableVnc = false,
            bool saveSessionLogs = false,
            bool shouldAutomaticallyScrollToVisible = true)
            : this(browser, browserVersion, behavior, recordVideo, enableVnc, saveSessionLogs, shouldAutomaticallyScrollToVisible)
            => ScreenResolution = WindowsSizeResolver.GetWindowSize(desktopWindowSize).ConvertToStringWithColorDepth();

        public string BrowserVersion { get; }

        public bool EnableVnc { get; } = true;

        public bool RecordVideo { get; } = true;

        public bool SaveSessionLogs { get; }

        public string ScreenResolution { get; set; }

        public dynamic CreateOptions(MemberInfo memberInfo, Type testClassType)
        {
            var runName = testClassType.Assembly.GetName().Name;
            var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";

            var driverOptions = GetDriverOptionsBasedOnBrowser(testClassType);
            AddAdditionalCapabilities(testClassType, driverOptions);
            driverOptions.BrowserVersion = BrowserVersion;
            switch (Browser)
            {
                // By default, all capabilities added to the browser specific implementation
                // of these DriverOptions is added as a subcapability of that browser's options.
                // Selenoid requires selenoid configuration be at the top level of the capabilities
                case BrowserType.Chrome:
                case BrowserType.Firefox:
                case BrowserType.InternetExplorer:
                case BrowserType.Opera:
                    driverOptions.AddAdditionalCapability("name", runName, true);
                    driverOptions.AddAdditionalCapability("videoName", $"{runName}.{timestamp}.mp4", true);
                    driverOptions.AddAdditionalCapability("logName", $"{runName}.{timestamp}.log", true);
                    driverOptions.AddAdditionalCapability("enableVNC", EnableVnc, true);
                    driverOptions.AddAdditionalCapability("enableVideo", RecordVideo, true);
                    driverOptions.AddAdditionalCapability("enableLog", SaveSessionLogs, true);
                    driverOptions.AddAdditionalCapability("screenResolution", ScreenResolution, true);
                    break;

                // Headless sessions are much simpler and do not support video or vnc
                case BrowserType.ChromeHeadless:
                case BrowserType.FirefoxHeadless:
                    driverOptions.AddAdditionalCapability("name", runName, true);
                    driverOptions.AddAdditionalCapability("logName", $"{runName}.{timestamp}.log", true);
                    driverOptions.AddAdditionalCapability("enableLog", SaveSessionLogs, true);
                    driverOptions.AddAdditionalCapability("screenResolution", ScreenResolution, true);
                    break;
                default:
                    driverOptions.AddAdditionalCapability("name", runName);
                    driverOptions.AddAdditionalCapability("videoName", $"{runName}.{timestamp}.mp4");
                    driverOptions.AddAdditionalCapability("enableVNC", EnableVnc);
                    driverOptions.AddAdditionalCapability("enableVideo", RecordVideo);
                    driverOptions.AddAdditionalCapability("enableLog", SaveSessionLogs);
                    driverOptions.AddAdditionalCapability("screenResolution", ScreenResolution);
                    break;
            }

            return driverOptions;
        }
    }
}