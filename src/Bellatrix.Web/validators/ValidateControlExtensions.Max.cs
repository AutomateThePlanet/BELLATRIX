// <copyright file="ValidateControlExtensions.Max.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web;

public static partial class ValidateControlExtensions
{
    public static void ValidateMaxIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMax, IComponent
    {
        WaitUntil(() => control.Max == null, $"The control's max should be null but was '{control.Max}'.", timeout, sleepInterval);
        ValidatedMaxIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateMaxIs<T>(this T control, int value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMax, IComponent
    {
        WaitUntil(() => control.Max.Equals(value), $"The control's max should be '{value}' but was '{control.Max}'.", timeout, sleepInterval);
        ValidatedMaxIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value.ToString()));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedMaxIsNullEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedMaxIsEvent;
}