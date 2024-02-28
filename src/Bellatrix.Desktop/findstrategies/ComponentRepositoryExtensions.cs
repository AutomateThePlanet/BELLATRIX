// <copyright file="ElementRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Controls.Core;
using Bellatrix.Desktop.Locators;

namespace Bellatrix.Desktop;

public static class ComponentRepositoryExtensions
{
    public static TComponent CreateByIdEndingWith<TComponent>(this ComponentCreateService repo, string tag)
        where TComponent : Component => repo.Create<TComponent, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(tag));

    public static TComponent CreateByTag<TComponent>(this ComponentCreateService repo, string tag)
        where TComponent : Component => repo.Create<TComponent, FindTagNameStrategy>(new FindTagNameStrategy(tag));

    public static TComponent CreateById<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component => repo.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id));

    public static TComponent CreateByAccessibilityId<TComponent>(this ComponentCreateService repo, string accessibilityId)
        where TComponent : Component => repo.Create<TComponent, FindAccessibilityIdStrategy>(new FindAccessibilityIdStrategy(accessibilityId));

    public static TComponent CreateByName<TComponent>(this ComponentCreateService repo, string name)
        where TComponent : Component => repo.Create<TComponent, FindNameStrategy>(new FindNameStrategy(name));

    public static TComponent CreateByClass<TComponent>(this ComponentCreateService repo, string elementClass)
        where TComponent : Component => repo.Create<TComponent, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

    public static TComponent CreateByAutomationId<TComponent>(this ComponentCreateService repo, string automationId)
        where TComponent : Component => repo.Create<TComponent, FindAutomationIdStrategy>(new FindAutomationIdStrategy(automationId));

    public static TComponent CreateByXPath<TComponent>(this ComponentCreateService repo, string xpath)
        where TComponent : Component => repo.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath));

    public static ComponentsList<TComponent> CreateAllByIdEndingWith<TComponent>(this ComponentCreateService repo, string tag)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdEndingWithStrategy(tag), null);

    public static ComponentsList<TComponent> CreateAllByTag<TComponent>(this ComponentCreateService repo, string tag)
       where TComponent : Component => new ComponentsList<TComponent>(new FindTagNameStrategy(tag), null);

    public static ComponentsList<TComponent> CreateAllById<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStrategy(id), null);

    public static ComponentsList<TComponent> CreateAllByAccessibilityId<TComponent>(this ComponentCreateService repo, string accessibilityId)
        where TComponent : Component => new ComponentsList<TComponent>(new FindAccessibilityIdStrategy(accessibilityId), null);

    public static ComponentsList<TComponent> CreateAllByName<TComponent>(this ComponentCreateService repo, string name)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameStrategy(name), null);

    public static ComponentsList<TComponent> CreateAllByClass<TComponent>(this ComponentCreateService repo, string elementClass)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassNameStrategy(elementClass), null);

    public static ComponentsList<TComponent> CreateAllByAutomationId<TComponent>(this ComponentCreateService repo, string automationId)
        where TComponent : Component => new ComponentsList<TComponent>(new FindAutomationIdStrategy(automationId), null);

    public static ComponentsList<TComponent> CreateAllByXPath<TComponent>(this ComponentCreateService repo, string xpath)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXPathStrategy(xpath), null);
}
