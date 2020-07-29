// <copyright file="EnsureControlExtensions.MinText.cs" company="Automate The Planet Ltd.">
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
        public static void EnsureMinTextIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
            where T : IElementMinText, IElement
        {
            WaitUntil(() => control.Min == null, $"The control's min should be null but was '{control.Min}'.", timeout, sleepInterval);
            EnsuredMinTextIsNullEvent?.Invoke(control, new ElementActionEventArgs(control));
        }

        public static void EnsureMinTextIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
            where T : IElementMinText, IElement
        {
            WaitUntil(() => control.Min.Equals(value), $"The control's min should be '{value}' but was '{control.Min}'.", timeout, sleepInterval);
            EnsuredMinTextIsEvent?.Invoke(control, new ElementActionEventArgs(control, value));
        }

        public static event EventHandler<ElementActionEventArgs> EnsuredMinTextIsNullEvent;
        public static event EventHandler<ElementActionEventArgs> EnsuredMinTextIsEvent;
    }
}