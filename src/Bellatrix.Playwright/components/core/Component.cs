// <copyright file="Element.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Settings;
using Bellatrix.Playwright.Untils;
using Bellatrix.Playwright.Waits;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace Bellatrix.Playwright;

[DebuggerDisplay("BELLATRIX Element")]
public partial class Component : IComponentVisible, IComponentCssClass, IComponent, IWebLayoutComponent
{
    public static event EventHandler<ComponentActionEventArgs> Focusing;
    public static event EventHandler<ComponentActionEventArgs> Focused;

    private ILocator _wrappedElement;
    private readonly ComponentWaitService _elementWaiter;
    private readonly List<WaitStrategy> _untils;

    //public string TagName => WrappedElement.TagName;

    public Component()
    {
        _elementWaiter = new ComponentWaitService();
        WrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
        _untils = new List<WaitStrategy>();
        JavaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
        BrowserService = ServicesCollection.Current.Resolve<BrowserService>();
        ComponentCreateService = ServicesCollection.Current.Resolve<ComponentCreateService>();
    }

    // ReSharper disable All
#pragma warning disable 67
    public static event EventHandler<ComponentActionEventArgs> ScrollingToVisible;
    public static event EventHandler<ComponentActionEventArgs> ScrolledToVisible;
    public static event EventHandler<ComponentActionEventArgs> SettingAttribute;
    public static event EventHandler<ComponentActionEventArgs> AttributeSet;
#pragma warning restore 67

    // ReSharper restore All
    public static event EventHandler<ComponentActionEventArgs> CreatingComponent;
    public static event EventHandler<ComponentActionEventArgs> CreatedComponent;
    public static event EventHandler<ComponentActionEventArgs> CreatingComponents;
    public static event EventHandler<ComponentActionEventArgs> CreatedComponents;
    public static event EventHandler<NativeElementActionEventArgs> ReturningWrappedElement;

    public WrappedBrowser WrappedBrowser { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public ILocator WrappedElement
    {
        get
        {
            var element = GetAndWaitWebElement(ShouldCacheElement);
            ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs(element));
            if (element == null)
            {
                throw new NullReferenceException($"The element with locator {By.Value} was not found. Probably you should change the locator or wait for a specific condition.");
            }

            return element;
        }
        set => _wrappedElement = value;
    }

    public ILocator ParentWrappedElement { get; set; }
    public int ElementIndex { get; set; }
    internal bool ShouldCacheElement { get; set; }

    protected readonly JavaScriptService JavaScriptService;
    protected readonly BrowserService BrowserService;
    protected readonly ComponentCreateService ComponentCreateService;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public FindStrategy By { get; internal set; }

    public string GetTitle() => string.IsNullOrEmpty(GetAttribute("title")) ? null : GetAttribute("title");

    public string GetTabIndex() => string.IsNullOrEmpty(GetAttribute("tabindex")) ? null : GetAttribute("tabindex");

    public string GetAccessKey() => string.IsNullOrEmpty(GetAttribute("accesskey")) ? null : GetAttribute("accesskey");

    public string GetStyle() => string.IsNullOrEmpty(GetAttribute("style")) ? null : GetAttribute("style");

    public string GetDir() => string.IsNullOrEmpty(GetAttribute("dir")) ? null : GetAttribute("dir");

    public string GetLang() => string.IsNullOrEmpty(GetAttribute("lang")) ? null : GetAttribute("lang");

    //public AssertedFormPage AIAnalyze()
    //{
    //    string currentComponentScreenshot = TakeScreenshot();
    //    var formRecognizer = ServicesCollection.Current.Resolve<FormRecognizer>();
    //    var analyzedComponent = formRecognizer.Analyze(currentComponentScreenshot);
    //    return analyzedComponent;
    //}

    public string TakeScreenshot(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            var screenshotOutputProvider = new ScreenshotOutputProvider();
            var screenshotSaveDir = screenshotOutputProvider.GetOutputFolder();
            var screenshotFileName = screenshotOutputProvider.GetUniqueFileName(ComponentName);
            filePath = Path.Combine(screenshotSaveDir, screenshotFileName);
        }

        _ = WrappedElement.ScreenshotAsync(new() { Path = filePath, Type = ScreenshotType.Png }).Result;

