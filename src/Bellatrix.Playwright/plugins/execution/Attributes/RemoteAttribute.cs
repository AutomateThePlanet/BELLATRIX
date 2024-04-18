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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Diagnostics;
using System.Reflection;
using Bellatrix.Playwright.Enums;
using Bellatrix.Playwright.plugins.execution.Attributes;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright;

[DebuggerDisplay("BELLATRIX RemoteAttribute")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RemoteAttribute : BrowserAttribute, IBrowserOptionsAttribute
{
    public RemoteAttribute(BrowserTypes browser, string browserVersion, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
    : base(browser, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserTypes browser, string browserVersion, int width, int height, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, width, height, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserTypes browser, string browserVersion, MobileWindowSize mobileWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, mobileWindowSize, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserTypes browser, string browserVersion, TabletWindowSize tabletWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, tabletWindowSize, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        ExecutionType = ExecutionType.Grid;
    }

    public RemoteAttribute(BrowserTypes browser, string browserVersion, DesktopWindowSize desktopWindowSize, Lifecycle behavior = Lifecycle.NotSet, bool shouldAutomaticallyScrollToVisible = true)
        : base(browser, desktopWindowSize, behavior, shouldAutomaticallyScrollToVisible)
    {
        BrowserVersion = browserVersion;
        ExecutionType = ExecutionType.Grid;
    }

    public string BrowserVersion { get; }

    public Dictionary<string, object> CreateOptions(MemberInfo memberInfo, Type testClassType)
    {
        return new Dictionary<string, object> { { "browserVersion", BrowserVersion } };
    }
}