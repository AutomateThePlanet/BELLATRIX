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
using Bellatrix.CognitiveServices;
using Bellatrix.CognitiveServices.services;
using Bellatrix.Desktop.Controls.Core;
using Bellatrix.Desktop.Events;
using Bellatrix.Desktop.Locators;
using Bellatrix.Desktop.Services;
using Bellatrix.Desktop.Untils;
using Bellatrix.Plugins.Screenshots;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop;

[DebuggerDisplay("BELLATRIX Element")]
public partial class Component
{
    private readonly ComponentWaitService _elementWait;
    private readonly List<WaitStrategy> _untils;
    private WindowsElement _wrappedElement;

    public Component()
    {
        _elementWait = new ComponentWaitService();
        WrappedDriver = ServicesCollection.Current.Resolve<WindowsDriver<WindowsElement>>();
        _untils = new List<WaitStrategy>();
    }

    public static event EventHandler<ComponentActionEventArgs> ScrollingToVisible;
    public static event EventHandler<ComponentActionEventArgs> ScrolledToVisible;
    public static event EventHandler<ComponentActionEventArgs> CreatingComponent;
    public static event EventHandler<ComponentActionEventArgs> CreatedComponent;
    public static event EventHandler<ComponentActionEventArgs> CreatingComponents;
    public static event EventHandler<ComponentActionEventArgs> CreatedComponents;
    public static event EventHandler<NativeElementActionEventArgs> ReturningWrappedElement;

    public WindowsDriver<WindowsElement> WrappedDriver { get; }

    public WindowsElement WrappedElement
    {
        get
        {
            ReturningWrappedElement?.Invoke(this, new NativeElementActionEventArgs(GetAndWaitWebDriverElement()));
            var element = GetWebDriverElement();
            return element;
        }
        internal set => _wrappedElement = value;
    }

    public WindowsElement ParentWrappedElement { get; set; }

    public WindowsElement FoundWrappedElement { get; set; }

    public int ElementIndex { get; set; }

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
         where TBy : FindStrategy
         where TComponent : Component
    {
        CreatingComponent?.Invoke(this, new ComponentActionEventArgs(this));

        var elementRepository = new ComponentsRepository();
        var element = elementRepository.CreateComponentWithParent<TComponent>(by, WrappedElement, null, 0);

        CreatedComponent?.Invoke(this, new ComponentActionEventArgs(this));

        return element;
    }

    public ComponentsList<TComponent> CreateAll<TComponent, TBy>(TBy by)
        where TBy : FindStrategy
        where TComponent : Component
    {
        CreatingComponents?.Invoke(this, new ComponentActionEventArgs(this));

        var elementsCollection = new ComponentsList<TComponent>(by, WrappedElement);

        CreatedComponents?.Invoke(this, new ComponentActionEventArgs(this));

        return elementsCollection;
    }

    public void WaitToBe()
    {
        GetAndWaitWebDriverElement();
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsPresent
    {
        get
        {
            try
            {
                if (WrappedElement != null)
                {
                    return true;
                }
            }
            catch (Exception)
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
                return WrappedElement.Displayed;
            }
            catch (WebDriverException)
            {
                return false;
            }
        }
    }

    public virtual void ScrollToVisible()
    {
        ScrollingToVisible?.Invoke(this, new ComponentActionEventArgs(this));

        var touchActions = new RemoteTouchScreen(WrappedDriver);
        System.Threading.Thread.Sleep(2000);
        touchActions.Scroll(WrappedElement.Coordinates, 0, 0);
        this.ToBeVisible().ToExists().WaitToBe();
        ScrolledToVisible?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public string ComponentName { get; internal set; }

    public string PageName { get; internal set; }

    public virtual Point Location => WrappedElement.Location;

    public virtual Size Size => WrappedElement.Size;

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

    protected WindowsElement GetAndWaitWebDriverElement()
    {
        if (_wrappedElement == null)
        {
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
                        _elementWait.Wait(this, until);
                    }

                    if (until.GetType().Equals(typeof(WaitNotExistStrategy)))
                    {
                        return _wrappedElement;
                    }
                }

                _wrappedElement = GetWebDriverElement();
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new TimeoutException($"The element with Name = {ComponentName} Locator {By.Value} was not found on the page or didn't fulfill the specified conditions.", ex);
            }
        }

        _untils.Clear();

        return _wrappedElement;
    }

    private WindowsElement GetWebDriverElement()
    {
        WindowsElement result = _wrappedElement;
        if (FoundWrappedElement != null)
        {
            result = FoundWrappedElement;
        }

        if (_wrappedElement != null)
        {
            result = _wrappedElement;
        }

        if (ParentWrappedElement == null && _wrappedElement == null)
        {
            result = By.FindElement(WrappedDriver);
        }

        if (ParentWrappedElement != null)
        {
            result = By.FindElement(ParentWrappedElement);
        }

        return result;
    }
}
