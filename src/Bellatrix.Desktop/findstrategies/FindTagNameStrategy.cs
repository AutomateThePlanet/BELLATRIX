// <copyright file="ByTagName.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Desktop.Locators;

public class FindTagNameStrategy : FindStrategy
{
    public FindTagNameStrategy(string name)
        : base(name)
    {
    }

    public override AppiumElement FindElement(ISearchContext searchContext)
    {
        return searchContext.FindElement(By.TagName(Value)) as AppiumElement;
    }

    public override IEnumerable<AppiumElement> FindAllElements(ISearchContext searchContext)
    {
        return searchContext.FindElements(By.TagName(Value)).Select(el => el as AppiumElement);
    }

    public override string ToString()
    {
        return $"TagName = {Value}";
    }
}