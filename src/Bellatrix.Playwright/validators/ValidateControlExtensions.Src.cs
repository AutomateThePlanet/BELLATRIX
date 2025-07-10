﻿// <copyright file="ValidateControlExtensions.Src.cs" company="Automate The Planet Ltd.">
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
    public static void ValidateSrcIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentSrc, IComponent
    {
        WaitUntil(() => control.Src == null, $"The control's src should be null but was '{control.Src}'.", timeout, sleepInterval);
        ValidatedSrcIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateSrcIsNotNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
       where T : IComponentSrc, IComponent
    {
        WaitUntil(() => control.Src != null, $"The control's src shouldn't be null but was.", timeout, sleepInterval);
        ValidatedSrcIsNotNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateSrcIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentSrc, IComponent
    {
        WaitUntil(() => control.Src.Equals(value), $"The control's src should be '{value}' but was '{control.Src}'.", timeout, sleepInterval);
        ValidatedSrcIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedSrcIsNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedSrcIsNotNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedSrcIsEvent;
}