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
using Bellatrix.LLM;
using Bellatrix.LLM.Plugins;
using Bellatrix.Playwright.LLM.Plugins;
using Bellatrix.Playwright.Locators;
using Microsoft.Identity.Client;
using Microsoft.SemanticKernel;
using Pipelines.Sockets.Unofficial.Arenas;
using System.Text.RegularExpressions;
using System.Threading;

namespace Bellatrix.Playwright.LLM;

public class FindByPrompt : FindStrategy
{
    private readonly bool _tryResolveFromPages;
    /// <summary>
    /// Initializes a new instance of the <see cref="FindByPrompt"/> class with the specified prompt value.
    /// </summary>
    /// <param name="value">The natural language prompt used to locate the element.</param>
    public FindByPrompt(string value, bool tryResolveFromPages = true) : base(value)
    {
        _tryResolveFromPages = tryResolveFromPages;
    }

    public override WebElement Resolve(BrowserPage searchContext)
    {
        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var location = searchContext.Url;
        var resolved = TryResolveLocator(location, snapshotProvider);
        return resolved.Resolve(searchContext);
    }

    public override WebElement Resolve(WebElement searchContext)
    {
        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var location = ServicesCollection.Current.Resolve<BrowserPage>().Url;
        var resolved = TryResolveLocator(location, snapshotProvider);
        return resolved.Resolve(searchContext);
    }

    private FindStrategy TryResolveLocator(string location, IViewSnapshotProvider snapshotProvider)
    {
        if (_tryResolveFromPages)
        {
            var ragLocator = TryResolveFromPageObjectMemory(Value);
            if (ragLocator != null && ragLocator.Resolve(WrappedBrowser.CurrentPage).All().Any())
            {
                Logger.LogInformation($"✅ Using RAG-located element '{ragLocator}' For '${Value}'");
                return ragLocator;
            }
        }

        // Step 2: Try local persistent cache
        Logger.LogInformation("⚠️ RAG-located element not present. Trying cached selectors...");
        var cached = LocatorCacheService.TryGetCached(WrappedBrowser.CurrentPage.Url, Value);

        var strategy = new FindXpathStrategy(cached);
        if (!string.IsNullOrEmpty(cached) && strategy.Resolve(WrappedBrowser.CurrentPage).All().Any())
        {
            Logger.LogInformation("✅ Using cached selector.");
            return strategy;
        }

        // Step 3: Fall back to AI + prompt regeneration
        Logger.LogInformation("⚠️ Cached selector failed or not found. Re-querying AI...");
        LocatorCacheService.Remove(WrappedBrowser.CurrentPage.Url, Value);

        return ResolveViaPromptFallback(location, snapshotProvider);
    }

    private static FindXpathStrategy TryResolveFromPageObjectMemory(string instruction)
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

        return ParsePromptLocatorToStrategy(locatorResult);
    }

    private static FindXpathStrategy ParsePromptLocatorToStrategy(string promptResult)
    {
        if (promptResult == "Unknown")
        {
            return null;
        }

        var parts = Regex.Match(promptResult, @"^\s*xpath\s*=\s*(//.+)$", RegexOptions.IgnoreCase);
        if (!parts.Success)
        {
            throw new ArgumentException($"❌ Invalid format. Expected: xpath=//... but received '{promptResult}'");
        }

        var xpath = parts.Groups[1].Value.Trim();

        return new FindXpathStrategy(xpath);
    }

    private FindStrategy ResolveViaPromptFallback(string location, IViewSnapshotProvider snapshotProvider, int maxAttempts = 3)
    {
        var failedSelectors = new List<string>();

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var summaryJson = snapshotProvider.GetCurrentViewSnapshot();
            var prompt = SemanticKernelService.Kernel
                .InvokeAsync(nameof(LocatorSkill), nameof(LocatorSkill.BuildLocatorPrompt),
                new()
                {
                    ["htmlSummary"] = summaryJson,
                    ["instruction"] = Value,
                    ["failedSelectors"] = failedSelectors
                }).Result.GetValue<string>();

            var result = SemanticKernelService.Kernel.InvokePromptAsync(prompt).Result;
            var rawSelector = result?.GetValue<string>()?.Trim();

            var strategy = new FindXpathStrategy(rawSelector);
            if (!string.IsNullOrWhiteSpace(rawSelector) && strategy.Resolve(WrappedBrowser.CurrentPage).All().Any())
            {
                LocatorCacheService.Update(location, Value, strategy.Value);
                return strategy;
            }

            failedSelectors.Add(rawSelector);
            Logger.LogInformation($"[Attempt {attempt}] Selector failed: {rawSelector}");
            Thread.Sleep(300);
        }

        throw new ArgumentException($"❌ No valid locator found for: {Value}");
    }

    public override string ToString() => $"Prompt = {Value}";
}