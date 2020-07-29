// <copyright file="EnsureControlExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System;
using Bellatrix.Mobile.Ensures;
using Bellatrix.Mobile.Exceptions;
using Bellatrix.Mobile.Untils;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace Bellatrix.Mobile
{
    internal static class EnsureControlWaitService
    {
        internal static void WaitUntil<TDriver, TDriverElement>(Func<bool> waitCondition, string exceptionMessage, int? timeoutInMilliseconds, int? sleepIntervalInMilliseconds)
            where TDriver : AppiumDriver<TDriverElement>
            where TDriverElement : AppiumWebElement
        {
            var localTimeout = timeoutInMilliseconds ?? 5000;
            var localSleepInterval = sleepIntervalInMilliseconds ?? 500;
            var wrappedWebDriver = ServicesCollection.Current.Resolve<TDriver>();
            var webDriverWait = new AppiumDriverWait<TDriver, TDriverElement>(wrappedWebDriver, new SystemClock(), TimeSpan.FromMilliseconds(localTimeout), TimeSpan.FromMilliseconds(localSleepInterval));
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
                var elementPropertyEnsureException = new ElementPropertyEnsureException(exceptionMessage);
                EnsuredExceptionThrowedEvent?.Invoke(waitCondition, new ElementNotFulfillingEnsureConditionEventArgs(elementPropertyEnsureException));
                throw elementPropertyEnsureException;
            }
        }

        public static event EventHandler<ElementNotFulfillingEnsureConditionEventArgs> EnsuredExceptionThrowedEvent;
    }
}
