// <copyright file="BrowserAttribute.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright;

[DebuggerDisplay("BELLATRIX BrowserAttribute")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class BrowserAttribute : Attribute
{
    public BrowserAttribute(BrowserChoice browser, ExecutionType executionType = ExecutionType.Regular, Lifecycle lifecycle = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    {
        OS = OS.Windows;
        Browser = browser;
        Lifecycle = lifecycle;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        Size = default;
        ExecutionType = executionType;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible && ConfigurationService.GetSection<WebSettings>().ShouldAutomaticallyScrollToVisible;
    }

    public BrowserAttribute(BrowserChoice browser, Lifecycle lifecycle = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
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

    public BrowserAttribute(BrowserChoice browser, int width, int height, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
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

    public BrowserAttribute(OS oS, BrowserChoice browser, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
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

    public BrowserAttribute(OS oS, BrowserChoice browser, int width, int height, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
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

    public BrowserAttribute(BrowserChoice browser, MobileWindowSize mobileWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    : this(browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(mobileWindowSize);

    public BrowserAttribute(BrowserChoice browser, TabletWindowSize tabletWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    : this(browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(tabletWindowSize);

    public BrowserAttribute(BrowserChoice browser, DesktopWindowSize desktopWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
    : this(browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(desktopWindowSize);

    public BrowserAttribute(OS oS, BrowserChoice browser, MobileWindowSize mobileWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
        : this(oS, browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(mobileWindowSize);

    public BrowserAttribute(OS oS, BrowserChoice browser, TabletWindowSize tabletWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
        : this(oS, browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(tabletWindowSize);

    public BrowserAttribute(OS oS, BrowserChoice browser, DesktopWindowSize desktopWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true, bool shouldCaptureHttpTraffic = false, bool shouldDisableJavaScript = false)
        : this(oS, browser, behavior, shouldCaptureHttpTraffic, shouldDisableJavaScript)
        => Size = WindowsSizeResolver.GetWindowSize(desktopWindowSize);

    public BrowserChoice Browser { get; }

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
        string testFullName = $"{testClassType.FullName}.{memberInfo.Name}";
        string testName = testFullName != null ? testFullName.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(",", string.Empty).Replace("\"", string.Empty) : testClassType.FullName;
        return testName;
    }
}