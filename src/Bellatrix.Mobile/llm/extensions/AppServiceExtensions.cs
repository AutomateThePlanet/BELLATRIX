// <copyright file="AppServiceExtensions.cs" company="Automate The Planet Ltd.">
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
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>
using Bellatrix.Mobile.Services;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Linq;

namespace Bellatrix.Mobile;

public static class AppServiceExtensions
{
    public static string GetPageSummaryJson<TDriver, TComponent>(this AppService<TDriver, TComponent> app)
        where TDriver : AppiumDriver
        where TComponent : AppiumElement
    {
        var xml = app.PageSource;
        var doc = new HtmlDocument();
        doc.LoadHtml(xml);

        var nodes = doc.DocumentNode.SelectNodes("//*");
        if (nodes == null || nodes.Count == 0)
        {
            return "[]";
        }

        var summary = nodes.Select(node => new MobileElementSummary
        {
            Tag = node.Name,
            Id = node.GetAttributeValue("resource-id", node.GetAttributeValue("name", null)),
            Text = node.GetAttributeValue("text", null),
            Class = node.GetAttributeValue("class", null),
            ContentDesc = node.GetAttributeValue("content-desc", node.GetAttributeValue("label", null)),
            Type = node.GetAttributeValue("type", null)
        })
        .Where(e => !string.IsNullOrEmpty(e.Id) || !string.IsNullOrEmpty(e.Text) || !string.IsNullOrEmpty(e.ContentDesc))
        .ToList();

        return JsonConvert.SerializeObject(summary, Formatting.None);
    }
}