// <copyright file="ElementRepository.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using System.Reflection;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.PageObjects;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Services;

internal class ComponentRepository
{
    public TComponent CreateComponentWithParent<TComponent, TBy, TDriver, TDriverElement>(TBy by, TDriverElement webElement)
        where TComponent : Component<TDriver, TDriverElement>
        where TBy : FindStrategy<TDriver, TDriverElement>
        where TDriver : AppiumDriver
        where TDriverElement : AppiumElement
    {
        DetermineComponentAttributes<TDriver, TDriverElement>(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponent>();
        element.By = by;
        element.ParentWrappedElement = webElement;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = string.IsNullOrEmpty(pageName) ? string.Empty : pageName;

        return element;
    }

    public TComponent CreateComponentThatIsFound<TComponent, TBy, TDriver, TDriverElement>(TBy by, TDriverElement webElement)
        where TComponent : Component<TDriver, TDriverElement>
        where TBy : FindStrategy<TDriver, TDriverElement>
        where TDriver : AppiumDriver
        where TDriverElement : AppiumElement
    {
        DetermineComponentAttributes<TDriver, TDriverElement>(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponent>();
        element.By = by;
        element.FoundWrappedElement = webElement;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = string.IsNullOrEmpty(pageName) ? string.Empty : pageName;

        return element;
    }

    private void DetermineComponentAttributes<TDriver, TDriverElement>(out string elementName, out string pageName)
        where TDriver : AppiumDriver
        where TDriverElement : AppiumElement
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
                frameMethodInfo.ReturnType.IsSubclassOf(typeof(Component<TDriver, TDriverElement>)))
            {
                elementName = frame.GetMethod().Name.Replace("get_", string.Empty);
                if (frameMethodInfo.ReflectedType.IsSubclassOf(typeof(MobilePage)))
                {
                    pageName = frameMethodInfo.ReflectedType.Name;
                }

                break;
            }
        }
    }
}
