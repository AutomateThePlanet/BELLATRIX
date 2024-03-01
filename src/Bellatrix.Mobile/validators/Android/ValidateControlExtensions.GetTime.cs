// <copyright file="ValidateControlExtensions.GetTime.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android;

public static partial class ValidateControlExtensions
{
    public static void ValidateTimeIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentTime, IComponent<AppiumElement>
    {
        ValidateControlWaitService.WaitUntil<AndroidDriver, AppiumElement>(() => control.GetTime().Equals(value), $"The control's time should be '{value}' but was '{control.GetTime()}'.", timeout, sleepInterval);
        ValidatedTimeIsEvent?.Invoke(control, new ComponentActionEventArgs<AppiumElement>(control, value));
    }

    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> ValidatedTimeIsEvent;
}