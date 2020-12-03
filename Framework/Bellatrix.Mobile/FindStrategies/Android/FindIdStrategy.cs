// <copyright file="ById.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Locators.Android
{
    public class FindIdStrategy : FindStrategy<AndroidDriver<AndroidElement>, AndroidElement>
    {
        private readonly string _locatorValue;

        public FindIdStrategy(string name)
            : base(name)
        {
            _locatorValue = $"new UiScrollable(new UiSelector()).scrollIntoView(new UiSelector().resourceId(\"{Value}\"));";
        }

        public override AndroidElement FindElement(AndroidDriver<AndroidElement> searchContext)
        {
            return searchContext.FindElementByAndroidUIAutomator(_locatorValue);
        }

        public override IEnumerable<AndroidElement> FindAllElements(AndroidDriver<AndroidElement> searchContext)
        {
            return searchContext.FindElementsByAndroidUIAutomator(_locatorValue);
        }

        public override AppiumWebElement FindElement(AndroidElement element)
        {
            return element.FindElementByAndroidUIAutomator(_locatorValue);
        }

        public override IEnumerable<AppiumWebElement> FindAllElements(AndroidElement element)
        {
            return element.FindElementsByAndroidUIAutomator(_locatorValue);
        }

        public override string ToString()
        {
            return $"ID = {Value}";
        }
    }
}
