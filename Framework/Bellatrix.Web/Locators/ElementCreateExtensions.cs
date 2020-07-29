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
using Bellatrix.Web.Locators;

namespace Bellatrix.Web
{
    public static class ElementCreateExtensions
    {
        public static TElement CreateByIdEndingWith<TElement>(this Element element, string idEnding, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByIdEndingWith>(new ByIdEndingWith(idEnding), shouldCacheElement);

        public static TElement CreateByTag<TElement>(this Element element, string tag, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByTag>(new ByTag(tag), shouldCacheElement);

        public static TElement CreateById<TElement>(this Element element, string id, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ById>(new ById(id), shouldCacheElement);

        public static TElement CreateByIdContaining<TElement>(this Element element, string idContaining, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByIdContaining>(new ByIdContaining(idContaining));

        public static TElement CreateByValueContaining<TElement>(this Element element, string valueEnding, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByValueContaining>(new ByValueContaining(valueEnding), shouldCacheElement);

        public static TElement CreateByXpath<TElement>(this Element element, string xpath, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByXpath>(new ByXpath(xpath), shouldCacheElement);

        public static TElement CreateByLinkText<TElement>(this Element element, string linkText, bool shouldCacheElement = false)
          where TElement : Element => element.Create<TElement, ByLinkText>(new ByLinkText(linkText), shouldCacheElement);

        public static TElement CreateByLinkTextContaining<TElement>(this Element element, string linkTextContaining, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByLinkTextContains>(new ByLinkTextContains(linkTextContaining), shouldCacheElement);

        public static TElement CreateByClass<TElement>(this Element element, string cssClass, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByClass>(new ByClass(cssClass), shouldCacheElement);

        public static TElement CreateByCss<TElement>(this Element element, string cssClass, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByCss>(new ByCss(cssClass), shouldCacheElement);

        public static TElement CreateByClassContaining<TElement>(this Element element, string cssClassContaining, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByClassContaining>(new ByClassContaining(cssClassContaining), shouldCacheElement);

        public static TElement CreateByInnerTextContaining<TElement>(this Element element, string innerText, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByInnerTextContains>(new ByInnerTextContains(innerText), shouldCacheElement);

        public static TElement CreateByNameEndingWith<TElement>(this Element element, string name, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByNameEndingWith>(new ByNameEndingWith(name), shouldCacheElement);

        public static TElement CreateByAttributesContaining<TElement>(this Element element, string attributeName, string value, bool shouldCacheElement = false)
            where TElement : Element => element.Create<TElement, ByAttributeContaining>(Find.By.AttributeContaining(attributeName, value), shouldCacheElement);

        public static ElementsList<TElement> CreateAllByIdEndingWith<TElement>(this Element element, string idEnding, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByIdEndingWith(idEnding), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByTag<TElement>(this Element element, string tag, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByTag(tag), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllById<TElement>(this Element element, string id, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ById(id), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByIdContaining<TElement>(this Element element, string idContaining, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByIdContaining(idContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByValueContaining<TElement>(this Element element, string valueEnding, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByValueContaining(valueEnding), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByXpath<TElement>(this Element element, string xpath, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByXpath(xpath), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByLinkText<TElement>(this Element element, string linkText, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByLinkText(linkText), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByLinkTextContaining<TElement>(this Element element, string linkTextContaining, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByLinkTextContains(linkTextContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByClass<TElement>(this Element element, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByClass(cssClass), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByCss<TElement>(this Element element, string cssClass, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByCss(cssClass), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByClassContaining<TElement>(this Element element, string cssClassContaining, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByClassContaining(cssClassContaining), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByInnerTextContaining<TElement>(this Element element, string innerText, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByInnerTextContains(innerText), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByNameEndingWith<TElement>(this Element element, string name, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByNameEndingWith(name), element.WrappedElement, shouldCacheFoundElements);

        public static ElementsList<TElement> CreateAllByAttributesContaining<TElement>(this Element element, string attributeName, string value, bool shouldCacheFoundElements = false)
            where TElement : Element => new ElementsList<TElement>(new ByAttributeContaining(attributeName, value), element.WrappedElement, shouldCacheFoundElements);
    }
}