// <copyright file="ShadowDomService.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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

using AngleSharp.Dom;
using AngleSharp.XPath;
using Bellatrix.Core.Utilities;
using Bellatrix.Playwright.Locators;
using Bellatrix.Playwright.Services;
using Microsoft.TeamFoundation.Common;
using System.Diagnostics;
using System.Text;
using System.Xml.XPath;

namespace Bellatrix.Playwright.Components.ShadowDom;
internal static class ShadowDomService
{
    private static readonly string ChildCombinator = " > ";
    private static readonly string ShadowRootTag = "shadow-root";

    internal static string GetShadowHtml(ShadowRoot shadowRoot)
    {
        var script = $@"
                element => {{
                           function clone(element, tag) {{
                               let cloneElement;
                             	if (element instanceof ShadowRoot && !tag) {{
                                 cloneElement = new DocumentFragment();
                               }} else if (tag) {{
                                 cloneElement = document.createElement(tag);
                               }}
                               else {{
                                 cloneElement = element.cloneNode();
                                 if (element.firstChild && element.firstChild.nodeType === 3) {{
                                   cloneElement.appendChild(element.firstChild.cloneNode());
                                 }}
                               }}

                               if (element.shadowRoot) {{
                                   cloneElement.appendChild(clone(element.shadowRoot, ""{ShadowRootTag}""));
                               }}

                               if (element.children) {{
                                   for (const child of element.children) {{
                                       cloneElement.appendChild(clone(child));
                                   }}
                               }}

                               return cloneElement;
                           }}
                       
                        		var temporaryDiv = document.createElement(""div"");
                         	temporaryDiv.appendChild(clone(element.shadowRoot, undefined));
                           return temporaryDiv.innerHTML;
                       }};
           ";

        return shadowRoot.Evaluate<string>(script);
    }

    internal static TComponent CreateInShadowContext<TComponent, TBy>(Component parentComponent, TBy findStrategy, bool shouldCacheElement = false)
        where TComponent : Component
        where TBy : FindStrategy
    {
        var shadowRoots = GetShadowRootAncestors(parentComponent);
        var initialShadowRoot = shadowRoots.Pop();

        var fullLocator = TraceBackLocator(parentComponent, ref shadowRoots);
        var parentElement = GetElement(initialShadowRoot, fullLocator);

        var newElement = GetElement(parentElement, findStrategy);

        return CreateComponent<TComponent>(initialShadowRoot, newElement, findStrategy, shouldCacheElement);
    }

    internal static dynamic CreateInShadowContext(Component parentComponent, FindStrategy findStrategy, Type componentType, bool shouldCacheElement = false)
    {
        var shadowRoots = GetShadowRootAncestors(parentComponent);
        var initialShadowRoot = shadowRoots.Pop();

        var fullLocator = TraceBackLocator(parentComponent, ref shadowRoots);
        var parentElement = GetElement(initialShadowRoot, fullLocator);

        var newElement = GetElement(parentElement, findStrategy);

        return CreateComponent(initialShadowRoot, newElement, findStrategy, componentType, shouldCacheElement);
    }

    internal static ComponentsList<TComponent> CreateAllInShadowContext<TComponent, TBy>(Component parentComponent, TBy findStrategy, bool shouldCacheElement = false)
        where TComponent : Component
        where TBy : FindStrategy
    {
        var componentList = new ComponentsList<TComponent>();

        var shadowRoots = GetShadowRootAncestors(parentComponent);
        var initialShadowRoot = shadowRoots.Pop();

        var fullLocator = TraceBackLocator(parentComponent, ref shadowRoots);
        var parentElement = GetElement(initialShadowRoot, fullLocator);

        foreach (var element in GetElements(parentElement, findStrategy))
        {
            componentList.Add(CreateComponent<TComponent>(initialShadowRoot, element, findStrategy, shouldCacheElement));
        }

        return componentList;
    }

    internal static TComponent CreateFromShadowRoot<TComponent, TBy>(ShadowRoot shadowRoot, TBy findStrategy, bool shouldCacheElement = false)
        where TComponent : Component
        where TBy : FindStrategy
    {
        return CreateComponent<TComponent>(shadowRoot, GetElement(shadowRoot, findStrategy), findStrategy, shouldCacheElement);
    }

