using Bellatrix.LLM;
using Microsoft.SemanticKernel;
using System;
using System.IO;
using System.Linq;

namespace Bellatrix.Web.LLM.services;

public static class PageObjectsIndexer
{
    /// <summary>
    /// Indexes all BELLATRIX PageObject files using Semantic Kernel + Kernel Memory.
    /// Groups partial page classes (e.g., Map, Actions, Assertions) and summarizes each logical PageObject once.
    /// Stores structured locator summaries in a persistent vector memory index.
    /// </summary>
    public static void IndexAllPageObjects(string folderName = "Pages", string memoryIndex = "PageObjects", bool resetIndex = true)
    {
        var kernel = SemanticKernelService.Kernel;
        var memory = SemanticKernelService.Memory;

        // Optionally clear the existing memory index
        if (resetIndex)
        {
            Logger.LogInformation($"🗑️ Deleting index '{memoryIndex}'...");
            memory.DeleteIndexAsync(memoryIndex).Wait();
        }

        // Resolve path to the Pages folder
        var folderPath = ResolvePagesFolderPath(folderName);
        Logger.LogInformation($"📁 Scanning folder: {folderPath}");

        // Group all .cs files by logical PageObject class name (e.g., CartPage)
        var groupedFiles = Directory.GetFiles(folderPath, "*.cs", SearchOption.AllDirectories)
                                    .GroupBy(GetLogicalPageObjectName);

        Logger.LogInformation($"🔎 Found {groupedFiles.Count()} PageObject group(s).");

        // Process each logical PageObject group (e.g., CartPage.Map + CartPage.Actions)
        foreach (var group in groupedFiles)
        {
            var documentId = group.Key;
            var combinedCode = string.Join("\n\n", group.Select(File.ReadAllText));

            Logger.LogInformation($"📄 Indexing combined PageObject: {documentId}");

            try
            {
                // Step 1: Generate SK prompt
                var summaryPrompt = kernel.InvokeAsync("PageSummarizer", "SummarizePageObjectCode", new()
                {
                    ["code"] = combinedCode
                }).Result.GetValue<string>();

                // Step 2: Execute prompt using LLM
                var result = kernel.InvokePromptAsync(summaryPrompt).Result;
                var summary = result.GetValue<string>()?.Trim();

                if (string.IsNullOrWhiteSpace(summary))
                {
                    Logger.LogInformation($"⚠️ Skipping {documentId} — no summary generated.");
                    continue;
                }

                // Step 3: Store in kernel memory index
                memory.ImportTextAsync(documentId: documentId, text: summary, index: memoryIndex).Wait();
                Logger.LogInformation($"✅ Indexed PageObject: {documentId}");
            }
            catch (Exception ex)
            {
                Logger.LogError($"❌ Failed to index {documentId}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Determines the base class name from a partial PageObject filename.
    /// Example: "CartPage.Map.cs" → "CartPage"
    /// </summary>
    private static string GetLogicalPageObjectName(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        return fileName.Split('.')[0]; // Handle CartPage.Map.cs, CheckoutPage.Actions.cs, etc.
    }

    /// <summary>
    /// Resolves the absolute path to the target Pages folder.
    /// </summary>
    private static string ResolvePagesFolderPath(string folderName)
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        return Path.Combine(projectRoot, folderName);
    }

}
