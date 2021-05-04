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
using Bellatrix.Web.Locators;

namespace Bellatrix.Web
{
    public static class ElementCreateExtensions
    {
        public static TElement CreateByIdEndingWith<TElement>(this Component element, string idEnding, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idEnding), shouldCacheElement);

        public static TElement CreateByTag<TElement>(this Component element, string tag, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindTagStrategy>(new FindTagStrategy(tag), shouldCacheElement);

        public static TElement CreateById<TElement>(this Component element, string id, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindIdStrategy>(new FindIdStrategy(id), shouldCacheElement);

        public static TElement CreateByIdContaining<TElement>(this Component element, string idContaining, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindIdContainingStrategy>(new FindIdContainingStrategy(idContaining));

        public static TElement CreateByValueContaining<TElement>(this Component element, string valueEnding, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindValueContainingStrategy>(new FindValueContainingStrategy(valueEnding), shouldCacheElement);

        public static TElement CreateByXpath<TElement>(this Component element, string xpath, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindXpathStrategy>(new FindXpathStrategy(xpath), shouldCacheElement);

        public static TElement CreateByLinkText<TElement>(this Component element, string linkText, bool shouldCacheElement = false)
          where TElement : Component => element.Create<TElement, FindLinkTextStrategy>(new FindLinkTextStrategy(linkText), shouldCacheElement);

        public static TElement CreateByLinkTextContaining<TElement>(this Component element, string linkTextContaining, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindLinkTextContainsStrategy>(new FindLinkTextContainsStrategy(linkTextContaining), shouldCacheElement);

        public static TElement CreateByClass<TElement>(this Component element, string cssClass, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindClassStrategy>(new FindClassStrategy(cssClass), shouldCacheElement);

        public static TElement CreateByCss<TElement>(this Component element, string cssClass, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindCssStrategy>(new FindCssStrategy(cssClass), shouldCacheElement);

        public static TElement CreateByClassContaining<TElement>(this Component element, string cssClassContaining, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining), shouldCacheElement);

        public static TElement CreateByInnerTextContaining<TElement>(this Component element, string innerText, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindInnerTextContainsStrategy>(new FindInnerTextContainsStrategy(innerText), shouldCacheElement);

        public static TElement CreateByNameEndingWith<TElement>(this Component element, string name, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindNameEndingWithStrategy>(new FindNameEndingWithStrategy(name), shouldCacheElement);

        public static TElement CreateByAttributesContaining<TElement>(this Component element, string attributeName, string value, bool shouldCacheElement = false)
            where TElement : Component => element.Create<TElement, FindAttributeContainingStrategy>(Find.By.AttributeContaining(attributeName, value), shouldCacheElement);

        public static ComponentsList<TElement> CreateAllByIdEndingWith<TElement>(this Component element, string idEnding, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindIdEndingWithStrategy(idEnding), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByTag<TElement>(this Component element, string tag, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindTagStrategy(tag), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllById<TElement>(this Component element, string id, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindIdStrategy(id), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByIdContaining<TElement>(this Component element, string idContaining, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindIdContainingStrategy(idContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByValueContaining<TElement>(this Component element, string valueEnding, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindValueContainingStrategy(valueEnding), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByXpath<TElement>(this Component element, string xpath, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindXpathStrategy(xpath), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByLinkText<TElement>(this Component element, string linkText, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindLinkTextStrategy(linkText), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByLinkTextContaining<TElement>(this Component element, string linkTextContaining, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindLinkTextContainsStrategy(linkTextContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByClass<TElement>(this Component element, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindClassStrategy(cssClass), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByCss<TElement>(this Component element, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindCssStrategy(cssClass), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByClassContaining<TElement>(this Component element, string cssClassContaining, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindClassContainingStrategy(cssClassContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByInnerTextContaining<TElement>(this Component element, string innerText, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindInnerTextContainsStrategy(innerText), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByNameEndingWith<TElement>(this Component element, string name, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindNameEndingWithStrategy(name), element.WrappedElement, shouldCacheFoundElements);

        public static ComponentsList<TElement> CreateAllByAttributesContaining<TElement>(this Component element, string attributeName, string value, bool shouldCacheFoundElements = false)
            where TElement : Component => new ComponentsList<TElement>(new FindAttributeContainingStrategy(attributeName, value), element.WrappedElement, shouldCacheFoundElements);
    }
}