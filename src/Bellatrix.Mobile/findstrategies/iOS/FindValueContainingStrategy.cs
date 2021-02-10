// <copyright file="ByValueContaining.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Locators.IOS
{
    public class FindValueContainingStrategy : FindStrategy<IOSDriver<IOSElement>, IOSElement>
    {
        private readonly string _locatorValue;

        public FindValueContainingStrategy(string name)
            : base(name)
        {
            _locatorValue = $"//*[contains(@value, '{Value}')]";
        }

        public override IOSElement FindElement(IOSDriver<IOSElement> searchContext)
        {
            return searchContext.FindElementByXPath(_locatorValue);
        }

        public override IEnumerable<IOSElement> FindAllElements(IOSDriver<IOSElement> searchContext)
        {
            return searchContext.FindElementsByXPath(_locatorValue);
        }

        public override AppiumWebElement FindElement(IOSElement element)
        {
            return element.FindElementByXPath(_locatorValue);
        }

        public override IEnumerable<AppiumWebElement> FindAllElements(IOSElement element)
        {
            return element.FindElementsByXPath(_locatorValue);
        }

        public override string ToString()
        {
            return $"Value contains {Value}";
        }
    }
}
