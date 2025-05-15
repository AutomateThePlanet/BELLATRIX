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
using Bellatrix.LLM;
using Bellatrix.LLM.cache;
using Bellatrix.LLM.Cache;
using Bellatrix.LLM.settings;
using Microsoft.SemanticKernel;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace Bellatrix.Web.LLM.services;
public static class LocatorSelfHealingService
{
    private static readonly LocatorCacheDbContext _db;
    private static readonly string _project;
    static LocatorSelfHealingService()
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
        var connectionString = SecretsResolver.GetSecret(() => settings.LocalCacheConnectionString);
        _db = new LocatorCacheDbContext(connectionString);
        _project = settings.LocalCacheProjectName;
    }

    public static FindXpathStrategy TryHeal(string failingLocator, string newestVersionViewSummary, string appLocation = "")
    {
        var known = _db.SelfHealingLocators.FirstOrDefault(x =>
            x.Project == _project && x.AppLocation == appLocation &&
            x.ValidLocator == failingLocator);

        if (known == null)
        {
            Logger.LogError($"❌ No known locator found for: {failingLocator}");
            return null;
        }

        var prompt = SemanticKernelService.Kernel.InvokeAsync("Locator", "HealBrokenLocator", new()
        {
            ["failedLocator"] = failingLocator,
            ["oldViewummary"] = known.ViewSummary,
            ["newViewSummary"] = newestVersionViewSummary
        }).Result.GetValue<string>();

        var suggestion = SemanticKernelService.Kernel.InvokePromptAsync(prompt).Result.GetValue<string>()?.Trim();

        Console.WriteLine("🧠 AI Healing Suggestion:");
        Console.WriteLine($"❌ Failed: {failingLocator}");
        Console.WriteLine($"✅ Suggest: {suggestion}");

        if (!string.IsNullOrWhiteSpace(suggestion))
        {
            return new FindXpathStrategy(suggestion);
        }

        return null;
    }

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

    public static void ClearProjectEntries()
    {
        var entriesToDelete = _db.SelfHealingLocators
            .Where(x => x.Project == _project)
            .ToList();

        if (entriesToDelete.Any())
        {
            _db.SelfHealingLocators.RemoveRange(entriesToDelete);
            _db.SaveChanges();
            Console.WriteLine($"🗑️ Deleted {entriesToDelete.Count} self-healing locator entries for project: {_project}");
        }
        else
        {
            Console.WriteLine($"ℹ️ No entries found for project: {_project}");
        }
    }

    public static void Dispose()
    {
        _db?.Dispose();
    }
}
