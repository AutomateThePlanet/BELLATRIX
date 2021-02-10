// <copyright file="AssertionsExtensions.TopInsideOf.cs" company="Automate The Planet Ltd.">
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
using BA = Bellatrix.Assertions;

namespace Bellatrix.Layout
{
    public static partial class AssertionsExtensions
    {
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedTopInsideOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedTopInsideOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedTopInsideOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedTopInsideOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedTopInsideOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedTopInsideOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedTopInsideOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedTopInsideOfApproximateEvent;

        public static void AssertTopInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance >= 0, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} top padding but was {actualTopDistance} px.");
            AssertedTopInsideOfNoExpectedValueEvent?.Invoke(innerElement, new LayoutTwoElementsNoExpectedActionEventArgs(innerElement, outerElement));
        }

        public static void AssertTopInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement, double top)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(top, actualTopDistance, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} top padding = {top} px but was {actualTopDistance} px.");
            AssertedTopInsideOfEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, top));
        }

        public static void AssertTopInsideOfBetween(this ILayoutElement innerElement, ILayoutElement outerElement, double from, double to)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance >= from && actualTopDistance <= to, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} top padding between {from}-{to} px but was {actualTopDistance} px.");
            AssertedTopInsideOfBetweenEvent?.Invoke(innerElement, new LayoutTwoElementsActionTwoValuesEventArgs(innerElement, outerElement, from, to));
        }

        public static void AssertTopInsideOfGreaterThan(this ILayoutElement innerElement, ILayoutElement outerElement, double top)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance > top, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} top padding > {top} px but was {actualTopDistance} px.");
            AssertedTopInsideOfGreaterThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, top));
        }

        public static void AssertTopInsideOfGreaterThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double top)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance >= top, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} top padding >= {top} px but was {actualTopDistance} px.");
            AssertedTopInsideOfGreaterOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, top));
        }

        public static void AssertTopInsideOfLessThan(this ILayoutElement innerElement, ILayoutElement outerElement, double top)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance < top, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} top padding < {top} px but was {actualTopDistance} px.");
            AssertedTopInsideOfLessThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, top));
        }

        public static void AssertTopInsideOfLessThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double top)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance <= top, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} top padding <= {top} px but was {actualTopDistance} px.");
            AssertedTopInsideOfLessOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, top));
        }

        public static void AssertTopInsideOfApproximate(this ILayoutElement innerElement, ILayoutElement outerElement, double top, double percent)
        {
            double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
            var actualPercentDifference = CalculatePercentDifference(top, actualTopDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{innerElement.ElementName} should be <= {percent}% of {top} px top padding of {outerElement.ElementName} but was {actualTopDistance} px.");
            AssertedTopInsideOfApproximateEvent?.Invoke(innerElement, new LayoutTwoElementsActionTwoValuesEventArgs(innerElement, outerElement, top, percent));
        }
    }
}