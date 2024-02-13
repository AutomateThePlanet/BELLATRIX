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
using Bellatrix.Mobile.Locators.Android;

namespace Bellatrix.Mobile.Android;

public class FindStrategyFactory
{
    public FindIdStrategy Id(string id) => new FindIdStrategy(id);

    public FindIdContainingStrategy IdContaining(string id) => new FindIdContainingStrategy(id);

    public FindDescriptionStrategy Description(string description) => new FindDescriptionStrategy(description);

    public FindDescriptionContainingStrategy DescriptionContaining(string description) => new FindDescriptionContainingStrategy(description);

    public FindTextStrategy Text(string text) => new FindTextStrategy(text);

    public FindTextContainingStrategy TextContaining(string text) => new FindTextContainingStrategy(text);

    public FindClassNameStrategy ClassName(string className) => new FindClassNameStrategy(className);

    public FindNameStrategy Name(string name) => new FindNameStrategy(name);

    public FindTagNameStrategy TagName(string tag) => new FindTagNameStrategy(tag);

    public FindXPathStrategy XPath(string name) => new FindXPathStrategy(name);

    public FindAndroidUIAutomatorStrategy AndroidUIAutomator(string name) => new FindAndroidUIAutomatorStrategy(name);
}
