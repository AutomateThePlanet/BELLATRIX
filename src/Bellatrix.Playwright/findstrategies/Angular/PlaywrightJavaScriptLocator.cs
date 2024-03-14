// <copyright file="PlaywrightJavaScriptLocator.cs" company="Automate The Planet Ltd.">
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
using System.Collections.ObjectModel;
using System.Text.Json;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.SyncPlaywright;

namespace Bellatrix.Playwright.Locators;

/// <summary>
/// Provides a mechanism by which to find elements within a document using JavaScript.
/// </summary>
public class PlaywrightJavaScriptLocator
{
    private readonly string _script;
    private readonly object[] _args;

    public PlaywrightJavaScriptLocator(string script, params object[] args)
    {
        _script = script;
        _args = args;
    }

    public object[] AdditionalScriptArguments { get; set; }

    public WebElement FindElement(IPage searchContext)
    {
        ReadOnlyCollection<WebElement> elements = FindElements(searchContext);
        if (elements.Count == 0)
        {
            throw new ArgumentException($"Unable to locate element.");
        }

        return elements[0];
    }

    public ReadOnlyCollection<WebElement> FindElements(IPage context)
    {
        object[] scriptArgs = _args;
        if (AdditionalScriptArguments != null && AdditionalScriptArguments.Length > 0)
        {
            scriptArgs = new object[_args.Length + AdditionalScriptArguments.Length];
            _args.CopyTo(scriptArgs, 0);
            AdditionalScriptArguments.CopyTo(scriptArgs, _args.Length);
        }

        // This might not work
        // TODO TEST
        return DeserializeJsonElement((JsonElement)ServicesCollection.Current.Resolve<JavaScriptService>().Execute(_script, scriptArgs));
    }

    public WebElement FindElement(WebElement searchContext)
    {
        ReadOnlyCollection<WebElement> elements = FindElements(searchContext);
        if (elements.Count == 0)
        {
            throw new ArgumentException($"Unable to locate element.");
        }

        return elements[0];
    }

    public ReadOnlyCollection<WebElement> FindElements(WebElement context)
    {
        object[] scriptArgs = _args;
        if (AdditionalScriptArguments != null && AdditionalScriptArguments.Length > 0)
        {
            scriptArgs = new object[_args.Length + AdditionalScriptArguments.Length];
            _args.CopyTo(scriptArgs, 0);
            AdditionalScriptArguments.CopyTo(scriptArgs, _args.Length);
        }

        // This might not work
        // TODO TEST
        return DeserializeJsonElement(ServicesCollection.Current.Resolve<JavaScriptService>().Execute(_script, context, scriptArgs));
    }

    private static ReadOnlyCollection<WebElement> DeserializeJsonElement(JsonElement? jsonElement)
    {
        byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(jsonElement);

        var locators = JsonSerializer.Deserialize<ReadOnlyCollection<ILocator>>(jsonBytes) ?? new ReadOnlyCollection<ILocator>(new List<ILocator>(0));
    
        var elements = new List<WebElement>();
        foreach (var locator in locators)
        {
            elements.Add(new WebElement(locator));
        }

        return elements.AsReadOnly();
    }
}
