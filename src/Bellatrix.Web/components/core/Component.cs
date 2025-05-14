﻿// <copyright file="Element.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using System.Text;
using Bellatrix.CognitiveServices;
using Bellatrix.CognitiveServices.services;
using Bellatrix.LLM.settings;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Web.Components.ShadowDom;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;
using Bellatrix.Web.LLM.Extensions;
using Bellatrix.Web.LLM.services;
using Bellatrix.Web.Untils;
using Bellatrix.Web.Waits;
using OpenQA.Selenium;

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
    public virtual IWebElement WrappedElement
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

    public Component ParentComponent { get; set; }
    public ISearchContext ParentWrappedElement { get; set; }
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

        dynamic component;

        if (InShadowContext)
        {
            if (GetType() == typeof(Components.ShadowRoot))
            {
                component = ShadowDomService.CreateFromShadowRoot(this as Components.ShadowRoot, by, newElementType);
            }
            else
            {
                component = ShadowDomService.CreateInShadowContext(this, by, newElementType);
            }
        }
        else
        {
            component = new ComponentRepository().CreateComponentWithParent(by, this, newElementType, false);
        }

        CreatedComponent?.Invoke(this, new ComponentActionEventArgs(this));

        return component;
    }

    public TComponent Create<TComponent, TBy>(TBy by, bool shouldCacheElement = false)
        where TBy : FindStrategy
        where TComponent : Component
    {
        CreatingComponent?.Invoke(this, new ComponentActionEventArgs(this));

        TComponent component;

        if (InShadowContext)
        {
            if (GetType() == typeof(Components.ShadowRoot))
            {
                component = ShadowDomService.CreateFromShadowRoot<TComponent, TBy>(this as Components.ShadowRoot, by);
            }
            else
            {
                component = ShadowDomService.CreateInShadowContext<TComponent, TBy>(this, by);
            }
        }
        else
        {
            component = new ComponentRepository().CreateComponentWithParent<TComponent>(by, this, null, 0, shouldCacheElement);
        }

        CreatedComponent?.Invoke(this, new ComponentActionEventArgs(this));

        return component;
    }

    public ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component
    {
        CreatingComponents?.Invoke(this, new ComponentActionEventArgs(this));

        var elementRepository = new ComponentRepository();

        ComponentsList<TComponent> elementsCollection;

        if (InShadowContext)
        {
            if (GetType() == typeof(Components.ShadowRoot))
            {
                elementsCollection = ShadowDomService.CreateAllFromShadowRoot<TComponent, TBy>(this as Components.ShadowRoot, by, false);
            }
            else
            {
                elementsCollection = ShadowDomService.CreateAllInShadowContext<TComponent, TBy>(this, by, false);
            }
        }
        else
        {
            elementsCollection = new ComponentsList<TComponent>(by, WrappedElement, ShouldCacheElement);
        }

        CreatedComponents?.Invoke(this, new ComponentActionEventArgs(this));

        return elementsCollection;
    }

    private bool InShadowContext
    {
        get
        {
            var component = this;

            while (component != null)
            {
                if (component.GetType() == typeof(Components.ShadowRoot))
                {
                    return true;
                }

                component = component.ParentComponent;
            }

            return false;
        }
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

        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
        var nativeElementFinderService = ParentWrappedElement == null
            ? new NativeElementFinderService(WrappedDriver)
            : new NativeElementFinderService(ParentWrappedElement);

        try
        {
            foreach (var until in _untils)
            {
                if (until != null)
                {
                    _elementWaiter.Wait(this, until);

                    if (until.GetType() == typeof(WaitNotToExistStrategy))
                    {
                        return _wrappedElement;
                    }
                }
            }

            _wrappedElement = nativeElementFinderService.FindAll(By).ElementAt(ElementIndex);

            if (settings.EnableSelfHealing)
            {
                var summary = BrowserService.GetPageSummaryJson();
                LocatorSelfHealingService.SaveWorkingLocator(By.ToString(), summary, WrappedDriver.Url);
            }

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
            return _wrappedElement;
        }
        catch (Exception ex)
        {
            if (!settings.EnableSelfHealing)
            {
                throw new TimeoutException($"\n\nThe element: \n Name: '{ComponentName}', \n Locator: '{LocatorType.Name} = {LocatorValue}', \n Type: '{ComponentType.Name}' \nWas not found or failed condition.\n\n", ex);
            }

            Console.WriteLine($"⚠️ Element not found with locator: {By}. Attempting self-heal...");

            var currentSummary = BrowserService.GetPageSummaryJson();
            var healedStrategy = LocatorSelfHealingService.TryHeal(By.ToString(), currentSummary, WrappedDriver.Url);
            if (healedStrategy != null)
            {
                try
                {
                    var healedElement = nativeElementFinderService.FindAll(healedStrategy).ElementAt(ElementIndex);
                    Console.WriteLine("🧠 Using AI-suggested fallback locator. Original not updated.");
                    return healedElement;
                }
                catch
                {
                    throw new NotFoundException($"❌ Healing attempt failed: {By.Value}", ex);
                }
            }

            throw new NotFoundException($"❌ Original and healed locators failed: {By.Value}", ex);
        }
    }

    private void ScrollToMakeElementVisible()
    {
        // By default scroll down to make the element visible.
        if (WrappedWebDriverCreateService.BrowserConfiguration.ShouldAutomaticallyScrollToVisible)
        {
            ScrollToVisible(false);
        }
    }
}