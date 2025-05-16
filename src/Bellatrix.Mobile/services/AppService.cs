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
using System;
using System.Linq;
using System.Runtime.InteropServices;
using Bellatrix.LLM;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Services;

public class AppService<TDriver, TComponent> : MobileService<TDriver, TComponent>, IViewSnapshotProvider
    where TDriver : AppiumDriver
    where TComponent : AppiumElement
{
    public AppService(TDriver wrappedDriver)
        : base(wrappedDriver)
    {
        ServicesCollection.Current.RegisterInstance<IViewSnapshotProvider>(this);
    }

    public string Context { get => WrappedAppiumDriver.Context; set => WrappedAppiumDriver.Context = value; }
    public string PageSource => WrappedAppiumDriver.PageSource;
    public string Title => WrappedAppiumDriver.Title;

    public void BackgroundApp(int seconds) => WrappedAppiumDriver.BackgroundApp(TimeSpan.FromSeconds(seconds));

    public void CloseApp() => WrappedAppiumDriver.TerminateApp(AppConfiguration.AppId);

    public void LaunchApp() => WrappedAppiumDriver.ActivateApp(AppConfiguration.AppId);

    // TODO: ResetApp() method
    public void ResetApp() => throw new NotImplementedException("Reset? No.");

    public void InstallApp(string appPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            appPath = appPath.Replace('\\', '/');
        }

        WrappedAppiumDriver.InstallApp(appPath);
    }

    public void RemoveApp(string appId) => WrappedAppiumDriver.RemoveApp(appId);

    public bool IsAppInstalled(string bundleId)
    {
        try
        {
            return WrappedAppiumDriver.IsAppInstalled(bundleId);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    /// <summary>
    /// Returns a structured JSON summary of the current view hierarchy.
    /// This method parses the full UI tree as provided by the Appium driver, not just elements currently visible on the screen.
    /// It includes all elements present in the page source, regardless of their visibility or interactability.
    /// For each element, it extracts key attributes such as tag, resource-id, name, text, class, content-desc, label, and type.
    /// The result is a JSON array of summarized element objects, filtered to include only those with at least one of: Id, Text, or ContentDesc.
    /// </summary>
    /// <returns>JSON string summarizing the current UI hierarchy.</returns>
    public string GetCurrentViewSnapshot()
    {
        var xml = PageSource;
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