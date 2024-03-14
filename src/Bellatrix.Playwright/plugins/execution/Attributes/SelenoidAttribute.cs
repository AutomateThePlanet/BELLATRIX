// <copyright file="SelenoidAttribute.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.plugins.execution.Attributes;
using Bellatrix.Playwright.Plugins.Browser;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SelenoidAttribute : BrowserAttribute, IBrowserOptionsAttribute
{
    private const string DefaultScreenResolution = "1920x1080x24";

    public SelenoidAttribute(
        BrowserChoice browser,
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
        BrowserChoice browser,
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
        BrowserChoice browser,
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
        BrowserChoice browser,
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
        BrowserChoice browser,
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

    public Dictionary<string, object> CreateOptions(MemberInfo memberInfo, Type testClassType)
    {
        // TODO CreateOptions for Selenoid
        return new Dictionary<string, object> { { "browserVersion", BrowserVersion } };
    }
}