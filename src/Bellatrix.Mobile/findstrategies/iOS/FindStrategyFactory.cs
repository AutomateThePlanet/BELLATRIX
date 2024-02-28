// <copyright file="ByFactory.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Locators.IOS;

namespace Bellatrix.Mobile.IOS;

public class FindStrategyFactory
{
    public FindStrategyId Id(string id) => new FindStrategyId(id);

    public FindAccessibilityIdStrategy AccessibilityId(string css) => new FindAccessibilityIdStrategy(css);

    public FindClassNameStrategy ClassName(string linkText) => new FindClassNameStrategy(linkText);

    public FindNameStrategy Name(string name) => new FindNameStrategy(name);

    public FindTagNameStrategy TagName(string tag) => new FindTagNameStrategy(tag);

    public FindXPathStrategy XPath(string name) => new FindXPathStrategy(name);

    public FindIOSUIAutomationStrategy IOSUIAutomation(string name) => new FindIOSUIAutomationStrategy(name);

    public FindValueContainingStrategy ValueContaining(string name) => new FindValueContainingStrategy(name);
}
