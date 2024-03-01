// <copyright file="ValidateControlExtensions.GetTitle.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;

namespace Bellatrix.Web;

public static partial class ValidateControlExtensions
{
    public static void ValidateTitleIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : Component
    {
        WaitUntil(() => control.GetTitle() == null, $"The control's title should be null but was '{control.GetTitle()}'.", timeout, sleepInterval);
        ValidatedTitleIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateTitleIsNotNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
       where T : Component
    {
        WaitUntil(() => control.GetTitle() != null, $"The control's title shouldn't be null but was.", timeout, sleepInterval);
        ValidatedTitleIsNotNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateTitleIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : Component
    {
        WaitUntil(() => control.GetTitle().Equals(value), $"The control's title should be '{value}' but was '{control.GetTitle()}'.", timeout, sleepInterval);
        ValidatedTitleIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedTitleIsNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedTitleIsNotNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedTitleIsEvent;
}