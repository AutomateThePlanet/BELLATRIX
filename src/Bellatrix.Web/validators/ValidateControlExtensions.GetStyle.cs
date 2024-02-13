// <copyright file="ValidateControlExtensions.GetStyle.cs" company="Automate The Planet Ltd.">
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
    public static void ValidateStyleIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : Component
    {
        WaitUntil(() => control.GetStyle() == null, $"The control's style should be null but was '{control.GetStyle()}'.", timeout, sleepInterval);
        ValidatedStyleIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateStyleIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : Component
    {
        WaitUntil(() => control.GetStyle().Equals(value), $"The control's style should be '{value}' but was '{control.GetStyle()}'.", timeout, sleepInterval);
        ValidatedStyleIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static void ValidateStyleContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
       where T : Component
    {
        WaitUntil(() => control.GetStyle().Contains(value), $"The control's style should contains '{value}' but it is '{control.GetStyle()}'.", timeout, sleepInterval);
        ValidatedStyleContainsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static void ValidateStyleNotContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
      where T : Component
    {
        WaitUntil(() => !control.GetStyle().Contains(value), $"The control's style should not contains '{value}' but it was '{control.GetStyle()}'.", timeout, sleepInterval);
        ValidatedStyleNotContainsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedStyleIsNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedStyleIsEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedStyleContainsEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedStyleNotContainsEvent;
}