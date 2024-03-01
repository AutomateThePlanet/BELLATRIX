// <copyright file="CrossBrowserTestingAttribute.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using Bellatrix.Web.Plugins.Browser;
using Bellatrix.Web.Services;
using OpenQA.Selenium;

namespace Bellatrix.Web;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CrossBrowserTestingAttribute : BrowserAttribute, IDriverOptionsAttribute
{
    public CrossBrowserTestingAttribute(
        BrowserType browser,
        string browserVersion,
        string platform,
        Lifecycle behavior = Lifecycle.NotSet,
        bool recordVideo = false,
        bool recordNetwork = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        Platform = platform;
        Build = build;
        RecordVideo = recordVideo;
        RecordNetwork = recordNetwork;
        ExecutionType = ExecutionType.Grid;
    }

    public CrossBrowserTestingAttribute(
        BrowserType browser,
        string browserVersion,
        string platform,
        int width,
        int height,
        Lifecycle behavior = Lifecycle.NotSet,
        bool recordVideo = false,
        bool recordNetwork = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
    : base(browser, width, height, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        Platform = platform;
        Build = build;
        RecordVideo = recordVideo;
        RecordNetwork = recordNetwork;
        ExecutionType = ExecutionType.Grid;
        ScreenResolution = new Size(width, height).ConvertToString();
    }

    public CrossBrowserTestingAttribute(
        BrowserType browser,
        string browserVersion,
        string platform,
        MobileWindowSize mobileWindowSize,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
        : this(browser, browserVersion, platform, behavior, captureVideo, captureNetworkLogs, build, shouldAutomaticallyScrollToVisible)
        => ScreenResolution = WindowsSizeResolver.GetWindowSize(mobileWindowSize).ConvertToString();

    public CrossBrowserTestingAttribute(
        BrowserType browser,
        string browserVersion,
        string platform,
        TabletWindowSize tabletWindowSize,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
        : this(browser, browserVersion, platform, behavior, captureVideo, captureNetworkLogs, build, shouldAutomaticallyScrollToVisible)
        => ScreenResolution = WindowsSizeResolver.GetWindowSize(tabletWindowSize).ConvertToString();

    public CrossBrowserTestingAttribute(
        BrowserType browser,
        string browserVersion,
        string platform,
        DesktopWindowSize desktopWindowSize,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
        : this(browser, browserVersion, platform, behavior, captureVideo, captureNetworkLogs, build, shouldAutomaticallyScrollToVisible)
        => ScreenResolution = WindowsSizeResolver.GetWindowSize(desktopWindowSize).ConvertToString();

    public string Build { get; }

    public string BrowserVersion { get; }

    public string Platform { get; }

    public bool RecordVideo { get; }

    public bool RecordNetwork { get; }

    public string ScreenResolution { get; set; }

    public dynamic CreateOptions(MemberInfo memberInfo, Type testClassType)
    {
        var driverOptions = GetDriverOptionsBasedOnBrowser(testClassType);
        AddAdditionalCapabilities(testClassType, driverOptions);

        if (!string.IsNullOrEmpty(Build))
        {
            // TODO: DriverOptions have no such method, but because driverOptions is dynamic, it doesn't show. This will fail if the code is ran.
            // Should be fixed.
            driverOptions.SetCapability("build", Build);
        }

        string browserName = Enum.GetName(typeof(BrowserType), Browser);
        driverOptions.AddAdditionalOption("browserName", browserName);
        driverOptions.AddAdditionalOption("platform", Platform);
        driverOptions.AddAdditionalOption("version", BrowserVersion);
        driverOptions.AddAdditionalOption("screen_resolution", ScreenResolution);
        driverOptions.AddAdditionalOption("record_video", RecordVideo);
        driverOptions.AddAdditionalOption("record_network", RecordNetwork);

        var credentials = CloudProviderCredentialsResolver.GetCredentials();
        driverOptions.AddAdditionalOption("username", credentials.Item1);
        driverOptions.AddAdditionalOption("password", credentials.Item2);

        var testName = GetTestFullName(memberInfo, testClassType);
        driverOptions.AddAdditionalOption("name", testName);

        return driverOptions;
    }
}