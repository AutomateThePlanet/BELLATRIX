// <copyright file="AiValidator.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.LLM;
using Bellatrix.LLM.Settings;
using Bellatrix.LLM.Skills;
using Microsoft.SemanticKernel;

namespace Bellatrix;

public static class AiValidator
{
    public static void ValidateByPrompt(string assertInstruction, int? timeout = null, int? sleepInterval = null)
    {
        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();

        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
        var effectiveTimeout = timeout ?? settings.ValidationsTimeout;
        var effectiveSleep = sleepInterval ?? settings.SleepInterval;

        var verdict = string.Empty;

        void WaitForAssertionToPass()
        {
            Utilities.Wait.Until(
                condition: () =>
                {
                    try
                    {
                        var appSnapshot = snapshotProvider.GetCurrentViewSnapshot();

                        var result = SemanticKernelService.Kernel.InvokeAsync(nameof(AssertionSkill), nameof(AssertionSkill.EvaluateAssertion), new()
                        {
                            ["htmlSummary"] = appSnapshot,
                            ["assertInstruction"] = assertInstruction
                        }).Result;

                        var fullPrompt = result.GetValue<string>();
                        verdict = SemanticKernelService.Kernel.InvokePromptAsync(fullPrompt).Result.GetValue<string>()?.Trim();

                        return !string.IsNullOrWhiteSpace(verdict) &&
                               verdict.Contains("PASS", StringComparison.OrdinalIgnoreCase);
                    }
                    catch
                    {
                        return false;
                    }
                },
                timeoutInSeconds: effectiveTimeout,
                exceptionMessage: $"❌ AI validation failed: {assertInstruction} within timeout ({effectiveTimeout}s).\n - {verdict}",
                retryRateDelay: effectiveSleep * 1000
            );
        }

        WaitForAssertionToPass();
        Console.WriteLine("✅ AI Validate passed: " + assertInstruction);
    }
}

