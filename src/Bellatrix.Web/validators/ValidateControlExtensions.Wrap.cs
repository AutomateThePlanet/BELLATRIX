﻿// <copyright file="ValidateControlExtensions.Wrap.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web;

public static partial class ValidateControlExtensions
{
    public static void ValidateWrapIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentWrap, IComponent
    {
        WaitUntil(() => control.Wrap == null, $"The control's wrap should be null but was '{control.Wrap}'.", timeout, sleepInterval);
        ValidatedWrapIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateWrapIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentWrap, IComponent
    {
        WaitUntil(() => control.Wrap.Equals(value), $"The control's wrap should be '{value}' but was '{control.Wrap}'.", timeout, sleepInterval);
        ValidatedWrapIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedWrapIsNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedWrapIsEvent;
}