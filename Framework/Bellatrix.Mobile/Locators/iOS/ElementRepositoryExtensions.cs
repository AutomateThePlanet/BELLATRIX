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
using Bellatrix.Mobile.Locators.IOS;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
   public static class ElementRepositoryExtensions
    {
        public static TElement CreateById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ById, IOSDriver<IOSElement>, IOSElement>(new ById(id));

        public static TElement CreateByName<TElement>(this ElementCreateService repo, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ByName, IOSDriver<IOSElement>, IOSElement>(new ByName(name));

        public static TElement CreateByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement>(new ByClassName(elementClass));

        public static TElement CreateByIOSUIAutomation<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ByIOSUIAutomation, IOSDriver<IOSElement>, IOSElement>(new ByIOSUIAutomation(automationId));

        public static TElement CreateByIOSNsPredicate<TElement>(this ElementCreateService repo, string predicate)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ByIOSNsPredicate, IOSDriver<IOSElement>, IOSElement>(new ByIOSNsPredicate(predicate));

        public static TElement CreateByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ByXPath, IOSDriver<IOSElement>, IOSElement>(new ByXPath(xpath));

        public static TElement CreateByValueContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => repo.Create<TElement, ByValueContaining, IOSDriver<IOSElement>, IOSElement>(new ByValueContaining(text));

        public static ElementsList<TElement, ByTagName, IOSDriver<IOSElement>, IOSElement> CreateAllByTag<TElement>(this ElementCreateService repo, string tag)
    where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByTagName, IOSDriver<IOSElement>, IOSElement>(new ByTagName(tag), null);

        public static ElementsList<TElement, ById, IOSDriver<IOSElement>, IOSElement> CreateAllById<TElement>(this ElementCreateService repo, string id)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ById, IOSDriver<IOSElement>, IOSElement>(new ById(id), null);

        public static ElementsList<TElement, ByAccessibilityId, IOSDriver<IOSElement>, IOSElement> CreateAllByAccessibilityId<TElement>(this ElementCreateService repo, string accessibilityId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByAccessibilityId, IOSDriver<IOSElement>, IOSElement>(new ByAccessibilityId(accessibilityId), null);

        public static ElementsList<TElement, ByName, IOSDriver<IOSElement>, IOSElement> CreateAllByName<TElement>(this ElementCreateService repo, string name)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByName, IOSDriver<IOSElement>, IOSElement>(new ByName(name), null);

        public static ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement> CreateAllByClass<TElement>(this ElementCreateService repo, string elementClass)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByClassName, IOSDriver<IOSElement>, IOSElement>(new ByClassName(elementClass), null);

        public static ElementsList<TElement, ByIOSUIAutomation, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSUIAutomation<TElement>(this ElementCreateService repo, string automationId)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByIOSUIAutomation, IOSDriver<IOSElement>, IOSElement>(new ByIOSUIAutomation(automationId), null);

        public static ElementsList<TElement, ByIOSNsPredicate, IOSDriver<IOSElement>, IOSElement> CreateAllByIOSNsPredicate<TElement>(this ElementCreateService repo, string predicate)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByIOSNsPredicate, IOSDriver<IOSElement>, IOSElement>(new ByIOSNsPredicate(predicate), null);

        public static ElementsList<TElement, ByXPath, IOSDriver<IOSElement>, IOSElement> CreateAllByXPath<TElement>(this ElementCreateService repo, string xpath)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByXPath, IOSDriver<IOSElement>, IOSElement>(new ByXPath(xpath), null);

        public static ElementsList<TElement, ByValueContaining, IOSDriver<IOSElement>, IOSElement> CreateAllByValueContaining<TElement>(this ElementCreateService repo, string text)
            where TElement : Element<IOSDriver<IOSElement>, IOSElement> => new ElementsList<TElement, ByValueContaining, IOSDriver<IOSElement>, IOSElement>(new ByValueContaining(text), null);
    }
}
