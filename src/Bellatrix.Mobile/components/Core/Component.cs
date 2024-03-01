// <copyright file="Component.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.CognitiveServices;
using Bellatrix.CognitiveServices.services;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Events;
using Bellatrix.Mobile.Locators;
using Bellatrix.Mobile.Services;
using Bellatrix.Mobile.Untils;
using Bellatrix.Plugins.Screenshots;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Core;

[DebuggerDisplay("BELLATRIX Element")]
public partial class Component<TDriver, TDriverElement> : IComponent<TDriverElement>
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    private readonly ComponentWaitService<TDriver, TDriverElement> _elementWait;
    private readonly List<WaitStrategy<TDriver, TDriverElement>> _untils;
    private TDriverElement _wrappedElement;

    public Component()
    {
        _elementWait = new ComponentWaitService<TDriver, TDriverElement>();
        WrappedDriver = ServicesCollection.Current.Resolve<TDriver>();
        _untils = new List<WaitStrategy<TDriver, TDriverElement>>();
    }

    public static event EventHandler<ComponentActionEventArgs<TDriverElement>> ScrollingToVisible;
    public static event EventHandler<ComponentActionEventArgs<TDriverElement>> ScrolledToVisible;
    public static event EventHandler<ComponentActionEventArgs<TDriverElement>> CreatingComponent;
    public static event EventHandler<ComponentActionEventArgs<TDriverElement>> CreatedComponent;
    public static event EventHandler<ComponentActionEventArgs<TDriverElement>> CreatingComponents;
    public static event EventHandler<ComponentActionEventArgs<TDriverElement>> CreatedComponents;
    public static event EventHandler<NativeElementActionEventArgs<TDriverElement>> ReturningWrappedElement;

    public TDriver WrappedDriver { get; }

    public TDriverElement WrappedElement
    {
        get
        {
            ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs<TDriverElement>(GetAndWaitWebDriverElement()));
            return GetAndWaitWebDriverElement(true);
        }
        internal set => _wrappedElement = value;
    }

    public TDriverElement ParentWrappedElement { get; set; }

    public TDriverElement FoundWrappedElement { get; set; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public dynamic By { get; internal set; }

    public virtual string GetAttribute(string name)
    {
        return WrappedElement.GetAttribute(name);
    }

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

    public TComponent Create<TComponent, TBy>(TBy by)
        where TBy : FindStrategy<TDriver, TDriverElement>
        where TComponent : Component<TDriver, TDriverElement>
    {
        CreatingComponent?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));

        var nativeElement = GetAndWaitWebDriverElement();
        var elementRepository = new ComponentRepository();
        var element = elementRepository.CreateComponentWithParent<TComponent, TBy, TDriver, TDriverElement>(by, nativeElement);

        CreatedComponent?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));

        return element;
    }

    public ComponentsList<TComponent, TBy, TDriver, TDriverElement> CreateAll<TComponent, TBy>(TBy by)
        where TBy : FindStrategy<TDriver, TDriverElement>
        where TComponent : Component<TDriver, TDriverElement>
    {
        CreatingComponents?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));
        TDriverElement nativeElement = null;
        try
        {
            nativeElement = GetWebDriverElement();
        }
        catch (InvalidOperationException)
        {
            // Ignore
        }

        var elementsCollection = new ComponentsList<TComponent, TBy, TDriver, TDriverElement>(by, nativeElement);

        CreatedComponents?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));

        return elementsCollection;
    }

    public void WaitToBe() => GetAndWaitWebDriverElement(true);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsPresent
    {
        get
        {
            try
            {
                var nativeElement = GetWebDriverElement(true);
                if (nativeElement != null)
                {
                    return true;
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return false;
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsVisible
    {
        get
        {
            try
            {
                var nativeElement = GetWebDriverElement(true);
                if (nativeElement != null)
                {
                    return GetWebDriverElement(true).Displayed;
                }
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return false;
        }
    }

    public virtual void ScrollToVisible(ScrollDirection direction)
    {
        ScrollingToVisible?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));
        int timeOut = 10000;

        while (!IsVisible && timeOut > 0)
        {
            MobileScroll(direction);
            timeOut -= 30;
        }

        ScrolledToVisible?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));
    }

    public string ComponentName { get; internal set; }

    public string PageName { get; internal set; }

    public virtual Point Location => WrappedElement.Location;

    public virtual Size Size => WrappedElement.Size;

    public void EnsureState(WaitStrategy<TDriver, TDriverElement> until) => _untils.Add(until);

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

    protected TDriverElement GetAndWaitWebDriverElement(bool shouldRefresh = false)
    {
        if (_wrappedElement == null || shouldRefresh)
        {
            if (_untils.Count == 0 || _untils[0] == null)
            {
                EnsureState(new WaitToExistStrategy<TDriver, TDriverElement>());
            }

            try
            {
                foreach (var until in _untils)
                {
                    if (until != null)
                    {
                        _elementWait.Wait(this, until);
                    }

                    if (until.GetType().Equals(typeof(WaitNotExistStrategy<TDriver, TDriverElement>)))
                    {
                        return _wrappedElement;
                    }
                }

                _wrappedElement = GetWebDriverElement(shouldRefresh);
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new TimeoutException($"The element with Name = {ComponentName} Locator {By.Value} was not found on the page or didn't fulfill the specified conditions.", ex);
            }
        }

        _untils.Clear();

        return _wrappedElement;
    }

    private TDriverElement GetWebDriverElement(bool shouldRefresh = false)
    {
        if (!shouldRefresh && _wrappedElement != null)
        {
            return _wrappedElement;
        }

        if (ParentWrappedElement == null && FoundWrappedElement == null)
        {
            return By.FindElement(WrappedDriver);
        }

        if (ParentWrappedElement != null)
        {
            return By.FindElement(ParentWrappedElement);
        }

        if (FoundWrappedElement != null)
        {
            return FoundWrappedElement;
        }

        return _wrappedElement;
    }
}
