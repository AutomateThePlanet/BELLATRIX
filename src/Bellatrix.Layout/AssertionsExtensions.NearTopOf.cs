// <copyright file="AssertionsExtensions.NearTopOf.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedNearTopOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearTopOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearTopOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearTopOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearTopOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearTopOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearTopOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearTopOfApproximateEvent;

        public static void AssertNearTopOf(this ILayoutElement element, ILayoutElement secondElement)
        {
            double actualDistance = CalculateAboveOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ElementName} should be near top of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearTopOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoElementsNoExpectedActionEventArgs(element, secondElement));
        }

        public static void AssertNearTopOf(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            double actualDistance = CalculateAboveOfDistance(element, secondElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ElementName} should be {expected} px near top of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearTopOfEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearTopOfBetween(this ILayoutElement element, ILayoutElement secondElement, double from, double to)
        {
            var actualDistance = CalculateAboveOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ElementName} should be between {from}-{to} px near top of {secondElement.ElementName}, but was {actualDistance} px.");
            AssertedNearTopOfBetweenEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, from, to));
        }

        public static void AssertNearTopOfGreaterThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateAboveOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ElementName} should be > {expected} px near top of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearTopOfGreaterThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearTopOfGreaterThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateAboveOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ElementName} should be >= {expected} px near top of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearTopOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearTopOfLessThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateAboveOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ElementName} should be < {expected} px near top of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearTopOfLessThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearTopOfLessThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateAboveOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ElementName} should be <= {expected} px near top of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearTopOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearTopOfApproximate(this ILayoutElement element, ILayoutElement secondElement, double expected, double percent)
        {
            var actualDistance = CalculateAboveOfDistance(element, secondElement);
            var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ElementName} should be <= {percent}% of {expected} px near top of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearTopOfApproximateEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, expected, percent));
        }
    }
}
