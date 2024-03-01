// <copyright file="ValidateControlExtensions.InnerText.cs" company="Automate The Planet Ltd.">
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
    public static void ValidateInnerTextIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => control.InnerText.Equals(value), $"The control's inner text should be '{value}' but was '{control.InnerText}'.", timeout, sleepInterval);
        ValidatedInnerTextIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static void ValidateInnerTextIsNot<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
    where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => !control.InnerText.Equals(value), $"The control's inner text should not be '{value}' but was '{control.InnerText}'.", timeout, sleepInterval);
        ValidatedInnerTextIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static void ValidateInnerTextContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerText, IComponent
    {
        WaitUntil(() => control.InnerText.Contains(value), $"The control's inner text should contain '{value}' but was '{control.InnerText}'.", timeout, sleepInterval);
        ValidatedInnerTextContainsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextIsEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerTextContainsEvent;
}