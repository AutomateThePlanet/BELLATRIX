// <copyright file="Component.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Diagnostics;
using System.Drawing;
using System.Text;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;
using Bellatrix.Playwright.Services;
using Bellatrix.Playwright.Settings;
using Bellatrix.Playwright.WaitStrategies;
using Bellatrix.Playwright.Waits;
using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings.Extensions;
using Bellatrix.CognitiveServices.services;
using Bellatrix.CognitiveServices;
using Bellatrix.Playwright.SyncPlaywright.Element;
using Bellatrix.Playwright.Components;
using Bellatrix.Playwright.Components.ShadowDom;
using Bellatrix.LLM;
using Bellatrix.LLM.Settings;


namespace Bellatrix.Playwright;

[DebuggerDisplay("BELLATRIX Component")]
public partial class Component : IComponentVisible, IComponentCssClass, IComponent, IWebLayoutComponent
{
    private readonly IViewSnapshotProvider _viewSnapshotProvider;
    private readonly LargeLanguageModelsSettings _llmSettings;

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

    public static event EventHandler<ComponentActionEventArgs> Focusing;
    public static event EventHandler<ComponentActionEventArgs> Focused;

    protected WebElement _wrappedElement;
    private readonly ComponentWaitService _elementWaiter;
    private readonly List<WaitStrategy> _untils;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public string TagName => WrappedElement.Evaluate("element => element.TagName;").GetValueOrDefault().ToString();

    public Component()
    {
        _elementWaiter = new ComponentWaitService();
        WrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
        _untils = new List<WaitStrategy>();
        JavaScriptService = ServicesCollection.Current.Resolve<JavaScriptService>();
        BrowserService = ServicesCollection.Current.Resolve<BrowserService>();
        ComponentCreateService = ServicesCollection.Current.Resolve<ComponentCreateService>();
        _viewSnapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        _llmSettings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
    }

    public WrappedBrowser WrappedBrowser { get; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public WebElement WrappedElement
    {
        get
        {
            if (_wrappedElement == null)
            {
                _wrappedElement = ResolveWithHealing();
                foreach (var until in _untils)
                {
                    until.WaitUntil(_wrappedElement);
                }
                _untils.Clear();
            }

            ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs(_wrappedElement));
            return _wrappedElement;
        }
        set => _wrappedElement = value;
    }

    public void EnsureState(WaitStrategy until)
    {
        _untils.Add(until);
    }

    private WebElement ResolveWithHealing()
    {
        try
        {
            var element = By.Resolve(WrappedBrowser.CurrentPage);

            if (_llmSettings.EnableSelfHealing)
            {
                var snapshot = _viewSnapshotProvider.GetCurrentViewSnapshot();
                LocatorSelfHealingService.SaveWorkingLocator(By.ToString(), snapshot, WrappedBrowser.CurrentPage.Url);
            }

            return element;
        }
        catch (Exception ex)
        {
            if (!_llmSettings.EnableSelfHealing)
            {
                throw new TimeoutException($"Element not found: {By?.Value}", ex);
            }

            var snapshot = _viewSnapshotProvider.GetCurrentViewSnapshot();
            var healedLocator = LocatorSelfHealingService.TryHeal(By.ToString(), snapshot, WrappedBrowser.CurrentPage.Url);

            if (!string.IsNullOrWhiteSpace(healedLocator))
            {
                try
                {
                    return new FindXpathStrategy(healedLocator).Resolve(WrappedBrowser.CurrentPage);
                }
                catch
                {
                    throw new InvalidOperationException($"Healing attempt failed: {By.Value}", ex);
                }
            }

            throw new InvalidOperationException($"Original and healed locators failed: {By.Value}", ex);
        }
    }

    public Component ParentComponent { get; set; }

    protected readonly JavaScriptService JavaScriptService;
    protected readonly BrowserService BrowserService;
    protected readonly ComponentCreateService ComponentCreateService;

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

        WrappedElement.Screenshot(new() { Path = filePath, Type = ScreenshotType.Png });

