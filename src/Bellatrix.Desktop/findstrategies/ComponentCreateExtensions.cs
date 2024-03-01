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
using Bellatrix.Desktop.Controls.Core;
using Bellatrix.Desktop.Locators;

namespace Bellatrix.Desktop;

public static class ComponentCreateExtensions
{
    public static TComponent CreateByIdEndingWith<TComponent>(this Component element, string idPart)
        where TComponent : Component => element.Create<TComponent, FindIdEndingWithStrategy>(new FindIdEndingWithStrategy(idPart));

    public static TComponent CreateByTag<TComponent>(this Component element, string tag)
        where TComponent : Component => element.Create<TComponent, FindTagNameStrategy>(new FindTagNameStrategy(tag));

    public static TComponent CreateById<TComponent>(this Component element, string id)
        where TComponent : Component => element.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id));

    public static TComponent CreateByAccessibilityId<TComponent>(this Component element, string accessibilityId)
        where TComponent : Component => element.Create<TComponent, FindAccessibilityIdStrategy>(new FindAccessibilityIdStrategy(accessibilityId));

    public static TComponent CreateByName<TComponent>(this Component element, string name)
        where TComponent : Component => element.Create<TComponent, FindNameStrategy>(new FindNameStrategy(name));

    public static TComponent CreateByClass<TComponent>(this Component element, string elementClass)
        where TComponent : Component => element.Create<TComponent, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

    public static TComponent CreateByAutomationId<TComponent>(this Component element, string automationId)
        where TComponent : Component => element.Create<TComponent, FindAutomationIdStrategy>(new FindAutomationIdStrategy(automationId));

    public static TComponent CreateByXPath<TComponent>(this Component element, string xpath)
        where TComponent : Component => element.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath));

    public static ComponentsList<TComponent> CreateAllByIdEndingWith<TComponent>(this Component element, string tag)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdEndingWithStrategy(tag), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByTag<TComponent>(this Component element, string tag)
        where TComponent : Component => new ComponentsList<TComponent>(new FindTagNameStrategy(tag), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllById<TComponent>(this Component element, string id)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStrategy(id), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByAccessibilityId<TComponent>(this Component element, string accessibilityId)
        where TComponent : Component => new ComponentsList<TComponent>(new FindAccessibilityIdStrategy(accessibilityId), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByName<TComponent>(this Component element, string name)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameStrategy(name), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByClass<TComponent>(this Component element, string elementClass)
        where TComponent : Component => new ComponentsList<TComponent>(new FindClassNameStrategy(elementClass), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByAutomationId<TComponent>(this Component element, string automationId)
        where TComponent : Component => new ComponentsList<TComponent>(new FindAutomationIdStrategy(automationId), element.WrappedElement);

    public static ComponentsList<TComponent> CreateAllByXPath<TComponent>(this Component element, string xpath)
        where TComponent : Component => new ComponentsList<TComponent>(new FindXPathStrategy(xpath), element.WrappedElement);
}