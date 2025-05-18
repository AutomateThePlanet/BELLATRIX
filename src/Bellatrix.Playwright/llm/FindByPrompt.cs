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
using System.Text.RegularExpressions;
using System.Threading;
using Bellatrix.LLM;
using Bellatrix.Playwright.Locators;

namespace Bellatrix.Playwright.LLM;

public class FindByPrompt : FindStrategy
{
    public FindByPrompt(string value) : base(value) { }

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
        // Try from memory
        var match = SemanticKernelService.Memory
            .SearchAsync(Value, index: "PageObjects", limit: 1)
            .Result.Results.FirstOrDefault();

        if (match != null)
        {
            var pageSummary = match.Partitions.FirstOrDefault()?.Text ?? "";
            var mappedPrompt = SemanticKernelService.Kernel
                .InvokeAsync("Mapper", "MatchPromptToKnownLocator", new()
                {
                    ["pageSummary"] = pageSummary,
                    ["instruction"] = Value
                }).Result.GetValue<string>();

            var locator = ExtractStrategy(mappedPrompt);
            if (locator != null)
            {
                return locator;
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
                .InvokeAsync("LocatorSkill", "BuildLocatorPrompt", new()
                {
                    ["htmlSummary"] = summaryJson,
                    ["instruction"] = Value,
                    ["failedSelectors"] = failedSelectors
                }).Result.GetValue<string>();

            var result = SemanticKernelService.Kernel
                .InvokePromptAsync(prompt).Result;
            var raw = result?.GetValue<string>()?.Trim();

            if (!string.IsNullOrWhiteSpace(raw))
            {
                var locator = ExtractStrategy(raw);
                if (locator != null)
                {
                    LocatorCacheService.Update(location, Value, locator.Value);
                    return locator;
                }

                failedSelectors.Add(raw);
            }

            Thread.Sleep(300);
        }

        throw new ArgumentException($"❌ No valid locator found for: {Value}");
    }

    private FindStrategy ExtractStrategy(string promptResult)
    {
        var match = Regex.Match(promptResult, @"^\s*(css|xpath)=([\s\S]+)", RegexOptions.IgnoreCase);
        if (!match.Success)
        {
            return null;
        }

        var type = match.Groups[1].Value.Trim().ToLowerInvariant();
        var locator = match.Groups[2].Value.Trim();

        return type switch
        {
            "css" => new FindCssStrategy(locator),
            "xpath" => new FindXpathStrategy(locator),
            _ => null
        };
    }

    public override string ToString() => $"Prompt = {Value}";
}
