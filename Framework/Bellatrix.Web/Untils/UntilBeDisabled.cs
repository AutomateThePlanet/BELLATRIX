// <copyright file="UntilBeDisabled.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium;

namespace Bellatrix.Web.Untils
{
    public class UntilBeDisabled : BaseUntil
    {
        public UntilBeDisabled(int? timeoutInterval = null, int? sleepInterval = null)
            : base(timeoutInterval, sleepInterval)
            => TimeoutInterval = timeoutInterval ?? ConfigurationService.Instance.GetTimeoutSettings().ElementNotToBeVisibleTimeout;

        public override void WaitUntil<TBy>(TBy by) => WaitUntil(ElementBeDisabled(WrappedWebDriver, by), TimeoutInterval, SleepInterval);
        public override void WaitUntil<TBy>(TBy by, Element parent) => WaitUntil(ElementBeDisabled(parent.WrappedElement, by), TimeoutInterval, SleepInterval);

        private Func<IWebDriver, bool> ElementBeDisabled<TBy>(ISearchContext searchContext, TBy by)
            where TBy : By => driver =>
                                        {
                                            try
                                            {
                                                var element = FindElement(searchContext, by);
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
                                        };
    }
}