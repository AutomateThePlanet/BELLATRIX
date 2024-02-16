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
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators.IOS;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS;

public static class ComponentCreateExtensions
{
    public static TComponent CreateByTag<TComponent>(this Component<IOSDriver, AppiumElement> element, string tag)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindTagNameStrategy>(new FindTagNameStrategy(tag));

    public static TComponent CreateById<TComponent>(this Component<IOSDriver, AppiumElement> element, string id)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindStrategyId>(new FindStrategyId(id));

    public static TComponent CreateByAccessibilityId<TComponent>(this Component<IOSDriver, AppiumElement> element, string accessibilityId)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindAccessibilityIdStrategy>(new FindAccessibilityIdStrategy(accessibilityId));

    public static TComponent CreateByName<TComponent>(this Component<IOSDriver, AppiumElement> element, string name)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindNameStrategy>(new FindNameStrategy(name));

    public static TComponent CreateByClass<TComponent>(this Component<IOSDriver, AppiumElement> element, string elementClass)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

    public static TComponent CreateByIOSUIAutomation<TComponent>(this Component<IOSDriver, AppiumElement> element, string automationId)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindIOSUIAutomationStrategy>(new FindIOSUIAutomationStrategy(automationId));

    public static TComponent CreateByXPath<TComponent>(this Component<IOSDriver, AppiumElement> element, string xpath)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath));

    public static TComponent CreateByValueContaining<TComponent>(this Component<IOSDriver, AppiumElement> element, string text)
        where TComponent : Component<IOSDriver, AppiumElement> => element.Create<TComponent, FindValueContainingStrategy>(new FindValueContainingStrategy(text));

    public static ComponentsList<TComponent, FindTagNameStrategy, IOSDriver, AppiumElement> CreateAllByTag<TComponent>(this Component<IOSDriver, AppiumElement> element, string tag)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindTagNameStrategy, IOSDriver, AppiumElement>(new FindTagNameStrategy(tag), element.WrappedElement);

    public static ComponentsList<TComponent, FindStrategyId, IOSDriver, AppiumElement> CreateAllById<TComponent>(this Component<IOSDriver, AppiumElement> element, string id)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindStrategyId, IOSDriver, AppiumElement>(new FindStrategyId(id), element.WrappedElement);

    public static ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver, AppiumElement> CreateAllByAccessibilityId<TComponent>(this Component<IOSDriver, AppiumElement> element, string accessibilityId)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver, AppiumElement>(new FindAccessibilityIdStrategy(accessibilityId), element.WrappedElement);

    public static ComponentsList<TComponent, FindNameStrategy, IOSDriver, AppiumElement> CreateAllByName<TComponent>(this Component<IOSDriver, AppiumElement> element, string name)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindNameStrategy, IOSDriver, AppiumElement>(new FindNameStrategy(name), element.WrappedElement);

    public static ComponentsList<TComponent, FindClassNameStrategy, IOSDriver, AppiumElement> CreateAllByClass<TComponent>(this Component<IOSDriver, AppiumElement> element, string elementClass)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindClassNameStrategy, IOSDriver, AppiumElement>(new FindClassNameStrategy(elementClass), element.WrappedElement);

    public static ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver, AppiumElement> CreateAllByIOSUIAutomation<TComponent>(this Component<IOSDriver, AppiumElement> element, string automationId)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver, AppiumElement>(new FindIOSUIAutomationStrategy(automationId), element.WrappedElement);

    public static ComponentsList<TComponent, FindXPathStrategy, IOSDriver, AppiumElement> CreateAllByXPath<TComponent>(this Component<IOSDriver, AppiumElement> element, string xpath)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindXPathStrategy, IOSDriver, AppiumElement>(new FindXPathStrategy(xpath), element.WrappedElement);

    public static ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver, AppiumElement> CreateAllByValueContaining<TComponent>(this Component<IOSDriver, AppiumElement> element, string text)
        where TComponent : Component<IOSDriver, AppiumElement> => new ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver, AppiumElement>(new FindValueContainingStrategy(text), element.WrappedElement);
}