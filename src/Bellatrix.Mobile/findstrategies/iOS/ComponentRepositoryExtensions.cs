// <copyright file="ComponentRepositoryExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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

namespace Bellatrix.Mobile.IOS
{
   public static class ComponentRepositoryExtensions
    {
        public static TComponent CreateById<TComponent>(this ComponentCreateService repo, string id)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindStrategyId, IOSDriver<IOSElement>, IOSElement>(new FindStrategyId(id));

        public static TComponent CreateByName<TComponent>(this ComponentCreateService repo, string name)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStrategy(name));

        public static TComponent CreateByClass<TComponent>(this ComponentCreateService repo, string elementClass)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindClassNameStrategy(elementClass));

        public static TComponent CreateByIOSUIAutomation<TComponent>(this ComponentCreateService repo, string automationId)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSUIAutomationStrategy(automationId));

        public static TComponent CreateByIOSNsPredicate<TComponent>(this ComponentCreateService repo, string predicate)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindIOSNsPredicateStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSNsPredicateStrategy(predicate));

        public static TComponent CreateByXPath<TComponent>(this ComponentCreateService repo, string xpath)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement>(new FindXPathStrategy(xpath));

        public static TComponent CreateByValueContaining<TComponent>(this ComponentCreateService repo, string text)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => repo.Create<TComponent, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement>(new FindValueContainingStrategy(text));

        public static ComponentsList<TComponent, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByTag<TComponent>(this ComponentCreateService repo, string tag)
    where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindTagNameStrategy(tag), null);

        public static ComponentsList<TComponent, FindStrategyId, IOSDriver<IOSElement>, IOSElement> CreateAllById<TComponent>(this ComponentCreateService repo, string id)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindStrategyId, IOSDriver<IOSElement>, IOSElement>(new FindStrategyId(id), null);

        public static ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByAccessibilityId<TComponent>(this ComponentCreateService repo, string accessibilityId)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement>(new FindAccessibilityIdStrategy(accessibilityId), null);

        public static ComponentsList<TComponent, FindNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByName<TComponent>(this ComponentCreateService repo, string name)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStrategy(name), null);

        public static ComponentsList<TComponent, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByClass<TComponent>(this ComponentCreateService repo, string elementClass)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindClassNameStrategy(elementClass), null);

        public static ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSUIAutomation<TComponent>(this ComponentCreateService repo, string automationId)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSUIAutomationStrategy(automationId), null);

        public static ComponentsList<TComponent, FindIOSNsPredicateStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSNsPredicate<TComponent>(this ComponentCreateService repo, string predicate)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindIOSNsPredicateStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSNsPredicateStrategy(predicate), null);

        public static ComponentsList<TComponent, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByXPath<TComponent>(this ComponentCreateService repo, string xpath)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement>(new FindXPathStrategy(xpath), null);

        public static ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByValueContaining<TComponent>(this ComponentCreateService repo, string text)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement>(new FindValueContainingStrategy(text), null);
    }
}
