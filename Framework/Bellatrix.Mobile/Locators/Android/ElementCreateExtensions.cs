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
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android
{
   public static class ElementCreateExtensions
    {
        ////public static TElement CreateByTag<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string tag)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByTagName>(new ByTagName(tag));

        public static TElement CreateById<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ById>(new ById(id));

        public static TElement CreateByIdContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByIdContaining>(new ByIdContaining(id));

        public static TElement CreateByDescription<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByDescription>(new ByDescription(description));

        public static TElement CreateByDescriptionContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByDescriptionContaining>(new ByDescriptionContaining(description));

        public static TElement CreateByText<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
           where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByText>(new ByText(text));

        public static TElement CreateByTextContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
           where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByTextContaining>(new ByTextContaining(text));

        ////public static TElement CreateByName<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByName>(new ByName(name));

        public static TElement CreateByClass<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string elementClass)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByClassName>(new ByClassName(elementClass));

        public static TElement CreateByAndroidUIAutomator<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string automationId)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByAndroidUIAutomator>(new ByAndroidUIAutomator(automationId));

        public static TElement CreateByXPath<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string xpath)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => element.Create<TElement, ByXPath>(new ByXPath(xpath));

        ////public static ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTag<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string tag)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByTagName, AndroidDriver<AndroidElement>, AndroidElement>(new ByTagName(tag), element.WrappedElement);

        public static ElementsList<TElement, ById, AndroidDriver<AndroidElement>, AndroidElement> CreateAllById<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ById, AndroidDriver<AndroidElement>, AndroidElement>(new ById(id), element.WrappedElement);

        public static ElementsList<TElement, ByIdContaining, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByIdContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string id)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByIdContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByIdContaining(id), element.WrappedElement);

        public static ElementsList<TElement, ByDescription, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescription<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByDescription, AndroidDriver<AndroidElement>, AndroidElement>(new ByDescription(description), element.WrappedElement);

        public static ElementsList<TElement, ByDescriptionContaining, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByDescriptionContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string description)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByDescriptionContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByDescriptionContaining(description), element.WrappedElement);

        public static ElementsList<TElement, ByText, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByText<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByText, AndroidDriver<AndroidElement>, AndroidElement>(new ByText(text), element.WrappedElement);

        public static ElementsList<TElement, ByTextContaining, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByTextContaining<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string text)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByTextContaining, AndroidDriver<AndroidElement>, AndroidElement>(new ByTextContaining(text), element.WrappedElement);

        ////public static ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByName<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string name)
        ////    where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByName, AndroidDriver<AndroidElement>, AndroidElement>(new ByName(name), element.WrappedElement);

        public static ElementsList<TElement, ByClassName, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByClass<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string elementClass)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByClassName, AndroidDriver<AndroidElement>, AndroidElement>(new ByClassName(elementClass), element.WrappedElement);

        public static ElementsList<TElement, ByAndroidUIAutomator, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByAndroidUIAutomator<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string automationId)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByAndroidUIAutomator, AndroidDriver<AndroidElement>, AndroidElement>(new ByAndroidUIAutomator(automationId), element.WrappedElement);

        public static ElementsList<TElement, ByXPath, AndroidDriver<AndroidElement>, AndroidElement> CreateAllByXPath<TElement>(this Element<AndroidDriver<AndroidElement>, AndroidElement> element, string xpath)
            where TElement : Element<AndroidDriver<AndroidElement>, AndroidElement> => new ElementsList<TElement, ByXPath, AndroidDriver<AndroidElement>, AndroidElement>(new ByXPath(xpath), element.WrappedElement);
    }
}