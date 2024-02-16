// <copyright file="ElementCreateExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android;

public static class ComponentCreateExtensions
{
    ////public static TComponent CreateByTag<TComponent>(this Element<AndroidDriver, AppiumElement> element, string tag)
    ////    where TComponent : Element<AndroidDriver, AppiumElement> => element.Create<TComponent, ByTagName>(new ByTagName(tag));

    public static TComponent CreateById<TComponent>(this Component<AndroidDriver, AppiumElement> element, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindIdStrategy>(new FindIdStrategy(id));

    public static TComponent CreateByIdContaining<TComponent>(this Component<AndroidDriver, AppiumElement> element, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindIdContainingStrategy>(new FindIdContainingStrategy(id));

    public static TComponent CreateByDescription<TComponent>(this Component<AndroidDriver, AppiumElement> element, string description)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindDescriptionStrategy>(new FindDescriptionStrategy(description));

    public static TComponent CreateByDescriptionContaining<TComponent>(this Component<AndroidDriver, AppiumElement> element, string description)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindDescriptionContainingStrategy>(new FindDescriptionContainingStrategy(description));

    public static TComponent CreateByText<TComponent>(this Component<AndroidDriver, AppiumElement> element, string text)
       where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindTextStrategy>(new FindTextStrategy(text));

    public static TComponent CreateByTextContaining<TComponent>(this Component<AndroidDriver, AppiumElement> element, string text)
       where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindTextContainingStrategy>(new FindTextContainingStrategy(text));

    ////public static TComponent CreateByName<TComponent>(this Element<AndroidDriver, AppiumElement> element, string name)
    ////    where TComponent : Element<AndroidDriver, AppiumElement> => element.Create<TComponent, ByName>(new ByName(name));

    public static TComponent CreateByClass<TComponent>(this Component<AndroidDriver, AppiumElement> element, string elementClass)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindClassNameStrategy>(new FindClassNameStrategy(elementClass));

    public static TComponent CreateByAndroidUIAutomator<TComponent>(this Component<AndroidDriver, AppiumElement> element, string automationId)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindAndroidUIAutomatorStrategy>(new FindAndroidUIAutomatorStrategy(automationId));

    public static TComponent CreateByXPath<TComponent>(this Component<AndroidDriver, AppiumElement> element, string xpath)
        where TComponent : Component<AndroidDriver, AppiumElement> => element.Create<TComponent, FindXPathStrategy>(new FindXPathStrategy(xpath));

    ////public static ElementsList<TComponent, ByTagName, AndroidDriver, AppiumElement> CreateAllByTag<TComponent>(this Element<AndroidDriver, AppiumElement> element, string tag)
    ////    where TComponent : Element<AndroidDriver, AppiumElement> => new ElementsList<TComponent, ByTagName, AndroidDriver, AppiumElement>(new ByTagName(tag), element.WrappedElement);

    public static ComponentsList<TComponent, FindIdStrategy, AndroidDriver, AppiumElement> CreateAllById<TComponent>(this Component<AndroidDriver, AppiumElement> element, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindIdStrategy, AndroidDriver, AppiumElement>(new FindIdStrategy(id), element.WrappedElement);

    public static ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver, AppiumElement> CreateAllByIdContaining<TComponent>(this Component<AndroidDriver, AppiumElement> element, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver, AppiumElement>(new FindIdContainingStrategy(id), element.WrappedElement);

    public static ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver, AppiumElement> CreateAllByDescription<TComponent>(this Component<AndroidDriver, AppiumElement> element, string description)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver, AppiumElement>(new FindDescriptionStrategy(description), element.WrappedElement);

    public static ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver, AppiumElement> CreateAllByDescriptionContaining<TComponent>(this Component<AndroidDriver, AppiumElement> element, string description)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver, AppiumElement>(new FindDescriptionContainingStrategy(description), element.WrappedElement);

    public static ComponentsList<TComponent, FindTextStrategy, AndroidDriver, AppiumElement> CreateAllByText<TComponent>(this Component<AndroidDriver, AppiumElement> element, string text)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindTextStrategy, AndroidDriver, AppiumElement>(new FindTextStrategy(text), element.WrappedElement);

    public static ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver, AppiumElement> CreateAllByTextContaining<TComponent>(this Component<AndroidDriver, AppiumElement> element, string text)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver, AppiumElement>(new FindTextContainingStrategy(text), element.WrappedElement);

    ////public static ElementsList<TComponent, ByName, AndroidDriver, AppiumElement> CreateAllByName<TComponent>(this Element<AndroidDriver, AppiumElement> element, string name)
    ////    where TComponent : Element<AndroidDriver, AppiumElement> => new ElementsList<TComponent, ByName, AndroidDriver, AppiumElement>(new ByName(name), element.WrappedElement);

    public static ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver, AppiumElement> CreateAllByClass<TComponent>(this Component<AndroidDriver, AppiumElement> element, string elementClass)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver, AppiumElement>(new FindClassNameStrategy(elementClass), element.WrappedElement);

    public static ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver, AppiumElement> CreateAllByAndroidUIAutomator<TComponent>(this Component<AndroidDriver, AppiumElement> element, string automationId)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver, AppiumElement>(new FindAndroidUIAutomatorStrategy(automationId), element.WrappedElement);

    public static ComponentsList<TComponent, FindXPathStrategy, AndroidDriver, AppiumElement> CreateAllByXPath<TComponent>(this Component<AndroidDriver, AppiumElement> element, string xpath)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindXPathStrategy, AndroidDriver, AppiumElement>(new FindXPathStrategy(xpath), element.WrappedElement);
}