// <copyright file="AppService.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Bellatrix.LLM;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium.Windows;
using System.Collections.Generic;
using System.Linq;

namespace Bellatrix.Desktop.Services;

public class AppService : DesktopService, IViewSnapshotProvider
{
    public AppService(WindowsDriver<WindowsElement> wrappedDriver)
        : base(wrappedDriver)
    {
        ServicesCollection.Current.RegisterInstance<IViewSnapshotProvider>(this);
    }

    public string Title => WrappedDriver.Title;

    public void Back() => WrappedDriver.Navigate().Back();

    public void Forward() => WrappedDriver.Navigate().Forward();

    public void Maximize() => WrappedDriver.Manage().Window.Maximize();

    /// <summary>
    /// Generates a JSON summary of the current desktop application's UI tree.
    /// Loads the current page source as XML, parses all nodes, and extracts key attributes
    /// (such as Tag, AutomationId, Name, ClassName, ControlType, Value, and HelpText) for each element.
    /// Only elements with a non-empty Name or AutomationId are included in the summary.
    /// Returns the result as a compact JSON array of element summaries.
    /// </summary>
    public string GetCurrentViewSnapshot()
    {
        var sourceXml = WrappedDriver.PageSource;

        var doc = new HtmlDocument();
        doc.LoadHtml(sourceXml);

        var nodes = doc.DocumentNode.SelectNodes("//*");
        if (nodes == null || nodes.Count == 0)
        {
            return "[]";
        }

        List<DesktopElementSummary> elements = nodes
            .Select(node => new DesktopElementSummary
            {
                Tag = node.Name,
                AutomationId = node.GetAttributeValue("AutomationId", null),
                Name = node.GetAttributeValue("Name", null),
                ClassName = node.GetAttributeValue("ClassName", null),
                ControlType = node.GetAttributeValue("ControlType", null),
                Value = node.GetAttributeValue("Value.Value", null),
                HelpText = node.GetAttributeValue("HelpText", null)
            })
            .Where(x => !string.IsNullOrWhiteSpace(x.Name) || !string.IsNullOrWhiteSpace(x.AutomationId))
            .ToList();

        return JsonConvert.SerializeObject(elements, Formatting.None);
    }
}