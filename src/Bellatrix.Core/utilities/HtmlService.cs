// <copyright file="cs" company="Automate The Planet Ltd.">
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

using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.XPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bellatrix.Core.Utilities;

public static class HtmlService
{
    private static IDocument ParseHtml(string html)
    {
        var config = Configuration.Default.WithXPath();
        var context = BrowsingContext.New(config);

        return context.OpenAsync(req => req.Content(html)).Result;
    }

    public static string FindCssLocator(string html, string xpath)
    {
        var absoluteXpath = FindElementAbsoluteXpath(html, xpath);

        return ConvertAbsoluteXpathToCss(absoluteXpath);
    }

    public static string FindRelativeCssLocator(IElement element, string xpath)
    {
        var absoluteXpath = FindRelativeElementAbsoluteXpath(element, xpath);

        return ConvertAbsoluteXpathToCss(absoluteXpath);
    }

    public static List<string> FindCssLocators(string html, string xpath)
    {
        var absoluteXpaths = FindElementsAbsoluteXpath(html, xpath);

        return ConvertAbsoluteXpathToCss(absoluteXpaths);
    }

    public static List<string> FindRelativeCssLocators(IElement element, string xpath)
    {
        var absoluteXpaths = FindRelativeElementsAbsoluteXpath(element, xpath);

        return ConvertAbsoluteXpathToCss(absoluteXpaths);
    }

    public static IElement FindNodeByCss(string html, string css)
    {
        var doc = ParseHtml(html);


        return doc.QuerySelector(css) ?? throw new ArgumentException($"No element was found with the css: {css}");
    }

    public static IElement FindNodeByXpath(string html, string xpath)
    {
        var doc = ParseHtml(html);

        return doc.Body.SelectSingleNode(xpath).ParentElement ?? throw new ArgumentException($"No element was found with the xpath: {xpath}");
    }

    private static string FindElementAbsoluteXpath(string html, string xpath)
    {
        var doc = ParseHtml(html);
        
        var node = doc.Body.SelectSingleNode(xpath);

        if (node == null) throw new ArgumentException($"No element was found with the xpath: {xpath}");

        return GetAbsoluteXPath(node);
    }

    public static List<string> FindElementsAbsoluteXpath(string html, string xpath)
    {
        var doc = ParseHtml(html);
        var nodes = doc.Body.SelectNodes(xpath).ToList();

        if (!nodes.Any()) throw new ArgumentException($"No elements were found with the xpath: {xpath}");

        var individualQueries = new List<string>();
        foreach (var node in nodes)
        {
            individualQueries.Add(GetAbsoluteXPath(node));
        }

        return individualQueries;
    }

    private static string FindRelativeElementAbsoluteXpath(IElement baseElement, string xpath)
    {
        var el = baseElement.SelectSingleNode(xpath);

        if (el == null) throw new ArgumentException($"No element was found with the xpath: {xpath}");

        return GetAbsoluteXPath(el);
    }

    private static List<string> FindRelativeElementsAbsoluteXpath(INode baseElement, string xpath)
    {
        var elements = baseElement.ParentElement.SelectNodes(xpath).ToList();

        if (!elements.Any()) throw new ArgumentException($"No elements were found with the xpath: {xpath}");

        var queries = new List<string>();

        foreach (var node in elements)
        {
            queries.Add(GetAbsoluteXPath(node));
        }

        return queries;
    }

    private static string ConvertAbsoluteXpathToCss(string xpath)
    {
        string cssSelector = xpath.Replace("/", " > ");

        // Use regular expression to replace type[number] with :nth-child(number of type)
        var pattern = new Regex(@"(\w+)\[(\d+)\]");
        var matches = pattern.Matches(cssSelector);
        var builder = new StringBuilder();
        int lastIndex = 0;

        foreach (Match match in matches)
        {
            builder.Append(cssSelector.Substring(lastIndex, match.Index - lastIndex));
            string type = match.Groups[1].Value;
            int number = int.Parse(match.Groups[2].Value);
            builder.Append(":nth-child(").Append(number).Append(" of ").Append(type).Append(")");
            lastIndex = match.Index + match.Length;
        }
        builder.Append(cssSelector.Substring(lastIndex));

        var locator = builder.ToString();

        if (locator.StartsWith(" > "))
        {
            locator = locator.Substring(3);
        }

        return locator;
    }

    private static List<string> ConvertAbsoluteXpathToCss(List<string> queries)
    {
        for (int i = 0; i < queries.Count; i++)
        {
            queries[i] = ConvertAbsoluteXpathToCss(queries[i]);
        }

        return queries;
    }

    // TODO: Maybe use IElement and TagName, instead of INode and NodeName.ToLower()
    private static string GetAbsoluteXPath(INode node)
    {
        var xpath = new StringBuilder("/");

        foreach (var ancestor in node.Ancestors())
        {
            if (ancestor.NodeName.ToLower().Equals("html") || ancestor.NodeName.ToLower().Equals("body") || ancestor.NodeName.StartsWith("#"))
            {
                // ignore the <html> and <body>
                continue;
            }

            int index = 1;

            var sibling = ancestor.PreviousSibling;

            while (sibling != null && !sibling.NodeName.StartsWith("#"))
            {
                if (sibling.NodeName.ToLower().Equals(ancestor.NodeName.ToLower()))
                {
                    index++;
                }

                sibling = sibling.PreviousSibling;
            }

            xpath.Insert(0, $"/{ancestor.NodeName.ToLower()}[{index}]");
        }

        xpath.Append(node.NodeName.ToLower());

        return xpath.ToString();
    }
}
