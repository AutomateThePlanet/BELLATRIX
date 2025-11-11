// <copyright file="ValidateControlExtensions.GetRange.cs" company="Automate The Planet Ltd.">
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
    public static void ValidateRangeIs<T>(this T control, int value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentRange, IComponent
    {
        WaitUntil(() => control.GetRange().Equals(value), $"The control's range should be '{value}' but was '{control.GetRange()}'.", timeout, sleepInterval);
        ValidatedRangeIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value.ToString()));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedRangeIsEvent;
}