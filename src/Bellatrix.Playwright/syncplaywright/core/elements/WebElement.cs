// <copyright file="WebElement.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.SyncPlaywright.Element;
using Microsoft.VisualStudio.Services.WebApi;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Bellatrix.Playwright.SyncPlaywright;

/// <summary>
/// Wrapper for ILocator with synchronous methods.
/// </summary>
public class WebElement
{
    internal WebElement(BrowserPage page, ILocator locator)
    {
        Page = page;
        WrappedLocator = locator;
    }

    internal WebElement(BrowserPage page, WebElement element)
    {
        Page = page;
        WrappedLocator = element.WrappedLocator;
    }

    public ILocator WrappedLocator { get; set; }

    public virtual WebElement First => new WebElement(Page, WrappedLocator.First);

    public virtual WebElement Last => new WebElement(Page, WrappedLocator.Last);

    public BrowserPage Page { get; internal init; }

    public IReadOnlyList<WebElement> All()
    {
        IReadOnlyCollection<ILocator> nativeLocators;

        try
        {
            // Maybe because of the async to sync conversion, it always fails on the first try.
            // Another reason could be the fact that Playwright doesn't wait for all elements to be loaded properly on the page before trying to find them.
            nativeLocators = WrappedLocator.AllAsync().Result;
        }
        catch
        {
            // patch: Adding a wait for no HTTP requests and responses sent in the last few hundred milliseconds.
            ServicesCollection.Current.Resolve<WrappedBrowser>().CurrentPage.WaitForLoadState(LoadState.NetworkIdle);

            nativeLocators = WrappedLocator.AllAsync().Result;
        }

        var elements = new List<WebElement>();

        foreach (var locator in nativeLocators)
        {
            elements.Add(new WebElement(Page, locator));
        }

        return elements;
    }

    public IReadOnlyList<string> AllInnerTexts()
    {
        return WrappedLocator.AllInnerTextsAsync().Result;
    }

    public IReadOnlyList<string> AllTextContents()
    {
        return WrappedLocator.AllTextContentsAsync().Result;
    }

    public virtual WebElement And(WebElement element)
    {
        return new WebElement(Page, WrappedLocator.And(element.WrappedLocator));
    }

    public void Blur(LocatorBlurOptions options = null)
    {
        WrappedLocator.BlurAsync(options).SyncResult();
    }

    public LocatorBoundingBoxResult BoundingBox(LocatorBoundingBoxOptions options = null)
    {
        return WrappedLocator.BoundingBoxAsync().Result;
    }

    public void Check(LocatorCheckOptions options = null)
    {
        WrappedLocator.CheckAsync(options).SyncResult();
    }

    public void Clear(LocatorClearOptions options = null)
    {
        WrappedLocator.ClearAsync(options).SyncResult();
    }

    public void Click(LocatorClickOptions options = null)
    {
        WrappedLocator.ClickAsync(options).SyncResult();
    }

    public int Count()
    {
        return WrappedLocator.CountAsync().Result;
    }

    public void DblClick(LocatorDblClickOptions options = null)
    {
        WrappedLocator.DblClickAsync(options).SyncResult();
    }

    public void DispatchEvent(string type, object eventInit = null, LocatorDispatchEventOptions options = null)
    {
        WrappedLocator.DispatchEventAsync(type, eventInit, options).SyncResult();
    }

    public void DragTo(WebElement target, LocatorDragToOptions options = null)
    {
        WrappedLocator.DragToAsync(target.WrappedLocator, options).SyncResult();
    }

    public IElementHandle ElementHandle(LocatorElementHandleOptions options = null)
    {
        return WrappedLocator.ElementHandleAsync(options).Result;
    }

    public IReadOnlyList<IElementHandle> ElementHandles()
    {
        return WrappedLocator.ElementHandlesAsync().Result;
    }

    public T Evaluate<T>(string expression, object arg = null, LocatorEvaluateOptions options = null)
    {
        return WrappedLocator.EvaluateAsync<T>(expression, arg, options).Result;
    }

    public T EvaluateAll<T>(string expression, object arg = null)
    {
        return WrappedLocator.EvaluateAllAsync<T>(expression, arg).Result;
    }

    public IJSHandle EvaluateHandle(string expression, object arg = null, LocatorEvaluateHandleOptions options = null)
    {
        return WrappedLocator.EvaluateHandleAsync(expression, arg, options).Result;
    }

