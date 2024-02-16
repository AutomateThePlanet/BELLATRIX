// <copyright file="DeviceService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Exceptions;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Services;

public class DeviceService<TDriver, TComponent> : MobileService<TDriver, TComponent>
    where TDriver : AppiumDriver
    where TComponent : AppiumElement
{
    public DeviceService(TDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public DateTime DeviceTime
    {
        get
        {
            DateTime result;
            try
            {
                string deviceTime = WrappedAppiumDriver.DeviceTime;
                result = DateTime.Parse(deviceTime);
            }
            catch (FormatException e) when (e.Message.Contains("There is an unknown word starting at index '0'."))
            {
                throw new AppiumEngineException(e);
            }

            return result;
        }
    }

    public ScreenOrientation Orientation
    {
        get => WrappedAppiumDriver.Orientation;
        set
        {
            try
            {
                WrappedAppiumDriver.Orientation = value;
            }
            catch (FormatException e) when (e.Message.Contains("Unknown Orientation Type Passed in"))
            {
                throw new AppiumEngineException(e);
            }
        }
    }

    public void Rotate(ScreenOrientation screenOrientation)
    {
        try
        {
            var rotatable = (IRotatable)WrappedAppiumDriver;
            rotatable.Orientation = screenOrientation;
        }
        catch (FormatException e) when (e.Message.Contains("Unknown Orientation Type Passed in"))
        {
            throw new AppiumEngineException(e);
        }
    }
}