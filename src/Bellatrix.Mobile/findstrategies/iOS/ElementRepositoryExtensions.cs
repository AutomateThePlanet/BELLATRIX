// <copyright file="ElementRepositoryExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
   public static class ElementRepositoryExtensions
    {
        public static TElement CreateById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, FindStrategyId, IOSDriver<IOSElement>, IOSElement>(new FindStrategyId(id));

        public static TElement CreateByName<TElement>(this ElementCreateService repo, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, FindNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStrategy(name));

        public static TElement CreateByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindClassNameStrategy(elementClass));

        public static TElement CreateByIOSUIAutomation<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSUIAutomationStrategy(automationId));

        public static TElement CreateByIOSNsPredicate<TElement>(this ElementCreateService repo, string predicate)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, FindIOSNsPredicateStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSNsPredicateStrategy(predicate));

        public static TElement CreateByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement>(new FindXPathStrategy(xpath));

        public static TElement CreateByValueContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement>(new FindValueContainingStrategy(text));

        public static ElementsList<TElement, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByTag<TElement>(this ElementCreateService repo, string tag)
    where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindTagNameStrategy(tag), null);

        public static ElementsList<TElement, FindStrategyId, IOSDriver<IOSElement>, IOSElement> CreateAllById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindStrategyId, IOSDriver<IOSElement>, IOSElement>(new FindStrategyId(id), null);

        public static ElementsList<TElement, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByAccessibilityId<TElement>(this ElementCreateService repo, string accessibilityId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement>(new FindAccessibilityIdStrategy(accessibilityId), null);

        public static ElementsList<TElement, FindNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByName<TElement>(this ElementCreateService repo, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStrategy(name), null);

        public static ElementsList<TElement, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindClassNameStrategy(elementClass), null);

        public static ElementsList<TElement, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSUIAutomation<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSUIAutomationStrategy(automationId), null);

        public static ElementsList<TElement, FindIOSNsPredicateStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSNsPredicate<TElement>(this ElementCreateService repo, string predicate)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindIOSNsPredicateStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSNsPredicateStrategy(predicate), null);

        public static ElementsList<TElement, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement>(new FindXPathStrategy(xpath), null);

        public static ElementsList<TElement, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByValueContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement>(new FindValueContainingStrategy(text), null);
    }
}
