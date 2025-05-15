﻿// <copyright file="BellatrixPlaywright.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Playwright.SyncPlaywright;

/// <summary>
/// Synchronous wrapper for Playwright.
/// </summary>
public class BellatrixPlaywright
{
    public BellatrixPlaywright(IPlaywright playwright)
    {
        WrappedPlaywright = playwright;
    }

    public IPlaywright WrappedPlaywright { get; internal init; }

    public BrowserType this[string browserType] => new BrowserType(WrappedPlaywright[browserType]);

    public BrowserType Chromium => new BrowserType(WrappedPlaywright.Chromium);

    public IReadOnlyDictionary<string, BrowserNewContextOptions> Devices => WrappedPlaywright.Devices;

    public BrowserType Firefox => new BrowserType(WrappedPlaywright.Firefox);

    public IAPIRequest APIRequest => WrappedPlaywright.APIRequest;

    public ISelectors Selectors => WrappedPlaywright.Selectors;

    public BrowserType Webkit => new BrowserType(WrappedPlaywright.Webkit);

    public void Dispose()
    {
        WrappedPlaywright.Dispose();
    }
}
