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
   public static class ElementRepositoryExtensions
    {
        ////public static TElement CreateByTag<TElement>(this ElementCreateService repo, string tag)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag));

        public static TElement CreateById<TElement>(this ElementCreateService repo, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStrategy(id));

        public static TElement CreateByIdContaining<TElement>(this ElementCreateService repo, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdContainingStrategy(id));

        public static TElement CreateByDescription<TElement>(this ElementCreateService repo, string description)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionStrategy(description));

        public static TElement CreateByDescriptionContaining<TElement>(this ElementCreateService repo, string description)
           where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionContainingStrategy(description));

        public static TElement CreateByText<TElement>(this ElementCreateService repo, string text)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextStrategy(text));

        public static TElement CreateByTextContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextContainingStrategy(text));

        ////public static TElement CreateByName<TElement>(this ElementCreateService repo, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name));

        public static TElement CreateByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindClassNameStrategy(elementClass));

        public static TElement CreateByAndroidUIAutomator<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindAndroidUIAutomatorStrategy(automationId));

        public static TElement CreateByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindXPathStrategy(xpath));

        ////public static ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTag<TElement>(this ElementCreateService repo, string tag)
        ////  where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag), null);

        public static ComponentsList<TElement, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllById<TElement>(this ElementCreateService repo, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindIdStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdStrategy(id), null);

        public static ComponentsList<TElement, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdContaining<TElement>(this ElementCreateService repo, string id)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindIdContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindIdContainingStrategy(id), null);

        public static ComponentsList<TElement, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescription<TElement>(this ElementCreateService repo, string description)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindDescriptionStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionStrategy(description), null);

        public static ComponentsList<TElement, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescriptionContaining<TElement>(this ElementCreateService repo, string description)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindDescriptionContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindDescriptionContainingStrategy(description), null);

        public static ComponentsList<TElement, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByText<TElement>(this ElementCreateService repo, string text)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindTextStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextStrategy(text), null);

        public static ComponentsList<TElement, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTextContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindTextContainingStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindTextContainingStrategy(text), null);

        ////public static ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByName<TElement>(this ElementCreateService repo, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name), null);

        public static ComponentsList<TElement, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindClassNameStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindClassNameStrategy(elementClass), null);

        public static ComponentsList<TElement, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByAndroidUIAutomator<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindAndroidUIAutomatorStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindAndroidUIAutomatorStrategy(automationId), null);

        public static ComponentsList<TElement, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Component<AndroidDriver<AndroidElement>, AndroidElement> => new ComponentsList<TElement, FindXPathStrategy, AndroidDriver<AndroidElement>, AndroidElement>(new FindXPathStrategy(xpath), null);
    }
}
