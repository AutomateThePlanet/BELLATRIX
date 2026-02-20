// <copyright file="LocatorSelfHealingService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.KeyVault;
using Bellatrix.LLM.Cache;
using Bellatrix.LLM.Settings;
using Microsoft.SemanticKernel;

namespace Bellatrix.LLM;
/// <summary>
/// Provides self-healing capabilities for UI locators by leveraging AI and a local cache of previously validated locators.
/// This service enables autonomous test agents to recover from locator failures by suggesting new working locators
/// based on historical data and semantic analysis of UI changes.
/// </summary>
public static class LocatorSelfHealingService
{
    private static readonly LocatorCacheDbContext _db;
    private static readonly string _project;

    /// <summary>
    /// Static constructor initializes the local cache database context and project name from configuration and secrets.
    /// </summary>
    static LocatorSelfHealingService()
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
        var connectionString = SecretsResolver.GetSecret(() => settings.LocalCacheConnectionString);
        _db = new LocatorCacheDbContext(connectionString);
        _project = settings.LocalCacheProjectName;
    }

    /// <summary>
    /// Attempts to heal a failing locator by leveraging AI and previously cached locator data.
    /// The method queries the local cache for a known valid locator matching the failing one and, if found,
    /// uses Semantic Kernel to generate a suggestion for a new working locator based on the differences between
    /// the old and new view summaries. The returned string is an XPath expression representing the healed locator,
    /// or null if no suggestion could be generated.
    /// </summary>
    /// <param name="failingLocator">The locator that is currently failing.</param>
    /// <param name="newestVersionViewSummary">The latest view summary of the application.</param>
    /// <param name="appLocation">Optional application location context.</param>
    /// <returns>
    /// The suggested healed XPath locator as a string, or null if healing is not possible.
    /// </returns>
    public static string TryHeal(string failingLocator, string newestVersionViewSummary, string appLocation = "")
    {
        var known = _db.SelfHealingLocators.FirstOrDefault(x =>
            x.Project == _project && x.AppLocation == appLocation &&
            x.ValidLocator == failingLocator);

        if (known == null)
        {
            Logger.LogError($"❌ No known locator found for: {failingLocator}");
            return null;
        }

        var prompt = SemanticKernelService.Kernel.InvokeAsync("LocatorSkill", "HealBrokenLocator", new()
        {
            ["failedLocator"] = failingLocator,
            ["oldSnapshot"] = known.ViewSummary,
            ["newSnapshot"] = newestVersionViewSummary
        }).Result.GetValue<string>();

        var suggestion = SemanticKernelService.Kernel.InvokePromptAsync(prompt).Result.GetValue<string>()?.Trim();

        Logger.LogWarning("🧠 AI Healing Suggestion:");
        Logger.LogError($"❌ Failed: {failingLocator}");
        Logger.LogWarning($"✅ Suggest: {suggestion}");

        if (!string.IsNullOrWhiteSpace(suggestion))
        {
            return suggestion;
        }

        return null;
    }

    /// <summary>
    /// Saves or updates a working locator and its associated view summary in the local cache.
    /// If the locator already exists for the given project and application location, its data is updated;
    /// otherwise, a new entry is created.
    /// </summary>
    /// <param name="locator">The working locator to save.</param>
    /// <param name="viewSummary">The view summary associated with the locator.</param>
    /// <param name="appLocation">Optional application location context.</param>
    public static void SaveWorkingLocator(string locator, string viewSummary, string appLocation = "")
    {
        var currentSummary = viewSummary;

        var existing = _db.SelfHealingLocators.FirstOrDefault(x =>
            x.Project == _project && x.AppLocation == appLocation && x.ValidLocator == locator);

        if (existing != null)
        {
            existing.ValidLocator = locator;
            existing.ViewSummary = currentSummary;
            existing.LastUpdated = DateTime.UtcNow;

            _db.SelfHealingLocators.Update(existing);
        }
        else
        {
            _db.SelfHealingLocators.Add(new SelfHealingLocatorEntry
            {
                Project = _project,
                AppLocation = appLocation,
                ValidLocator = locator,
                ViewSummary = currentSummary,
                LastUpdated = DateTime.UtcNow
            });
        }

        _db.SaveChanges();
    }

    /// <summary>
    /// Removes all self-healing locator entries for the current project from the local cache.
    /// This is useful for cleanup or resetting the cache for a specific project.
    /// </summary>
    public static void ClearProjectEntries()
    {
        var entriesToDelete = _db.SelfHealingLocators
            .Where(x => x.Project == _project)
            .ToList();

        if (entriesToDelete.Any())
        {
            _db.SelfHealingLocators.RemoveRange(entriesToDelete);
            _db.SaveChanges();
            Logger.LogInformation($"🗑️ Deleted {entriesToDelete.Count} self-healing locator entries for project: {_project}");
        }
        else
        {
            Logger.LogInformation($"ℹ️ No entries found for project: {_project}");
        }
    }

    /// <summary>
    /// Disposes the local cache database context and releases any associated resources.
    /// </summary>
    public static void Dispose()
    {
        _db?.Dispose();
    }
}
