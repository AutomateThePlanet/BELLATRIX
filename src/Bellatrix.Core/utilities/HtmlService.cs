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
using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Bellatrix.Core.Utilities;

public static class HtmlService
{
    private static string ChildCombinator => " > ";
    private static string Node => "/";
    private static string NodeOrSelf => "//";
    private static string RootElementTag => "bellatrix-root";

    public static IDocument AddRootElementIfNeeded(IDocument doc)
    {
        var topLevelElements = doc.ChildNodes.OfType<IElement>().ToList();
        bool hasMultipleTopLevelElements = topLevelElements.Count > 1;

        if (hasMultipleTopLevelElements)
        {
            var root = doc.CreateElement(RootElementTag);

            foreach (var node in doc.ChildNodes.ToList())
            {
                root.AppendChild(node.Clone(true));
            }

            while (doc.ChildNodes.Length > 0)
            {
                doc.RemoveChild(doc.ChildNodes[0]);
            }

            doc.AppendChild(root);
        }

        return doc;
    }

    public static IDocument ParseHtml(string html)
    {
        var config = Configuration.Default.WithXPath();
        var context = BrowsingContext.New(config);

        return context.OpenAsync(req => req.Content(html)).Result;
    }

    public static string ConvertAbsoluteXpathToCss(string xpath)
    {
        string cssSelector = xpath.Replace(Node, ChildCombinator);

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
            builder.Append($"{type}:nth-of-type({number})");
            //builder.Append(":nth-child(").Append(number).Append(" of ").Append(type).Append(")");
            lastIndex = match.Index + match.Length;
        }
        builder.Append(cssSelector.Substring(lastIndex));

        var locator = builder.ToString();

        if (locator.StartsWith(ChildCombinator))
        {
            locator = locator.Substring(3);
        }

        return locator;
    }

    public static string GetAbsoluteXpath(INode node)
    {
        StringBuilder xpath = new StringBuilder(Node);
        var currentNode = node;
        while (currentNode != null)
        {
            if (currentNode.NodeName.ToLower().Equals("html") ||  currentNode.NodeName.ToLower().Equals("body") ||  currentNode.NodeName.StartsWith("#"))
            {
                // ignore the <html> and <body> and other invalid element tags
                // ignore added BELLATRIX root element
                // ignore invalid element tags
                break;
            }

            xpath.Insert(0, IndexElement(currentNode));

            currentNode = currentNode.Parent;
        }

        return xpath.ToString();
    }

    private static string IndexElement(INode element)
    {
        int index = 1;

        var previousSibling = element.PreviousSibling;
        while (previousSibling != null)
        {
            if (previousSibling.NodeName.ToLower().Equals(element.NodeName.ToLower()))
            {
                index++;
            }
            previousSibling = previousSibling.PreviousSibling;
        }

        return $"/{element.NodeName.ToLower()}[{index}]";
    }

    public static string RemoveDanglingChildCombinatorsFromCss(string css)
    {
        // Split the CSS string by the child combinator and remove empty steps
        var steps = css.Split(new[] { ChildCombinator }, StringSplitOptions.None)
                       .Where(x => !string.IsNullOrWhiteSpace(x))
                       .ToArray();

        // Join the remaining steps with the child combinator operator
        return string.Join(ChildCombinator, steps);
    }
}