    internal static dynamic CreateFromShadowRoot(ShadowRoot shadowRoot, FindStrategy findStrategy, Type componentType, bool shouldCacheElement = false)
    {
        return CreateComponent(shadowRoot, GetElement(shadowRoot, findStrategy), findStrategy, componentType, shouldCacheElement);
    }

    internal static ComponentsList<TComponent> CreateAllFromShadowRoot<TComponent, TBy>(ShadowRoot shadowRoot, TBy findStrategy, bool shouldCacheElement = false)
        where TComponent : Component
        where TBy : FindStrategy
    {
        var componentList = new ComponentsList<TComponent>();

        foreach (var element in GetElements(shadowRoot, findStrategy))
        {
            componentList.Add(CreateComponent<TComponent>(shadowRoot, element, findStrategy, shouldCacheElement));
        }

        return componentList;
    }

    private static FindStrategy TraceBackLocator(Component parentComponent, ref Stack<ShadowRoot> shadowRoots)
    {
        var locatorBuilder = new StringBuilder();
        while (shadowRoots.TryPop(out ShadowRoot shadowRoot))
        {
            locatorBuilder.Append(shadowRoot.By.Value).Append(ChildCombinator + ShadowRootTag);
        }
        return new FindCssStrategy(locatorBuilder.ToString() + parentComponent.By.Value);
    }

    private static TComponent CreateComponent<TComponent>(ShadowRoot initialShadowRoot, IElement element, FindStrategy findStrategy, bool shouldCacheElement = false)
        where TComponent : Component
    {
        var shadowRoot = initialShadowRoot;

        // initial absolute css, relative to the outermost shadow root element
        var elementCss = SplitByChildOperator(HtmlService.ConvertAbsoluteXpathToCss(HtmlService.GetAbsoluteXpath(element)));

        // if there are <shadow-root> elements found between the outermost shadow root and the element
        // populate the Stack<Element>
        if (TryFindNestedShadowRoots(element, out var nestedShadowRootStack))
        {
            string[] previousCss = null;
            while (!nestedShadowRootStack.IsNullOrEmpty())
            {
                var parent = nestedShadowRootStack.Pop();
                var css = SplitByChildOperator(HtmlService.ConvertAbsoluteXpathToCss(HtmlService.GetAbsoluteXpath(parent)));
                if (previousCss != null)
                {
                    css = RemoveRedundantSteps(css, previousCss);
                }


                shadowRoot = CreateNestedShadowRoot(shadowRoot, CleanFromShadowRootTags(css));

                elementCss = RemoveRedundantSteps(elementCss, css);

                previousCss = SplitByChildOperator(HtmlService.ConvertAbsoluteXpathToCss(HtmlService.GetAbsoluteXpath(parent)));
            }
        }

        var finalCss = string.Join(ChildCombinator, CleanFromShadowRootTags(elementCss));

        if (findStrategy.GetType() == typeof(FindXpathStrategy))
        {
            return ComponentRepository.CreateComponentWithParent<TComponent>(new FindShadowXpathStrategy(findStrategy.Value, finalCss), shadowRoot);
        }
        else
        {
            return ComponentRepository.CreateComponentWithParent<TComponent>(new FindCssStrategy(finalCss), shadowRoot);
        }
    }

