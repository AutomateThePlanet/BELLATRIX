// <copyright file="BrowserAttribute.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Bellatrix.Web.Enums;
using Bellatrix.Web.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;

namespace Bellatrix.Web;

[DebuggerDisplay("BELLATRIX BrowserAttribute")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class BrowserAttribute : Attribute
{
    public BrowserAttribute(BrowserType browser, Lifecycle lifecycle = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    {
        OS = OS.Windows;
        Browser = browser;
        Lifecycle = lifecycle;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        Size = default;
        ExecutionType = ExecutionType.Regular;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible && ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
    }

    public BrowserAttribute(BrowserType browser, int width, int height, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    {
        OS = OS.Windows;
        Browser = browser;
        Lifecycle = behavior;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        Size = new Size(width, height);
        ExecutionType = ExecutionType.Regular;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible && ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
    }

    public BrowserAttribute(OS oS, BrowserType browser, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    {
        OS = oS;
        Browser = browser;
        Lifecycle = behavior;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        Size = default;
        ExecutionType = ExecutionType.Regular;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible && ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
    }

    public BrowserAttribute(OS oS, BrowserType browser, int width, int height, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    {
        OS = oS;
        Browser = browser;
        Lifecycle = behavior;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        Size = new Size(width, height);
        ExecutionType = ExecutionType.Regular;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible && ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
    }

    public BrowserAttribute(BrowserType browser, MobileWindowSize mobileWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    : this(browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(mobileWindowSize);

    public BrowserAttribute(BrowserType browser, TabletWindowSize tabletWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    : this(browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(tabletWindowSize);

    public BrowserAttribute(BrowserType browser, DesktopWindowSize desktopWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    : this(browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(desktopWindowSize);

    public BrowserAttribute(OS oS, BrowserType browser, MobileWindowSize mobileWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
        : this(oS, browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(mobileWindowSize);

    public BrowserAttribute(OS oS, BrowserType browser, TabletWindowSize tabletWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
        : this(oS, browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(tabletWindowSize);

    public BrowserAttribute(OS oS, BrowserType browser, DesktopWindowSize desktopWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
        : this(oS, browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(desktopWindowSize);

    public BrowserType Browser { get; }

    public Lifecycle Lifecycle { get; }

    public Size Size { get; }

    public bool ShouldCaptureHttpTraffic { get; }
    public bool ShouldDisableJavaScript { get; }

    public ExecutionType ExecutionType { get; protected set; }

    public OS OS { get; internal set; }

    public bool ShouldAutomaticallyScrollToVisible { get; }
    public bool IsLighthouseEnabled { get; protected set; }

    protected string GetTestFullName(MemberInfo memberInfo, Type testClassType)
    {
        string testFullName = $"{testClassType.FullName}.{memberInfo?.Name}".Trim('.');
        string testName = testFullName != null ? testFullName.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(",", string.Empty).Replace("\"", string.Empty) : testClassType.FullName;
        return testName;
    }

    // We allow users to set custom capabilities and profiles from the web App class. We register the type by the unique name of the class.
    protected TDriverOptions AddAdditionalCapabilities<TDriverOptions>(Type type, TDriverOptions driverOptions)
        where TDriverOptions : DriverOptions, new()
    {
        var additionalCaps = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{type.FullName}");
        if (additionalCaps != null)
        {
            foreach (var key in additionalCaps.Keys)
            {
                driverOptions.AddAdditionalOption(key, additionalCaps[key]);
            }
        }

        return driverOptions;
    }

    protected dynamic GetDriverOptionsBasedOnBrowser(Type type)
    {
        dynamic driverOptions;
        switch (Browser)
        {
            case BrowserType.Chrome:
            case BrowserType.ChromeHeadless:
                driverOptions = ServicesCollection.Current.Resolve<ChromeOptions>(type.FullName) ?? new ChromeOptions();
                break;
            case BrowserType.Firefox:
            case BrowserType.FirefoxHeadless:
                driverOptions = ServicesCollection.Current.Resolve<FirefoxOptions>(type.FullName) ?? new FirefoxOptions();
                var firefoxProfile = ServicesCollection.Current.Resolve<FirefoxProfile>(type.FullName);

                if (firefoxProfile != null)
                {
                    driverOptions.Profile = firefoxProfile;
                }

                break;
            case BrowserType.InternetExplorer:
                driverOptions = ServicesCollection.Current.Resolve<InternetExplorerOptions>(type.FullName) ?? new InternetExplorerOptions();
                break;
            case BrowserType.Edge:
                driverOptions = ServicesCollection.Current.Resolve<EdgeOptions>(type.FullName) ?? new EdgeOptions();
                break;
            case BrowserType.Safari:
                driverOptions = ServicesCollection.Current.Resolve<SafariOptions>(type.FullName) ?? new SafariOptions();
                break;
            default:
                {
                    throw new ArgumentException("You should specify a browser.");
                }
        }

        return driverOptions;
    }
}