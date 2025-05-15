// <copyright file="SmartFailureAnalyzer.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bellatrix.Web.LLM.services;
public static class SmartFailureAnalyzer
{
    private static readonly LocatorCacheDbContext _db;
    private static readonly string _project;

    static SmartFailureAnalyzer()
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
        var connectionString = SecretsResolver.GetSecret(() => settings.LocalCacheConnectionString);
        _db = new LocatorCacheDbContext(connectionString);
        _project = settings.LocalCacheProjectName;
    }

    public static void SaveTestPass(string testName, string fullLog, string summaryJson)
    {
        var cleaned = FilterLog(fullLog);
        var entry = new SmartTestExecutionEntry
        {
            TestFullName = testName,
            ExecutionTime = DateTime.UtcNow,
            BddLog = cleaned,
            PageSummaryJson = summaryJson,
            Project = _project,
        };

        _db.SmartTestExecutions.Add(entry);
        _db.SaveChanges();
    }

    public static string Diagnose(string testName, string exceptionDetails, string currentLog, string currentSummary, string screenshotFilePath)
    {
        var lastPass = _db.SmartTestExecutions
            .Where(x => x.TestFullName == testName)
            .OrderByDescending(x => x.ExecutionTime)
            .FirstOrDefault();

        if (lastPass == null)
        {
            return "⚠️ No previous successful run available for comparison.";
        }

        var arguments = new KernelArguments
        {
            ["testName"] = testName,
            ["failingLog"] = currentLog,
            ["passedLog"] = lastPass.BddLog,
            ["failingSummary"] = currentSummary,
            ["passedSummary"] = lastPass.PageSummaryJson,
            ["exceptionDetails"] = exceptionDetails,
            ["screenshotHint"] = "See attached screenshot of the failed test."
        };

        if (!string.IsNullOrWhiteSpace(screenshotFilePath) && File.Exists(screenshotFilePath))
        {
            var imageBytes = File.ReadAllBytes(screenshotFilePath);
            var imageContent = new ImageContent(imageBytes, "image/png");
            arguments.Add("screenshot", imageContent);
        }

        var prompt = SemanticKernelService.Kernel
            .InvokeAsync("FailureAnalyzer", "GenerateFailureDiagnosis", arguments).Result;

        var suggestion = SemanticKernelService.Kernel.InvokePromptAsync(prompt.GetValue<string>()).Result.GetValue<string>()?.Trim();

        return suggestion;
    }

    public static (string Summary, string Body) ExtractSummaryAndBody(string fullText)
    {
        if (string.IsNullOrWhiteSpace(fullText))
        {
            return ("⚠️ No AI summary provided.", fullText);
        }

        // Extract up to Recommended Actions section as summary
        var summaryLines = new List<string>();
        var bodyLines = new List<string>();
        bool isInBody = false;

        using var reader = new StringReader(fullText);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            if (line.StartsWith("### 🛠 Recommended Actions:", StringComparison.OrdinalIgnoreCase))
            {
                isInBody = true;
            }

            if (isInBody)
            {
                bodyLines.Add(line);
            }
            else
            {
                summaryLines.Add(line);
            }
        }

        return (
            string.Join(Environment.NewLine, summaryLines).Trim(),
            string.Join(Environment.NewLine, bodyLines).Trim()
        );
    }


    public static string FilterLog(string fullLog) =>
        string.Join(Environment.NewLine, fullLog
            .Split(Environment.NewLine)
            .Where(l => !l.StartsWith("⚠️") && !l.StartsWith("🧠") && !l.StartsWith("✅") && !l.StartsWith("❌")));
}
