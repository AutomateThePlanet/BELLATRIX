// <copyright file="UntilBeDisabled.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using Bellatrix.Web.Configuration;
using OpenQA.Selenium;

namespace Bellatrix.Web.Untils
{
    public class WaitToBeDisabledStrategy : WaitStrategy
    {
        public WaitToBeDisabledStrategy(int? timeoutInterval = null, int? sleepInterval = null)
            : base(timeoutInterval, sleepInterval)
        {
            TimeoutInterval = timeoutInterval ?? SettingsService.GetSection<TimeoutSettings>().ElementNotToBeVisibleTimeout;
        }

        public override void WaitUntil<TBy>(TBy by)
        {
            WaitUntil(d => ElementBeDisabled(WrappedWebDriver, by), TimeoutInterval, SleepInterval);
        }

        public override void WaitUntil<TBy>(TBy by, Element parent)
        {
            WaitUntil(d => ElementBeDisabled(parent.WrappedElement, by), TimeoutInterval, SleepInterval);
        }

        private bool ElementBeDisabled<TBy>(ISearchContext searchContext, TBy by)
            where TBy : FindStrategy
        {
            try
            {
                var element = searchContext.FindElement(by.Convert());
                return element != null && !element.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
        }
    }
}