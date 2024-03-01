// <copyright file="ComponentCreateExtensions.cs" company="Automate The Planet Ltd.">
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

public static class ComponentCreateExtensions
{
    public static TComponent CreateByIdEndingWith<TComponent>(this Component element, string idEnding, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idEnding), shouldCacheElement);

    public static TComponent CreateByTag<TComponent>(this Component element, string tag, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindTagStrategy>(new FindTagStrategy(tag), shouldCacheElement);

    public static TComponent CreateById<TComponent>(this Component element, string id, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id), shouldCacheElement);

    public static TComponent CreateByIdContaining<TComponent>(this Component element, string idContaining, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindIdContainingStrategy>(new FindIdContainingStrategy(idContaining));

    public static TComponent CreateByValueContaining<TComponent>(this Component element, string valueEnding, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindValueContainingStrategy>(new FindValueContainingStrategy(valueEnding), shouldCacheElement);

    public static TComponent CreateByXpath<TComponent>(this Component element, string xpath, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindXpathStrategy>(new FindXpathStrategy(xpath), shouldCacheElement);

    public static TComponent CreateByLinkText<TComponent>(this Component element, string linkText, bool shouldCacheElement = false)
      where TComponent : Component => element.Create<TComponent, FindLinkTextStrategy>(new FindLinkTextStrategy(linkText), shouldCacheElement);

    public static TComponent CreateByLinkTextContaining<TComponent>(this Component element, string linkTextContaining, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindLinkTextContainsStrategy>(new FindLinkTextContainsStrategy(linkTextContaining), shouldCacheElement);

    public static TComponent CreateByClass<TComponent>(this Component element, string cssClass, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindClassStrategy>(new FindClassStrategy(cssClass), shouldCacheElement);

    public static TComponent CreateByCss<TComponent>(this Component element, string cssClass, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindCssStrategy>(new FindCssStrategy(cssClass), shouldCacheElement);

    public static TComponent CreateByClassContaining<TComponent>(this Component element, string cssClassContaining, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining), shouldCacheElement);

    public static TComponent CreateByInnerTextContaining<TComponent>(this Component element, string innerText, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindInnerTextContainsStrategy>(new FindInnerTextContainsStrategy(innerText), shouldCacheElement);

    public static TComponent CreateByNameEndingWith<TComponent>(this Component element, string name, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindNameEndingWithStrategy>(new FindNameEndingWithStrategy(name), shouldCacheElement);

    public static TComponent CreateByAttributesContaining<TComponent>(this Component element, string attributeName, string value, bool shouldCacheElement = false)
        where TComponent : Component => element.Create<TComponent, FindAttributeContainingStrategy>(Find.By.AttributeContaining(attributeName, value), shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByIdEndingWith<TComponent>(this Component element, string idEnding, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdEndingWithStrategy(idEnding), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByTag<TComponent>(this Component element, string tag, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindTagStrategy(tag), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllById<TComponent>(this Component element, string id, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStrategy(id), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByIdContaining<TComponent>(this Component element, string idContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdContainingStrategy(idContaining), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByValueContaining<TComponent>(this Component element, string valueEnding, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindValueContainingStrategy(valueEnding), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByXpath<TComponent>(this Component element, string xpath, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXpathStrategy(xpath), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByLinkText<TComponent>(this Component element, string linkText, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextStrategy(linkText), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByLinkTextContaining<TComponent>(this Component element, string linkTextContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextContainsStrategy(linkTextContaining), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByClass<TComponent>(this Component element, string cssClass, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassStrategy(cssClass), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByCss<TComponent>(this Component element, string cssClass, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindCssStrategy(cssClass), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByClassContaining<TComponent>(this Component element, string cssClassContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassContainingStrategy(cssClassContaining), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByInnerTextContaining<TComponent>(this Component element, string innerText, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindInnerTextContainsStrategy(innerText), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByNameEndingWith<TComponent>(this Component element, string name, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameEndingWithStrategy(name), element.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByAttributesContaining<TComponent>(this Component element, string attributeName, string value, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindAttributeContainingStrategy(attributeName, value), element.WrappedElement, shouldCacheFoundElements);
}