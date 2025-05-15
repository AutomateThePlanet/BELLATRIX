// <copyright file="AiAssert.cs" company="Automate The Planet Ltd.">
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
