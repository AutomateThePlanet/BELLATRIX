// <copyright file="Button.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Diagnostics;
using Bellatrix.Desktop.Configuration;
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;
using Bellatrix.Desktop.Services;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop;

public class Window : Component
{
    public static event EventHandler<ComponentActionEventArgs> Attaching;
    public static event EventHandler<ComponentActionEventArgs> Attached;

    private string _windowHandle;

    public string WindowHandle
    {
        get
        {
            if (_windowHandle != null)
            {
                return _windowHandle;
            }

            try
            {
                var windowHandleStr = WrappedElement.GetAttribute("NativeWindowHandle");
                var windowHandleInt = int.Parse(windowHandleStr);
                var windowHandleHex = "0x" + windowHandleInt.ToString("X6");

                _windowHandle = windowHandleHex;
            }
            catch
            {
                return null;
            }
            

            return _windowHandle;
        }
    }

    public virtual void Attach()
    {
        Attaching?.Invoke(this, new ComponentActionEventArgs(this));

        this.ToExists().WaitToBe();
        
        var currentAppConfiguration =
            ServicesCollection.Current.Resolve<AppInitializationInfo>("_currentAppConfiguration");
        currentAppConfiguration.WindowHandle = WindowHandle;
        var driver = WrappedWebDriverCreateService.Create(currentAppConfiguration, ServicesCollection.Current);

        WrappedDriver.Quit();
        ServicesCollection.Current.UnregisterSingleInstance<WindowsDriver<WindowsElement>>();
        ServicesCollection.Current.RegisterInstance(driver);

        Attached?.Invoke(this, new ComponentActionEventArgs(this));
    }

    public virtual void Detach()
    {
        Attaching?.Invoke(this, new ComponentActionEventArgs(this));

        var currentAppConfiguration = ServicesCollection.Current.Resolve<AppInitializationInfo>("_currentAppConfiguration");

        if (currentAppConfiguration.WindowHandle != WindowHandle)
        {
            throw new InvalidOperationException($"This window ({WindowHandle}) is not currently attached. Currently attached window: {currentAppConfiguration.WindowHandle}");
        }

        currentAppConfiguration.WindowHandle = null;
        var driver = WrappedWebDriverCreateService.Create(currentAppConfiguration, ServicesCollection.Current);

        WrappedDriver.Quit();
        ServicesCollection.Current.UnregisterSingleInstance<WindowsDriver<WindowsElement>>();
        ServicesCollection.Current.RegisterInstance(driver);

        Attached?.Invoke(this, new ComponentActionEventArgs(this));
    }
}