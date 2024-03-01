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
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators.IOS;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS;

public static class ComponentRepositoryExtensions
{
    public static TComponent CreateById<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<IOSDriver, AppiumElement> => repo.Create<TComponent, FindStrategyId, IOSDriver, AppiumElement>(new FindStrategyId(id));

    public static TComponent CreateByName<TComponent>(this ComponentCreateService repo, string name)
        where TComponent : Component<IOSDriver, AppiumElement> => repo.Create<TComponent, FindNameStrategy, IOSDriver, AppiumElement>(new FindNameStrategy(name));

    public static TComponent CreateByClass<TComponent>(this ComponentCreateService repo, string elementClass)
        where TComponent : Component<IOSDriver, AppiumElement> => repo.Create<TComponent, FindClassNameStrategy, IOSDriver, AppiumElement>(new FindClassNameStrategy(elementClass));

    public static TComponent CreateByIOSUIAutomation<TComponent>(this ComponentCreateService repo, string automationId)
        where TComponent : Component<IOSDriver, AppiumElement> => repo.Create<TComponent, FindIOSUIAutomationStrategy, IOSDriver, AppiumElement>(new FindIOSUIAutomationStrategy(automationId));

    public static TComponent CreateByIOSNsPredicate<TComponent>(this ComponentCreateService repo, string predicate)
        where TComponent : Component<IOSDriver, AppiumElement> => repo.Create<TComponent, FindIOSNsPredicateStrategy, IOSDriver, AppiumElement>(new FindIOSNsPredicateStrategy(predicate));

    public static TComponent CreateByXPath<TComponent>(this ComponentCreateService repo, string xpath)
        where TComponent : Component<IOSDriver, AppiumElement> => repo.Create<TComponent, FindXPathStrategy, IOSDriver, AppiumElement>(new FindXPathStrategy(xpath));

    public static TComponent CreateByValueContaining<TComponent>(this ComponentCreateService repo, string text)
        where TComponent : Component<IOSDriver, AppiumElement> => repo.Create<TComponent, FindValueContainingStrategy, IOSDriver, AppiumElement>(new FindValueContainingStrategy(text));

    public static ComponentsList<TComponent, FindTagNameStrategy, IOSDriver, AppiumElement> CreateAllByTag<TComponent>(this ComponentCreateService repo, string tag)
where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindTagNameStrategy, IOSDriver, AppiumElement>(new FindTagNameStrategy(tag), null);

    public static ComponentsList<TComponent, FindStrategyId, IOSDriver, AppiumElement> CreateAllById<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindStrategyId, IOSDriver, AppiumElement>(new FindStrategyId(id), null);

    public static ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver, AppiumElement> CreateAllByAccessibilityId<TComponent>(this ComponentCreateService repo, string accessibilityId)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver, AppiumElement>(new FindAccessibilityIdStrategy(accessibilityId), null);

    public static ComponentsList<TComponent, FindNameStrategy, IOSDriver, AppiumElement> CreateAllByName<TComponent>(this ComponentCreateService repo, string name)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindNameStrategy, IOSDriver, AppiumElement>(new FindNameStrategy(name), null);

    public static ComponentsList<TComponent, FindClassNameStrategy, IOSDriver, AppiumElement> CreateAllByClass<TComponent>(this ComponentCreateService repo, string elementClass)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindClassNameStrategy, IOSDriver, AppiumElement>(new FindClassNameStrategy(elementClass), null);

    public static ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver, AppiumElement> CreateAllByIOSUIAutomation<TComponent>(this ComponentCreateService repo, string automationId)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver, AppiumElement>(new FindIOSUIAutomationStrategy(automationId), null);

    public static ComponentsList<TComponent, FindIOSNsPredicateStrategy, IOSDriver, AppiumElement> CreateAllByIOSNsPredicate<TComponent>(this ComponentCreateService repo, string predicate)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindIOSNsPredicateStrategy, IOSDriver, AppiumElement>(new FindIOSNsPredicateStrategy(predicate), null);

    public static ComponentsList<TComponent, FindXPathStrategy, IOSDriver, AppiumElement> CreateAllByXPath<TComponent>(this ComponentCreateService repo, string xpath)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindXPathStrategy, IOSDriver, AppiumElement>(new FindXPathStrategy(xpath), null);

    public static ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver, AppiumElement> CreateAllByValueContaining<TComponent>(this ComponentCreateService repo, string text)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver, AppiumElement>(new FindValueContainingStrategy(text), null);
}
