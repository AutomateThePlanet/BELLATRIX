// <copyright file="ByAccessibilityId.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Locators.IOS;

public class FindAccessibilityIdStrategy : FindStrategy<IOSDriver, AppiumElement>
{
    public FindAccessibilityIdStrategy(string name)
        : base(name)
    {
    }

    public override AppiumElement FindElement(IOSDriver searchContext)
    {
        return searchContext.FindElement(MobileBy.AccessibilityId(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(IOSDriver searchContext)
    {
        return searchContext.FindElements(MobileBy.AccessibilityId(Value)).AsEnumerable();
    }

    public override AppiumElement FindElement(AppiumElement element)
    {
        return element.FindElement(MobileBy.AccessibilityId(Value));
    }

    public override IEnumerable<AppiumElement> FindAllElements(AppiumElement element)
    {
        return element.FindElements(MobileBy.AccessibilityId(Value)).AsEnumerable();
    }

    public override string ToString()
    {
        return $"AccessibilityId = {Value}";
    }
}
