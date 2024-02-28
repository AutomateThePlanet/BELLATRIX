// <copyright file="BrowserStackAttribute.cs" company="Automate The Planet Ltd.">
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
public class BrowserStackAttribute : BrowserAttribute, IDriverOptionsAttribute
{
    public BrowserStackAttribute(
        BrowserType browser,
        string browserVersion,
        string operatingSystem,
        string osVersion,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        BrowserStackConsoleLogType consoleLogType = BrowserStackConsoleLogType.Disable,
        bool debug = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        OperatingSystem = operatingSystem;
        OSVersion = osVersion;
        Debug = debug;
        Build = build;
        CaptureVideo = captureVideo;
        CaptureNetworkLogs = captureNetworkLogs;
        ConsoleLogType = consoleLogType;
        ExecutionType = ExecutionType.Grid;
    }

    public BrowserStackAttribute(
        BrowserType browser,
        string browserVersion,
        string operatingSystem,
        string osVersion,
        int width,
        int height,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        BrowserStackConsoleLogType consoleLogType = BrowserStackConsoleLogType.Disable,
        bool debug = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
    : base(browser, width, height, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        OperatingSystem = operatingSystem;
        OSVersion = osVersion;
        Debug = debug;
        Build = build;
        CaptureVideo = captureVideo;
        CaptureNetworkLogs = captureNetworkLogs;
        ConsoleLogType = consoleLogType;
        ExecutionType = ExecutionType.Grid;
        ScreenResolution = new Size(width, height).ConvertToString();
    }

    public BrowserStackAttribute(
        BrowserType browser,
        string browserVersion,
        string operatingSystem,
        string osVersion,
        MobileWindowSize mobileWindowSize,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        BrowserStackConsoleLogType browserStackConsoleLogType = BrowserStackConsoleLogType.Disable,
        bool debug = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
        : this(browser, browserVersion, operatingSystem, osVersion, behavior, captureVideo, captureNetworkLogs, browserStackConsoleLogType, debug, build, shouldAutomaticallyScrollToVisible)
        => ScreenResolution = WindowsSizeResolver.GetWindowSize(mobileWindowSize).ConvertToString();

    public BrowserStackAttribute(
        BrowserType browser,
        string browserVersion,
        string operatingSystem,
        string osVersion,
        TabletWindowSize tabletWindowSize,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        BrowserStackConsoleLogType browserStackConsoleLogType = BrowserStackConsoleLogType.Disable,
        bool debug = false,
        string build = null)
        : this(browser, browserVersion, operatingSystem, osVersion, behavior, captureVideo, captureNetworkLogs, browserStackConsoleLogType, debug, build)
        => ScreenResolution = WindowsSizeResolver.GetWindowSize(tabletWindowSize).ConvertToString();

    public BrowserStackAttribute(
        BrowserType browser,
        string browserVersion,
        string operatingSystem,
        string osVersion,
        DesktopWindowSize desktopWindowSize,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        BrowserStackConsoleLogType browserStackConsoleLogType = BrowserStackConsoleLogType.Disable,
        bool debug = false,
        string build = null,
        bool shouldAutomaticallyScrollToVisible = true)
        : this(browser, browserVersion, operatingSystem, osVersion, behavior, captureVideo, captureNetworkLogs, browserStackConsoleLogType, debug, build, shouldAutomaticallyScrollToVisible)
        => ScreenResolution = WindowsSizeResolver.GetWindowSize(desktopWindowSize).ConvertToString();

    public bool Debug { get; }

    public string Build { get; }

    public string BrowserVersion { get; }

    public string OperatingSystem { get; }

    public string OSVersion { get; }

    public bool CaptureVideo { get; }

    public bool CaptureNetworkLogs { get; }

    public BrowserStackConsoleLogType ConsoleLogType { get; }

    public string ScreenResolution { get; set; }

    public dynamic CreateOptions(MemberInfo memberInfo, Type testClassType)
    {
        var driverOptions = GetDriverOptionsBasedOnBrowser(testClassType);
        AddAdditionalCapabilities(testClassType, driverOptions);

        driverOptions.Add("browserstack.debug", Debug);

        if (!string.IsNullOrEmpty(Build))
        {
            driverOptions.SetCapability("build", Build);
        }

        string browserName = Enum.GetName(typeof(BrowserType), Browser);
        driverOptions.AddAdditionalOption("browser", browserName);
        driverOptions.AddAdditionalOption("os", OperatingSystem);
        driverOptions.AddAdditionalOption("os_version", OSVersion);
        driverOptions.AddAdditionalOption("browser_version", BrowserVersion);
        driverOptions.AddAdditionalOption("resolution", ScreenResolution);
        driverOptions.AddAdditionalOption("browserstack.video", CaptureVideo);
        driverOptions.AddAdditionalOption("browserstack.networkLogs", CaptureNetworkLogs);
        string consoleLogTypeText = Enum.GetName(typeof(BrowserStackConsoleLogType), ConsoleLogType).ToLower();
        driverOptions.AddAdditionalOption("browserstack.console", consoleLogTypeText);

        var credentials = CloudProviderCredentialsResolver.GetCredentials();
        driverOptions.AddAdditionalOption("browserstack.user", credentials.Item1);
        driverOptions.AddAdditionalOption("browserstack.key", credentials.Item2);

        var testName = GetTestFullName(memberInfo, testClassType);
        driverOptions.AddAdditionalOption("name", testName);

        return driverOptions;
    }      
}