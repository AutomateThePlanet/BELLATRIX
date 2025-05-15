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
using Bellatrix.Web.LLM.Extensions;
using Bellatrix.Web.LLM.services;

namespace Bellatrix.Web.LLM;

/// <summary>
/// A BELLATRIX FindStrategy that uses Semantic Kernel to locate elements by natural language prompts.
/// Primary resolution attempts to match known PageObject locators via RAG.
/// Fallback resolution builds fresh XPath from current DOM snapshot via prompt.
/// </summary>
public class FindByPrompt : FindStrategy
{
    public FindByPrompt(string value) : base(value) { }

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
        var cached = LocatorCacheService.TryGetCached(Value);
        if (cached != null && IsElementPresent(driver, cached))
        {
            Logger.LogInformation($"✅ Using cached selector.");
            return cached;
        }

        // Step 3: Fall back to AI + prompt regeneration
        Logger.LogInformation($"⚠️ Cached selector failed or not found. Re-querying AI...");
        LocatorCacheService.Remove(Value);

        return ResolveViaPromptFallback(driver, Value);
    }

    private By ResolveViaPromptFallback(IWebDriver driver, string instruction, int maxAttempts = 3)
    {
        var browser = ServicesCollection.Current.Resolve<BrowserService>();
        var summaryJson = browser.GetPageSummaryJson();
        var failedSelectors = new List<string>();

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var prompt = SemanticKernelService.Kernel?.InvokeAsync("Locator", "BuildLocatorPrompt", new()
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
                LocatorCacheService.Update(instruction, rawSelector);
                Logger.LogInformation($"🧠 Caching new selector for '{instruction}': {rawSelector}");
                return by;
            }

            failedSelectors.Add(rawSelector);
            Logger.LogInformation($"[Attempt {attempt}] Selector failed: {rawSelector}");
            Thread.Sleep(300);
        }

        throw new NotFoundException($"❌ No element found for instruction: {instruction}");
    }

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

    private static By ParsePromptLocatorToBy(string promptResult)
    {
        var parts = Regex.Match(promptResult, "(xpath|id|name|tag|cssSelector|class|linktext|partiallinktext|attribute)=(.+)", RegexOptions.IgnoreCase);
        if (!parts.Success) return null;

        var type = parts.Groups[1].Value.Trim().ToLower();
        var locator = parts.Groups[2].Value.Trim();

        return type switch
        {
            "id" => By.Id(locator),
            "name" => By.Name(locator),
            "class" or "classname" => By.ClassName(locator),
            "tag" or "tagname" => By.TagName(locator),
            "css" or "cssselector" => By.CssSelector(locator),
            "linktext" => By.LinkText(locator),
            "partiallinktext" => By.PartialLinkText(locator),
            "xpath" => By.XPath(locator),
            "attribute" => By.XPath($"//*[@{locator}]"),
            _ => throw new ArgumentException($"Unsupported selector type: {type}")
        };
    }

    private static bool IsElementPresent(IWebDriver driver, By by)
    {
        try { return driver.FindElements(by).Any(); }
        catch { return false; }
    }

    public override string ToString() => $"Prompt = {Value}";
}
