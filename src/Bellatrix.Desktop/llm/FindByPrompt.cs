// <copyright file="FindByPrompt.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>
using Bellatrix.Desktop.LLM.Plugins;
using Bellatrix.Desktop.Locators;
using Bellatrix.LLM;
using Bellatrix.LLM.Plugins;
using Microsoft.SemanticKernel;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Bellatrix.Desktop.LLM;

/// <summary>
/// A BELLATRIX FindStrategy for Desktop that uses Semantic Kernel to locate elements by natural language prompts.
/// Primary resolution attempts to match known PageObject locators via Retrieval-Augmented Generation (RAG).
/// Fallback resolution builds fresh XPath from the current view snapshot via prompt.
/// </summary>
public class FindByPrompt : FindStrategy
{
    private bool _tryResolveFromPages = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="FindByPrompt"/> class with the specified prompt value.
    /// </summary>
    /// <param name="value">The natural language prompt used to locate the element.</param>
    /// <param name="tryResolveFromPages">If true, try to resolve from RAG memory first.</param>
    public FindByPrompt(string value, bool tryResolveFromPages = true) : base(value)
    {
        _tryResolveFromPages = tryResolveFromPages;
    }

    /// <summary>
    /// Locates a single WindowsElement using the resolved XPath.
    /// </summary>
    public override WindowsElement FindElement(WindowsDriver<WindowsElement> driver)
    {
        var location = driver.CurrentWindowHandle;
        var xpath = ResolveLocator(location, driver);
        return driver.FindElementByXPath(xpath);
    }

    /// <summary>
    /// Locates all matching WindowsElements using the resolved XPath.
    /// </summary>
    public override IEnumerable<WindowsElement> FindAllElements(WindowsDriver<WindowsElement> driver)
    {
        var location = driver.CurrentWindowHandle;
        var xpath = ResolveLocator(location, driver);
        return driver.FindElementsByXPath(xpath);
    }

    /// <summary>
    /// Locates a single AppiumWebElement in the context of a parent element using the resolved XPath.
    /// </summary>
    public override AppiumWebElement FindElement(WindowsElement element)
    {
        var location = element.WrappedDriver.CurrentWindowHandle;
        var xpath = ResolveLocator(location, element.WrappedDriver as WindowsDriver<WindowsElement>);
        return element.FindElementByXPath(xpath);
    }

    /// <summary>
    /// Locates all matching AppiumWebElements in the context of a parent element using the resolved XPath.
    /// </summary>
    public override IEnumerable<AppiumWebElement> FindAllElements(WindowsElement element)
    {
        var location = element.WrappedDriver.CurrentWindowHandle;
        var xpath = ResolveLocator(location, element.WrappedDriver as WindowsDriver<WindowsElement>);
        return element.FindElementsByXPath(xpath);
    }

    /// <summary>
    /// Resolves the XPath locator using RAG memory, local cache, or fallback prompt regeneration.
    /// </summary>
    /// <param name="location">Desktop app window handle or location identifier.</param>
    /// <param name="driver">WindowsDriver for presence checking.</param>
    /// <returns>A valid XPath string.</returns>
    private string ResolveLocator(string location, WindowsDriver<WindowsElement> driver)
    {
        // Step 1: Try RAG memory (PageObjects index)
        if (_tryResolveFromPages)
        {
            var ragLocator = TryResolveFromPageObjectMemory(Value, driver);
            if (!string.IsNullOrEmpty(ragLocator) && IsElementPresent(driver, ragLocator))
            {
                Logger.LogInformation($"✅ Using RAG-located element '{ragLocator}' For '{Value}'");
                return ragLocator;
            }
        }

        // Step 2: Try local persistent cache
        Logger.LogInformation("⚠️ RAG-located element not present. Trying cached selectors...");
        var cached = LocatorCacheService.TryGetCached(location, Value);
        if (!string.IsNullOrEmpty(cached) && IsElementPresent(driver, cached))
        {
            Logger.LogInformation("✅ Using cached selector.");
            return cached;
        }

        // Step 3: Fall back to AI + prompt regeneration
        Logger.LogInformation("⚠️ Cached selector failed or not found. Re-querying AI...");
        LocatorCacheService.Remove(location, Value);

        return ResolveViaPromptFallback(location, driver);
    }

