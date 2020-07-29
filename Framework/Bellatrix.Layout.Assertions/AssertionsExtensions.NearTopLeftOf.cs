// <copyright file="AssertionsExtensions.NearTopLeftOf.cs" company="Automate The Planet Ltd.">
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
using BA = Bellatrix.Assertions;

namespace Bellatrix.Layout
{
    public static partial class AssertionsExtensions
    {
        public static void AssertNearTopLeftOf(this ILayoutElement element, ILayoutElement secondElement)
        {
            AssertNearTopOf(element, secondElement);
            AssertNearLeftOf(secondElement, element);
        }

        public static void AssertNearTopLeftOf(this ILayoutElement element, ILayoutElement secondElement, double top, double left)
        {
            AssertNearTopOf(element, secondElement, top);
            AssertNearLeftOf(secondElement, element, left);
        }

        public static void AssertNearTopLeftOfBetween(this ILayoutElement element, ILayoutElement secondElement, double fromTop, double toTop, double fromLeft, double toLeft)
        {
            AssertNearTopOfBetween(element, secondElement, fromTop, toTop);
            AssertNearLeftOfBetween(secondElement, element, fromLeft, toLeft);
        }

        public static void AssertNearTopLeftOfGreaterThan(this ILayoutElement element, ILayoutElement secondElement, double top, double left)
        {
            AssertNearTopOfGreaterThan(element, secondElement, top);
            AssertNearLeftOfGreaterThan(secondElement, element, left);
        }

        public static void AssertNearTopLeftOfGreaterThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double top, double left)
        {
            AssertNearTopOfGreaterThanOrEqual(element, secondElement, top);
            AssertNearLeftOfGreaterThanOrEqual(secondElement, element, left);
        }

        public static void AssertNearTopLeftOfLessThan(this ILayoutElement element, ILayoutElement secondElement, double top, double left)
        {
            AssertNearTopOfLessThan(element, secondElement, top);
            AssertNearLeftOfLessThan(secondElement, element, left);
        }

        public static void AssertNearTopLeftOfLessThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double top, double left)
        {
            AssertNearTopOfLessThanOrEqual(element, secondElement, top);
            AssertNearLeftOfLessThanOrEqual(secondElement, element, left);
        }

        public static void AssertNearTopLeftOfApproximate(this ILayoutElement element, ILayoutElement secondElement, double top, double left, double percent)
        {
            AssertNearTopOfApproximate(element, secondElement, top, percent);
            AssertNearLeftOfApproximate(secondElement, element, left, percent);
        }
    }
}
