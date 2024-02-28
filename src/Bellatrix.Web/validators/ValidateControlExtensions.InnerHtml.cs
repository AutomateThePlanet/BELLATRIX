// <copyright file="ValidateControlExtensions.InnerHtml.cs" company="Automate The Planet Ltd.">
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
    public static void ValidateInnerHtmlIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
        where T : IComponentInnerHtml, IComponent
    {
        WaitUntil(() => control.InnerHtml.Trim().Equals(value), $"The control's inner HTML should be '{value}' but was '{control.InnerHtml}'.", timeout, sleepInterval);
        ValidatedInnerHtmlIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static void ValidateInnerHtmlContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
       where T : IComponentInnerHtml, IComponent
    {
        WaitUntil(() => control.InnerHtml.Trim().Contains(value), $"The control's inner HTML should contain '{value}' but was '{control.InnerHtml}'.", timeout, sleepInterval);
        ValidatedInnerHtmlContainsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static void ValidateInnerHtmlNotContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
      where T : IComponentInnerHtml, IComponent
    {
        WaitUntil(() => !control.InnerHtml.Trim().Contains(value), $"The control's inner HTML should not contain '{value}' but was '{control.InnerHtml}'.", timeout, sleepInterval);
        ValidatedInnerHtmlContainsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerHtmlIsEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedInnerHtmlContainsEvent;
}