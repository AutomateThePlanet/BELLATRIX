using Microsoft.SemanticKernel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System;
using Bellatrix.LLM;
using Bellatrix.Desktop.Locators;
using Bellatrix.Desktop.Services;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Bellatrix.Desktop.LLM.Plugins;

namespace Bellatrix.Desktop.LLM;

/// <summary>
/// A BELLATRIX Desktop FindStrategy that uses Semantic Kernel to locate elements by natural language prompts.
/// Attempts retrieval from RAG memory or local cache, falling back to AI-based XPath generation if needed.
/// </summary>
public class FindByPrompt : FindStrategy
{
    public FindByPrompt(string value) : base(value) { }

    public override WindowsElement FindElement(WindowsDriver<WindowsElement> driver)
    {
        string location = driver.CurrentWindowHandle;
        var cachedXpath = TryResolveLocator(location);
        return driver.FindElementByXPath(cachedXpath);
    }

    public override IEnumerable<WindowsElement> FindAllElements(WindowsDriver<WindowsElement> driver)
    {
        string location = driver.CurrentWindowHandle;
        var cachedXpath = TryResolveLocator(location);
        return driver.FindElementsByXPath(cachedXpath);
    }

    public override AppiumWebElement FindElement(WindowsElement element)
    {
        string location = element.WrappedDriver.CurrentWindowHandle;
        var cachedXpath = TryResolveLocator(location);
        return element.FindElementByXPath(cachedXpath);
    }

    public override IEnumerable<AppiumWebElement> FindAllElements(WindowsElement element)
    {
        string location = element.WrappedDriver.CurrentWindowHandle;
        var cachedXpath = TryResolveLocator(location);
        return element.FindElementsByXPath(cachedXpath);
    }

    /// <summary>
    /// Attempts to resolve the XPath locator using RAG memory, cache, or fallback prompt generation.
    /// </summary>
    /// <param name="location">Desktop app window handle or location identifier.</param>
    /// <returns>A valid XPath string.</returns>
    private string TryResolveLocator(string location)
    {
        // Try RAG
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

            var locator = ExtractXPath(mappedPrompt);
            if (!string.IsNullOrEmpty(locator))
            {
                return locator;
            }
        }

        // Try cache
        var cached = LocatorCacheService.TryGetCached(location, Value);
        if (!string.IsNullOrEmpty(cached))
        {
            return cached;
        }

        // Remove any broken cache
        LocatorCacheService.Remove(location, Value);

        // Fallback prompt
        return ResolveViaPromptFallback(location);
    }

    private string ResolveViaPromptFallback(string location, int maxAttempts = 3)
    {
        var viewSnapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var summaryJson = viewSnapshotProvider.GetCurrentViewSnapshot();
        var failedSelectors = new List<string>();

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            var prompt = SemanticKernelService.Kernel?.InvokeAsync(nameof(LocatorSkill), nameof(LocatorSkill.BuildLocatorPrompt), 
            new()
            {
                ["viewSummaryJson"] = summaryJson,
                ["instruction"] = Value,
                ["failedSelectors"] = failedSelectors
            }).Result.GetValue<string>();

            var result = SemanticKernelService.Kernel?.InvokePromptAsync(prompt).Result;
            var rawSelector = result?.GetValue<string>()?.Trim();

            if (!string.IsNullOrWhiteSpace(rawSelector))
            {
                LocatorCacheService.Update(location, Value, rawSelector);
                return rawSelector;
            }

            failedSelectors.Add(rawSelector);
            Thread.Sleep(300);
        }

        throw new InvalidOperationException($"❌ No XPath found for prompt: {Value}");
    }

    private string ExtractXPath(string promptResult)
    {
        var parts = Regex.Match(promptResult, @"(xpath)=([^\n\r]+)", RegexOptions.IgnoreCase);
        return parts.Success ? parts.Groups[2].Value.Trim() : null;
    }

    public override string ToString() => $"Prompt = {Value}";
}
