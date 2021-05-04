// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
        public static TElement CreateByIdEndingWith<TElement>(this Component element, string idPart)
            where TElement : Component => element.Create<TElement, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idPart));

        public static TElement CreateByTag<TElement>(this Component element, string tag)
            where TElement : Component => element.Create<TElement, FindTagNameStrategy>(new FindTagNameStrategy(tag));

        public static TElement CreateById<TElement>(this Component element, string id)
            where TElement : Component => element.Create<TElement, FindIdStrategy>(new FindIdStrategy(id));

        public static TElement CreateByAccessibilityId<TElement>(this Component element, string accessibilityId)
            where TElement : Component => element.Create<TElement, FindAccessibilityIdStrategy>(new FindAccessibilityIdStrategy(accessibilityId));

        public static TElement CreateByName<TElement>(this Component element, string name)
            where TElement : Component => element.Create<TElement, FindNameStrategy>(new FindNameStrategy(name));

        public static TElement CreateByClass<TElement>(this Component element, string elementClass)
            where TElement : Component => element.Create<TElement, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

        public static TElement CreateByAutomationId<TElement>(this Component element, string automationId)
            where TElement : Component => element.Create<TElement, FindAutomationIdStrategy>(new FindAutomationIdStrategy(automationId));

        public static TElement CreateByXPath<TElement>(this Component element, string xpath)
            where TElement : Component => element.Create<TElement, FindXPathStrategy>(new FindXPathStrategy(xpath));

        public static ComponentsList<TElement> CreateAllByIdEndingWith<TElement>(this Component element, string tag)
            where TElement : Component => new ComponentsList<TElement>(new FindIdEndingWithStrategy(tag), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByTag<TElement>(this Component element, string tag)
            where TElement : Component => new ComponentsList<TElement>(new FindTagNameStrategy(tag), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllById<TElement>(this Component element, string id)
            where TElement : Component => new ComponentsList<TElement>(new FindIdStrategy(id), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByAccessibilityId<TElement>(this Component element, string accessibilityId)
            where TElement : Component => new ComponentsList<TElement>(new FindAccessibilityIdStrategy(accessibilityId), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByName<TElement>(this Component element, string name)
            where TElement : Component => new ComponentsList<TElement>(new FindNameStrategy(name), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByClass<TElement>(this Component element, string elementClass)
            where TElement : Component => new ComponentsList<TElement>(new FindClassNameStrategy(elementClass), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByAutomationId<TElement>(this Component element, string automationId)
            where TElement : Component => new ComponentsList<TElement>(new FindAutomationIdStrategy(automationId), element.WrappedElement);

        public static ComponentsList<TElement> CreateAllByXPath<TElement>(this Component element, string xpath)
            where TElement : Component => new ComponentsList<TElement>(new FindXPathStrategy(xpath), element.WrappedElement);
    }
}