    /// <summary>
    /// Attempts to resolve a locator from the PageObject memory using Retrieval-Augmented Generation (RAG).
    /// </summary>
    /// <param name="instruction">The natural language instruction for the element.</param>
    /// <param name="driver">WindowsDriver for presence checking.</param>
    /// <returns>XPath string if found; otherwise null.</returns>
    private string TryResolveFromPageObjectMemory(string instruction, WindowsDriver<WindowsElement> driver)
    {
        var match = SemanticKernelService.Memory
            .SearchAsync(instruction, index: "PageObjects", limit: 1)
            .Result.Results.FirstOrDefault();

        if (match == null) return null;

        var pageSummary = match.Partitions.FirstOrDefault()?.Text ?? string.Empty;
        var mappedPrompt = SemanticKernelService.Kernel
            .InvokeAsync(nameof(LocatorMapperSkill), nameof(LocatorMapperSkill.MatchPromptToKnownLocator),
                new()
                {
                    ["pageSummary"] = pageSummary,
                    ["instruction"] = instruction
                }).Result.GetValue<string>();

        var locatorResult = SemanticKernelService.Kernel
            .InvokePromptAsync(mappedPrompt).Result.GetValue<string>();

        return ParsePromptLocatorToXPath(locatorResult);
    }

    /// <summary>
    /// Attempts to resolve a locator by generating a prompt to the AI, retrying up to <paramref name="maxAttempts"/> times.
    /// Caches successful selectors for future use.
    /// </summary>
    /// <param name="location">Desktop app window handle or location identifier.</param>
    /// <param name="driver">WindowsDriver for presence checking.</param>
    /// <param name="maxAttempts">Maximum number of attempts to generate a working selector.</param>
    /// <returns>XPath string if found; otherwise throws InvalidOperationException.</returns>
    private string ResolveViaPromptFallback(string location, WindowsDriver<WindowsElement> driver, int maxAttempts = 3)
    {
        var viewSnapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var failedSelectors = new List<string>();

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var summaryJson = viewSnapshotProvider.GetCurrentViewSnapshot();
            var prompt = SemanticKernelService.Kernel?.InvokeAsync(nameof(LocatorSkill), nameof(LocatorSkill.BuildLocatorPrompt),
                new()
                {
                    ["viewSummaryJson"] = summaryJson,
                    ["instruction"] = Value,
                    ["failedSelectors"] = failedSelectors
                }).Result.GetValue<string>();

            var result = SemanticKernelService.Kernel?.InvokePromptAsync(prompt).Result;
            var rawSelector = result?.GetValue<string>()?.Trim();

            if (string.IsNullOrWhiteSpace(rawSelector))
                continue;

            if (IsElementPresent(driver, rawSelector))
            {
                LocatorCacheService.Update(location, Value, rawSelector);
                Logger.LogInformation($"🧠 Caching new selector for '{Value}': {rawSelector}");
                return rawSelector;
            }

            failedSelectors.Add(rawSelector);
            Logger.LogInformation($"[Attempt {attempt}] Selector failed: {rawSelector}");
            Thread.Sleep(300);
        }

        throw new InvalidOperationException($"❌ No element found for instruction: {Value}");
    }

    /// <summary>
    /// Parses a prompt result string into an XPath string (expects 'xpath=//...').
    /// </summary>
    /// <param name="promptResult">Prompt result, e.g., "xpath=//div[@id='main']".</param>
    /// <returns>XPath string if format is valid; otherwise null.</returns>
    private static string ParsePromptLocatorToXPath(string promptResult)
    {
        if (promptResult == "Unknown")
            return null;

        var parts = Regex.Match(promptResult, @"^\s*xpath\s*=\s*(//.+)$", RegexOptions.IgnoreCase);
        if (!parts.Success)
        {
            throw new ArgumentException($"❌ Invalid format. Expected: xpath=//... but received '{promptResult}'");
        }

        return parts.Groups[1].Value.Trim();
    }

    /// <summary>
    /// Checks if any elements are present for the given XPath on the driver.
    /// </summary>
    /// <param name="driver">The WindowsDriver instance.</param>
    /// <param name="xpath">The XPath locator to check.</param>
    /// <returns>true if at least one element is found; otherwise false.</returns>
    private static bool IsElementPresent(WindowsDriver<WindowsElement> driver, string xpath)
    {
        try { return driver.FindElementsByXPath(xpath).Any(); }
        catch { return false; }
    }

    public override string ToString() => $"Prompt = {Value}";
}