﻿// <copyright file="BrowserType.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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

namespace Bellatrix.Playwright.SyncPlaywright;

/// <summary>
/// Synchronous wrapper for Playwright IBrowserType.
/// </summary>
public class BrowserType
{
    internal BrowserType(IBrowserType browserType)
    {
        WrappedBrowserType = browserType;
    }

    public IBrowserType WrappedBrowserType { get; internal init; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string ExecutablePath => WrappedBrowserType.ExecutablePath;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string Name => WrappedBrowserType.Name;

    public BellatrixBrowser Connect(string wsEndpoint, BrowserTypeConnectOptions options = null)
    {
        return new BellatrixBrowser(this, WrappedBrowserType.ConnectAsync(wsEndpoint, options).Result);
    }

    public BellatrixBrowser ConnectOverCDP(string endpointURL, BrowserTypeConnectOverCDPOptions options = null)
    {
        return new BellatrixBrowser(this, WrappedBrowserType.ConnectOverCDPAsync(endpointURL, options).Result);
    }

    public BellatrixBrowser Launch(BrowserTypeLaunchOptions options = null)
    {
        return new BellatrixBrowser(this, WrappedBrowserType.LaunchAsync(options).Result);
    }

    public BrowserContext LaunchPersistentContext(string userDataDir, BrowserTypeLaunchPersistentContextOptions options = null)
    {
        var persistentContext = WrappedBrowserType.LaunchPersistentContextAsync(userDataDir, options).Result;

        return new BrowserContext(new BellatrixBrowser(this, persistentContext.Browser), persistentContext);
    }
}
