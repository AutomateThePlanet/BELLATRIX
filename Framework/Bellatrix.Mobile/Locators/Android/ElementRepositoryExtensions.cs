// <copyright file="ElementRepositoryExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android
{
   public static class ElementRepositoryExtensions
    {
        ////public static TElement CreateByTag<TElement>(this ElementCreateService repo, string tag)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag));

        public static TElement CreateById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ById, AndroidDriver<AndroidElement>, AndroidElement>(new ById(id));

        public static TElement CreateByIdContaining<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByIdContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByIdContaining(id));

        public static TElement CreateByDescription<TElement>(this ElementCreateService repo, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByDescription, AndroidDriver<AndroidElement>, AndroidElement>(new ByDescription(description));

        public static TElement CreateByDescriptionContaining<TElement>(this ElementCreateService repo, string description)
           where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByDescriptionContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByDescriptionContaining(description));

        public static TElement CreateByText<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByText, AndroidDriver<AndroidElement>, AndroidElement>(new ByText(text));

        public static TElement CreateByTextContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByTextContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByTextContaining(text));

        ////public static TElement CreateByName<TElement>(this ElementCreateService repo, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name));

        public static TElement CreateByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByClassName, AndroidDriver<AndroidElement>, AndroidElement>(new ByClassName(elementClass));

        public static TElement CreateByAndroidUIAutomator<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByAndroidUIAutomator, AndroidDriver<AndroidElement>, AndroidElement>(new ByAndroidUIAutomator(automationId));

        public static TElement CreateByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => repo.Create<TElement, ByXPath, AndroidDriver<AndroidElement>, AndroidElement>(new ByXPath(xpath));

        ////public static ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTag<TElement>(this ElementCreateService repo, string tag)
        ////  where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag), null);

        public static ElementsList<TElement, ById, AndroidDriver<AndroidElement>, AndroidElement> CreateAllById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ById, AndroidDriver<AndroidElement>, AndroidElement>(new ById(id), null);

        public static ElementsList<TElement, ByIdContaining, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdContaining<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByIdContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByIdContaining(id), null);

        public static ElementsList<TElement, ByDescription, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescription<TElement>(this ElementCreateService repo, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByDescription, AndroidDriver<AndroidElement>, AndroidElement>(new ByDescription(description), null);

        public static ElementsList<TElement, ByDescriptionContaining, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescriptionContaining<TElement>(this ElementCreateService repo, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByDescriptionContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByDescriptionContaining(description), null);

        public static ElementsList<TElement, ByText, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByText<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByText, AndroidDriver<AndroidElement>, AndroidElement>(new ByText(text), null);

        public static ElementsList<TElement, ByTextContaining, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTextContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByTextContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByTextContaining(text), null);

        ////public static ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByName<TElement>(this ElementCreateService repo, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name), null);

        public static ElementsList<TElement, ByClassName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByClassName, AndroidDriver<AndroidElement>, AndroidElement>(new ByClassName(elementClass), null);

        public static ElementsList<TElement, ByAndroidUIAutomator, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByAndroidUIAutomator<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByAndroidUIAutomator, AndroidDriver<AndroidElement>, AndroidElement>(new ByAndroidUIAutomator(automationId), null);

        public static ElementsList<TElement, ByXPath, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByXPath, AndroidDriver<AndroidElement>, AndroidElement>(new ByXPath(xpath), null);
    }
}
