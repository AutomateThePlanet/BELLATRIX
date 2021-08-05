// <copyright file="ValidateControlExtensions.Height.cs" company="Automate The Planet Ltd.">
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
        public static void ValidateHeightIsNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
            where T : IComponentHeight, IComponent
        {
            WaitUntil(() => control.Height == null, $"The control's height should be null but was '{control.Height}'.", timeout, sleepInterval);
            ValidatedHeightIsNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        public static void ValidateHeightIsNotNull<T>(this T control, int? timeout = null, int? sleepInterval = null)
            where T : IComponentHeight, IComponent
        {
            WaitUntil(() => control.Height != null, "The control's height should be NOT be null but it was.", timeout, sleepInterval);
            ValidatedHeightIsNotNullEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        /// <summary>
        /// This method validate if the control's height is large than the height from before (obtained as a parameter th this method).
        /// </summary>
        /// <typeparam name="T">Generic template for control.</typeparam>
        /// <param name="control">The control to be used. </param>
        /// <param name="minHeight">The minimum height needed. </param>
        /// <param name="timeout">Timeout for sleep time. </param>
        /// <param name="sleepInterval">Polling interval. </param>
        public static void ValidatedHeightIsLargeThanMinHeight<T>(this T control, int minHeight, int? timeout = null, int? sleepInterval = null)
            where T : IComponentHeight, IComponent
        {
            WaitUntil(() => control.WrappedElement.Size.Height > minHeight, $"The control's height should be larger than '{minHeight}' but was '{control.WrappedElement.Size.Height}'.", timeout, sleepInterval);
            ValidatedHeightIsLargeThanMinHeightEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        /// <summary>
        /// This method validate if the control's height is small than the height from before (obtained as a parameter th this method).
        /// </summary>
        /// <typeparam name="T">Generic template for control.</typeparam>
        /// <param name="control">The control to be used. </param>
        /// <param name="maxHeight">The maximum height needed. </param>
        /// <param name="timeout">Timeout for sleep time. </param>
        /// <param name="sleepInterval">Polling interval. </param>
        public static void ValidatedHeightIsSmallThanMaxHeight<T>(this T control, int maxHeight, int? timeout = null, int? sleepInterval = null)
            where T : IComponentHeight, IComponent
        {
            WaitUntil(() => control.WrappedElement.Size.Height < maxHeight, $"The control's height should be smaller than '{maxHeight}' but was '{control.WrappedElement.Size.Height}'.", timeout, sleepInterval);
            ValidatedHeightIsSmallThanMaxHeightEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        /// <summary>
        /// This method validate if the control's height is equal to the height from before (obtained as a parameter th this method).
        /// </summary>
        /// <typeparam name="T">Generic template for control.</typeparam>
        /// <param name="control">The control to be used. </param>
        /// <param name="height">The height needed. </param>
        /// <param name="tolerence">The tolerance needed. </param>
        /// <param name="timeout">Timeout for sleep time. </param>
        /// <param name="sleepInterval">Polling interval. </param>
        public static void ValidatedHeightIsEqualToHeight<T>(this T control, int height, int tolerence = 0, int? timeout = null, int? sleepInterval = null)
            where T : IComponentHeight, IComponent
        {
            WaitUntil(() => Math.Abs(Convert.ToDecimal(control.WrappedElement.Size.Height - height)) <= tolerence, $"The control's width should be equal to '{height}' or between to '{height}' and '{tolerence}', but was not '{control.WrappedElement.Size.Height}'.", timeout, sleepInterval);
            ValidatedHeightIsEqualToHeightEvent?.Invoke(control, new ComponentActionEventArgs(control));
        }

        public static event EventHandler<ComponentActionEventArgs> ValidatedHeightIsNullEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedHeightIsNotNullEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedHeightIsLargeThanMinHeightEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedHeightIsSmallThanMaxHeightEvent;
        public static event EventHandler<ComponentActionEventArgs> ValidatedHeightIsEqualToHeightEvent;
    }
}