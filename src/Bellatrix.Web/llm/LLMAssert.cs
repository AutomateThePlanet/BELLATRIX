using Bellatrix.Assertions;
using Bellatrix.LLM;
using Microsoft.SemanticKernel;
using System;

namespace Bellatrix.Web.llm;
public static class LLMAssert
{
    public static void AssertByPrompt(this Assert assert, string assertInstruction)
    {
        var browser = ServicesCollection.Current.Resolve<BrowserService>();
        var summaryJson = browser.GetPageSummaryJson();

        var result = SemanticKernelService.Kernel.InvokeAsync("Assertions", "EvaluateAssertion", new()
        {
            ["htmlSummary"] = summaryJson,
            ["assertInstruction"] = assertInstruction
        }).Result;

        var fullPrompt = result.GetValue<string>();

        // Step 2: Execute the prompt with the LLM
        result = SemanticKernelService.Kernel.InvokePromptAsync(fullPrompt).Result;
        var verdict = result.GetValue<string>()?.Trim();

        if (string.IsNullOrWhiteSpace(verdict) || !verdict.StartsWith("PASS", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("AI Assertion Failed: " + verdict);
        }

        Console.WriteLine("✅ AI Assert passed: " + assertInstruction);
    }
}
