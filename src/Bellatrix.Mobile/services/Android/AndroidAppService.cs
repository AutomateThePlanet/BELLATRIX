// <copyright file="AndroidAppService.cs" company="Automate The Planet Ltd.">
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
using DocumentFormat.OpenXml.Bibliography;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System.Collections.Generic;

namespace Bellatrix.Mobile.Services.Android;

public class AndroidAppService : AppService<AndroidDriver, AppiumElement>
{
    public AndroidAppService(AndroidDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public string CurrentActivity { get => WrappedAppiumDriver.CurrentActivity; }

    public void StartActivity(
        string appPackage,
        string appActivity,
        string appWaitPackage = "",
        string appWaitActivity = "",
        bool stopApp = true)
    {
        try
        {
            WrappedAppiumDriver.HideKeyboard();
        }
        catch
        {
            // ignore
        }

        var args = new Dictionary<string, object>
        {
            ["appPackage"] = appPackage,
            ["appActivity"] = appActivity,
            ["appWaitPackage"] = appWaitPackage,
            ["appWaitActivity"] = appWaitActivity,
            ["dontStopAppOnReset"] = !stopApp
        };

        ((IJavaScriptExecutor)WrappedAppiumDriver).ExecuteScript("mobile: startActivity", args);
    }

    public void StartActivityWithIntent(
    string appPackage,
    string appActivity,
    string intentAction,
    string appWaitPackage = "",
    string appWaitActivity = "",
    string intentCategory = "",
    string intentFlags = "",
    string intentOptionalArgs = "",
    bool stopApp = true)
    {
        try
        {
            WrappedAppiumDriver.HideKeyboard();
        }
        catch
        {
            // ignore
        }

        // Build optional intent parameters
        var optionalIntentArguments = new List<string>();

        if (!string.IsNullOrWhiteSpace(intentAction))
            optionalIntentArguments.Add($"-a {intentAction}");

        if (!string.IsNullOrWhiteSpace(intentCategory))
            optionalIntentArguments.Add($"-c {intentCategory}");

        // flags can be like: "0x10200000" or already "-f 0x10200000"
        if (!string.IsNullOrWhiteSpace(intentFlags))
        {
            optionalIntentArguments.Add(intentFlags.TrimStart().StartsWith("-f ")
                ? intentFlags
                : $"-f {intentFlags}");
        }

        // anything extra the caller passes, e.g. "-d https://..." or "--es key value"
        if (!string.IsNullOrWhiteSpace(intentOptionalArgs))
            optionalIntentArguments.Add(intentOptionalArgs);

        var args = new Dictionary<string, object>
        {
            ["appPackage"] = appPackage,
            ["appActivity"] = appActivity,

            // These two are optional, but very useful when the launch triggers redirects/splash
            ["appWaitPackage"] = string.IsNullOrWhiteSpace(appWaitPackage) ? appPackage : appWaitPackage,
            ["appWaitActivity"] = string.IsNullOrWhiteSpace(appWaitActivity) ? appActivity : appWaitActivity,

            // Appium 2+ accepts intent args here for UiAutomator2
            ["intentArguments"] = optionalIntentArguments,

            // stopApp behavior mapping (depends on your old semantics)
            ["dontStopAppOnReset"] = !stopApp
        };

        ((IJavaScriptExecutor)WrappedAppiumDriver).ExecuteScript("mobile: startActivity", args);
    }
}