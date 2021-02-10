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
        public static TElement CreateByIdEndingWith<TElement>(this Element element, string idEnding, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idEnding), shouldCacheElement);

        public static TElement CreateByTag<TElement>(this Element element, string tag, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindTagStrategy>(new FindTagStrategy(tag), shouldCacheElement);

        public static TElement CreateById<TElement>(this Element element, string id, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindIdStrategy>(new FindIdStrategy(id), shouldCacheElement);

        public static TElement CreateByIdContaining<TElement>(this Element element, string idContaining, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindIdContainingStrategy>(new FindIdContainingStrategy(idContaining));

        public static TElement CreateByValueContaining<TElement>(this Element element, string valueEnding, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindValueContainingStrategy>(new FindValueContainingStrategy(valueEnding), shouldCacheElement);

        public static TElement CreateByXpath<TElement>(this Element element, string xpath, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindXpathStrategy>(new FindXpathStrategy(xpath), shouldCacheElement);

        public static TElement CreateByLinkText<TElement>(this Element element, string linkText, bool shouldCacheElement = false)
          where TElement : Element => element.Create<TElement, FindLinkTextStrategy>(new FindLinkTextStrategy(linkText), shouldCacheElement);

        public static TElement CreateByLinkTextContaining<TElement>(this Element element, string linkTextContaining, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindLinkTextContainsStrategy>(new FindLinkTextContainsStrategy(linkTextContaining), shouldCacheElement);

        public static TElement CreateByClass<TElement>(this Element element, string cssClass, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindClassStrategy>(new FindClassStrategy(cssClass), shouldCacheElement);

        public static TElement CreateByCss<TElement>(this Element element, string cssClass, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindCssStrategy>(new FindCssStrategy(cssClass), shouldCacheElement);

        public static TElement CreateByClassContaining<TElement>(this Element element, string cssClassContaining, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining), shouldCacheElement);

        public static TElement CreateByInnerTextContaining<TElement>(this Element element, string innerText, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindInnerTextContainsStrategy>(new FindInnerTextContainsStrategy(innerText), shouldCacheElement);

        public static TElement CreateByNameEndingWith<TElement>(this Element element, string name, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindNameEndingWithStrategy>(new FindNameEndingWithStrategy(name), shouldCacheElement);

        public static TElement CreateByAttributesContaining<TElement>(this Element element, string attributeName, string value, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, FindAttributeContainingStrategy>(Find.By.AttributeContaining(attributeName, value), shouldCacheElement);

        public static ElementsList<TElement> CreateAllByIdEndingWith<TElement>(this Element element, string idEnding, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindIdEndingWithStrategy(idEnding), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByTag<TElement>(this Element element, string tag, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindTagStrategy(tag), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllById<TElement>(this Element element, string id, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindIdStrategy(id), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByIdContaining<TElement>(this Element element, string idContaining, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindIdContainingStrategy(idContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByValueContaining<TElement>(this Element element, string valueEnding, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindValueContainingStrategy(valueEnding), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByXpath<TElement>(this Element element, string xpath, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindXpathStrategy(xpath), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByLinkText<TElement>(this Element element, string linkText, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindLinkTextStrategy(linkText), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByLinkTextContaining<TElement>(this Element element, string linkTextContaining, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindLinkTextContainsStrategy(linkTextContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByClass<TElement>(this Element element, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindClassStrategy(cssClass), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByCss<TElement>(this Element element, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindCssStrategy(cssClass), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByClassContaining<TElement>(this Element element, string cssClassContaining, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindClassContainingStrategy(cssClassContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByInnerTextContaining<TElement>(this Element element, string innerText, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindInnerTextContainsStrategy(innerText), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByNameEndingWith<TElement>(this Element element, string name, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindNameEndingWithStrategy(name), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByAttributesContaining<TElement>(this Element element, string attributeName, string value, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new FindAttributeContainingStrategy(attributeName, value), element.WrappedElement, shouldCacheFoundElements);
    }
}