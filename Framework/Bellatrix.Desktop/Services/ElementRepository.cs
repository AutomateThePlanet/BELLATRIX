// <copyright file="ElementRepository.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using System.Reflection;
using Bellatrix.Desktop.Locators;
using Bellatrix.Desktop.PageObjects;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Services
{
    internal class ElementRepository
    {
        public TElementType CreateElementWithParent<TElementType>(By by, WindowsElement webElement)
            where TElementType : Element
        {
            DetermineElementAttributes(out var elementName, out var pageName);

            var element = Activator.CreateInstance<TElementType>();
            element.By = by;
            element.ParentWrappedElement = webElement;
            element.ElementName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
            var appService = ServicesCollection.Current.Resolve<AppService>();
            element.PageName = string.IsNullOrEmpty(pageName) ? appService.Title : pageName;

            return element;
        }

        public TElementType CreateElementThatIsFound<TElementType>(By by, WindowsElement webElement)
            where TElementType : Element
        {
            DetermineElementAttributes(out var elementName, out var pageName);

            var element = Activator.CreateInstance<TElementType>();
            element.By = by;
            element.FoundWrappedElement = webElement;
            element.ElementName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
            var appService = ServicesCollection.Current.Resolve<AppService>();
            element.PageName = string.IsNullOrEmpty(pageName) ? appService.Title : pageName;

            return element;
        }

        private void DetermineElementAttributes(out string elementName, out string pageName)
        {
            elementName = string.Empty;
            pageName = string.Empty;
            var callStackTrace = new StackTrace();
            var currentAssembly = GetType().Assembly;

            foreach (var frame in callStackTrace.GetFrames())
            {
                var frameMethodInfo = frame.GetMethod() as MethodInfo;
                if (!frameMethodInfo?.ReflectedType?.Assembly.Equals(currentAssembly) == true &&
                    !frameMethodInfo.IsStatic &&
                    frameMethodInfo.ReturnType.IsSubclassOf(typeof(Element)))
                {
                    elementName = frame.GetMethod().Name.Replace("get_", string.Empty);
                    if (frameMethodInfo.ReflectedType.IsSubclassOf(typeof(Page)))
                    {
                        pageName = frameMethodInfo.ReflectedType.Name;
                    }

                    break;
                }
            }
        }
    }
}