        return filePath;
    }

    public dynamic Create<TBy>(TBy by, Type newElementType)
        where TBy : FindStrategy
    {
        CreatingComponent?.Invoke(this, new ComponentActionEventArgs(this));

        var elementRepository = new ComponentRepository();
        var element = elementRepository.CreateComponentWithParent(by, WrappedElement, newElementType, ShouldCacheElement);

        CreatedComponent?.Invoke(this, new ComponentActionEventArgs(this));

        return element;
    }

    public TComponent Create<TComponent, TBy>(TBy by, bool shouldCacheElement = false)
        where TBy : FindStrategy
        where TComponent : Component
    {
        CreatingComponent?.Invoke(this, new ComponentActionEventArgs(this));

        var elementRepository = new ComponentRepository();
        var element = elementRepository.CreateComponentWithParent<TComponent>(by, WrappedElement, null, 0, shouldCacheElement);

        CreatedComponent?.Invoke(this, new ComponentActionEventArgs(this));

        return element;
    }

    public ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component
    {
        CreatingComponents?.Invoke(this, new ComponentActionEventArgs(this));

        var elementsCollection = new ComponentsList<TComponent>(by, WrappedElement, ShouldCacheElement);

        CreatedComponents?.Invoke(this, new ComponentActionEventArgs(this));

        return elementsCollection;
    }

    public void WaitToBe() => GetAndWaitWebElement(true);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsPresent
    {
        get
        {
            try
            {
                return GetAndWaitWebElement(false).ElementHandleAsync(new LocatorElementHandleOptions { Timeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.InMilliseconds().ElementToExistTimeout }).Result != null;
            }
            catch
            {
                return false;
            }
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsVisible
    {
        get
        {
            if (IsPresent)
            {
                return WrappedElement.IsVisibleAsync().Result;
            }

            return false;
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string CssClass
    {
        get
        {
            return GetClassAttribute();
        }
    }

    public string GetAttribute(string name)
    {
        return WrappedElement.GetAttributeAsync(name).Result;
    }

    public void ScrollToVisible()
    {
        ScrollToVisible(true);
    }

    public void SetAttribute(string name, string value)
    {
        SettingAttribute?.Invoke(this, new ComponentActionEventArgs(this));

        _ = WrappedElement.EvaluateAsync($"element => element.setAttribute('{name}', '{value}');").Result;

        AttributeSet?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public void Focus()
    {
        Focusing?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.FocusAsync().GetAwaiter().GetResult();

        Focused?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public string ComponentName { get; internal set; }

    public string PageName { get; internal set; }

    public virtual Type ComponentType => GetType();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public Type LocatorType => By.GetType();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string LocatorValue => By.Value;

    public void Hide()
    {
        SetAttribute("style", "display:none");
    }

    public void EnsureState(WaitStrategy until)
    {
        _untils.Add(until);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{ComponentName}");
        sb.AppendLine($"X = {Location.X}");
        sb.AppendLine($"Y = {Location.Y}");
        sb.AppendLine($"Height = {Size.Height}");
        sb.AppendLine($"Width = {Size.Width}");
        return sb.ToString();
    }

    private ILocator GetAndWaitWebElement(bool shouldCacheElement = false)
    {
        if (_wrappedElement != null && shouldCacheElement)
        {
            return _wrappedElement;
        }

        try
        {
            BrowserService.WaitForLoadState(LoadState.DOMContentLoaded);


            if (ConfigurationService.GetSection<WebSettings>().ShouldWaitForAngular)
            {
                BrowserService.WaitForAngular();
            }

            _wrappedElement = GetWebElement(shouldCacheElement);

            ScrollToMakeElementVisible();
        }
        catch (Exception e)
        {
            throw new TimeoutException($"\n\nThe element: \n Name: '{ComponentName}', \n Locator: '{LocatorType.Name} = {LocatorValue}', \n Type: '{ComponentType.Name}' \nWas not found on the page or didn't fulfill the specified conditions.\n\n {Environment.NewLine} {e}");
        }

        return _wrappedElement;
    }

    private void ScrollToMakeElementVisible()
    {
        // By default scroll down to make the element visible.
        if (WrappedBrowserCreateService.BrowserConfiguration.ShouldAutomaticallyScrollToVisible)
        {
            ScrollToVisible(false);
        }
    }

    private ILocator GetWebElement(bool shouldCacheElement = false)
    {
        if (_wrappedElement != null && shouldCacheElement)
        {
            return _wrappedElement;
        }

        if (ParentWrappedElement == null && _wrappedElement == null)
        {
            var nativeElementFinderService = new NativeElementFinderService(WrappedBrowser);
            return nativeElementFinderService.Find(By);
        }

        if (ParentWrappedElement != null)
        {
            var nativeElementFinderService = new NativeElementFinderService(ParentWrappedElement);
            return nativeElementFinderService.Find(By);
        }

        return _wrappedElement;
    }
}