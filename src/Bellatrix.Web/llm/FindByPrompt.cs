using Microsoft.SemanticKernel;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System;
using Bellatrix.LLM;
using System.Linq;

namespace Bellatrix.Web.llm;

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

        var ragLocator = TryResolveFromPageObjectMemory(driver, Value);
        if (ragLocator != null)
        {
            if (IsElementPresent(driver, ragLocator))
            {
                return ragLocator;
            }

            Logger.LogInformation($"⚠️ RAG-located element not present. Falling back to prompt-based resolution.");
        }

        return ResolveViaPromptFallback(driver, Value);
    }

    private By TryResolveFromPageObjectMemory(IWebDriver driver, string instruction)
    {
        var enrichedPrompt = $"{instruction} (Current URL: {driver.Url})";

        var match = SemanticKernelService.Memory
            .SearchAsync(enrichedPrompt, index: "PageObjects", limit: 1)
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

    private By ResolveViaPromptFallback(IWebDriver driver, string instruction, int maxAttempts = 3)
    {
        var url = driver.Url;
        var cacheKey = instruction;

        if (SemanticKernelService.LocatorCache.TryGetValue(cacheKey, out var cachedSelector))
        {
            if (IsElementPresent(driver, By.XPath(cachedSelector)))
            {
                Logger.LogInformation($"✅ Using cached selector for: {instruction}");
                return By.XPath(cachedSelector);
            }

            Logger.LogInformation($"⚠️ Cached selector failed. Re-querying AI: {cachedSelector}");
            SemanticKernelService.LocatorCache.TryRemove(cacheKey, out _);
        }

        var browser = ServicesCollection.Current.Resolve<BrowserService>();
        var summaryJson = browser.GetPageSummaryJson();
        var failedSelectors = new List<string>();

        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var promptBuild = SemanticKernelService.Kernel?.InvokeAsync("Locator", "BuildLocatorPrompt", new()
            {
                ["htmlSummary"] = summaryJson,
                ["instruction"] = instruction,
                ["failedSelectors"] = failedSelectors
            }).Result;

            var fullPrompt = promptBuild?.GetValue<string>();
            var promptResult = SemanticKernelService.Kernel?.InvokePromptAsync(fullPrompt).Result;
            var rawSelector = promptResult?.GetValue<string>()?.Trim();

            if (string.IsNullOrWhiteSpace(rawSelector))
                continue;

            if (IsElementPresent(driver, By.XPath(rawSelector)))
            {
                SemanticKernelService.LocatorCache[cacheKey] = rawSelector;
                Logger.LogInformation($"🧠 Caching selector for '{instruction}' on '{url}': {rawSelector}");
                return By.XPath(rawSelector);
            }

            failedSelectors.Add(rawSelector);
            Logger.LogInformation($"[Attempt {attempt}] Selector failed: {rawSelector}");
            Thread.Sleep(300);
        }

        throw new NotFoundException($"❌ No element found via prompt: \"{instruction}\" after {maxAttempts} attempts.");
    }

    private static By ParsePromptLocatorToBy(string promptResult)
    {
        var parts = Regex.Match(promptResult, "(xpath|id|name|tag|cssSelector|class|linktext|partiallinktext|attribute)=(.+)", RegexOptions.IgnoreCase);
        if (!parts.Success)
            return null;

        var type = parts.Groups[1].Value.Trim().ToLower();
        var locator = parts.Groups[2].Value.Trim();

        if (type == "attribute")
        {
            var attrParts = Regex.Match(locator, @"^(.+?)=(.+)$");
            if (!attrParts.Success)
                throw new ArgumentException($"Invalid attribute locator format: {locator}");

            var attrName = attrParts.Groups[1].Value.Trim();
            var attrValue = attrParts.Groups[2].Value.Trim();
            return By.XPath($"//*[@{attrName}='{attrValue}']");
        }

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
            _ => throw new ArgumentException($"Unsupported selector type: {type}")
        };
    }

    private static bool IsElementPresent(IWebDriver driver, By by)
    {
        try
        {
            return driver.FindElements(by).Any();
        }
        catch
        {
            return false;
        }
    }

    public override string ToString() => $"Prompt = {Value}";
}

