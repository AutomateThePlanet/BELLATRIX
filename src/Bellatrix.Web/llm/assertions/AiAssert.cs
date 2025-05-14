using Bellatrix.Assertions;
using Bellatrix.LLM;
using Bellatrix.Web.LLM.Extensions;
using Microsoft.SemanticKernel;
using System;

namespace Bellatrix.Web.LLM.assertions;
public static class AiAssert
{
    public static void AssertByPrompt(string assertInstruction)
    {
        var browser = ServicesCollection.Current.Resolve<BrowserService>();
        browser.WaitForAjax();
        browser.WaitUntilReady();
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

        if (string.IsNullOrWhiteSpace(verdict) || !verdict.Contains("PASS", StringComparison.OrdinalIgnoreCase))
        {
            Assert.Fail($"AI Assert failed: {assertInstruction} - {verdict}");
        }

        Console.WriteLine("✅ AI Assert passed: " + assertInstruction);
    }
}
