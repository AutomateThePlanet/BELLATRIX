// <copyright file="AssertionsExtensions.NearLeftOf.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedNearLeftOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearLeftOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearLeftOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearLeftOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearLeftOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearLeftOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearLeftOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearLeftOfApproximateEvent;

        public static void AssertNearLeftOf(this ILayoutElement element, ILayoutElement secondElement)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ElementName} should be left from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearLeftOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoElementsNoExpectedActionEventArgs(element, secondElement));
        }

        public static void AssertNearLeftOf(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ElementName} should be {expected} px left from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearLeftOfEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearLeftOfBetween(this ILayoutElement element, ILayoutElement secondElement, double from, double to)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ElementName} should be between {from}-{to} px left from {secondElement.ElementName}, but {actualDistance}.");
            AssertedNearLeftOfBetweenEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, from, to));
        }

        public static void AssertNearLeftOfGreaterThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ElementName} should be > {expected} px left from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearLeftOfGreaterThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearLeftOfGreaterThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ElementName} should be >= {expected} px left from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearLeftOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearLeftOfLessThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ElementName} should be < {expected} px left from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearLeftOfLessThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearLeftOfLessThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ElementName} should be <= {expected} px left from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearLeftOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearLeftOfApproximate(this ILayoutElement element, ILayoutElement secondElement, double expected, double percent)
        {
            var actualDistance = CalculateLeftOfDistance(element, secondElement);
            var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ElementName} should be <= {percent}% of {expected} px left from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearLeftOfApproximateEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, expected, percent));
        }
    }
}
