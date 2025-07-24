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
    private bool _tryResolveFromPages = true;
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
            // Try from memory
            var match = SemanticKernelService.Memory
            .SearchAsync(Value, index: "PageObjects", limit: 1)
            .Result.Results.FirstOrDefault();

            if (match != null)
            {
                var pageSummary = match.Partitions.FirstOrDefault()?.Text ?? "";
                var mappedPrompt = SemanticKernelService.Kernel
                    .InvokeAsync(nameof(LocatorMapperSkill), nameof(LocatorMapperSkill.MatchPromptToKnownLocator), new()
                    {
                        ["pageSummary"] = pageSummary,
                        ["instruction"] = Value
                    }).Result.GetValue<string>();

                var result = SemanticKernelService.Kernel.InvokePromptAsync(mappedPrompt).Result;
                var rawLocator = result?.GetValue<string>()?.Trim();
                var ragLocator = new FindXpathStrategy(rawLocator);
                if (ragLocator != null)
                {
                    Logger.LogInformation($"✅ Using RAG-located element '{ragLocator}' For '${Value}'");
                    return ragLocator;
                }
            }
        }

        // Try cache
        var cached = LocatorCacheService.TryGetCached(location, Value);
        if (!string.IsNullOrEmpty(cached))
        {
            return new FindXpathStrategy(cached);
        }

        // Remove broken and fall back
        LocatorCacheService.Remove(location, Value);
        return ResolveViaPromptFallback(location, snapshotProvider);
    }

    private FindStrategy ResolveViaPromptFallback(string location, IViewSnapshotProvider snapshotProvider, int maxAttempts = 3)
    {
        var summaryJson = snapshotProvider.GetCurrentViewSnapshot();
        var failedSelectors = new List<string>();

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
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

            if (!string.IsNullOrWhiteSpace(rawSelector))
            {
                var strategy = new FindXpathStrategy(rawSelector);
                try
                {
                    _ = strategy.Resolve(WrappedBrowser.CurrentPage).WrappedLocator.First.IsVisibleAsync().Result;
                    LocatorCacheService.Update(location, Value, strategy.Value);
                    return strategy;
                }
                catch (PlaywrightException)
                {
                    // continue
                }
            }

            failedSelectors.Add(rawSelector);
            Logger.LogInformation($"[Attempt {attempt}] Selector failed: {rawSelector}");
            Thread.Sleep(300);
        }

        throw new ArgumentException($"❌ No valid locator found for: {Value}");
    }

    public override string ToString() => $"Prompt = {Value}";
}