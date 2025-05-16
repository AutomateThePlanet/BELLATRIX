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
using Microsoft.SemanticKernel;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System;
using Bellatrix.LLM;
using System.Linq;
using Bellatrix.Web.LLM.Plugins;

namespace Bellatrix.Web.LLM;

/// <summary>
/// A BELLATRIX FindStrategy that uses Semantic Kernel to locate elements by natural language prompts.
/// Primary resolution attempts to match known PageObject locators via Retrieval-Augmented Generation (RAG).
/// Fallback resolution builds fresh XPath from current DOM snapshot via prompt.
/// </summary>
public class FindByPrompt : FindStrategy
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FindByPrompt"/> class with the specified prompt value.
    /// </summary>
    /// <param name="value">The natural language prompt used to locate the element.</param>
    public FindByPrompt(string value) : base(value) { }

    /// <summary>
    /// Converts the natural language prompt to a Selenium <see cref="By"/> locator.
    /// Tries to resolve using RAG memory, then cache, and finally falls back to AI prompt regeneration.
    /// </summary>
    /// <returns>A <see cref="By"/> locator for the element matching the prompt.</returns>
    /// <exception cref="NotFoundException">Thrown if no element can be found for the prompt.</exception>
    public override By Convert()
    {
        var driver = ServicesCollection.Current.Resolve<IWebDriver>();

        // Step 1: Try to match from RAG memory
        var ragLocator = TryResolveFromPageObjectMemory(driver, Value);
        if (ragLocator != null && IsElementPresent(driver, ragLocator))
        {
            return ragLocator;
        }

        // Step 2: Try local persistent cache
        Logger.LogInformation($"⚠️ RAG-located element not present. Trying cached selectors...");
        var cached = LocatorCacheService.TryGetCached(driver.Url, Value);
        if (!string.IsNullOrEmpty(cached) && IsElementPresent(driver, By.XPath(cached)))
        {
            Logger.LogInformation($"✅ Using cached selector.");
            return By.XPath(cached);
        }

        // Step 3: Fall back to AI + prompt regeneration
        Logger.LogInformation($"⚠️ Cached selector failed or not found. Re-querying AI...");
        LocatorCacheService.Remove(driver.Url, Value);

        return ResolveViaPromptFallback(driver, Value);
    }

    /// <summary>
    /// Attempts to resolve a locator by generating a prompt to the AI, retrying up to <paramref name="maxAttempts"/> times.
    /// Caches successful selectors for future use.
    /// </summary>
    /// <param name="driver">The Selenium WebDriver instance.</param>
    /// <param name="instruction">The natural language instruction for the element.</param>
    /// <param name="maxAttempts">Maximum number of attempts to generate a working selector.</param>
    /// <returns>A <see cref="By"/> locator if found; otherwise, throws <see cref="NotFoundException"/>.</returns>
    /// <exception cref="NotFoundException">Thrown if no element can be found after all attempts.</exception>
    private By ResolveViaPromptFallback(IWebDriver driver, string instruction, int maxAttempts = 3)
    {
        var viewSnapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var summaryJson = viewSnapshotProvider.GetCurrentViewSnapshot();
        var failedSelectors = new List<string>();

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var prompt = SemanticKernelService.Kernel?.InvokeAsync(nameof(LocatorSkill), nameof(LocatorSkill.BuildLocatorPrompt), 
            new()
            {
                ["htmlSummary"] = summaryJson,
                ["instruction"] = instruction,
                ["failedSelectors"] = failedSelectors
            }).Result.GetValue<string>();

            var result = SemanticKernelService.Kernel?.InvokePromptAsync(prompt).Result;
            var rawSelector = result?.GetValue<string>()?.Trim();

            if (string.IsNullOrWhiteSpace(rawSelector))
                continue;

            var by = By.XPath(rawSelector);
            if (IsElementPresent(driver, by))
            {
                LocatorCacheService.Update(driver.Url, instruction, rawSelector);
                Logger.LogInformation($"🧠 Caching new selector for '{instruction}': {rawSelector}");
                return by;
            }

            failedSelectors.Add(rawSelector);
            Logger.LogInformation($"[Attempt {attempt}] Selector failed: {rawSelector}");
            Thread.Sleep(300);
        }

        throw new NotFoundException($"❌ No element found for instruction: {instruction}");
    }

    /// <summary>
    /// Attempts to resolve a locator from the PageObject memory using Retrieval-Augmented Generation (RAG).
    /// </summary>
    /// <param name="driver">The Selenium WebDriver instance.</param>
    /// <param name="instruction">The natural language instruction for the element.</param>
    /// <returns>
    /// A <see cref="By"/> locator if a match is found in memory; otherwise, <c>null</c>.
    /// </returns>
    private By TryResolveFromPageObjectMemory(IWebDriver driver, string instruction)
    {
        var match = SemanticKernelService.Memory
            .SearchAsync(instruction, index: "PageObjects", limit: 1)
            .Result.Results.FirstOrDefault();

        if (match == null) return null;

        var pageSummary = match.Partitions.FirstOrDefault()?.Text ?? string.Empty;
        var mappedPrompt = SemanticKernelService.Kernel
            .InvokeAsync("Mapper", "MatchPromptToKnownLocator", new()
            {
                ["pageSummary"] = pageSummary,
                ["instruction"] = instruction
            }).Result.GetValue<string>();

        var locatorResult = SemanticKernelService.Kernel
            .InvokePromptAsync(mappedPrompt).Result.GetValue<string>();

        return ParsePromptLocatorToBy(locatorResult);
    }

    /// <summary>
    /// Parses a prompt result string into a Selenium <see cref="By"/> locator.
    /// </summary>
    /// <param name="promptResult">The prompt result string, e.g., "xpath=//div[@id='main']".</param>
    /// <returns>
    /// A <see cref="By"/> locator if parsing is successful; otherwise, <c>null</c>.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown if the selector type is unsupported.</exception>
    private static By ParsePromptLocatorToBy(string promptResult)
    {
        var parts = Regex.Match(promptResult, @"^\s*xpath\s*=\s*(//.+)$", RegexOptions.IgnoreCase);
        if (!parts.Success)
        {
            throw new ArgumentException($"❌ Invalid format. Expected: xpath=//... but received '{promptResult}'");
        }

        var xpath = parts.Groups[1].Value.Trim();

        return By.XPath(xpath);
    }

    /// <summary>
    /// Checks if any elements are present for the given <see cref="By"/> locator.
    /// </summary>
    /// <param name="driver">The Selenium WebDriver instance.</param>
    /// <param name="by">The locator to check.</param>
    /// <returns><c>true</c> if at least one element is found; otherwise, <c>false</c>.</returns>
    private static bool IsElementPresent(IWebDriver driver, By by)
    {
        try { return driver.FindElements(by).Any(); }
        catch { return false; }
    }

    /// <summary>
    /// Returns a string representation of the FindByPrompt strategy.
    /// </summary>
    /// <returns>A string describing the prompt value.</returns>
    public override string ToString() => $"Prompt = {Value}";
}
