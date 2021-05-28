// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android
{
   public static class ComponentCreateExtensions
    {
        ////public static TComponent CreateByTag<TComponent>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string tag)
        ////    where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, ByTagName>(new ByTagName(tag));

        public static TComponent CreateById<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id));

        public static TComponent CreateByIdContaining<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindIdContainingStrategy>(new FindIdContainingStrategy(id));

        public static TComponent CreateByDescription<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindDescriptionStrategy>(new FindDescriptionStrategy(description));

        public static TComponent CreateByDescriptionContaining<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindDescriptionContainingStrategy>(new FindDescriptionContainingStrategy(description));

        public static TComponent CreateByText<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
           where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindTextStrategy>(new FindTextStrategy(text));

        public static TComponent CreateByTextContaining<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
           where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindTextContainingStrategy>(new FindTextContainingStrategy(text));

        ////public static TComponent CreateByName<TComponent>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string name)
        ////    where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, ByName>(new ByName(name));

        public static TComponent CreateByClass<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string elementClass)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

        public static TComponent CreateByAndroidUIAutomator<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string automationId)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindAndroidUIAutomatorStrategy>(new FindAndroidUIAutomatorStrategy(automationId));

        public static TComponent CreateByXPath<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string xpath)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath));

        ////public static ElementsList<TComponent, ByTagName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTag<TComponent>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string tag)
        ////    where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TComponent, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag), element.WrappedElement);

        public static ComponentsList<TComponent, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllById<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStrategy(id), element.WrappedElement);

        public static ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdContaining<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdContainingStrategy(id), element.WrappedElement);

        public static ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescription<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionStrategy(description), element.WrappedElement);

        public static ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescriptionContaining<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionContainingStrategy(description), element.WrappedElement);

        public static ComponentsList<TComponent, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByText<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextStrategy(text), element.WrappedElement);

        public static ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTextContaining<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextContainingStrategy(text), element.WrappedElement);

        ////public static ElementsList<TComponent, ByName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByName<TComponent>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string name)
        ////    where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TComponent, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name), element.WrappedElement);

        public static ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByClass<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string elementClass)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindClassNameStrategy(elementClass), element.WrappedElement);

        public static ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByAndroidUIAutomator<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string automationId)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindAndroidUIAutomatorStrategy(automationId), element.WrappedElement);

        public static ComponentsList<TComponent, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByXPath<TComponent>(this Component<AndroidDriver<AndroidElement>, AndroidElement> element, string xpath)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindXPathStrategy(xpath), element.WrappedElement);
    }
}