// <copyright file="ElementRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Locators;

namespace Bellatrix.Web
{
    public static class ElementRepositoryExtensions
    {
        public static TElement CreateByIdEndingWith<TElement>(this ElementCreateService repository, string idEnding, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idEnding), shouldCacheElement);

        public static TElement CreateByTag<TElement>(this ElementCreateService repository, string tag, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindTagStrategy>(new FindTagStrategy(tag), shouldCacheElement);

        public static TElement CreateById<TElement>(this ElementCreateService repository, string id, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindIdStrategy>(new FindIdStrategy(id), shouldCacheElement);

        public static TElement CreateByIdContaining<TElement>(this ElementCreateService repository, string idContaining, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindIdContainingStrategy>(new FindIdContainingStrategy(idContaining), shouldCacheElement);

        public static TElement CreateByValueContaining<TElement>(this ElementCreateService repository, string valueEnding, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindValueContainingStrategy>(new FindValueContainingStrategy(valueEnding), shouldCacheElement);

        public static TElement CreateByXpath<TElement>(this ElementCreateService repository, string xpath, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindXpathStrategy>(new FindXpathStrategy(xpath), shouldCacheElement);

        public static TElement CreateByLinkText<TElement>(this ElementCreateService repository, string linkText, bool shouldCacheElement = false)
          where TElement : Component => repository.Create<TElement, FindLinkTextStrategy>(new FindLinkTextStrategy(linkText), shouldCacheElement);

        public static TElement CreateByLinkTextContaining<TElement>(this ElementCreateService repository, string linkTextContaining, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindLinkTextContainsStrategy>(new FindLinkTextContainsStrategy(linkTextContaining), shouldCacheElement);

        public static TElement CreateByClass<TElement>(this ElementCreateService repository, string cssClass, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindClassStrategy>(new FindClassStrategy(cssClass), shouldCacheElement);

        public static TElement CreateByCss<TElement>(this ElementCreateService repository, string cssClass, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindCssStrategy>(new FindCssStrategy(cssClass), shouldCacheElement);

        public static TElement CreateByClassContaining<TElement>(this ElementCreateService repository, string cssClassContaining, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining), shouldCacheElement);

        public static TElement CreateByInnerTextContaining<TElement>(this ElementCreateService repository, string innerText, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindInnerTextContainsStrategy>(new FindInnerTextContainsStrategy(innerText), shouldCacheElement);

        public static TElement CreateByNameEndingWith<TElement>(this ElementCreateService repository, string name, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindNameEndingWithStrategy>(new FindNameEndingWithStrategy(name), shouldCacheElement);

        public static TElement CreateByAttributesContaining<TElement>(this ElementCreateService repository, string attributeName, string value, bool shouldCacheElement = false)
            where TElement : Component => repository.Create<TElement, FindAttributeContainingStrategy>(Find.By.AttributeContaining(attributeName, value), shouldCacheElement);

        public static ComponentsList<TElement> CreateAllByIdEndingWith<TElement>(this ElementCreateService repository, string idEnding, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindIdEndingWithStrategy(idEnding), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByTag<TElement>(this ElementCreateService repository, string tag, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindTagStrategy(tag), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllById<TElement>(this ElementCreateService repository, string id, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindIdStrategy(id), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByIdContaining<TElement>(this ElementCreateService repository, string idContaining, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindIdContainingStrategy(idContaining), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByValueContaining<TElement>(this ElementCreateService repository, string valueEnding, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindValueContainingStrategy(valueEnding), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByXpath<TElement>(this ElementCreateService repository, string xpath, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindXpathStrategy(xpath), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByLinkText<TElement>(this ElementCreateService repository, string linkText, bool shouldCacheFoundElements = false)
          where TElement : Component => new ComponentsList<TElement>(new FindLinkTextStrategy(linkText), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByLinkTextContaining<TElement>(this ElementCreateService repository, string linkTextContaining, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindLinkTextContainsStrategy(linkTextContaining), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByClass<TElement>(this ElementCreateService repository, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindClassStrategy(cssClass), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByCss<TElement>(this ElementCreateService repository, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindCssStrategy(cssClass), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByClassContaining<TElement>(this ElementCreateService repository, string classContaining, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindClassContainingStrategy(classContaining), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByInnerTextContaining<TElement>(this ElementCreateService repository, string innerText, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindInnerTextContainsStrategy(innerText), null, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByNameEndingWith<TElement>(this ElementCreateService repository, string name, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindNameEndingWithStrategy(name), null);

        public static ComponentsList<TElement> CreateAllByAttributesContaining<TElement>(this ElementCreateService repository, string attributeName, string value, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(Find.By.AttributeContaining(attributeName, value), null, shouldCacheFoundElements);
    }
}