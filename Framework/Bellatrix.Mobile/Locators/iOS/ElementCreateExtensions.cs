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
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ByTagName>(new ByTagName(tag));

        public static TElement CreateById<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ById>(new ById(id));

        public static TElement CreateByAccessibilityId<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string accessibilityId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ByAccessibilityId>(new ByAccessibilityId(accessibilityId));

        public static TElement CreateByName<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ByName>(new ByName(name));

        public static TElement CreateByClass<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ByClassName>(new ByClassName(elementClass));

        public static TElement CreateByIOSUIAutomation<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ByIOSUIAutomation>(new ByIOSUIAutomation(automationId));

        public static TElement CreateByXPath<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ByXPath>(new ByXPath(xpath));

        public static TElement CreateByValueContaining<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => element.Create<TElement, ByValueContaining>(new ByValueContaining(text));

        public static ElementsList<TElement, ByTagName, IOSDriver<IOSElement>, IOSElement> CreateAllByTag<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string tag)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByTagName, IOSDriver<IOSElement>, IOSElement>(new ByTagName(tag), element.WrappedElement);

        public static ElementsList<TElement, ById, IOSDriver<IOSElement>, IOSElement> CreateAllById<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ById, IOSDriver<IOSElement>, IOSElement>(new ById(id), element.WrappedElement);

        public static ElementsList<TElement, ByAccessibilityId, IOSDriver<IOSElement>, IOSElement> CreateAllByAccessibilityId<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string accessibilityId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByAccessibilityId, IOSDriver<IOSElement>, IOSElement>(new ByAccessibilityId(accessibilityId), element.WrappedElement);

        public static ElementsList<TElement, ByName, IOSDriver<IOSElement>, IOSElement> CreateAllByName<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByName, IOSDriver<IOSElement>, IOSElement>(new ByName(name), element.WrappedElement);

        public static ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement> CreateAllByClass<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement>(new ByClassName(elementClass), element.WrappedElement);

        public static ElementsList<TElement, ByIOSUIAutomation, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSUIAutomation<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByIOSUIAutomation, IOSDriver<IOSElement>, IOSElement>(new ByIOSUIAutomation(automationId), element.WrappedElement);

        public static ElementsList<TElement, ByXPath, IOSDriver<IOSElement>, IOSElement> CreateAllByXPath<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByXPath, IOSDriver<IOSElement>, IOSElement>(new ByXPath(xpath), element.WrappedElement);

        public static ElementsList<TElement, ByValueContaining, IOSDriver<IOSElement>, IOSElement> CreateAllByValueContaining<TElement>(this Element<IOSDriver<IOSElement>, IOSElement> element, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByValueContaining, IOSDriver<IOSElement>, IOSElement>(new ByValueContaining(text), element.WrappedElement);
    }
}