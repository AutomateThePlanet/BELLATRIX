// <copyright file="ElementCreateService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Services;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile
{
    public class ElementCreateService
    {
        public TElement Create<TElement, TBy, TDriver, TDriverElement>(TBy by)
            where TElement : Element<TDriver, TDriverElement>
            where TBy : By<TDriver, TDriverElement>
            where TDriver : AppiumDriver<TDriverElement>
            where TDriverElement : AppiumWebElement
        {
            var elementRepository = new ElementRepository();
            return elementRepository.CreateElementThatIsFound<TElement, TBy, TDriver, TDriverElement>(by, null);
        }

        public ElementsList<TElement, TBy, TDriver, TDriverElement> CreateAll<TElement, TBy, TDriver, TDriverElement>(TBy by)
            where TElement : Element<TDriver, TDriverElement>
            where TBy : By<TDriver, TDriverElement>
            where TDriver : AppiumDriver<TDriverElement>
            where TDriverElement : AppiumWebElement => new ElementsList<TElement, TBy, TDriver, TDriverElement>(by, null);
    }
}