// <copyright file="UntilBeClickable.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Web.Untils;

public class WaitToBeClickableStrategy : WaitStrategy
{
    public WaitToBeClickableStrategy(int? timeoutInterval = null, int? sleepInterval = null)
        : base(timeoutInterval, sleepInterval)
    {
        TimeoutInterval = timeoutInterval ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.ElementToBeClickableTimeout;
    }

    public override void WaitUntil<TBy>(TBy by)
    {
        WaitUntil(x => ElementIsClickable(WrappedWebDriver, by), TimeoutInterval, SleepInterval);
    }

    public override void WaitUntil<TBy>(TBy by, Component parent)
    {
        WaitUntil(x => ElementIsClickable(parent.WrappedElement, by), TimeoutInterval, SleepInterval);
    }

    private bool ElementIsClickable<TBy>(ISearchContext searchContext, TBy by)
        where TBy : FindStrategy
    {
        var element = searchContext.FindElement(by.Convert());
        try
        {
            return element != null && element.Enabled;
        }
        catch (StaleElementReferenceException)
        {
            return false;
        }
    }
}