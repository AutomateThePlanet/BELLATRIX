// <copyright file="AssertionsExtensions.TopRightInsideOf.cs" company="Automate The Planet Ltd.">
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
        public static void AssertTopRightInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement)
        {
            AssertRightInsideOf(innerElement, outerElement);
            AssertTopInsideOf(innerElement, outerElement);
        }

        public static void AssertTopRightInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement, double top, double right)
        {
            AssertRightInsideOf(innerElement, outerElement, right);
            AssertTopInsideOf(innerElement, outerElement, top);
        }

        public static void AssertTopRightInsideOfBetween(this ILayoutElement innerElement, ILayoutElement outerElement, double topFrom, double topTo, double rightFrom, double rightTo)
        {
            AssertRightInsideOfBetween(innerElement, outerElement, rightFrom, rightTo);
            AssertTopInsideOfBetween(innerElement, outerElement, topFrom, topTo);
        }

        public static void AssertTopRightInsideOfGreaterThan(this ILayoutElement innerElement, ILayoutElement outerElement, double top, double right)
        {
            AssertRightInsideOfGreaterThan(innerElement, outerElement, right);
            AssertTopInsideOfGreaterThan(innerElement, outerElement, top);
        }

        public static void AssertTopRightInsideOfGreaterThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double top, double right)
        {
            AssertRightInsideOfGreaterThanOrEqual(innerElement, outerElement, right);
            AssertTopInsideOfGreaterThanOrEqual(innerElement, outerElement, top);
        }

        public static void AssertTopRightInsideOfLessThan(this ILayoutElement innerElement, ILayoutElement outerElement, double top, double right)
        {
            AssertRightInsideOfLessThan(innerElement, outerElement, right);
            AssertTopInsideOfLessThan(innerElement, outerElement, top);
        }

        public static void AssertTopRightInsideOfLessThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double top, double right)
        {
            AssertRightInsideOfLessThanOrEqual(innerElement, outerElement, right);
            AssertTopInsideOfLessThanOrEqual(innerElement, outerElement, top);
        }

        public static void AssertTopRightInsideOfApproximate(this ILayoutElement innerElement, ILayoutElement outerElement, double top, double right, double percent)
        {
            AssertRightInsideOfApproximate(innerElement, outerElement, right, percent);
            AssertTopInsideOfApproximate(innerElement, outerElement, top, percent);
        }
    }
}