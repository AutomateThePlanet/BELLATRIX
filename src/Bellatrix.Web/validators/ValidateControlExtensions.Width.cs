// <copyright file="ValidateControlExtensions.Width.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web
{
    public static partial class ValidateControlExtensions
    {
        public static void ValidateWidthIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
            where T : IComponentWidth, IComponent
        {
            WaitUntil(() => control.Width == null, $"The control's width should be null but was '{control.Width}'.", timeout, sleepInterval);
            ValidatedWidthIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        public static void ValidateWidthIsNotNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
            where T : IComponentWidth, IComponent
        {
            WaitUntil(() => control.Width != null, "The control's width should be NOT be null but it was.", timeout, sleepInterval);
            ValidatedWidthIsNotNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        /// <summary>
        /// This method validate if the control's width is large than the width from before (obtained as a parameter th this method).
        /// </summary>
        /// <typeparam name="T">Generic template for control.</typeparam>
        /// <param name="control">The control to be used. </param>
        /// <param name="minWidth">The minimum width needed. </param>
        /// <param name="timeout">Timeout for sleep time. </param>
        /// <param name="sleepInterval">Polling interval. </param>
        public static void ValidatedWidthIsLargeThanMinWidth<T>(this T control, int minWidth, int? timeout = null, int? sleepInterval = null)
    where T : IComponentWidth, IComponent
        {
            WaitUntil(() => control.WrappedElement.Size.Width > minWidth, $"The control's width should be larger than '{minWidth}', but was '{control.WrappedElement.Size.Width}'.", timeout, sleepInterval);
            ValidatedWidthIsLargeThanMinWidthEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        /// <summary>
        /// This method validate if the control's width is small than the width from before (obtained as a parameter th this method).
        /// </summary>
        /// <typeparam name="T">Generic template for control.</typeparam>
        /// <param name="control">The control to be used. </param>
        /// <param name="maxWidth">The maximum width needed. </param>
        /// <param name="timeout">Timeout for sleep time. </param>
        /// <param name="sleepInterval">Polling interval. </param>
        public static void ValidatedWidthIsSmallThanMaxWidth<T>(this T control, int maxWidth, int? timeout = null, int? sleepInterval = null)
            where T : IComponentWidth, IComponent
        {
            WaitUntil(() => control.WrappedElement.Size.Width < maxWidth, $"The control's width should be smaller than '{maxWidth}', but was '{control.WrappedElement.Size.Width}'.", timeout, sleepInterval);
            ValidatedWidthIsSmallThanMaxWidthEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        /// <summary>
        /// This method validate if the control's width is equal to the width from before (obtained as a parameter th this method).
        /// </summary>
        /// <typeparam name="T">Generic template for control.</typeparam>
        /// <param name="control">The control to be used. </param>
        /// <param name="width">The width needed. </param>
        /// <param name="tolerence">The tolerance needed. </param>
        /// <param name="timeout">Timeout for sleep time. </param>
        /// <param name="sleepInterval">Polling interval. </param>
        public static void ValidatedWidthIsEqualToMaxWidth<T>(this T control, int width, int tolerence = 0, int? timeout = null, int? sleepInterval = null)
            where T : IComponentWidth, IComponent
        {
            WaitUntil(() => Math.Abs(Convert.ToDecimal(control.WrappedElement.Size.Width - width)) <= tolerence, $"The control's width should be equal to '{width}' orbetween to '{width}' and '{tolerence}', but was not '{control.WrappedElement.Size.Width}'.", timeout, sleepInterval);
            ValidatedWidthIsEqualToWidthEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        public static event EventHandler<ComponentActionEventArgs> ValidatedWidthIsNullEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedWidthIsNotNullEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedWidthIsLargeThanMinWidthEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedWidthIsSmallThanMaxWidthEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedWidthIsEqualToWidthEvent;
    }
}