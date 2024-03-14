// <copyright file="JavaScriptFindStrategy.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Locators;
using Bellatrix.Playwright.SyncPlaywright;

namespace Bellatrix.Playwright;

public class JavaScriptFindStrategy : FindStrategy
{
    private readonly object[] _args;

    public JavaScriptFindStrategy(string value, params object[] args)
        : base(value) => _args = args;

    public override WebElement Convert(IPage searchContext)
    {
        return new PlaywrightJavaScriptLocator(Value, _args).FindElement(searchContext);
    }

    public override WebElement Convert(WebElement searchContext)
    {
        return new PlaywrightJavaScriptLocator(Value, _args).FindElement(searchContext);
    }
}