        return filePath;
    }

    public dynamic Create<TBy>(TBy by, Type newElementType)
        where TBy : FindStrategy
    {
        CreatingComponent?.Invoke(this, new ComponentActionEventArgs(this));

        dynamic element;

        if (InShadowContext)
        {
            if (this is ShadowRoot)
            {
                element = ShadowDomService.CreateFromShadowRoot(this as ShadowRoot, by, newElementType);
            }
            else
            {
                element = ShadowDomService.CreateInShadowContext(this, by, newElementType);
            }
        }
        else
        {
            element = ComponentRepository.CreateComponentWithParent(by, this, newElementType);
        }


        CreatedComponent?.Invoke(this, new ComponentActionEventArgs(this));

        return element;
    }

    public TComponent Create<TComponent, TBy>(TBy by, bool shouldCacheElement = false)
        where TBy : FindStrategy
        where TComponent : Component
    {
        CreatingComponent?.Invoke(this, new ComponentActionEventArgs(this));

        TComponent component;

        if (InShadowContext)
        {
            if (this is ShadowRoot)
            {
                component = ShadowDomService.CreateFromShadowRoot<TComponent, TBy>(this as ShadowRoot, by, shouldCacheElement);
            }
            else
            {
                component = ShadowDomService.CreateInShadowContext<TComponent, TBy>(this, by, shouldCacheElement);
            }
        } else
        {
            component = ComponentRepository.CreateComponentWithParent<TComponent>(by, this);
        }

        CreatedComponent?.Invoke(this, new ComponentActionEventArgs(this));

        return component;
    }

    public ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component
    {
        CreatingComponents?.Invoke(this, new ComponentActionEventArgs(this));

        var elementsCollection = new ComponentsList<TComponent>(by, this);

        if (InShadowContext)
        {
            if (this is ShadowRoot)
            {
                elementsCollection = ShadowDomService.CreateAllFromShadowRoot<TComponent, TBy>(this as ShadowRoot, by, false);
            }
            else
            {
                elementsCollection = ShadowDomService.CreateAllInShadowContext<TComponent, TBy>(this, by, false);
            }
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
                if (component is ShadowRoot)
                {
                    return true;
                }

                component = component.ParentComponent;
            }

            return false;
        }
    }

    /// <summary>
    /// Execute JavaScript against the web element.
    /// </summary>
    public T Evaluate<T>(string expression, object arg = null)
    {
        return WrappedElement.Evaluate<T>(expression, arg);
    }

    public void WaitToBe()
    {
        if (_untils.Count == 0 || _untils[0] == null)
        {
            _wrappedElement.WaitFor(new() { State = WaitForSelectorState.Attached, Timeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.InMilliseconds().ElementToExistTimeout });
            return;
        }


        foreach (var item in _untils)
        {
            item.WaitUntil(_wrappedElement);
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsPresent
    {
        get
        {
            try
            {
                return WrappedElement.ElementHandle(new LocatorElementHandleOptions { Timeout = ConfigurationService.GetSection<WebSettings>().TimeoutSettings.InMilliseconds().ElementToExistTimeout }) != null;
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
                return WrappedElement.IsVisible();
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

        WrappedElement.Evaluate($"element => element.setAttribute('{name}', '{value}');");

        AttributeSet?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public void Focus()
    {
        Focusing?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.Focus();

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

    public virtual TComponent As<TComponent>()
        where TComponent : Component
    {
        var component = Activator.CreateInstance<TComponent>();
        component.By = this.By;

        if (component is Frame)
        {
            component.WrappedElement = new FrameElement(WrappedBrowser.CurrentPage, this.WrappedElement);
        }
        else
        {
            component.WrappedElement = this.WrappedElement;
        }

        return component;
    }

    public ShadowRoot ShadowRoot => this.As<ShadowRoot>();

    private void ScrollToMakeElementVisible()
    {
        // By default scroll down to make the element visible.
        if (WrappedBrowserCreateService.BrowserConfiguration.ShouldAutomaticallyScrollToVisible)
        {
            // TODO get if should wait from the config file
            ScrollToVisible(false);
        }
    }
}