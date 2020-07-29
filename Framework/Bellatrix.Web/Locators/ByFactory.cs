// <copyright file="ByFactory.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Locators;

namespace Bellatrix.Web
{
    public class ByFactory
    {
        public ById Id(string id) => new ById(id);

        public ByXpath Xpath(string xpath) => new ByXpath(xpath);

        public ByCss CssClass(string css) => new ByCss(css);

        public ByLinkText LinkText(string linkText) => new ByLinkText(linkText);

        public ByName Name(string name) => new ByName(name);

        public ByTag Tag(string tag) => new ByTag(tag);

        public By CssClassContaining(string cssClass) => new ByClassContaining(cssClass);

        public ByIdContaining IdContaining(string id) => new ByIdContaining(id);

        public ByIdEndingWith IdEndingWith(string id) => new ByIdEndingWith(id);

        public ByInnerTextContains InnerTextContains(string text) => new ByInnerTextContains(text);

        public ByLinkTextContains LinkTextContains(string text) => new ByLinkTextContains(text);

        public ByValueContaining ValueEndingWith(string value) => new ByValueContaining(value);

        public ByAttributeContaining AttributeContaining(string attributeName, string value) => new ByAttributeContaining(attributeName, value);
    }
}