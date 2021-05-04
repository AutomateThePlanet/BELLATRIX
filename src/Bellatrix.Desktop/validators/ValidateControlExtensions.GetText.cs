﻿// <copyright file="ValidateControlExtensions.GetText.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop
{
    public static partial class ValidateControlExtensions
    {
        public static void ValidateTextIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
            where T : IComponentText, IComponent
        {
            WaitUntil(() => control.GetText() == null, $"The control's text should be null but was '{control.GetText()}'.", timeout, sleepInterval);
            ValidatedTextIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        public static void ValidateTextIs<T>(this T control, string value, int? timeout = null, int? sleepInterval = null)
            where T : IComponentText, IComponent
        {
            WaitUntil(() => control.GetText().Equals(value), $"The control's text should be '{value}' but was '{control.GetText()}'.", timeout, sleepInterval);
            ValidatedTextIsEvent?.Invoke(control, new ComponentActionEventArgs(control, value));
        }

        public static event EventHandler<ComponentActionEventArgs> ValidatedTextIsNullEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedTextIsEvent;
    }
}