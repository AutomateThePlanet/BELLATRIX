// <copyright file="AndroidDeviceService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Services.Android;

public class AndroidDeviceService : DeviceService<AndroidDriver, AppiumElement>
{
    public AndroidDeviceService(AndroidDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public bool IsLocked
    {
        get
        {
            bool result;
            try
            {
                result = WrappedAppiumDriver.IsLocked();
            }
            catch (InvalidCastException ex) when (ex.Message.Contains("Unable to cast object of type 'System.String' to type 'System.Boolean"))
            {
                throw new AppiumEngineException(ex);
            }

            return result;
        }
    }

    public ConnectionType ConnectionType
    {
        get => WrappedAppiumDriver.ConnectionType;
        set
        {
            try
            {
                WrappedAppiumDriver.ConnectionType = value;
            }
            catch (WebDriverException ex) when (ex.Message.Contains("An unknown server-side error occurred while processing the command"))
            {
                throw new AppiumEngineException(ex);
            }
        }
    }

    public Dictionary<string, object> Settings { get => WrappedAppiumDriver.Settings; set => WrappedAppiumDriver.Settings = value; }
    public void Lock() => WrappedAppiumDriver.Lock();
    public void Unlock() => WrappedAppiumDriver.Lock();
    public void TurnOnLocationService() => WrappedAppiumDriver.ToggleLocationServices();
    public void OpenNotifications() => WrappedAppiumDriver.OpenNotifications();
    public void SetSetting(string setting, object value) => WrappedAppiumDriver.SetSetting(setting, value);
}
