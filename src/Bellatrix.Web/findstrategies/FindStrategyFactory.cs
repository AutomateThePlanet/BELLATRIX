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
using Bellatrix.Web.Locators;

namespace Bellatrix.Web;

public class FindStrategyFactory
{
    public FindIdStrategy Id(string id) => new FindIdStrategy(id);

    public FindXpathStrategy Xpath(string xpath) => new FindXpathStrategy(xpath);

    public FindCssStrategy CssClass(string css) => new FindCssStrategy(css);

    public FindLinkTextStrategy LinkText(string linkText) => new FindLinkTextStrategy(linkText);

    public FindNameStrategy Name(string name) => new FindNameStrategy(name);

    public FindTagStrategy Tag(string tag) => new FindTagStrategy(tag);

    public FindStrategy CssClassContaining(string cssClass) => new FindClassContainingStrategy(cssClass);

    public FindIdContainingStrategy IdContaining(string id) => new FindIdContainingStrategy(id);

    public FindIdEndingWithStrategy IdEndingWith(string id) => new FindIdEndingWithStrategy(id);

    public FindInnerTextContainsStrategy InnerTextContains(string text) => new FindInnerTextContainsStrategy(text);

    public FindLinkTextContainsStrategy LinkTextContains(string text) => new FindLinkTextContainsStrategy(text);

    public FindValueContainingStrategy ValueEndingWith(string value) => new FindValueContainingStrategy(value);

    public FindAttributeContainingStrategy AttributeContaining(string attributeName, string value) => new FindAttributeContainingStrategy(attributeName, value);
}