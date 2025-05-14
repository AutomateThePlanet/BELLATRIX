using Bellatrix.KeyVault;
using Bellatrix.LLM;
using Bellatrix.LLM.cache;
using Bellatrix.LLM.Cache;
using Bellatrix.LLM.settings;
using Microsoft.SemanticKernel;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace Bellatrix.Web.llm;
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
