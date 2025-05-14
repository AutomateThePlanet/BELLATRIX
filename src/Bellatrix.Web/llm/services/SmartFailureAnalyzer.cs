using Bellatrix.KeyVault;
using Bellatrix.LLM;
using Bellatrix.LLM.cache;
using Bellatrix.LLM.Cache;
using Bellatrix.LLM.settings;
using Microsoft.Identity.Client;
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
