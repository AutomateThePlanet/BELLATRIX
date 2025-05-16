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
// Please cite or credit appropriately if reusing in academic or commercial work.</note>
using Bellatrix.LLM;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using Microsoft.SemanticKernel;
using Bellatrix.Desktop.LLM.Extensions;
using Bellatrix.Desktop.Services;
using Bellatrix.Desktop.Exceptions;

namespace Bellatrix.Desktop.LLM.assertions;
public static class AiValidator
{
    public static void ValidateByPrompt(string assertInstruction, int? timeout = null, int? sleepInterval = null)
    {
        var app = ServicesCollection.Current.Resolve<AppService>();

        var driver = app.WrappedDriver;

        var effectiveTimeout = timeout ?? ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.ValidationsTimeout;
        var effectiveSleep = sleepInterval ?? ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.SleepInterval;

        var wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(effectiveTimeout), TimeSpan.FromSeconds(effectiveSleep));
        wait.IgnoreExceptionTypes(typeof(Exception));

        var verdict = string.Empty;
        bool AssertionPassed(IWebDriver _)
        {
            try
            {
                var summaryJson = app.GetPageSummaryJson();

                var result = SemanticKernelService.Kernel.InvokeAsync("Assertions", "EvaluateAssertion", new()
                {
                    ["htmlSummary"] = summaryJson,
                    ["assertInstruction"] = assertInstruction
                }).Result;

                var fullPrompt = result.GetValue<string>();
                verdict = SemanticKernelService.Kernel.InvokePromptAsync(fullPrompt).Result.GetValue<string>()?.Trim();

                if (string.IsNullOrWhiteSpace(verdict)) return false;
                return verdict.Contains("PASS", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        try
        {
            wait.Until(AssertionPassed);
            Console.WriteLine("✅ AI Validate passed: " + assertInstruction);
        }
        catch (WebDriverTimeoutException)
        {
            throw new ComponentPropertyValidateException($"❌ AI validation failed: {assertInstruction} within timeout ({effectiveTimeout}s).\n - {verdict}");
        }
    }
}
   
