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
using Bellatrix.LLM.settings;
using OpenQA.Selenium;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Bellatrix.Web.LLM.services;
public static class LocatorCacheService
{
    private static readonly ConcurrentDictionary<string, string> _cache = new();
    private static readonly string _project;
    private static readonly LocatorCacheDbContext _db;
    private static IWebDriver _driver => ServicesCollection.Current.Resolve<IWebDriver>();
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

    public static By TryGetCached(string instruction)
    {
        var key = $"{_driver.Url}|{instruction}";
        return _cache.TryGetValue(key, out var xpath) ? By.XPath(xpath) : null;
    }

    public static void Remove(string instruction)
    {
        var key = $"{_driver.Url}|{instruction}";
        _cache.TryRemove(key, out _);

        var dbEntry = _db.LocatorCache.FirstOrDefault(x =>
            x.Project == _project && x.AppLocation == _driver.Url && x.Instruction == instruction);

        if (dbEntry != null)
        {
            _db.LocatorCache.Remove(dbEntry);
            _db.SaveChanges();
        }
    }

    public static void Update(string instruction, string xpath)
    {
        var key = $"{_driver.Url}|{instruction}";
        _cache[key] = xpath;

        var existing = _db.LocatorCache.FirstOrDefault(x =>
            x.Project == _project && x.AppLocation == _driver.Url && x.Instruction == instruction);

        if (existing != null)
        {
            existing.XPath = xpath;
            existing.LastValidated = DateTime.UtcNow;

            _db.LocatorCache.Update(existing); // <== ensure it's attached + marked as modified
        }
        else
        {
            var entry = new LocatorCacheEntry
            {
                Project = _project,
                AppLocation = _driver.Url,
                Instruction = instruction,
                XPath = xpath
            };

            _db.LocatorCache.Add(entry);
        }

        _db.SaveChanges();
    }
    public static void Dispose()
    {
        _db?.Dispose();
    }
}
