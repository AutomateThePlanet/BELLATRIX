// <copyright file="Element.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using Azure.AI.FormRecognizer.Models;
using Bellatrix.CognitiveServices;
using Bellatrix.CognitiveServices.services;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;
using Bellatrix.Web.Untils;
using Bellatrix.Web.Waits;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Web;

[DebuggerDisplay("BELLATRIX Element")]
public partial class Component : IComponentVisible, IComponentCssClass, IComponent, IWebLayoutComponent
{
    public static event EventHandler<ComponentActionEventArgs> Focusing;
    public static event EventHandler<ComponentActionEventArgs> Focused;

    private readonly ComponentWaitService _elementWaiter;
    private readonly List<WaitStrategy> _untils;
    private IWebElement _wrappedElement;

    public string TagName => WrappedElement.TagName;

    public Component()
    {
        _elementWaiter = new ComponentWaitService();
        WrappedDriver = ServicesCollection.Current.Resolve<IWebDriver>();
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

    public IWebDriver WrappedDriver { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public IWebElement WrappedElement
    {
        get
        {
            var element = GetAndWaitWebDriverElement(ShouldCacheElement);
            ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs(element));
            if (element == null)
            {
                throw new NullReferenceException($"The element with locator {By.Value} was not found. Probably you should change the locator or wait for a specific condition.");
            }

            return element;
        }
        set => _wrappedElement = value;
    }

    public IWebElement ParentWrappedElement { get; set; }
    public int ElementIndex { get; set; }
    internal bool ShouldCacheElement { get; set; }

    protected readonly JavaScriptService JavaScriptService;
    protected readonly BrowserService BrowserService;
    protected readonly ComponentCreateService ComponentCreateService;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public dynamic By { get; internal set; }

    public string GetTitle() => string.IsNullOrEmpty(GetAttribute("title")) ? null : GetAttribute("title");

    public string GetTabIndex() => string.IsNullOrEmpty(GetAttribute("tabindex")) ? null : GetAttribute("tabindex");

    public string GetAccessKey() => string.IsNullOrEmpty(GetAttribute("accesskey")) ? null : GetAttribute("accesskey");

    public string GetStyle() => string.IsNullOrEmpty(GetAttribute("style")) ? null : GetAttribute("style");

    public string GetDir() => string.IsNullOrEmpty(GetAttribute("dir")) ? null : GetAttribute("dir");

    public string GetLang() => string.IsNullOrEmpty(GetAttribute("lang")) ? null : GetAttribute("lang");

    public AssertedFormPage AIAnalyze()
    {
        string currentComponentScreenshot = TakeScreenshot();
        var formRecognizer = ServicesCollection.Current.Resolve<FormRecognizer>();
        var analyzedComponent = formRecognizer.Analyze(currentComponentScreenshot);
        return analyzedComponent;
    }

    public string TakeScreenshot(string filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            var screenshotOutputProvider = new ScreenshotOutputProvider();
            var screenshotSaveDir = screenshotOutputProvider.GetOutputFolder();
            var screenshotFileName = screenshotOutputProvider.GetUniqueFileName(ComponentName);
            filePath = Path.Combine(screenshotSaveDir, screenshotFileName);
        }

        var screenshotDriver = WrappedDriver as ITakesScreenshot;
        Screenshot screenshot = screenshotDriver.GetScreenshot();
        var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));
        var cropArea = new Rectangle(WrappedElement.Location, WrappedElement.Size);
        var bitmap = bmpScreen.Clone(cropArea, bmpScreen.PixelFormat);
        bitmap.Save(filePath);

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

    public void WaitToBe() => GetAndWaitWebDriverElement(true);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsPresent
    {
        get
        {
            try
            {
                return GetAndWaitWebDriverElement(false) != null;
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
                return WrappedElement.Displayed;
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
        return WrappedElement.GetAttribute(name);
    }

    public void ScrollToVisible()
    {
        ScrollToVisible(true);
    }

    public void SetAttribute(string name, string value)
    {
        SettingAttribute?.Invoke(this, new ComponentActionEventArgs(this));

        JavaScriptService.Execute(
            $"arguments[0].setAttribute('{name}', '{value}');", WrappedElement);

        AttributeSet?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public void Focus()
    {
        Focusing?.Invoke(this, new ComponentActionEventArgs(this));

        JavaScriptService.Execute("window.focus();");
        JavaScriptService.Execute("arguments[0].focus();", WrappedElement);

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

    private IWebElement GetAndWaitWebDriverElement(bool shouldCacheElement = false)
    {
        if (_wrappedElement != null && shouldCacheElement)
        {
            return _wrappedElement;
        }

        if (_untils.Count == 0 || _untils[0] == null)
        {
            EnsureState(Wait.To.Exists());
        }

        try
        {
            foreach (var until in _untils)
            {
                if (until != null)
                {
                    _elementWaiter.Wait(this, until);
                }

                if (until != null && until.GetType() == typeof(WaitNotToExistStrategy))
                {
                    return _wrappedElement;
                }
            }

            _wrappedElement = GetWebDriverElement(shouldCacheElement);

            ScrollToMakeElementVisible();
            if (ConfigurationService.GetSection<WebSettings>().ShouldWaitUntilReadyOnElementFound)
            {
                BrowserService.WaitUntilReady();
            }

            if (ConfigurationService.GetSection<WebSettings>().ShouldWaitForAngular)
            {
                BrowserService.WaitForAngular();
            }

            _untils.Clear();
        }
        catch (WebDriverTimeoutException)
        {
            throw new TimeoutException($"\n\nThe element: \n Name: '{ComponentName}', \n Locator: '{LocatorType.Name} = {LocatorValue}', \n Type: '{ComponentType.Name}' \nWas not found on the page or didn't fulfill the specified conditions.\n\n");
        }

        return _wrappedElement;
    }

    private void ScrollToMakeElementVisible()
    {
        // By default scroll down to make the element visible.
        if (WrappedWebDriverCreateService.BrowserConfiguration.ShouldAutomaticallyScrollToVisible)
        {
            ScrollToVisible(false);
        }
    }

    private IWebElement GetWebDriverElement(bool shouldCacheElement = false)
    {
        if (_wrappedElement != null && shouldCacheElement)
        {
            return _wrappedElement;
        }

        if (ParentWrappedElement == null && _wrappedElement == null)
        {
            var nativeElementFinderService = new NativeElementFinderService(WrappedDriver);
            return nativeElementFinderService.FindAll(By)[ElementIndex];
        }

        if (ParentWrappedElement != null)
        {
            var nativeElementFinderService = new NativeElementFinderService(ParentWrappedElement);
            return nativeElementFinderService.FindAll(By)[ElementIndex];
        }

        return _wrappedElement;
    }
}