// <copyright file="WaitNotBeVisibleStrategy.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.Desktop.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Untils;

public class WaitNotBeVisibleStrategy : WaitStrategy
{
    public WaitNotBeVisibleStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.ElementNotToBeVisibleTimeout;
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(d => ElementIsInvisible(WrappedWebDriver, by), TimeoutInterval, SleepInterval);
    }

    private bool ElementIsInvisible<TBy>(WindowsDriver<WindowsElement> searchContext, TBy by)
        where TBy : Locators.FindStrategy
    {
        try
        {
            var element = by.FindElement(searchContext);
            return !element.Displayed;
        }
        catch (NoSuchElementException)
        {
            // Returns true because the element is not present in DOM. The
            // try block checks if the element is present but is invisible.
            return true;
        }
        catch (InvalidOperationException)
        {
            return true;
        }
        catch (StaleElementReferenceException)
        {
            // Returns true because stale element reference implies that element
            // is no longer visible.
            return true;
        }
    }
}
