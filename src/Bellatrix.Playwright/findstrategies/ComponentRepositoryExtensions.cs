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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Locators;
using Bellatrix.Playwright.Services;

namespace Bellatrix.Playwright;

public static class ComponentRepositoryExtensions
{
    public static TComponent CreateByIdEndingWith<TComponent>(this ComponentCreateService repository, string idEnding)
        where TComponent : Component => repository.Create<TComponent, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idEnding));

    public static TComponent CreateByTag<TComponent>(this ComponentCreateService repository, string tag)
        where TComponent : Component => repository.Create<TComponent, FindTagStrategy>(new FindTagStrategy(tag));

    public static TComponent CreateById<TComponent>(this ComponentCreateService repository, string id)
        where TComponent : Component => repository.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id));

    public static TComponent CreateByIdContaining<TComponent>(this ComponentCreateService repository, string idContaining)
        where TComponent : Component => repository.Create<TComponent, FindIdContainingStrategy>(new FindIdContainingStrategy(idContaining));

    public static TComponent CreateByValueContaining<TComponent>(this ComponentCreateService repository, string valueEnding)
        where TComponent : Component => repository.Create<TComponent, FindValueContainingStrategy>(new FindValueContainingStrategy(valueEnding));

    public static TComponent CreateByXpath<TComponent>(this ComponentCreateService repository, string xpath)
        where TComponent : Component => repository.Create<TComponent, FindXpathStrategy>(new FindXpathStrategy(xpath));

    public static TComponent CreateByLinkText<TComponent>(this ComponentCreateService repository, string linkText)
      where TComponent : Component => repository.Create<TComponent, FindLinkTextStrategy>(new FindLinkTextStrategy(linkText));

    public static TComponent CreateByLinkTextContaining<TComponent>(this ComponentCreateService repository, string linkTextContaining)
        where TComponent : Component => repository.Create<TComponent, FindLinkTextContainsStrategy>(new FindLinkTextContainsStrategy(linkTextContaining));

    public static TComponent CreateByClass<TComponent>(this ComponentCreateService repository, string cssClass)
        where TComponent : Component => repository.Create<TComponent, FindClassStrategy>(new FindClassStrategy(cssClass));

    public static TComponent CreateByCss<TComponent>(this ComponentCreateService repository, string cssClass)
        where TComponent : Component => repository.Create<TComponent, FindCssStrategy>(new FindCssStrategy(cssClass));

    public static TComponent CreateByClassContaining<TComponent>(this ComponentCreateService repository, string cssClassContaining)
        where TComponent : Component => repository.Create<TComponent, FindClassContainingStrategy>(new FindClassContainingStrategy(cssClassContaining));

    public static TComponent CreateByInnerTextContaining<TComponent>(this ComponentCreateService repository, string innerText)
        where TComponent : Component => repository.Create<TComponent, FindInnerTextContainsStrategy>(new FindInnerTextContainsStrategy(innerText));

    public static TComponent CreateByNameEndingWith<TComponent>(this ComponentCreateService repository, string name)
        where TComponent : Component => repository.Create<TComponent, FindNameEndingWithStrategy>(new FindNameEndingWithStrategy(name));

    public static TComponent CreateByAttributesContaining<TComponent>(this ComponentCreateService repository, string attributeName, string value)
        where TComponent : Component => repository.Create<TComponent, FindAttributeContainingStrategy>(Find.By.AttributeContaining(attributeName, value));

    public static ComponentsList<TComponent> CreateAllByIdEndingWith<TComponent>(this ComponentCreateService repository, string idEnding)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdEndingWithStrategy(idEnding));

    public static ComponentsList<TComponent> CreateAllByTag<TComponent>(this ComponentCreateService repository, string tag)
        where TComponent : Component => new ComponentsList<TComponent>(new FindTagStrategy(tag));

    public static ComponentsList<TComponent> CreateAllById<TComponent>(this ComponentCreateService repository, string id)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStrategy(id));

    public static ComponentsList<TComponent> CreateAllByIdContaining<TComponent>(this ComponentCreateService repository, string idContaining)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdContainingStrategy(idContaining));

    public static ComponentsList<TComponent> CreateAllByValueContaining<TComponent>(this ComponentCreateService repository, string valueEnding)
        where TComponent : Component => new ComponentsList<TComponent>(new FindValueContainingStrategy(valueEnding));

    public static ComponentsList<TComponent> CreateAllByXpath<TComponent>(this ComponentCreateService repository, string xpath)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXpathStrategy(xpath));

    public static ComponentsList<TComponent> CreateAllByLinkText<TComponent>(this ComponentCreateService repository, string linkText)
      where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextStrategy(linkText));

    public static ComponentsList<TComponent> CreateAllByLinkTextContaining<TComponent>(this ComponentCreateService repository, string linkTextContaining)
        where TComponent : Component => new ComponentsList<TComponent>(new FindLinkTextContainsStrategy(linkTextContaining));

    public static ComponentsList<TComponent> CreateAllByClass<TComponent>(this ComponentCreateService repository, string cssClass)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassStrategy(cssClass));

    public static ComponentsList<TComponent> CreateAllByCss<TComponent>(this ComponentCreateService repository, string cssClass)
        where TComponent : Component => new ComponentsList<TComponent>(new FindCssStrategy(cssClass));

    public static ComponentsList<TComponent> CreateAllByClassContaining<TComponent>(this ComponentCreateService repository, string classContaining)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassContainingStrategy(classContaining));

    public static ComponentsList<TComponent> CreateAllByInnerTextContaining<TComponent>(this ComponentCreateService repository, string innerText)
        where TComponent : Component => new ComponentsList<TComponent>(new FindInnerTextContainsStrategy(innerText));

    public static ComponentsList<TComponent> CreateAllByNameEndingWith<TComponent>(this ComponentCreateService repository, string name)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameEndingWithStrategy(name));

    public static ComponentsList<TComponent> CreateAllByAttributesContaining<TComponent>(this ComponentCreateService repository, string attributeName, string value)
        where TComponent : Component => new ComponentsList<TComponent>(Find.By.AttributeContaining(attributeName, value));
}