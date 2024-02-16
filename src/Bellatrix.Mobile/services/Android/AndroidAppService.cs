// <copyright file="AndroidAppService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using OpenQA.Selenium.Appium.Android;

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

        WrappedAppiumDriver.StartActivity(appPackage, appActivity, appWaitPackage, appWaitActivity, stopApp);
    }

    public void StartActivityWithIntent(string appPackage, string appActivity, string intentAction, string appWaitPackage = "", string appWaitActivity = "", string intentCategory = "", string intentFlags = "", string intentOptionalArgs = "", bool stopApp = true)
    {
        try
        {
            WrappedAppiumDriver.HideKeyboard();
        }
        catch
        {
            // ignore
        }

        WrappedAppiumDriver.StartActivityWithIntent(appPackage, appActivity, intentAction, appWaitPackage, appWaitActivity, intentCategory, intentFlags, intentOptionalArgs, stopApp);
    }
}