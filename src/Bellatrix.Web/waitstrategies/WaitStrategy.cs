// <copyright file="BaseUntil.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Bellatrix.Web.Untils;

public abstract class WaitStrategy
{
    protected WaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
    {
        WrappedWebDriver = ServicesCollection.Current.Resolve<IWebDriver>();
        TimeoutInterval = timeoutInterval;
        SleepInterval = sleepInterval ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.SleepInterval;
    }

    protected IWebDriver WrappedWebDriver { get; }

    protected int? TimeoutInterval { get; set; }

    protected int? SleepInterval { get; }

    public abstract void WaitUntil<TBy>(TBy by)
        where TBy : FindStrategy;

    public abstract void WaitUntil<TBy>(TBy by, Component parent)
        where TBy : FindStrategy;

    protected void WaitUntil(Func<IWebDriver, bool> waitCondition, int? timeout, int? sleepInterval)
    {
        if (timeout != null && sleepInterval != null)
        {
            var timeoutTimeSpan = TimeSpan.FromSeconds((int)timeout);
            var sleepIntervalTimeSpan = TimeSpan.FromSeconds((int)sleepInterval);
            var wait = new WebDriverWait(new SystemClock(), WrappedWebDriver, timeoutTimeSpan, sleepIntervalTimeSpan);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(waitCondition);
        }
    }

    protected void WaitUntil(Func<IWebDriver, IWebElement> waitCondition, int? timeout, int? sleepInterval)
    {
        if (timeout != null && sleepInterval != null)
        {
            var timeoutTimeSpan = TimeSpan.FromSeconds((int)timeout);
            var sleepIntervalTimeSpan = TimeSpan.FromSeconds((int)sleepInterval);
            var wait = new WebDriverWait(new SystemClock(), WrappedWebDriver, timeoutTimeSpan, sleepIntervalTimeSpan);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(waitCondition);
        }
    }

    protected IWebElement FindElement<TBy>(ISearchContext searchContext, TBy by)
        where TBy : FindStrategy
    {
        var nativeElementFinder = new NativeElementFinderService(searchContext);
        var element = nativeElementFinder.Find(by);
        return element;
    }
}