    public void Fill(string value, LocatorFillOptions options = null)
    {
        WrappedLocator.FillAsync(value, options).SyncResult();
    }

    public virtual WebElement Filter(LocatorFilterOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.Filter(options));
    }

    public void Focus(LocatorFocusOptions options = null)
    {
        WrappedLocator.FocusAsync(options).SyncResult();
    }

    public void Highlight()
    {
        WrappedLocator.HighlightAsync().SyncResult();
    }

    public void Hover(LocatorHoverOptions options = null)
    {
        WrappedLocator.HoverAsync(options).SyncResult();
    }

    public string InnerHTML(LocatorInnerHTMLOptions options = null)
    {
        return WrappedLocator.InnerHTMLAsync(options).Result;
    }

    public string InnerText(LocatorInnerTextOptions options = null)
    {
        return WrappedLocator.InnerTextAsync(options).Result;
    }

    public string InputValue(LocatorInputValueOptions options = null)
    {
        return WrappedLocator.InputValueAsync(options).Result;
    }

    public bool IsChecked(LocatorIsCheckedOptions options = null)
    {
        return WrappedLocator.IsCheckedAsync(options).Result;
    }

    public bool IsDisabled(LocatorIsDisabledOptions options = null)
    {
        return WrappedLocator.IsDisabledAsync(options).Result;
    }

    public bool IsEditable(LocatorIsEditableOptions options = null)
    {
        return WrappedLocator.IsEditableAsync(options).Result;
    }

    public bool IsEnabled(LocatorIsEnabledOptions options = null)
    {
        return WrappedLocator.IsEnabledAsync(options).Result;
    }

    public bool IsHidden(LocatorIsHiddenOptions options = null)
    {
        return WrappedLocator.IsHiddenAsync(options).Result;
    }

    public bool IsVisible(LocatorIsVisibleOptions options = null)
    {
        return WrappedLocator.IsVisibleAsync(options).Result;
    }

    public virtual WebElement Nth(int index)
    {
        return new WebElement(Page, WrappedLocator.Nth(index));
    }

    public virtual WebElement Or(WebElement element)
    {
        return new WebElement(Page, WrappedLocator.Or(element.WrappedLocator));
    }

    public void Press(string key, LocatorPressOptions options = null)
    {
        WrappedLocator.PressAsync(key, options).SyncResult();
    }

    public void PressSequentially(string text, LocatorPressSequentiallyOptions options = null)
    {
        WrappedLocator.PressSequentiallyAsync(text, options).SyncResult();
    }

    public byte[] Screenshot(LocatorScreenshotOptions options = null)
    {
        return WrappedLocator.ScreenshotAsync(options).Result;
    }

    public void ScrollIntoViewIfNeeded(LocatorScrollIntoViewIfNeededOptions options = null)
    {
        WrappedLocator.ScrollIntoViewIfNeededAsync(options).SyncResult();
    }

    public IReadOnlyList<string> SelectOption(string values, LocatorSelectOptionOptions options = null)
    {
        return WrappedLocator.SelectOptionAsync(values, options).Result;
    }

    public IReadOnlyList<string> SelectOption(IElementHandle values, LocatorSelectOptionOptions options = null)
    {
        return WrappedLocator.SelectOptionAsync(values, options).Result;
    }

    public IReadOnlyList<string> SelectOption(IEnumerable<string> values, LocatorSelectOptionOptions options = null)
    {
        return WrappedLocator.SelectOptionAsync(values, options).Result;
    }

    public IReadOnlyList<string> SelectOption(SelectOptionValue values, LocatorSelectOptionOptions options = null)
    {
        return WrappedLocator.SelectOptionAsync(values, options).Result;
    }

    public IReadOnlyList<string> SelectOption(IEnumerable<IElementHandle> values, LocatorSelectOptionOptions options = null)
    {
        return WrappedLocator.SelectOptionAsync(values, options).Result;
    }

    public IReadOnlyList<string> SelectOption(IEnumerable<SelectOptionValue> values, LocatorSelectOptionOptions options = null)
    {
        return WrappedLocator.SelectOptionAsync(values, options).Result;
    }

    public void SelectText(LocatorSelectTextOptions options = null)
    {
        WrappedLocator.SelectTextAsync(options).SyncResult();
    }

    public void SetChecked(bool checkedState, LocatorSetCheckedOptions options = null)
    {
        WrappedLocator.SetCheckedAsync(checkedState, options).SyncResult();
    }

    public void SetInputFiles(string files, LocatorSetInputFilesOptions options = null)
    {
        WrappedLocator.SetInputFilesAsync(files, options).SyncResult();
    }

    public void SetInputFiles(IEnumerable<string> files, LocatorSetInputFilesOptions options = null)
    {
        WrappedLocator.SetInputFilesAsync(files, options).SyncResult();
    }

    public void SetInputFiles(FilePayload files, LocatorSetInputFilesOptions options = null)
    {
        WrappedLocator.SetInputFilesAsync(files, options).SyncResult();
    }

    public void SetInputFiles(IEnumerable<FilePayload> files, LocatorSetInputFilesOptions options = null)
    {
        WrappedLocator.SetInputFilesAsync(files, options).SyncResult();
    }

    public void Tap(LocatorTapOptions options = null)
    {
        WrappedLocator.TapAsync(options).SyncResult();
    }

    public string TextContent(LocatorTextContentOptions options = null)
    {
        return WrappedLocator.TextContentAsync(options).Result;
    }

    [Obsolete]
    public void Type(string text, LocatorTypeOptions options = null)
    {
        WrappedLocator.TypeAsync(text, options).SyncResult();
    }

    public void Uncheck(LocatorUncheckOptions options = null)
    {
        WrappedLocator.UncheckAsync(options).SyncResult();
    }

    public void WaitFor(LocatorWaitForOptions options = null)
    {
        WrappedLocator.WaitForAsync(options).SyncResult();
    }

    public JsonElement? Evaluate(string expression, object arg = null, LocatorEvaluateOptions options = null)
    {
        return WrappedLocator.EvaluateAsync(expression, arg, options).Result;
    }


    public string GetAttribute(string name, LocatorGetAttributeOptions options = null)
    {
        return WrappedLocator.GetAttributeAsync(name, options).Result;
    }



    public virtual WebElement GetByAltText(string text, GetByAltTextOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByAltText(text, options.ConvertTo<LocatorGetByAltTextOptions>()));
    }

    public virtual WebElement GetByAltText(Regex text, GetByAltTextOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByAltText(text, options.ConvertTo<LocatorGetByAltTextOptions>()));
    }

    public virtual WebElement GetByLabel(string text, GetByLabelOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByLabel(text, options.ConvertTo<LocatorGetByLabelOptions>()));
    }

    public virtual WebElement GetByLabel(Regex text, GetByLabelOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByLabel(text, options.ConvertTo<LocatorGetByLabelOptions>()));
    }

    public virtual WebElement GetByPlaceholder(string text, GetByPlaceholderOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByPlaceholder(text, options.ConvertTo<LocatorGetByPlaceholderOptions>()));
    }

    public virtual WebElement GetByPlaceholder(Regex text, GetByPlaceholderOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByPlaceholder(text, options.ConvertTo<LocatorGetByPlaceholderOptions>()));
    }

    public virtual WebElement GetByRole(AriaRole role, GetByRoleOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByRole(role, options.ConvertTo<LocatorGetByRoleOptions>()));
    }

    public virtual WebElement GetByTestId(string testId)
    {
        return new WebElement(Page, WrappedLocator.GetByTestId(testId));
    }

    public virtual WebElement GetByTestId(Regex testId)
    {
        return new WebElement(Page, WrappedLocator.GetByTestId(testId));
    }

    public virtual WebElement GetByText(string text, GetByTextOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByText(text, options.ConvertTo<LocatorGetByTextOptions>()));
    }

    public virtual WebElement GetByText(Regex text, GetByTextOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByText(text, options.ConvertTo<LocatorGetByTextOptions>()));
    }

    public virtual WebElement GetByTitle(string text, GetByTitleOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByTitle(text, options.ConvertTo<LocatorGetByTitleOptions>()));
    }

    public virtual WebElement GetByTitle(Regex text, GetByTitleOptions options = null)
    {
        return new WebElement(Page, WrappedLocator.GetByTitle(text, options.ConvertTo<LocatorGetByTitleOptions>()));
    }

    public virtual WebElement Locate(string selectorOrElement)
    {
        return new WebElement(Page, WrappedLocator.Locator(selectorOrElement));
    }

    public virtual WebElement Locate(WebElement selectorOrElement)
    {
        return new WebElement(Page, WrappedLocator.Locator(selectorOrElement.WrappedLocator));
    }

    public virtual FrameElement LocateFrame(string selector)
    {
        return new FrameElement(Page, Locate(selector));
    }
}
