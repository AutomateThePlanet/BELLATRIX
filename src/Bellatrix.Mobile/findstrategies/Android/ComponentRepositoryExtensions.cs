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
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android;

public static class ComponentRepositoryExtensions
{
    ////public static TComponent CreateByTag<TComponent>(this ComponentCreateService repo, string tag)
    ////    where TComponent : Element<AndroidDriver, AppiumElement> => repo.Create<TComponent, ByTagName, AndroidDriver, AppiumElement>(new ByTagName(tag));

    public static TComponent CreateById<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindIdStrategy, AndroidDriver, AppiumElement>(new FindIdStrategy(id));

    public static TComponent CreateByIdContaining<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindIdContainingStrategy, AndroidDriver, AppiumElement>(new FindIdContainingStrategy(id));

    public static TComponent CreateByDescription<TComponent>(this ComponentCreateService repo, string description)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindDescriptionStrategy, AndroidDriver, AppiumElement>(new FindDescriptionStrategy(description));

    public static TComponent CreateByDescriptionContaining<TComponent>(this ComponentCreateService repo, string description)
       where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindDescriptionContainingStrategy, AndroidDriver, AppiumElement>(new FindDescriptionContainingStrategy(description));

    public static TComponent CreateByText<TComponent>(this ComponentCreateService repo, string text)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindTextStrategy, AndroidDriver, AppiumElement>(new FindTextStrategy(text));

    public static TComponent CreateByTextContaining<TComponent>(this ComponentCreateService repo, string text)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindTextContainingStrategy, AndroidDriver, AppiumElement>(new FindTextContainingStrategy(text));

    ////public static TComponent CreateByName<TComponent>(this ComponentCreateService repo, string name)
    ////    where TComponent : Element<AndroidDriver, AppiumElement> => repo.Create<TComponent, ByName, AndroidDriver, AppiumElement>(new ByName(name));

    public static TComponent CreateByClass<TComponent>(this ComponentCreateService repo, string elementClass)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindClassNameStrategy, AndroidDriver, AppiumElement>(new FindClassNameStrategy(elementClass));

    public static TComponent CreateByAndroidUIAutomator<TComponent>(this ComponentCreateService repo, string automationId)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver, AppiumElement>(new FindAndroidUIAutomatorStrategy(automationId));

    public static TComponent CreateByXPath<TComponent>(this ComponentCreateService repo, string xpath)
        where TComponent : Component<AndroidDriver, AppiumElement> => repo.Create<TComponent, FindXPathStrategy, AndroidDriver, AppiumElement>(new FindXPathStrategy(xpath));

    ////public static ElementsList<TComponent, ByTagName, AndroidDriver, AppiumElement> CreateAllByTag<TComponent>(this ComponentCreateService repo, string tag)
    ////  where TComponent : Element<AndroidDriver, AppiumElement> => new ElementsList<TComponent, ByTagName, AndroidDriver, AppiumElement>(new ByTagName(tag), null);

    public static ComponentsList<TComponent, FindIdStrategy, AndroidDriver, AppiumElement> CreateAllById<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindIdStrategy, AndroidDriver, AppiumElement>(new FindIdStrategy(id), null);

    public static ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver, AppiumElement> CreateAllByIdContaining<TComponent>(this ComponentCreateService repo, string id)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver, AppiumElement>(new FindIdContainingStrategy(id), null);

    public static ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver, AppiumElement> CreateAllByDescription<TComponent>(this ComponentCreateService repo, string description)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver, AppiumElement>(new FindDescriptionStrategy(description), null);

    public static ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver, AppiumElement> CreateAllByDescriptionContaining<TComponent>(this ComponentCreateService repo, string description)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver, AppiumElement>(new FindDescriptionContainingStrategy(description), null);

    public static ComponentsList<TComponent, FindTextStrategy, AndroidDriver, AppiumElement> CreateAllByText<TComponent>(this ComponentCreateService repo, string text)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindTextStrategy, AndroidDriver, AppiumElement>(new FindTextStrategy(text), null);

    public static ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver, AppiumElement> CreateAllByTextContaining<TComponent>(this ComponentCreateService repo, string text)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver, AppiumElement>(new FindTextContainingStrategy(text), null);

    ////public static ElementsList<TComponent, ByName, AndroidDriver, AppiumElement> CreateAllByName<TComponent>(this ComponentCreateService repo, string name)
    ////    where TComponent : Element<AndroidDriver, AppiumElement> => new ElementsList<TComponent, ByName, AndroidDriver, AppiumElement>(new ByName(name), null);

    public static ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver, AppiumElement> CreateAllByClass<TComponent>(this ComponentCreateService repo, string elementClass)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver, AppiumElement>(new FindClassNameStrategy(elementClass), null);

    public static ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver, AppiumElement> CreateAllByAndroidUIAutomator<TComponent>(this ComponentCreateService repo, string automationId)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver, AppiumElement>(new FindAndroidUIAutomatorStrategy(automationId), null);

    public static ComponentsList<TComponent, FindXPathStrategy, AndroidDriver, AppiumElement> CreateAllByXPath<TComponent>(this ComponentCreateService repo, string xpath)
        where TComponent : Component<AndroidDriver, AppiumElement> => new ComponentsList<TComponent, FindXPathStrategy, AndroidDriver, AppiumElement>(new FindXPathStrategy(xpath), null);
}
