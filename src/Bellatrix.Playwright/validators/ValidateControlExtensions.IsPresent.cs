// <copyright file="ValidateControlExtensions.GetVisible.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright;

public static partial class ValidateControlExtensions
{
    public static void ValidateIsPresent<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponent
    {
        WaitUntil(() => control.WrappedElement.IsPresent.Equals(true), $"The control {control.ComponentName} should be present but was NOT.", timeout, sleepInterval);
        ValidatedIsPresentEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateIsNotPresent<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentVisible, IComponent
    {
        WaitUntil(() => !control.WrappedElement.IsPresent.Equals(true), $"The control {control.ComponentName} should NOT be present but it was.", timeout, sleepInterval);
        ValidatedIsNotPresentEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedIsPresentEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedIsNotPresentEvent;
}