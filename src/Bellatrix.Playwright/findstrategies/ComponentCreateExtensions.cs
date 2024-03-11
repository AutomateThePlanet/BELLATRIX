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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Locators;

namespace Bellatrix.Playwright;

public static class ComponentCreateExtensions
{
    public static TComponent CreateByIdEndingWith<TComponent>(this Component component, string idEnding, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idEnding), shouldCacheElement);

    public static TComponent CreateByTag<TComponent>(this Component component, string tag, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindTagStrategy>(new FindTagStrategy(tag), shouldCacheElement);

    public static TComponent CreateById<TComponent>(this Component component, string id, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id), shouldCacheElement);

    public static TComponent CreateByIdContaining<TComponent>(this Component component, string idContaining, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindIdContainingStrategy>(new FindIdContainingStrategy(idContaining));

    public static TComponent CreateByValueContaining<TComponent>(this Component component, string valueEnding, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindValueContainingStrategy>(new FindValueContainingStrategy(valueEnding), shouldCacheElement);

    public static TComponent CreateByXpath<TComponent>(this Component component, string xpath, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindXpathStrategy>(new FindXpathStrategy(xpath), shouldCacheElement);

    public static TComponent CreateByLinkText<TComponent>(this Component component, string linkText, bool shouldCacheElement = false)
      where TComponent : Component => component.Create<TComponent, FindLinkTextStrategy>(new FindLinkTextStrategy(linkText), shouldCacheElement);

    public static TComponent CreateByLinkTextContaining<TComponent>(this Component component, string linkTextContaining, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindLinkTextContainsStrategy>(new FindLinkTextContainsStrategy(linkTextContaining), shouldCacheElement);

    public static TComponent CreateByClass<TComponent>(this Component component, string cssClass, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindClassStrategy>(new FindClassStrategy(cssClass), shouldCacheElement);

    public static TComponent CreateByCss<TComponent>(this Component component, string cssClass, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindCssStrategy>(new FindCssStrategy(cssClass), shouldCacheElement);

    public static TComponent CreateByClassContaining<TComponent>(this Component component, string cssClassContaining, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining), shouldCacheElement);

    public static TComponent CreateByInnerTextContaining<TComponent>(this Component component, string innerText, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindInnerTextContainsStrategy>(new FindInnerTextContainsStrategy(innerText), shouldCacheElement);

    public static TComponent CreateByNameEndingWith<TComponent>(this Component component, string name, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindNameEndingWithStrategy>(new FindNameEndingWithStrategy(name), shouldCacheElement);

    public static TComponent CreateByAttributesContaining<TComponent>(this Component component, string attributeName, string value, bool shouldCacheElement = false)
        where TComponent : Component => component.Create<TComponent, FindAttributeContainingStrategy>(Find.By.AttributeContaining(attributeName, value), shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByIdEndingWith<TComponent>(this Component component, string idEnding, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdEndingWithStrategy(idEnding), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByTag<TComponent>(this Component component, string tag, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindTagStrategy(tag), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllById<TComponent>(this Component component, string id, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStrategy(id), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByIdContaining<TComponent>(this Component component, string idContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdContainingStrategy(idContaining), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByValueContaining<TComponent>(this Component component, string valueEnding, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindValueContainingStrategy(valueEnding), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByXpath<TComponent>(this Component component, string xpath, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXpathStrategy(xpath), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByLinkText<TComponent>(this Component component, string linkText, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextStrategy(linkText), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByLinkTextContaining<TComponent>(this Component component, string linkTextContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextContainsStrategy(linkTextContaining), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByClass<TComponent>(this Component component, string cssClass, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassStrategy(cssClass), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByCss<TComponent>(this Component component, string cssClass, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindCssStrategy(cssClass), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByClassContaining<TComponent>(this Component component, string cssClassContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassContainingStrategy(cssClassContaining), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByInnerTextContaining<TComponent>(this Component component, string innerText, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindInnerTextContainsStrategy(innerText), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByNameEndingWith<TComponent>(this Component component, string name, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameEndingWithStrategy(name), component.WrappedElement, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByAttributesContaining<TComponent>(this Component component, string attributeName, string value, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindAttributeContainingStrategy(attributeName, value), component.WrappedElement, shouldCacheFoundElements);
}