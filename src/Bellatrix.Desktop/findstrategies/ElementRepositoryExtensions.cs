// <copyright file="ElementRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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
   public static class ElementRepositoryExtensions
    {
        public static TElement CreateByIdEndingWith<TElement>(this ElementCreateService repo, string tag)
            where TElement : Element => repo.Create<TElement, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(tag));

        public static TElement CreateByTag<TElement>(this ElementCreateService repo, string tag)
            where TElement : Element => repo.Create<TElement, FindTagNameStrategy>(new FindTagNameStrategy(tag));

        public static TElement CreateById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element => repo.Create<TElement, FindIdStrategy>(new FindIdStrategy(id));

        public static TElement CreateByAccessibilityId<TElement>(this ElementCreateService repo, string accessibilityId)
            where TElement : Element => repo.Create<TElement, FindAccessibilityIdStrategy>(new FindAccessibilityIdStrategy(accessibilityId));

        public static TElement CreateByName<TElement>(this ElementCreateService repo, string name)
            where TElement : Element => repo.Create<TElement, FindNameStrategy>(new FindNameStrategy(name));

        public static TElement CreateByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element => repo.Create<TElement, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

        public static TElement CreateByAutomationId<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element => repo.Create<TElement, FindAutomationIdStrategy>(new FindAutomationIdStrategy(automationId));

        public static TElement CreateByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element => repo.Create<TElement, FindXPathStrategy>(new FindXPathStrategy(xpath));

        public static ElementsList<TElement> CreateAllByIdEndingWith<TElement>(this ElementCreateService repo, string tag)
            where TElement : Element => new ElementsList<TElement>(new FindIdEndingWithStrategy(tag), null);

        public static ElementsList<TElement> CreateAllByTag<TElement>(this ElementCreateService repo, string tag)
           where TElement : Element => new ElementsList<TElement>(new FindTagNameStrategy(tag), null);

        public static ElementsList<TElement> CreateAllById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element => new ElementsList<TElement>(new FindIdStrategy(id), null);

        public static ElementsList<TElement> CreateAllByAccessibilityId<TElement>(this ElementCreateService repo, string accessibilityId)
            where TElement : Element => new ElementsList<TElement>(new FindAccessibilityIdStrategy(accessibilityId), null);

        public static ElementsList<TElement> CreateAllByName<TElement>(this ElementCreateService repo, string name)
            where TElement : Element => new ElementsList<TElement>(new FindNameStrategy(name), null);

        public static ElementsList<TElement> CreateAllByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element => new ElementsList<TElement>(new FindClassNameStrategy(elementClass), null);

        public static ElementsList<TElement> CreateAllByAutomationId<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element => new ElementsList<TElement>(new FindAutomationIdStrategy(automationId), null);

        public static ElementsList<TElement> CreateAllByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element => new ElementsList<TElement>(new FindXPathStrategy(xpath), null);
    }
}
