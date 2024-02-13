// <copyright file="ValidateControlExtensions.IsMultiple.cs" company="Automate The Planet Ltd.">
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
    public static void ValidateIsMultiple<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMultiple, IComponent
    {
        WaitUntil(() => control.IsMultiple.Equals(true), "The control should be multiple but was not.", timeout, sleepInterval);
        ValidatedIsMultipleEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static void ValidateIsNotMultiple<T>(this T control, int? timeout = null, int? sleepInterval = null)
        where T : IComponentMultiple, IComponent
    {
        WaitUntil(() => !control.IsMultiple.Equals(true), "The control should be NOT multiple but was not.", timeout, sleepInterval);
        ValidatedIsNotMultipleEvent?.Invoke(control, new ComponentActionEventArgs(control));
    }

    public static event EventHandler<ComponentActionEventArgs> ValidatedIsMultipleEvent;
    public static event EventHandler<ComponentActionEventArgs> ValidatedIsNotMultipleEvent;
}