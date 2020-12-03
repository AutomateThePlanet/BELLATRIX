// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
   public static class ElementCreateExtensions
    {
        public static TElement CreateByTag<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string tag)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindTagNameStrategy>(new FindTagNameStrategy(tag));

        public static TElement CreateById<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindStrategyId>(new FindStrategyId(id));

        public static TElement CreateByAccessibilityId<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string accessibilityId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindAccessibilityIdStrategy>(new FindAccessibilityIdStrategy(accessibilityId));

        public static TElement CreateByName<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindNameStrategy>(new FindNameStrategy(name));

        public static TElement CreateByClass<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

        public static TElement CreateByIOSUIAutomation<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindIOSUIAutomationStrategy>(new FindIOSUIAutomationStrategy(automationId));

        public static TElement CreateByXPath<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindXPathStrategy>(new FindXPathStrategy(xpath));

        public static TElement CreateByValueContaining<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, FindValueContainingStrategy>(new FindValueContainingStrategy(text));

        public static ElementsList<TElement, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByTag<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string tag)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindTagNameStrategy(tag), element.WrappedElement);

        public static ElementsList<TElement, FindStrategyId, IOSDriver<IOSElement>, IOSElement> CreateAllById<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindStrategyId, IOSDriver<IOSElement>, IOSElement>(new FindStrategyId(id), element.WrappedElement);

        public static ElementsList<TElement, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByAccessibilityId<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string accessibilityId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement>(new FindAccessibilityIdStrategy(accessibilityId), element.WrappedElement);

        public static ElementsList<TElement, FindNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByName<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStrategy(name), element.WrappedElement);

        public static ElementsList<TElement, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByClass<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindClassNameStrategy(elementClass), element.WrappedElement);

        public static ElementsList<TElement, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSUIAutomation<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSUIAutomationStrategy(automationId), element.WrappedElement);

        public static ElementsList<TElement, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByXPath<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement>(new FindXPathStrategy(xpath), element.WrappedElement);

        public static ElementsList<TElement, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByValueContaining<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement>(new FindValueContainingStrategy(text), element.WrappedElement);
    }
}