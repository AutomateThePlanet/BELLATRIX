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
        public dynamic CreateElementWithParent(FindStrategy by, WindowsElement parentElement, Type newElementType)
        {
            DetermineElementAttributes(out var elementName, out var pageName);

            dynamic element = Activator.CreateInstance(newElementType);
            element.By = by;
            element.ParentWrappedElement = parentElement;
            element.ElementName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
            element.PageName = pageName ?? string.Empty;

            return element;
        }

        public TElementType CreateElementWithParent<TElementType>(FindStrategy by, WindowsElement parentElement, WindowsElement foundElement, int elementsIndex)
            where TElementType : Element
        {
            DetermineElementAttributes(out var elementName, out var pageName);

            var element = Activator.CreateInstance<TElementType>();
            element.By = by;
            element.ParentWrappedElement = parentElement;
            element.WrappedElement = foundElement;
            element.FoundWrappedElement = foundElement;
            element.ElementIndex = elementsIndex;
            element.ElementName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
            element.PageName = pageName ?? string.Empty;

            return element;
        }

        public dynamic CreateElementThatIsFound(FindStrategy by, WindowsElement webElement, Type newElementType)
        {
            DetermineElementAttributes(out var elementName, out var pageName);

            dynamic element = Activator.CreateInstance(newElementType);
            element.By = by;
            element.WrappedElement = webElement;
            element.FoundWrappedElement = webElement;
            element.ElementName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
            element.PageName = pageName ?? string.Empty;

            return element;
        }

        public TElementType CreateElementThatIsFound<TElementType>(FindStrategy by, WindowsElement webElement)
            where TElementType : Element
        {
            DetermineElementAttributes(out var elementName, out var pageName);

            var element = Activator.CreateInstance<TElementType>();
            element.By = by;
            element.WrappedElement = webElement;
            element.FoundWrappedElement = webElement;
            element.ElementName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
            element.PageName = pageName ?? string.Empty;

            return element;
        }

        private void DetermineElementAttributes(out string elementName, out string pageName)
        {
            elementName = string.Empty;
            pageName = string.Empty;
            try
            {
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
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}
