// <copyright file="ComponentCreateExtensions.cs" company="Automate The Planet Ltd.">
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
   public static class ComponentCreateExtensions
    {
        public static TComponent CreateByTag<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string tag)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindTagNameStrategy>(new FindTagNameStrategy(tag));

        public static TComponent CreateById<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindStrategyId>(new FindStrategyId(id));

        public static TComponent CreateByAccessibilityId<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string accessibilityId)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindAccessibilityIdStrategy>(new FindAccessibilityIdStrategy(accessibilityId));

        public static TComponent CreateByName<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string name)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindNameStrategy>(new FindNameStrategy(name));

        public static TComponent CreateByClass<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string elementClass)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

        public static TComponent CreateByIOSUIAutomation<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string automationId)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindIOSUIAutomationStrategy>(new FindIOSUIAutomationStrategy(automationId));

        public static TComponent CreateByXPath<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string xpath)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath));

        public static TComponent CreateByValueContaining<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string text)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => element.Create<TComponent, FindValueContainingStrategy>(new FindValueContainingStrategy(text));

        public static ComponentsList<TComponent, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByTag<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string tag)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindTagNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindTagNameStrategy(tag), element.WrappedElement);

        public static ComponentsList<TComponent, FindStrategyId, IOSDriver<IOSElement>, IOSElement> CreateAllById<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindStrategyId, IOSDriver<IOSElement>, IOSElement>(new FindStrategyId(id), element.WrappedElement);

        public static ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByAccessibilityId<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string accessibilityId)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindAccessibilityIdStrategy, IOSDriver<IOSElement>, IOSElement>(new FindAccessibilityIdStrategy(accessibilityId), element.WrappedElement);

        public static ComponentsList<TComponent, FindNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByName<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string name)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindNameStrategy(name), element.WrappedElement);

        public static ComponentsList<TComponent, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByClass<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string elementClass)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindClassNameStrategy, IOSDriver<IOSElement>, IOSElement>(new FindClassNameStrategy(elementClass), element.WrappedElement);

        public static ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSUIAutomation<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string automationId)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindIOSUIAutomationStrategy, IOSDriver<IOSElement>, IOSElement>(new FindIOSUIAutomationStrategy(automationId), element.WrappedElement);

        public static ComponentsList<TComponent, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByXPath<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string xpath)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindXPathStrategy, IOSDriver<IOSElement>, IOSElement>(new FindXPathStrategy(xpath), element.WrappedElement);

        public static ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement> CreateAllByValueContaining<TComponent>(this Component<IOSDriver<IOSElement>, IOSElement> element, string text)
            where TComponent : Component<IOSDriver<IOSElement>, IOSElement> => new ComponentsList<TComponent, FindValueContainingStrategy, IOSDriver<IOSElement>, IOSElement>(new FindValueContainingStrategy(text), element.WrappedElement);
    }
}