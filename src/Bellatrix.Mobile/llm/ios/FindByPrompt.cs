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
using Bellatrix.Mobile.Locators.IOS;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Services.Android;
using Bellatrix.Mobile.Services.IOS;
using Bellatrix.LLM.Plugins;
using Bellatrix.Mobile.LLM.Skills.iOS;

namespace Bellatrix.Mobile.LLM.IOS;

public class FindByPrompt : FindStrategy<IOSDriver, AppiumElement>
{
    public FindByPrompt(string value) : base(value) { }

    public override AppiumElement FindElement(IOSDriver driver)
    {
        var appService = ServicesCollection.Current.Resolve<IOSAppService>();
        var location = appService.Title ?? "default";
        var locator = TryResolveLocator(location);
        return new FindIOSNsPredicateStrategy(locator).FindElement(driver);
    }

    public override IEnumerable<AppiumElement> FindAllElements(IOSDriver driver)
    {
        var appService = ServicesCollection.Current.Resolve<IOSAppService>();
        var location = appService.Title ?? "default";
        var locator = TryResolveLocator(location);
        return new FindIOSNsPredicateStrategy(locator).FindAllElements(driver);
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        var appService = ServicesCollection.Current.Resolve<IOSAppService>();
        var location = appService.Title ?? "default";
        var locator = TryResolveLocator(location);
        return new FindIOSNsPredicateStrategy(locator).FindElement(element);
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        var appService = ServicesCollection.Current.Resolve<IOSAppService>();
        var location = appService.Title ?? "default";
        var locator = TryResolveLocator(location);
        return new FindIOSNsPredicateStrategy(locator).FindAllElements(element);
    }

    private string TryResolveLocator(string location)
    {
        var match = SemanticKernelService.Memory
            .SearchAsync(Value, index: "PageObjects", limit: 1)
            .Result.Results.FirstOrDefault();

        if (match != null)
        {
            var pageSummary = match.Partitions.FirstOrDefault()?.Text ?? "";
            var mappedPrompt = SemanticKernelService.Kernel
                .InvokeAsync(nameof(LocatorMapperSkill), nameof(LocatorMapperSkill.MatchPromptToKnownLocator),
                new()
                {
                    ["pageSummary"] = pageSummary,
                    ["instruction"] = Value
                }).Result.GetValue<string>();

            var locator = ExtractNsPredicate(mappedPrompt);
            if (!string.IsNullOrEmpty(locator))
            {
                return locator;
            }
        }

        var cached = LocatorCacheService.TryGetCached(location, Value);
        if (!string.IsNullOrEmpty(cached))
        {
            return cached;
        }

        LocatorCacheService.Remove(location, Value);
        return ResolveViaPromptFallback(location);
    }

    private string ResolveViaPromptFallback(string location, int maxAttempts = 3)
    {
        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var summaryJson = snapshotProvider.GetCurrentViewSnapshot();
        var failedSelectors = new List<string>();

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var prompt = SemanticKernelService.Kernel?.InvokeAsync(nameof(IOSLocatorSkill), nameof(IOSLocatorSkill.BuildLocatorPrompt),
            new()
            {
                ["htmlSummary"] = summaryJson,
                ["instruction"] = Value,
                ["failedSelectors"] = failedSelectors
            }).Result.GetValue<string>();

            var result = SemanticKernelService.Kernel?.InvokePromptAsync(prompt).Result;
            var raw = result?.GetValue<string>()?.Trim();

            if (!string.IsNullOrWhiteSpace(raw))
            {
                var locator = ExtractNsPredicate(raw);
                if (!string.IsNullOrWhiteSpace(locator))
                {
                    LocatorCacheService.Update(location, Value, locator);
                    return locator;
                }

                failedSelectors.Add(raw);
            }

            Thread.Sleep(300);
        }

        throw new InvalidOperationException($"❌ No NSPredicate locator found for: {Value}");
    }

    private string ExtractNsPredicate(string promptResult)
    {
        var parts = Regex.Match(promptResult, @"^nspredicate\s*=\s*(.+)$", RegexOptions.IgnoreCase);
        return parts.Success ? parts.Groups[1].Value.Trim() : null;
    }

    public override string ToString() => $"Prompt = {Value}";
}
