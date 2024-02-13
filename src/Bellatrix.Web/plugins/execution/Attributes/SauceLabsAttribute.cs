// <copyright file="SauceLabsAttribute.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using Bellatrix.Web.Enums;
using Bellatrix.Web.Plugins.Browser;
using Bellatrix.Web.Services;

namespace Bellatrix.Web;

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
        int width,
        int height,
        Lifecycle behavior = Lifecycle.NotSet,
        bool recordVideo = false,
        bool recordScreenshots = false,
        bool useTunnel = false,
        string tunnelId = "",
        string parentTunnel = "",
        bool enableExtendedDebugging = false,
        bool capturePerformance = false,
        string buildName = "BELLATRIX",
        bool shouldAutomaticallyScrollToVisible = true)
        : this(browser, browserVersion, platform, width, height, behavior, recordVideo, recordScreenshots, shouldAutomaticallyScrollToVisible)
    {
        UseTunnel = useTunnel;
        TunnelId = tunnelId;
        ParentTunnel = parentTunnel;
        EnableExtendedDebugging = capturePerformance || enableExtendedDebugging;
        CapturePerformance = capturePerformance;
        BuildName = buildName;
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

    public string BuildName { get; }

    public bool CapturePerformance { get; }

    public bool EnableExtendedDebugging { get; }

    public string ParentTunnel { get; }

    public string Platform { get; }

    public bool RecordScreenshots { get; }

    public bool RecordVideo { get; }

    public string ScreenResolution { get; }

    public string TunnelId { get; }

    public bool UseTunnel { get; }

    public dynamic CreateOptions(MemberInfo memberInfo, Type testClassType)
    {
        var sauceBuildName = Environment.GetEnvironmentVariable("SAUCE_BUILD_NAME");
        var buildName = string.IsNullOrEmpty(sauceBuildName) ? BuildName : sauceBuildName;

        var driverOptions = GetDriverOptionsBasedOnBrowser(testClassType);
        AddAdditionalCapabilities(testClassType, driverOptions);

        var sauceOptions = new Dictionary<string, string>();

        string browserName = Enum.GetName(typeof(BrowserType), Browser);
        sauceOptions.Add("platform", Platform);
        sauceOptions.Add("version", BrowserVersion);
        sauceOptions.Add("screenResolution", ScreenResolution);
        sauceOptions.Add("recordVideo", RecordVideo.ToString());
        sauceOptions.Add("recordScreenshots", RecordScreenshots.ToString());
        sauceOptions.Add("extendedDebugging", EnableExtendedDebugging.ToString());
        sauceOptions.Add("capturePerformance", CapturePerformance.ToString());
        sauceOptions.Add("build", buildName);
        sauceOptions.Add("name", testClassType.FullName);

        try
        {
            var credentials = CloudProviderCredentialsResolver.GetCredentials();
            sauceOptions.Add("username", credentials.Item1);
            sauceOptions.Add("accessKey", credentials.Item2);
        }
        catch (ArgumentException)
        {
            // Need to be able to support retrieving credentials from testFrameworkSettings using the values
            // present in the template testFrameworkSettings files, especially where environment variable names
            // containing a "." character are not supported
            var username = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0]["username"];
            var accessKey = ConfigurationService.GetSection<WebSettings>().ExecutionSettings.Arguments[0]["accessKey"];
            sauceOptions.Add("username", username);
            sauceOptions.Add("accessKey", accessKey);
        }

        if (UseTunnel)
        {
            sauceOptions.Add("tunnelIdentifier", TunnelId);
            sauceOptions.Add("parentTunnel", ParentTunnel);
        }

        driverOptions.PlatformName = Platform;
        driverOptions.BrowserVersion = BrowserVersion;

        switch (Browser)
        {
            // By default, all capabilities added to the browser specific implementation
            // of these DriverOptions is added as a subcapability of that browser's options.
            // Sauce Labs requires sauce:options configuration be at the top level of the capabilities
            case BrowserType.Chrome:
            case BrowserType.ChromeHeadless:
            case BrowserType.Firefox:
            case BrowserType.FirefoxHeadless:
            case BrowserType.InternetExplorer:
            case BrowserType.Opera:
                driverOptions.AddAdditionalOption("sauce:options", sauceOptions);
                break;

            default:
                driverOptions.AddAdditionalOption("sauce:options", sauceOptions);
                break;
        }

        return driverOptions;
    }
}