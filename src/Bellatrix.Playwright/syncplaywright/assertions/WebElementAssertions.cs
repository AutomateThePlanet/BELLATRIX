// <copyright file="WebElementAssertions.cs" company="Automate The Planet Ltd.">
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

using Microsoft.VisualStudio.Services.WebApi;
using System.Text.RegularExpressions;

namespace Bellatrix.Playwright.SyncPlaywright;

public class WebElementAssertions
{
    public WebElementAssertions(WebElement element)
    {
        Element = element;
        NativeAssertions = Expect(Element.WrappedLocator);
    }

    public WebElement Element { get; init; }
    public ILocatorAssertions NativeAssertions { get; init; }

    public WebElementAssertions Not
    {
        get
        {
            _ = NativeAssertions.Not;
            return this;
        }
    }

    public void ToBeAttached(LocatorAssertionsToBeAttachedOptions options = null)
    {
        NativeAssertions.ToBeAttachedAsync(options).SyncResult();
    }

    public void ToBeChecked(LocatorAssertionsToBeCheckedOptions options = null)
    {
        NativeAssertions.ToBeCheckedAsync(options).SyncResult();
    }

    public void ToBeDisabled(LocatorAssertionsToBeDisabledOptions options = null)
    {
        NativeAssertions.ToBeDisabledAsync(options).SyncResult();
    }

    public void ToBeEditable(LocatorAssertionsToBeEditableOptions options = null)
    {
        NativeAssertions.ToBeEditableAsync(options).SyncResult();
    }

    public void ToBeEmpty(LocatorAssertionsToBeEmptyOptions options = null)
    {
        NativeAssertions.ToBeEmptyAsync(options).SyncResult();
    }

    public void ToBeEnabled(LocatorAssertionsToBeEnabledOptions options = null)
    {
        NativeAssertions.ToBeEnabledAsync(options).SyncResult();
    }

    public void ToBeFocused(LocatorAssertionsToBeFocusedOptions options = null)
    {
        NativeAssertions.ToBeFocusedAsync(options).SyncResult();
    }

    public void ToBeHidden(LocatorAssertionsToBeHiddenOptions options = null)
    {
        NativeAssertions.ToBeHiddenAsync(options).SyncResult();
    }

    public void ToBeInViewport(LocatorAssertionsToBeInViewportOptions options = null)
    {
        NativeAssertions.ToBeInViewportAsync(options).SyncResult();
    }

    public void ToBeVisible(LocatorAssertionsToBeVisibleOptions options = null)
    {
        NativeAssertions.ToBeVisibleAsync(options).SyncResult();
    }

    public void ToContainText(string expected, LocatorAssertionsToContainTextOptions options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    public void ToContainText(Regex expected, LocatorAssertionsToContainTextOptions options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    public void ToContainText(IEnumerable<string> expected, LocatorAssertionsToContainTextOptions options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    public void ToContainText(IEnumerable<Regex> expected, LocatorAssertionsToContainTextOptions options = null)
    {
        NativeAssertions.ToContainTextAsync(expected, options).SyncResult();
    }

    public void ToHaveAttribute(string name, string value, LocatorAssertionsToHaveAttributeOptions options = null)
    {
        NativeAssertions.ToHaveAttributeAsync(name, value, options).SyncResult();
    }

    public void ToHaveAttribute(string name, Regex value, LocatorAssertionsToHaveAttributeOptions options = null)
    {
        NativeAssertions.ToHaveAttributeAsync(name, value, options).SyncResult();
    }

    public void ToHaveClass(string expected, LocatorAssertionsToHaveClassOptions options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    public void ToHaveClass(Regex expected, LocatorAssertionsToHaveClassOptions options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    public void ToHaveClass(IEnumerable<string> expected, LocatorAssertionsToHaveClassOptions options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    public void ToHaveClass(IEnumerable<Regex> expected, LocatorAssertionsToHaveClassOptions options = null)
    {
        NativeAssertions.ToHaveClassAsync(expected, options).SyncResult();
    }

    public void ToHaveCount(int count, LocatorAssertionsToHaveCountOptions options = null)
    {
        NativeAssertions.ToHaveCountAsync(count, options).SyncResult();
    }

    public void ToHaveCSS(string name, string value, LocatorAssertionsToHaveCSSOptions options = null)
    {
        NativeAssertions.ToHaveCSSAsync(name, value, options).SyncResult();
    }

    public void ToHaveCSS(string name, Regex value, LocatorAssertionsToHaveCSSOptions options = null)
    {
        NativeAssertions.ToHaveCSSAsync(name, value, options).SyncResult();
    }

    public void ToHaveId(string id, LocatorAssertionsToHaveIdOptions options = null)
    {
        NativeAssertions.ToHaveIdAsync(id, options).SyncResult();
    }

    public void ToHaveId(Regex id, LocatorAssertionsToHaveIdOptions options = null)
    {
        NativeAssertions.ToHaveIdAsync(id, options).SyncResult();
    }

    public void ToHaveJSProperty(string name, object value, LocatorAssertionsToHaveJSPropertyOptions options = null)
    {
        NativeAssertions.ToHaveJSPropertyAsync(name, value, options).SyncResult();
    }

    public void ToHaveText(string expected, LocatorAssertionsToHaveTextOptions options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();
    }

    public void ToHaveText(Regex expected, LocatorAssertionsToHaveTextOptions options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();
    }

    public void ToHaveText(IEnumerable<string> expected, LocatorAssertionsToHaveTextOptions options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();

    }

    public void ToHaveText(IEnumerable<Regex> expected, LocatorAssertionsToHaveTextOptions options = null)
    {
        NativeAssertions.ToHaveTextAsync(expected, options).SyncResult();
    }

    public void ToHaveValue(string value, LocatorAssertionsToHaveValueOptions options = null)
    {
        NativeAssertions.ToHaveValueAsync(value, options).SyncResult();
    }

    public void ToHaveValue(Regex value, LocatorAssertionsToHaveValueOptions options = null)
    {
        NativeAssertions.ToHaveValueAsync(value, options).SyncResult();
    }

    public void ToHaveValues(IEnumerable<string> values, LocatorAssertionsToHaveValuesOptions options = null)
    {
        NativeAssertions.ToHaveValuesAsync(values, options).SyncResult();
    }

    public void ToHaveValues(IEnumerable<Regex> values, LocatorAssertionsToHaveValuesOptions options = null)
    {
        NativeAssertions.ToHaveValuesAsync(values, options).SyncResult();
    }
}
