// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Controls.Core;
using Bellatrix.Desktop.Locators;

namespace Bellatrix.Desktop
{
   public static class ElementCreateExtensions
    {
        public static TElement CreateByIdEndingWith<TElement>(this Element element, string idPart)
            where TElement : Element => element.Create<TElement, ByIdEndingWith>(new ByIdEndingWith(idPart));

        public static TElement CreateByTag<TElement>(this Element element, string tag)
            where TElement : Element => element.Create<TElement, ByTagName>(new ByTagName(tag));

        public static TElement CreateById<TElement>(this Element element, string id)
            where TElement : Element => element.Create<TElement, ById>(new ById(id));

        public static TElement CreateByAccessibilityId<TElement>(this Element element, string accessibilityId)
            where TElement : Element => element.Create<TElement, ByAccessibilityId>(new ByAccessibilityId(accessibilityId));

        public static TElement CreateByName<TElement>(this Element element, string name)
            where TElement : Element => element.Create<TElement, ByName>(new ByName(name));

        public static TElement CreateByClass<TElement>(this Element element, string elementClass)
            where TElement : Element => element.Create<TElement, ByClassName>(new ByClassName(elementClass));

        public static TElement CreateByAutomationId<TElement>(this Element element, string automationId)
            where TElement : Element => element.Create<TElement, ByAutomationId>(new ByAutomationId(automationId));

        public static TElement CreateByXPath<TElement>(this Element element, string xpath)
            where TElement : Element => element.Create<TElement, ByXPath>(new ByXPath(xpath));

        public static ElementsList<TElement> CreateAllByIdEndingWith<TElement>(this Element element, string tag)
            where TElement : Element => new ElementsList<TElement>(new ByIdEndingWith(tag), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByTag<TElement>(this Element element, string tag)
            where TElement : Element => new ElementsList<TElement>(new ByTagName(tag), element.WrappedElement);

        public static ElementsList<TElement> CreateAllById<TElement>(this Element element, string id)
            where TElement : Element => new ElementsList<TElement>(new ById(id), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByAccessibilityId<TElement>(this Element element, string accessibilityId)
            where TElement : Element => new ElementsList<TElement>(new ByAccessibilityId(accessibilityId), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByName<TElement>(this Element element, string name)
            where TElement : Element => new ElementsList<TElement>(new ByName(name), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByClass<TElement>(this Element element, string elementClass)
            where TElement : Element => new ElementsList<TElement>(new ByClassName(elementClass), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByAutomationId<TElement>(this Element element, string automationId)
            where TElement : Element => new ElementsList<TElement>(new ByAutomationId(automationId), element.WrappedElement);

        public static ElementsList<TElement> CreateAllByXPath<TElement>(this Element element, string xpath)
            where TElement : Element => new ElementsList<TElement>(new ByXPath(xpath), element.WrappedElement);
    }
}