    private static dynamic CreateComponent(ShadowRoot initialShadowRoot, IElement element, FindStrategy findStrategy, Type componentType, bool shouldCacheElement = false)
    {
        var shadowRoot = initialShadowRoot;

        // initial absolute css, relative to the outermost shadow root element
        var elementCss = SplitByChildOperator(HtmlService.ConvertAbsoluteXpathToCss(HtmlService.GetAbsoluteXpath(element)));

        // if there are <shadow-root> elements found between the outermost shadow root and the element
        // populate the Stack<Element>
        if (TryFindNestedShadowRoots(element, out var nestedShadowRootStack))
        {
            string[] previousCss = null;
            while (!nestedShadowRootStack.IsNullOrEmpty())
            {
                var parent = nestedShadowRootStack.Pop();
                var css = SplitByChildOperator(HtmlService.ConvertAbsoluteXpathToCss(HtmlService.GetAbsoluteXpath(parent)));
                if (previousCss != null)
                {
                    css = RemoveRedundantSteps(css, previousCss);
                }


                shadowRoot = CreateNestedShadowRoot(shadowRoot, CleanFromShadowRootTags(css));

                elementCss = RemoveRedundantSteps(elementCss, css);

                previousCss = SplitByChildOperator(HtmlService.ConvertAbsoluteXpathToCss(HtmlService.GetAbsoluteXpath(parent)));
            }
        }

        var finalCss = string.Join(ChildCombinator, CleanFromShadowRootTags(elementCss));

        if (findStrategy.GetType() == typeof(FindXpathStrategy))
        {
            return ComponentRepository.CreateComponentWithParent(new FindShadowXpathStrategy(findStrategy.Value, finalCss), shadowRoot, componentType);
        }
        else
        {
            return ComponentRepository.CreateComponentWithParent(new FindCssStrategy(finalCss), shadowRoot, componentType);
        }
    }

    private static string[] RemoveRedundantSteps(string[] elementCss, string[] currentCss)
    {
        return elementCss.Skip(currentCss.Length).ToArray();
    }

    private static string[] CleanFromShadowRootTags(string[] css)
    {
        return css.Where(x => !x.Contains(ShadowRootTag)).ToArray();
    }

    private static string[] SplitByChildOperator(string css)
    {
        return css.Split(new[] { ChildCombinator }, StringSplitOptions.None)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();
    }

    private static ShadowRoot CreateNestedShadowRoot(ShadowRoot parent, string[] locator)
    {
        return ComponentRepository.CreateComponentWithParent<ShadowRoot>(new FindCssStrategy(string.Join(ChildCombinator, locator)), parent);
    }

    private static bool TryFindNestedShadowRoots(IElement element, out Stack<IElement> parentShadowRootStack)
    {
        parentShadowRootStack = new Stack<IElement>();
        var parent = element.Parent;

        while (parent != null)
        {
            if (parent.NodeName.ToLower().Equals(ShadowRootTag))
            {
                parentShadowRootStack.Push(parent as IElement);
            }

            parent = parent.Parent;
        }

        return !parentShadowRootStack.IsNullOrEmpty();
    }

    private static IElement GetElement(ShadowRoot component, FindStrategy findStrategy)
    {
        var docBody = HtmlService.ParseHtml(component.InnerHtml).Body;

        return GetElements(docBody, findStrategy).FirstOrDefault();
    }

    private static IElement GetElement(IElement element, FindStrategy findStrategy)
    {
        return GetElements(element, findStrategy).FirstOrDefault();
    }

    private static List<IElement> GetElements(ShadowRoot component, FindStrategy findStrategy)
    {
        var docBody = HtmlService.ParseHtml(component.InnerHtml).Body;

        return GetElements(docBody, findStrategy);
    }

    private static List<IElement> GetElements(IElement element, FindStrategy findStrategy)
    {
        var mechanism = findStrategy.GetType();
        var elements = new List<IElement>();

        try
        {
            if (mechanism == typeof(FindXpathStrategy))
            {
                elements = element.SelectNodes(findStrategy.Value).Select(x => x as IElement).ToList();
            }
            else if (mechanism == typeof(FindCssStrategy))
            {
                elements = element.QuerySelectorAll(findStrategy.Value).ToList();
            }
        }
        catch (XPathException)
        {
            Debug.WriteLine($"{mechanism}: {findStrategy.Value}");
        }

        return elements;
    }

    private static Stack<ShadowRoot> GetShadowRootAncestors(Component initialComponent)
    {
        var component = initialComponent.ParentComponent;

        var shadowRoots = new Stack<ShadowRoot>();

        while (component != null)
        {
            if (component.GetType() == typeof(ShadowRoot))
            {
                shadowRoots.Push((ShadowRoot)component);
            }

            component = component.ParentComponent;
        }

        return shadowRoots;
    }
}