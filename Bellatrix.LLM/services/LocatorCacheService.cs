// <copyright file="LocatorCacheService.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Concurrent;

namespace Bellatrix.LLM;

/// <summary>
/// A shared locator cache service for AI-assisted test automation.
/// It stores and retrieves XPath selectors for natural language instructions,
/// scoped by application location (e.g., URL or screen ID).
/// Useful for enabling cross-platform support (Web, Android, iOS, Desktop).
/// </summary>
public static class LocatorCacheService
{
    private static readonly ConcurrentDictionary<string, string> _cache = new();
    private static readonly string _project;
    private static readonly LocatorCacheDbContext _db;

    static LocatorCacheService()
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
        var connectionString = SecretsResolver.GetSecret(() => settings.LocalCacheConnectionString);
        _db = new LocatorCacheDbContext(connectionString);
        _project = settings.LocalCacheProjectName;

        var entries = _db.LocatorCache
            .Where(x => x.Project == _project)
            .ToList();

        foreach (var entry in entries)
        {
            _cache.TryAdd($"{entry.AppLocation}|{entry.Instruction}", entry.XPath);
        }
    }

    /// <summary>
    /// Attempts to retrieve a cached XPath for the given instruction and location.
    /// </summary>
    /// <param name="location">The app location (URL, screen ID, etc.)</param>
    /// <param name="instruction">The natural language instruction used for element lookup.</param>
    /// <returns>The XPath selector if cached, otherwise null.</returns>
    public static string TryGetCached(string location, string instruction)
    {
        var key = $"{location}|{instruction}";
        return _cache.TryGetValue(key, out var xpath) ? xpath : null;
    }

    /// <summary>
    /// Removes the cached entry (if any) for the given instruction and location.
    /// </summary>
    /// <param name="location">The app location.</param>
    /// <param name="instruction">The instruction key to remove.</param>
    public static void Remove(string location, string instruction)
    {
        var key = $"{location}|{instruction}";
        _cache.TryRemove(key, out _);

        var dbEntry = _db.LocatorCache.FirstOrDefault(x =>
            x.Project == _project && x.AppLocation == location && x.Instruction == instruction);

        if (dbEntry != null)
        {
            _db.LocatorCache.Remove(dbEntry);
            _db.SaveChanges();
        }
    }

    /// <summary>
    /// Updates or inserts a new cached XPath selector for a given instruction and location.
    /// </summary>
    /// <param name="location">The app location.</param>
    /// <param name="instruction">The natural language instruction used for lookup.</param>
    /// <param name="xpath">The XPath selector to store.</param>
    public static void Update(string location, string instruction, string xpath)
    {
        var key = $"{location}|{instruction}";
        _cache[key] = xpath;

        var existing = _db.LocatorCache.FirstOrDefault(x =>
            x.Project == _project && x.AppLocation == location && x.Instruction == instruction);

        if (existing != null)
        {
            existing.XPath = xpath;
            existing.LastValidated = DateTime.UtcNow;
            _db.LocatorCache.Update(existing);
        }
        else
        {
            var entry = new LocatorCacheEntry
            {
                Project = _project,
                AppLocation = location,
                Instruction = instruction,
                XPath = xpath
            };
            _db.LocatorCache.Add(entry);
        }

        _db.SaveChanges();
    }

    /// <summary>
    /// Disposes the database context when no longer needed.
    /// </summary>
    public static void Dispose()
    {
        _db?.Dispose();
    }
}

