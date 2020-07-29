// <copyright file="EnsureControlExtensions.GetText.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

namespace Bellatrix.Web
{
    public static partial class EnsureControlExtensions
    {
        public static void EnsureTextIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
            where T : IElementText, IElement
        {
            WaitUntil(() => control.GetText() == null, $"The control's text should be null but was '{control.GetText()}'.", timeout, sleepInterval);
            EnsuredTextIsNullEvent?.Invoke(control, new ElementActionEventArgs(control));
        }

        public static void EnsureTextIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
            where T : IElementText, IElement
        {
            WaitUntil(() => control.GetText().Equals(value), $"The control's text should be '{value}' but was '{control.GetText()}'.", timeout, sleepInterval);
            EnsuredTextIsEvent?.Invoke(control, new ElementActionEventArgs(control, value));
        }

        public static void EnsureTextContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
          where T : IElementText, IElement
        {
            WaitUntil(() => control.GetText().Contains(value), $"The control's text should contain '{value}' but was '{control.GetText()}'.", timeout, sleepInterval);
            EnsuredTextContainsEvent?.Invoke(control, new ElementActionEventArgs(control, value));
        }

        public static void EnsureInnerTextNotContains<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
            where T : IElementInnerText, IElement
        {
            WaitUntil(() => !control.InnerText.Contains(value), $"The control's inner text should not contain '{value}' but does: '{control.InnerText}'.", timeout, sleepInterval);
            EnsuredTextNotContainsEvent?.Invoke(control, new ElementActionEventArgs(control, value));
        }

        public static event EventHandler<ElementActionEventArgs> EnsuredTextIsNullEvent;
        public static event EventHandler<ElementActionEventArgs> EnsuredTextIsEvent;
        public static event EventHandler<ElementActionEventArgs> EnsuredTextContainsEvent;
        public static event EventHandler<ElementActionEventArgs> EnsuredTextNotContainsEvent;
    }
}