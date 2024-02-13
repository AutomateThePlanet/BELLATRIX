// <copyright file="ComponentRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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

public static class ComponentRepositoryExtensions
{
    public static TComponent CreateByIdEndingWith<TComponent>(this ComponentCreateService repository, string idEnding, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idEnding), shouldCacheElement);

    public static TComponent CreateByTag<TComponent>(this ComponentCreateService repository, string tag, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindTagStrategy>(new FindTagStrategy(tag), shouldCacheElement);

    public static TComponent CreateById<TComponent>(this ComponentCreateService repository, string id, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id), shouldCacheElement);

    public static TComponent CreateByIdContaining<TComponent>(this ComponentCreateService repository, string idContaining, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindIdContainingStrategy>(new FindIdContainingStrategy(idContaining), shouldCacheElement);

    public static TComponent CreateByValueContaining<TComponent>(this ComponentCreateService repository, string valueEnding, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindValueContainingStrategy>(new FindValueContainingStrategy(valueEnding), shouldCacheElement);

    public static TComponent CreateByXpath<TComponent>(this ComponentCreateService repository, string xpath, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindXpathStrategy>(new FindXpathStrategy(xpath), shouldCacheElement);

    public static TComponent CreateByLinkText<TComponent>(this ComponentCreateService repository, string linkText, bool shouldCacheElement = false)
      where TComponent : Component => repository.Create<TComponent, FindLinkTextStrategy>(new FindLinkTextStrategy(linkText), shouldCacheElement);

    public static TComponent CreateByLinkTextContaining<TComponent>(this ComponentCreateService repository, string linkTextContaining, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindLinkTextContainsStrategy>(new FindLinkTextContainsStrategy(linkTextContaining), shouldCacheElement);

    public static TComponent CreateByClass<TComponent>(this ComponentCreateService repository, string cssClass, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindClassStrategy>(new FindClassStrategy(cssClass), shouldCacheElement);

    public static TComponent CreateByCss<TComponent>(this ComponentCreateService repository, string cssClass, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindCssStrategy>(new FindCssStrategy(cssClass), shouldCacheElement);

    public static TComponent CreateByClassContaining<TComponent>(this ComponentCreateService repository, string cssClassContaining, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining), shouldCacheElement);

    public static TComponent CreateByInnerTextContaining<TComponent>(this ComponentCreateService repository, string innerText, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindInnerTextContainsStrategy>(new FindInnerTextContainsStrategy(innerText), shouldCacheElement);

    public static TComponent CreateByNameEndingWith<TComponent>(this ComponentCreateService repository, string name, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindNameEndingWithStrategy>(new FindNameEndingWithStrategy(name), shouldCacheElement);

    public static TComponent CreateByAttributesContaining<TComponent>(this ComponentCreateService repository, string attributeName, string value, bool shouldCacheElement = false)
        where TComponent : Component => repository.Create<TComponent, FindAttributeContainingStrategy>(Find.By.AttributeContaining(attributeName, value), shouldCacheElement);

    public static ComponentsList<TComponent> CreateAllByIdEndingWith<TComponent>(this ComponentCreateService repository, string idEnding, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdEndingWithStrategy(idEnding), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByTag<TComponent>(this ComponentCreateService repository, string tag, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindTagStrategy(tag), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllById<TComponent>(this ComponentCreateService repository, string id, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStrategy(id), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByIdContaining<TComponent>(this ComponentCreateService repository, string idContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdContainingStrategy(idContaining), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByValueContaining<TComponent>(this ComponentCreateService repository, string valueEnding, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindValueContainingStrategy(valueEnding), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByXpath<TComponent>(this ComponentCreateService repository, string xpath, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXpathStrategy(xpath), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByLinkText<TComponent>(this ComponentCreateService repository, string linkText, bool shouldCacheFoundElements = false)
      where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextStrategy(linkText), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByLinkTextContaining<TComponent>(this ComponentCreateService repository, string linkTextContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextContainsStrategy(linkTextContaining), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByClass<TComponent>(this ComponentCreateService repository, string cssClass, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassStrategy(cssClass), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByCss<TComponent>(this ComponentCreateService repository, string cssClass, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindCssStrategy(cssClass), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByClassContaining<TComponent>(this ComponentCreateService repository, string classContaining, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassContainingStrategy(classContaining), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByInnerTextContaining<TComponent>(this ComponentCreateService repository, string innerText, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindInnerTextContainsStrategy(innerText), null, shouldCacheFoundElements);

    public static ComponentsList<TComponent> CreateAllByNameEndingWith<TComponent>(this ComponentCreateService repository, string name, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameEndingWithStrategy(name), null);

    public static ComponentsList<TComponent> CreateAllByAttributesContaining<TComponent>(this ComponentCreateService repository, string attributeName, string value, bool shouldCacheFoundElements = false)
        where TComponent : Component => new ComponentsList<TComponent>(Find.By.AttributeContaining(attributeName, value), null, shouldCacheFoundElements);
}