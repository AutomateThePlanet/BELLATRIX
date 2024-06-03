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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Diagnostics;
using System.Reflection;
using Bellatrix.Core.Utilities;
using OpenQA.Selenium;

namespace Bellatrix.Web;

public class ComponentRepository
{
    /// <summary>
    /// OBSOLETE. <br></br>
    /// The whole component should be passed as argument, instead of only the wrapped element. Components contain useful information.
    /// </summary>    
    public dynamic CreateComponentWithParent(FindStrategy by, IWebElement parenTComponent, Type newElementType, bool shouldCacheElement)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);
        element.By = by;
        element.ParentWrappedElement = parenTComponent;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    public dynamic CreateComponentWithParent(FindStrategy by, Component parenTComponent, Type newElementType, bool shouldCacheElement)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);

        DetermineFindStrategy(element, parenTComponent, by);

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    /// <summary>
    /// OBSOLETE. <br></br>
    /// The whole component should be passed as argument, instead of only the wrapped element. Components contain useful information.
    /// </summary>
    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, IWebElement parenTComponent, IWebElement foundElement, int elementsIndex, bool shouldCacheElement)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;
        element.ParentWrappedElement = parenTComponent;
        element.WrappedElement = foundElement;
        element.ElementIndex = elementsIndex;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, Component parenTComponent, IWebElement foundElement, int elementsIndex, bool shouldCacheElement)
    where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();

        DetermineFindStrategy(element, parenTComponent, by);

        element.WrappedElement = foundElement;
        element.ElementIndex = elementsIndex;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    private static void DetermineFindStrategy(Component component, Component parenTComponent, FindStrategy by)
    {
        if (parenTComponent is Components.ShadowRoot && by.Convert().Mechanism.ToLower().Equals("xpath"))
        {
            var absoluteCss = HtmlService.FindCssLocator(((Components.ShadowRoot)parenTComponent).InnerHtml, by.Value);

            component.By = new FindShadowXpathStrategy(by.Value, absoluteCss);
            component.ParentComponent = parenTComponent;
            component.ParentWrappedElement = parenTComponent.WrappedElement;
        }
        else if (GetAncestor(parenTComponent) is Components.ShadowRoot ancestor && by.Convert().Mechanism.ToLower().Equals("xpath"))
        {
            var absoluteCss = HtmlService.FindRelativeCssLocator(HtmlService.FindNodeByCss(ancestor.InnerHtml, parenTComponent.By.Value), by.Value);

            component.By = new FindShadowXpathStrategy(by.Value, absoluteCss);
            component.ParentComponent = ancestor;
            component.ParentWrappedElement = ancestor.WrappedElement;
        } 
        else if (GetAncestor(parenTComponent) is Components.ShadowRoot && by is FindShadowXpathStrategy)
        {
            component.By = by;
            component.ParentComponent = parenTComponent;
            component.ParentWrappedElement = GetAncestor(parenTComponent).WrappedElement;
        }
        else
        {
            component.By = by;
            component.ParentComponent = parenTComponent;
            component.ParentWrappedElement = parenTComponent.WrappedElement;
        }
    }

    private static Component GetAncestor(Component parenTComponent)
    {
        var ancestor = parenTComponent.ParentComponent;

        while (ancestor != null)
        {
            if (ancestor is Components.ShadowRoot)
            {
                return ancestor;
            }

            ancestor = ancestor.ParentComponent;
        }

        return ancestor;
    }

    public dynamic CreateComponentThatIsFound(FindStrategy by, IWebElement webElement, Type newElementType, bool shouldCacheElement)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);
        element.By = by;
        element.WrappedElement = webElement;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

        return element;
    }

    public TComponentType CreateComponentThatIsFound<TComponentType>(FindStrategy by, IWebElement webElement, bool shouldCacheElement)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;
        element.WrappedElement = webElement;
        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;
        element.ShouldCacheElement = shouldCacheElement;

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