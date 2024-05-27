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

using Bellatrix.Core.Utilities;
using Bellatrix.Playwright.Components;
using Bellatrix.Playwright.Locators;
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

        ResolveFindStrategy(element, by, parenTComponent);

        ResolveRelativeWebElement(element);

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, Component parenTComponent)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();

        ResolveFindStrategy(element, by, parenTComponent);

        ResolveRelativeWebElement(element);

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({element.By})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, Component parenTComponent, WebElement element)
    where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var component = Activator.CreateInstance<TComponentType>();

        component.WrappedElement = element;
        component.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({component.By})" : elementName;
        component.PageName = pageName ?? string.Empty;
        component.ParentComponent = parenTComponent;

        return component;
    }

    public ComponentsList<TComponentType> CreateComponentListWithParent<TComponentType>(FindStrategy by, Component parenTComponent)
        where TComponentType : Component
    {
        var list = new List<TComponentType>();

        if (parenTComponent is ShadowRoot && by is FindXpathStrategy)
        {
            var cssLocators = HtmlService.FindCssLocators(((ShadowRoot)parenTComponent).InnerHtml, by.Value);

            foreach (var locator in cssLocators)
            {
                list.Add(CreateComponentWithParent<TComponentType>(new FindShadowXpathStrategy(by.Value, locator), parenTComponent));
            }
        }
        else if (GetAncestor(parenTComponent) is ShadowRoot && by is FindXpathStrategy)
        {
            var ancestor = GetAncestor(parenTComponent);

            var cssLocators = HtmlService.FindRelativeCssLocators(HtmlService.FindNodeByCss(((ShadowRoot)ancestor).InnerHtml, parenTComponent.By.Value), by.Value);

            foreach (var locator in cssLocators)
            {
                list.Add(CreateComponentWithParent<TComponentType>(new FindShadowXpathStrategy(by.Value, locator), ancestor));
            }
        }
        else
        {
            var webElements = by.Resolve(parenTComponent.WrappedElement).All();
            foreach (var element in webElements)
            {
                list.Add(CreateComponentWithParent<TComponentType>(by, parenTComponent, element));
            }
        }

        return new ComponentsList<TComponentType>(list);
    }

    public ComponentsList<TComponentType> CreateComponentList<TComponentType>(FindStrategy by)
        where TComponentType : Component
    {
        var list = new List<TComponentType>();
        var webElements = by.Resolve(WrappedBrowser.CurrentPage).All();
        foreach (var element in webElements)
        {
            list.Add(CreateComponent<TComponentType>(by));
        }

        return new ComponentsList<TComponentType>(list);
    }

    public dynamic CreateComponentWithParent(FindStrategy by, WebElement parentElement, Type newElementType)
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        dynamic element = Activator.CreateInstance(newElementType);
        element.By = by;

        ResolveRelativeWebElement(element, parentElement);

        element.ComponentName = string.IsNullOrEmpty(elementName) ? $"control ({by})" : elementName;
        element.PageName = pageName ?? string.Empty;

        return element;
    }

    public TComponentType CreateComponentWithParent<TComponentType>(FindStrategy by, WebElement parentElement)
        where TComponentType : Component
    {
        DetermineComponentAttributes(out var elementName, out var pageName);

        var element = Activator.CreateInstance<TComponentType>();
        element.By = by;

        ResolveRelativeWebElement(element, parentElement);

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

        ResolveWebElement(element);

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

    private void ResolveRelativeWebElement(Component component, WebElement parentElement)
    {
        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, component.By.Resolve(parentElement));
        }
        else
        {
            component.WrappedElement = component.By.Resolve(parentElement);
        }
    }

    private void ResolveWebElement(Component component)
    {
        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, component.By.Resolve(WrappedBrowser.CurrentPage));
        }
        else
        {
            component.WrappedElement = component.By.Resolve(WrappedBrowser.CurrentPage);
        }
    }

    private void ResolveRelativeWebElement(Component component)
    {
        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, component.By.Resolve(component.ParentComponent.WrappedElement));
        }
        else
        {
            component.WrappedElement = component.By.Resolve(component.ParentComponent.WrappedElement);
        }
    }

    private void ResolveFindStrategy(Component component, FindStrategy by, Component parenTComponent)
    {
        if (parenTComponent is ShadowRoot && by is FindXpathStrategy)
        {
            var cssLocator = HtmlService.FindCssLocator(((ShadowRoot)parenTComponent).InnerHtml, by.Value);

            component.By = new FindShadowXpathStrategy(by.Value, cssLocator);
            component.ParentComponent = parenTComponent;
        }
        else if (GetAncestor(parenTComponent) is ShadowRoot && by is FindXpathStrategy)
        {
            var ancestor = GetAncestor(parenTComponent);

            var cssLocator = HtmlService.FindRelativeCssLocator(HtmlService.FindNodeByCss(((ShadowRoot)ancestor).InnerHtml, parenTComponent.By.Value), by.Value);

            component.By = new FindShadowXpathStrategy(by.Value, cssLocator);
            component.ParentComponent = ancestor;
        }
        else
        {
            component.By = by;
            component.ParentComponent = parenTComponent;
        }
    }

    private Component GetAncestor(Component parentComponent)
    {
        var component = parentComponent;

        while (component != null)
        {
            if (component.GetType() == typeof(ShadowRoot))
            {
                return component;
            }
            component = component.ParentComponent;
        }

        return component;
    }
}
