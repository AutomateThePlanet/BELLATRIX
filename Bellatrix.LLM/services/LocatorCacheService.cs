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
public static class LocatorCacheService
{
    // In-memory cache for selectors. Key is a composite: {location}|{instruction}
    private static readonly ConcurrentDictionary<string, string> _cache = new();

    // Project name (to support scoping and multi-project setups)
    private static readonly string _project;

    // EF DbContext for persistent locator cache (can be null if DB unavailable)
    private static readonly LocatorCacheDbContext _db;

    // Flag that disables DB calls if connection fails, so tests never fail due to DB outage
    private static bool _dbAvailable = true;

    // Static constructor runs once on first use. Attempts to initialize cache DB and fill in-memory cache.
    static LocatorCacheService()
    {
        try
        {
            // Read settings and secrets for DB connection
            var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
            var connectionString = SecretsResolver.GetSecret(() => settings.LocalCacheConnectionString);
            _db = new LocatorCacheDbContext(connectionString);
            _project = settings.LocalCacheProjectName;

            // Read all entries from DB for this project and preload in-memory cache
            var entries = _db.LocatorCache
                .Where(x => x.Project == _project)
                .ToList();

            foreach (var entry in entries)
            {
                _cache.TryAdd($"{entry.AppLocation}|{entry.Instruction}", entry.XPath);
            }
        }
        catch (Exception ex)
        {
            _dbAvailable = false;
            // If DB is down, only warn; continue using in-memory cache (never fail test)
            Logger.LogWarning($"⚠️ WARNING! [LocatorCacheService] Cache DB unavailable. Running in in-memory mode. Details: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a cached XPath selector (from in-memory cache only).
    /// DB is not queried at runtime for reads; only at startup.
    /// </summary>
    public static string TryGetCached(string location, string instruction)
    {
        var key = $"{location}|{instruction}";
        return _cache.TryGetValue(key, out var xpath) ? xpath : null;
    }

    /// <summary>
    /// Removes an entry from both in-memory cache and DB (if available).
    /// If DB is down, only affects in-memory cache and logs warning.
    /// </summary>
    public static void Remove(string location, string instruction)
    {
        var key = $"{location}|{instruction}";
        _cache.TryRemove(key, out _);

        if (_dbAvailable)
        {
            try
            {
                var dbEntry = _db.LocatorCache.FirstOrDefault(x =>
                    x.Project == _project && x.AppLocation == location && x.Instruction == instruction);

                if (dbEntry != null)
                {
                    _db.LocatorCache.Remove(dbEntry);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _dbAvailable = false;
                // Logs only, does NOT throw, keeps test running
                Logger.LogWarning($"⚠️ WARNING! [LocatorCacheService] Failed to remove from DB. Details: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Updates or inserts a selector in the cache for a given instruction and location.
    /// Always updates in-memory cache, and tries to persist in DB if available.
    /// If DB fails, disables DB and logs warning; does NOT throw.
    /// </summary>
    public static void Update(string location, string instruction, string xpath)
    {
        var key = $"{location}|{instruction}";
        _cache[key] = xpath;

        if (_dbAvailable)
        {
            try
            {
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
            catch (Exception ex)
            {
                _dbAvailable = false;
                Logger.LogWarning($"⚠️ WARNING! [LocatorCacheService] Failed to update DB. Details: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Removes all selectors for the current project from both memory and persistent storage.
    /// If DB is down, only clears in-memory cache.
    /// </summary>
    public static void RemoveAllLocators()
    {
        _cache.Clear();

        if (_dbAvailable)
        {
            try
            {
                var allEntries = _db.LocatorCache
                    .Where(x => x.Project == _project)
                    .ToList();

                if (allEntries.Count > 0)
                {
                    _db.LocatorCache.RemoveRange(allEntries);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _dbAvailable = false;
                Logger.LogWarning($"⚠️ WARNING! [LocatorCacheService] Failed to clear DB. Details: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Disposes the DB context. Logs and swallows any exceptions.
    /// </summary>
    public static void Dispose()
    {
        try
        {
            _db?.Dispose();
        }
        catch (Exception ex)
        {
            Logger.LogWarning($"⚠️ WARNING! [LocatorCacheService] Error on dispose. Details: {ex.Message}");
        }
    }
}
