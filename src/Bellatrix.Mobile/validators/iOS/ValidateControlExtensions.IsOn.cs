// <copyright file="ValidateControlExtensions.IsOn.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS;

public static partial class ValidateControlExtensions
{
    public static void ValidateIsOn<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentOn, IComponent<AppiumElement>
    {
        ValidateControlWaitService.WaitUntil<IOSDriver, AppiumElement>(() => control.IsOn.Equals(true), "The control should be ON but was OFF.", timeout, sleepInterval);
        ValidatedIsOnEvent?.Invoke(control, new ComponentActionEventArgs<AppiumElement>(control));
    }

    public static void ValidateIsOff<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentOn, IComponent<AppiumElement>
    {
        ValidateControlWaitService.WaitUntil<IOSDriver, AppiumElement>(() => control.IsOn.Equals(false), "The control should be OFF but it was ON.", timeout, sleepInterval);
        ValidatedIsOffEvent?.Invoke(control, new ComponentActionEventArgs<AppiumElement>(control));
    }

    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> ValidatedIsOnEvent;
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> ValidatedIsOffEvent;
}