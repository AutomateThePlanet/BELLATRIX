using Bellatrix.LLM;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using Microsoft.SemanticKernel;

namespace Bellatrix.Web.llm;
public static class AiValidator
{
    public static void ValidateByPrompt(string assertInstruction, int? timeout = null, int? sleepInterval = null)
    {
        var browser = ServicesCollection.Current.Resolve<BrowserService>();
        var driver = browser.WrappedDriver;

        int effectiveTimeout = timeout ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ValidationsTimeout;
        int effectiveSleep = sleepInterval ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.SleepInterval;

        var wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(effectiveTimeout), TimeSpan.FromSeconds(effectiveSleep));
        wait.IgnoreExceptionTypes(typeof(Exception));

        string verdict = string.Empty;
        bool AssertionPassed(IWebDriver _)
        {
            try
            {
                browser.WaitForAjax();
                browser.WaitUntilReady();
                var summaryJson = browser.GetPageSummaryJson();

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
            throw new ComponentPropertyValidateException(
                $"❌ AI validation failed: {assertInstruction} within timeout ({effectiveTimeout}s).\n - {verdict}",
                driver.Url);
        }
    }
}
   
