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
using Microsoft.VisualStudio.Services.WebApi;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Bellatrix.Playwright.SyncPlaywright;

/// <summary>
/// Wrapper for ILocator and IFrameLocator with synchronous methods.
/// </summary>
public class WebElement
{
    public WebElement(ILocator locator)
    {
        WrappedLocator = locator;
        WrappedFrameLocator = locator.FrameLocator(":scope");
    }

    public ILocator WrappedLocator { get; set; }
    public IFrameLocator WrappedFrameLocator { get; set; }

    public bool IsFrame { get; set; }

    public WebElement First => new WebElement(WrappedLocator.First);

    public WebElement Last => new WebElement(WrappedLocator.Last);

    // TODO IPage
    public IPage Page => WrappedLocator.Page;

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
            ServicesCollection.Current.Resolve<WrappedBrowser>().CurrentPage.WaitForLoadStateAsync(LoadState.NetworkIdle).SyncResult();

            nativeLocators = WrappedLocator.AllAsync().Result;
        }

        var elements = new List<WebElement>();

        foreach (var locator in nativeLocators)
        {
            elements.Add(new WebElement(locator));
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

    public WebElement And(WebElement element)
    {
        return new WebElement(WrappedLocator.And(element.WrappedLocator));
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

    public WebElement Filter(LocatorFilterOptions options = null)
    {
        return new WebElement(WrappedLocator.Filter(options));
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

    public WebElement Nth(int index)
    {
        return new WebElement(WrappedLocator.Nth(index));
    }

    public WebElement Or(WebElement element)
    {
        return new WebElement(WrappedLocator.Or(element.WrappedLocator));
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



    public WebElement GetByAltText(string text, GetByAltTextOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByAltText(text, options.ConvertTo<FrameLocatorGetByAltTextOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByAltText(text, options.ConvertTo<LocatorGetByAltTextOptions>()));
        }
    }

    public WebElement GetByAltText(Regex text, GetByAltTextOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByAltText(text, options.ConvertTo<FrameLocatorGetByAltTextOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByAltText(text, options.ConvertTo<LocatorGetByAltTextOptions>()));
        }
    }

    public WebElement GetByLabel(string text, GetByLabelOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByLabel(text, options.ConvertTo<FrameLocatorGetByLabelOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByLabel(text, options.ConvertTo<LocatorGetByLabelOptions>()));
        }
    }

    public WebElement GetByLabel(Regex text, GetByLabelOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByLabel(text, options.ConvertTo<FrameLocatorGetByLabelOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByLabel(text, options.ConvertTo<LocatorGetByLabelOptions>()));
        }
    }

    public WebElement GetByPlaceholder(string text, GetByPlaceholderOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByPlaceholder(text, options.ConvertTo<FrameLocatorGetByPlaceholderOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByPlaceholder(text, options.ConvertTo<LocatorGetByPlaceholderOptions>()));
        }
    }

    public WebElement GetByPlaceholder(Regex text, GetByPlaceholderOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByPlaceholder(text, options.ConvertTo<FrameLocatorGetByPlaceholderOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByPlaceholder(text, options.ConvertTo<LocatorGetByPlaceholderOptions>()));
        }
    }

    public WebElement GetByRole(AriaRole role, GetByRoleOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByRole(role, options.ConvertTo<FrameLocatorGetByRoleOptions>()));
        }

        else
        {
            return new WebElement(WrappedLocator.GetByRole(role, options.ConvertTo<LocatorGetByRoleOptions>()));

        }
    }

    public WebElement GetByTestId(string testId)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByTestId(testId));
        }

        else
        {
            return new WebElement(WrappedLocator.GetByTestId(testId));

        }
    }

    public WebElement GetByTestId(Regex testId)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByTestId(testId));
        }

        else
        {
            return new WebElement(WrappedLocator.GetByTestId(testId));

        }
    }

    public WebElement GetByText(string text, GetByTextOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByText(text, options.ConvertTo<FrameLocatorGetByTextOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByText(text, options.ConvertTo<LocatorGetByTextOptions>()));
        }
    }

    public WebElement GetByText(Regex text, GetByTextOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByText(text, options.ConvertTo<FrameLocatorGetByTextOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByText(text, options.ConvertTo<LocatorGetByTextOptions>()));
        }
    }

    public WebElement GetByTitle(string text, GetByTitleOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByTitle(text, options.ConvertTo<FrameLocatorGetByTitleOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByTitle(text, options.ConvertTo<LocatorGetByTitleOptions>()));
        }
    }

    public WebElement GetByTitle(Regex text, GetByTitleOptions options = null)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.GetByTitle(text, options.ConvertTo<FrameLocatorGetByTitleOptions>()));

        }

        else
        {
            return new WebElement(WrappedLocator.GetByTitle(text, options.ConvertTo<LocatorGetByTitleOptions>()));
        }
    }

    public WebElement Locate(string selectorOrElement)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.Locator(selectorOrElement));

        }

        else
        {
            return new WebElement(WrappedLocator.Locator(selectorOrElement));
        }
    }

    public WebElement Locate(WebElement selectorOrElement)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.Locator(selectorOrElement.WrappedLocator));
        }

        else
        {
            return new WebElement(WrappedLocator.Locator(selectorOrElement.WrappedLocator));
        }
    }

    public WebElement LocateFrame(string selector)
    {
        if (IsFrame)
        {
            return new WebElement(WrappedFrameLocator.Locator(selector));
        }

        else
        {
            return new WebElement(WrappedLocator.Locator(selector));
        }
    }
}
