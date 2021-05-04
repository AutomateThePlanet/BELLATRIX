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
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android
{
   public static class ComponentRepositoryExtensions
    {
        ////public static TComponent CreateByTag<TComponent>(this ComponentCreateService repo, string tag)
        ////    where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag));

        public static TComponent CreateById<TComponent>(this ComponentCreateService repo, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStrategy(id));

        public static TComponent CreateByIdContaining<TComponent>(this ComponentCreateService repo, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdContainingStrategy(id));

        public static TComponent CreateByDescription<TComponent>(this ComponentCreateService repo, string description)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionStrategy(description));

        public static TComponent CreateByDescriptionContaining<TComponent>(this ComponentCreateService repo, string description)
           where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionContainingStrategy(description));

        public static TComponent CreateByText<TComponent>(this ComponentCreateService repo, string text)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextStrategy(text));

        public static TComponent CreateByTextContaining<TComponent>(this ComponentCreateService repo, string text)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextContainingStrategy(text));

        ////public static TComponent CreateByName<TComponent>(this ComponentCreateService repo, string name)
        ////    where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name));

        public static TComponent CreateByClass<TComponent>(this ComponentCreateService repo, string elementClass)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindClassNameStrategy(elementClass));

        public static TComponent CreateByAndroidUIAutomator<TComponent>(this ComponentCreateService repo, string automationId)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindAndroidUIAutomatorStrategy(automationId));

        public static TComponent CreateByXPath<TComponent>(this ComponentCreateService repo, string xpath)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TComponent, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindXPathStrategy(xpath));

        ////public static ElementsList<TComponent, ByTagName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTag<TComponent>(this ComponentCreateService repo, string tag)
        ////  where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TComponent, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag), null);

        public static ComponentsList<TComponent, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllById<TComponent>(this ComponentCreateService repo, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStrategy(id), null);

        public static ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdContaining<TComponent>(this ComponentCreateService repo, string id)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdContainingStrategy(id), null);

        public static ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescription<TComponent>(this ComponentCreateService repo, string description)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionStrategy(description), null);

        public static ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescriptionContaining<TComponent>(this ComponentCreateService repo, string description)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionContainingStrategy(description), null);

        public static ComponentsList<TComponent, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByText<TComponent>(this ComponentCreateService repo, string text)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextStrategy(text), null);

        public static ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTextContaining<TComponent>(this ComponentCreateService repo, string text)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextContainingStrategy(text), null);

        ////public static ElementsList<TComponent, ByName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByName<TComponent>(this ComponentCreateService repo, string name)
        ////    where TComponent : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TComponent, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name), null);

        public static ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByClass<TComponent>(this ComponentCreateService repo, string elementClass)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindClassNameStrategy(elementClass), null);

        public static ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByAndroidUIAutomator<TComponent>(this ComponentCreateService repo, string automationId)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindAndroidUIAutomatorStrategy(automationId), null);

        public static ComponentsList<TComponent, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByXPath<TComponent>(this ComponentCreateService repo, string xpath)
            where TComponent : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TComponent, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindXPathStrategy(xpath), null);
    }
}
