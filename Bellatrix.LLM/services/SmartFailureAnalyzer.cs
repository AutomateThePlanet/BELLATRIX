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
using Bellatrix.LLM.Cache;
using Bellatrix.LLM.Settings;
using Bellatrix.LLM.Skills;
using Microsoft.SemanticKernel;

namespace Bellatrix.LLM;
/// <summary>
/// Provides smart analysis and diagnosis of test failures using historical test execution data and LLM-powered reasoning.
/// Responsible for saving successful test executions, diagnosing failures by comparing with previous passes, and extracting summaries from AI-generated reports.
/// </summary>
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

    /// <summary>
    /// Saves a successful test execution to the local cache database, after filtering the log.
    /// </summary>
    /// <param name="testName">The full name of the test.</param>
    /// <param name="fullLog">The complete log output from the test run.</param>
    /// <param name="summaryJson">A JSON summary of the page state or test context.</param>
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

    /// <summary>
    /// Diagnoses a test failure by comparing the current failing log and summary with the most recent passing execution.
    /// Uses an LLM to generate a diagnosis and recommended actions.
    /// </summary>
    /// <param name="testName">The full name of the test.</param>
    /// <param name="exceptionDetails">Details of the exception thrown during the test failure.</param>
    /// <param name="currentLog">The log output from the failing test run.</param>
    /// <param name="currentSummary">A summary of the current page or test state.</param>
    /// <param name="screenshotFilePath">Optional file path to a screenshot of the failure.</param>
    /// <returns>A string containing the AI-generated diagnosis and recommendations.</returns>
    public static string Diagnose(string testName, string exceptionDetails, string currentLog, string currentSummary, string screenshotFilePath)
    {
        var lastPass = _db.SmartTestExecutions
            .Where(x => x.TestFullName == testName)
            .OrderByDescending(x => x.ExecutionTime)
            .FirstOrDefault();

        var arguments = new KernelArguments
        {
            ["testName"] = testName,
            ["failingLog"] = currentLog,
            ["passedLog"] = lastPass?.BddLog ?? "no previous runs - log not available",
            ["failingSummary"] = currentSummary,
            ["passedSummary"] = lastPass?.PageSummaryJson ?? "no previous runs - page summary not available",
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
            .InvokeAsync(nameof(FailureAnalyzerSkill), nameof(FailureAnalyzerSkill.GenerateFailureDiagnosis), arguments).Result;

        var suggestion = SemanticKernelService.Kernel.InvokePromptAsync(prompt.GetValue<string>()).Result.GetValue<string>()?.Trim();

        return suggestion;
    }

    /// <summary>
    /// Extracts a summary and the main body from an AI-generated report.
    /// The summary is everything up to the "Recommended Actions" section; the body is the rest.
    /// </summary>
    /// <param name="fullText">The full AI-generated report text.</param>
    /// <returns>
    /// A tuple where the first item is the summary (string), and the second item is the body (string).
    /// </returns>
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

    /// <summary>
    /// Filters out lines from the test log that start with specific emoji markers (warnings, AI, pass/fail indicators).
    /// </summary>
    /// <param name="fullLog">The complete log output from the test run.</param>
    /// <returns>The filtered log as a string, with unwanted lines removed.</returns>
    public static string FilterLog(string fullLog) =>
        string.Join(Environment.NewLine, fullLog
            .Split(Environment.NewLine)
            .Where(l => !l.StartsWith("⚠️") && !l.StartsWith("🧠") && !l.StartsWith("✅") && !l.StartsWith("❌")));
}
