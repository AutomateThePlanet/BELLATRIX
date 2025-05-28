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
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System;
using Bellatrix.LLM;
using Bellatrix.Mobile.Locators;
using OpenQA.Selenium.Appium.Android;
using Bellatrix.Mobile.Locators.Android;
using Bellatrix.LLM.Plugins;
using Bellatrix.Mobile.Services.Android;
using Bellatrix.Mobile.LLM.Skills;

namespace Bellatrix.Mobile.LLM.Android;

/// <summary>
/// A BELLATRIX FindStrategy for Android that uses Semantic Kernel to locate elements by natural language prompts.
/// Primary resolution attempts to match known PageObject locators via Retrieval-Augmented Generation (RAG).
/// Fallback resolution builds a fresh UIAutomator string from the current view snapshot via prompt.
/// </summary>
public class FindByPrompt : FindStrategy<AndroidDriver, AppiumElement>
{
    private bool _tryResolveFromPages = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="FindByPrompt"/> class with the specified prompt value.
    /// </summary>
    /// <param name="value">The natural language prompt used to locate the element.</param>
    /// <param name="tryResolveFromPages">If true, will try to resolve using RAG memory first.</param>
    public FindByPrompt(string value, bool tryResolveFromPages = true) : base(value)
    {
        _tryResolveFromPages = tryResolveFromPages;
    }

    /// <summary>
    /// Locates a single AppiumElement using the resolved UIAutomator strategy.
    /// </summary>
    public override AppiumElement FindElement(AndroidDriver driver)
    {
        var location = driver.CurrentActivity;
        var locator = ResolveLocator(location, driver);
        return new FindAndroidUIAutomatorStrategy(locator).FindElement(driver);
    }

    /// <summary>
    /// Locates all matching AppiumElements using the resolved UIAutomator strategy.
    /// </summary>
    public override IEnumerable<AppiumElement> FindAllElements(AndroidDriver driver)
    {
        var location = driver.CurrentActivity;
        var locator = ResolveLocator(location, driver);
        return new FindAndroidUIAutomatorStrategy(locator).FindAllElements(driver);
    }

    /// <summary>
    /// Locates a single AppiumElement in the context of a parent element.
    /// </summary>
    public override AppiumElement FindElement(AppiumElement element)
    {
        var appService = ServicesCollection.Current.Resolve<AndroidAppService>();
        var location = appService.CurrentActivity;
        var locator = ResolveLocator(location, element);
        return new FindAndroidUIAutomatorStrategy(locator).FindElement(element);
    }

    /// <summary>
    /// Locates all matching AppiumElements in the context of a parent element.
    /// </summary>
    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        var appService = ServicesCollection.Current.Resolve<AndroidAppService>();
        var location = appService.CurrentActivity;
        var locator = ResolveLocator(location, element);
        return new FindAndroidUIAutomatorStrategy(locator).FindAllElements(element);
    }

    /// <summary>
    /// Resolves the UIAutomator locator using RAG memory, cache, or fallback prompt regeneration.
    /// </summary>
    private string ResolveLocator(string location, object context)
    {
        // Step 1: Try RAG memory (PageObjects index)
        if (_tryResolveFromPages)
        {
            var ragLocator = TryResolveFromPageObjectMemory(Value, context);
            if (!string.IsNullOrEmpty(ragLocator) && IsElementPresent(context, ragLocator))
            {
                Logger.LogInformation($"✅ Using RAG-located element '{ragLocator}' For '{Value}'");
                return ragLocator;
            }
        }

        // Step 2: Try local persistent cache
        Logger.LogInformation("⚠️ RAG-located element not present. Trying cached selectors...");
        var cached = LocatorCacheService.TryGetCached(location, Value);
        if (!string.IsNullOrEmpty(cached) && IsElementPresent(context, cached))
        {
            Logger.LogInformation("✅ Using cached selector.");
            return cached;
        }

        // Step 3: Fall back to AI + prompt regeneration
        Logger.LogInformation("⚠️ Cached selector failed or not found. Re-querying AI...");
        LocatorCacheService.Remove(location, Value);

        return ResolveViaPromptFallback(location, context);
    }

    /// <summary>
    /// Attempts to resolve a locator from the PageObject memory using Retrieval-Augmented Generation (RAG).
    /// </summary>
    private string TryResolveFromPageObjectMemory(string instruction, object context)
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

        return ParsePromptLocatorToUIAutomator(locatorResult);
    }

    /// <summary>
    /// Attempts to resolve a locator by generating a prompt to the AI, retrying up to <paramref name="maxAttempts"/> times.
    /// Caches successful selectors for future use.
    /// </summary>
    private string ResolveViaPromptFallback(string location, object context, int maxAttempts = 3)
    {
        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var summaryJson = snapshotProvider.GetCurrentViewSnapshot();
        var failedSelectors = new List<string>();

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var prompt = SemanticKernelService.Kernel.InvokeAsync(nameof(AndroidLocatorSkill), nameof(AndroidLocatorSkill.BuildLocatorPrompt), new()
            {
                ["viewSummaryJson"] = summaryJson,
                ["instruction"] = Value,
                ["failedSelectors"] = failedSelectors
            }).Result.GetValue<string>();

            var result = SemanticKernelService.Kernel.InvokePromptAsync(prompt).Result;
            var rawSelector = result?.GetValue<string>()?.Trim();

            if (string.IsNullOrWhiteSpace(rawSelector))
                continue;

            if (IsElementPresent(context, rawSelector))
            {
                LocatorCacheService.Update(location, Value, rawSelector);
                Logger.LogInformation($"🧠 Caching new selector for '{Value}': {rawSelector}");
                return rawSelector;
            }

            failedSelectors.Add(rawSelector);
            Logger.LogInformation($"[Attempt {attempt}] Selector failed: {rawSelector}");
            Thread.Sleep(300);
        }

        throw new InvalidOperationException($"❌ No valid Android UIAutomator locator found for: {Value}");
    }

    /// <summary>
    /// Parses a prompt result string into a UIAutomator string (expects 'uiautomator=new UiScrollable(...)').
    /// </summary>
    private static string ParsePromptLocatorToUIAutomator(string promptResult)
    {
        if (promptResult == "Unknown")
            return null;

        var match = Regex.Match(promptResult, @"^uiautomator\s*=\s*(new UiScrollable\(.*\))", RegexOptions.IgnoreCase);
        if (!match.Success)
        {
            throw new ArgumentException($"❌ Invalid format. Expected: uiautomator=new UiScrollable(...) but received '{promptResult}'");
        }

        return match.Groups[1].Value.Trim();
    }

    /// <summary>
    /// Checks if any elements are present for the given UIAutomator string in the provided context.
    /// </summary>
    private static bool IsElementPresent(object context, string uiautomator)
    {
        try
        {
            if (context is AndroidDriver driver)
                return driver.FindElements(MobileBy.AndroidUIAutomator(uiautomator)).Any();
            if (context is AppiumElement element)
                return element.FindElements(MobileBy.AndroidUIAutomator(uiautomator)).Any();
            return false;
        }
        catch { return false; }
    }

    public override string ToString() => $"Prompt = {Value}";
}
