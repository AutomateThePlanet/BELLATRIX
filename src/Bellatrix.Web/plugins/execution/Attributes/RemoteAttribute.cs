// <copyright file="RemoteAttribute.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.Reflection;
using Bellatrix.Web.Enums;
using Bellatrix.Web.Plugins.Browser;
using OpenQA.Selenium;

namespace Bellatrix.Web;

[DebuggerDisplay("BELLATRIX RemoteAttribute")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RemoteAttribute : BrowserAttribute, IDriverOptionsAttribute
{
    public RemoteAttribute(BrowserType browser, string browserVersion, PlatformType platform, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
    : base(browser, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        PlatformType = platform;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserType browser, string browserVersion, PlatformType platform, int width, int height, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, width, height, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        PlatformType = platform;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserType browser, string browserVersion, PlatformType platform, MobileWindowSize mobileWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, mobileWindowSize, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        PlatformType = platform;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserType browser, string browserVersion, PlatformType platform, TabletWindowSize tabletWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, tabletWindowSize, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        PlatformType = platform;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserType browser, string browserVersion, PlatformType platform, DesktopWindowSize desktopWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, desktopWindowSize, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        PlatformType = platform;
        ExecutionType = ExecutionType.Grid;
    }

    public string BrowserVersion { get; }

    public PlatformType PlatformType { get; }

    public dynamic CreateOptions(MemberInfo memberInfo, Type testClassType)
    {
        var platform = new Platform(PlatformType);
        var driverOptions = GetDriverOptionsBasedOnBrowser(testClassType);
        driverOptions.BrowserVersion = BrowserVersion;
        driverOptions.PlatformName = platform.ToString();
        AddAdditionalCapabilities(testClassType, driverOptions);

        return driverOptions;
    }
}