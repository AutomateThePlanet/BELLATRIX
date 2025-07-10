﻿// <copyright file="ValidateControlExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Exceptions;
using Bellatrix.Desktop.Validates;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;

namespace Bellatrix.Desktop;

public static partial class ValidateControlExtensions
{
    private static void WaitUntil(Func<bool> waitCondition, string exceptionMessage, int? timeoutInSeconds, int? sleepIntervalInSeconds)
    {
        var localTimeout = timeoutInSeconds ?? ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.ValidationsTimeout;
        var localSleepInterval = sleepIntervalInSeconds ?? ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.SleepInterval;
        var wrappedWebDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
        var webDriverWait = new WebDriverWait(new SystemClock(), wrappedWebDriver, TimeSpan.FromSeconds(localTimeout), TimeSpan.FromSeconds(localSleepInterval));
        webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        bool LocalCondition(IWebDriver s)
        {
            try
            {
                return waitCondition();
            }
            catch (Exception)
            {
                return false;
            }
        }

        try
        {
            webDriverWait.Until(LocalCondition);
        }
        catch (WebDriverTimeoutException)
        {
            var elementPropertyValidateException = new ComponentPropertyValidateException(exceptionMessage);
            ValidatedExceptionThrowedEvent?.Invoke(waitCondition, new ComponentNotFulfillingValidateConditionEventArgs(elementPropertyValidateException));
            throw elementPropertyValidateException;
        }
    }

    public static event EventHandler<ComponentNotFulfillingValidateConditionEventArgs> ValidatedExceptionThrowedEvent;
}
