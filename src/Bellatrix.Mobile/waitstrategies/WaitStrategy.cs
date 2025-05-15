﻿// <copyright file="BaseUntil.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.Mobile.Configuration;
using Bellatrix.Mobile.Locators;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace Bellatrix.Mobile.Untils;

public abstract class WaitStrategy<TDriver, TDriverElement>
   where TDriver : AppiumDriver
   where TDriverElement : AppiumElement
{
    protected WaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
    {
        WrappedWebDriver = ServicesCollection.Current.Resolve<TDriver>();
        TimeoutInterval = timeoutInterval;
        SleepInterval = sleepInterval ?? ConfigurationService.GetSection<MobileSettings>().TimeoutSettings.SleepInterval;
    }

    protected TDriver WrappedWebDriver { get; }

    protected int? TimeoutInterval { get; set; }

    protected int? SleepInterval { get; }

    public abstract void WaitUntil<TBy>(TBy by)
        where TBy : FindStrategy<TDriver, TDriverElement>;

    protected void WaitUntil(Func<TDriver, bool> waitCondition, int? timeout, int? sleepInterval)
    {
        if (timeout != null && sleepInterval != null)
        {
            var timeoutTimeSpan = TimeSpan.FromSeconds((int)timeout);
            var sleepIntervalTimeSpan = TimeSpan.FromSeconds((int)sleepInterval);
            var wait = new AppiumDriverWait<TDriver, TDriverElement>(WrappedWebDriver, new SystemClock(), timeoutTimeSpan, sleepIntervalTimeSpan);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException), typeof(InvalidOperationException));
            wait.Until(waitCondition);
        }
    }
}
