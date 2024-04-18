// <copyright file="ComponentRepository.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.SyncPlaywright.Element;
using System.Diagnostics;
using System.Reflection;

namespace Bellatrix.Playwright.Services;
public class ComponentRepository
{
    private WrappedBrowser WrappedBrowser => ServicesCollection.Current.Resolve<WrappedBrowser>();

    public dynamic CreateComponentWithParent(FindStrategy by, Component parenTComponent, Type newElementType)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);
        element.By = by;

        if (element is Frame)
        {
            element.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, by.Resolve(parenTComponent.WrappedElement));
        }
        else
        {
            element.WrappedElement = by.Resolve(parenTComponent.WrappedElement);
        }

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, Component parenTComponent)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;

        if (element is Frame)
        {
            element.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, by.Resolve(parenTComponent.WrappedElement));
        }
        else
        {
            element.WrappedElement = by.Resolve(parenTComponent.WrappedElement);
        }

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public dynamic CreateComponentWithParent(FindStrategy by, WebElement parenTComponent, Type newElementType)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);
        element.By = by;

        if (element is Frame)
        {
            element.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, by.Resolve(parenTComponent));
        }
        else
        {
            element.WrappedElement = by.Resolve(parenTComponent);
        }

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, WebElement parenTComponent)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;

        if (element is Frame)
        {
            element.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, by.Resolve(parenTComponent));
        }
        else
        {
            element.WrappedElement = by.Resolve(parenTComponent);
        }

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public TComponentType CreateComponent<TComponentType>(FindStrategy by)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;

        if (element is Frame)
        {
            element.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, by.Resolve(WrappedBrowser.CurrentPage));
        }
        else
        {
            element.WrappedElement = by.Resolve(WrappedBrowser.CurrentPage);
        }

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    private void DetermineComponentAttributes(out string elementName, out string pageName)
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
                    frameMethodInfo.ReturnType.IsSubclassOf(typeof(Component)))
                {
                    elementName = frame.GetMethod().Name.Replace("get_", string.Empty);
                    if (frameMethodInfo.ReflectedType.IsSubclassOf(typeof(WebPage)))
                    {
                        pageName = frameMethodInfo.ReflectedType.Name;
                    }

                    break;
                }
            }
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
        }
    }
}
