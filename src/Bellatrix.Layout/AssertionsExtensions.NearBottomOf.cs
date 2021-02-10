// <copyright file="AssertionsExtensions.NearBottomOf.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedNearBottomOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearBottomOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearBottomOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearBottomOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearBottomOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearBottomOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearBottomOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearBottomOfApproximateEvent;

        public static void AssertNearBottomOf(this ILayoutElement element, ILayoutElement secondElement)
        {
            double actualDistance = CalculateBelowOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ElementName} should be below of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearBottomOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoElementsNoExpectedActionEventArgs(element, secondElement));
        }

        public static void AssertNearBottomOf(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            double actualDistance = CalculateBelowOfDistance(element, secondElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ElementName} should be {expected} px below of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearBottomOfEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearBottomOfBetween(this ILayoutElement element, ILayoutElement secondElement, double from, double to)
        {
            var actualDistance = CalculateBelowOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ElementName} should be between {from}-{to} px below of {secondElement.ElementName}, but {actualDistance} px.");
            AssertedNearBottomOfBetweenEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, from, to));
        }

        public static void AssertNearBottomOfGreaterThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateBelowOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ElementName} should be > {expected} px below of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearBottomOfGreaterThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearBottomOfGreaterThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateBelowOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ElementName} should be >= {expected} px below of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearBottomOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearBottomOfLessThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateBelowOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ElementName} should be < {expected} px below of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearBottomOfLessThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearBottomOfLessThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateBelowOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ElementName} should be <= {expected} px below of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearBottomOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearBottomOfApproximate(this ILayoutElement element, ILayoutElement secondElement, double expected, double percent)
        {
            var actualDistance = CalculateBelowOfDistance(element, secondElement);
            var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ElementName} should be <= {percent}% of {expected} px below of {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearBottomOfApproximateEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, expected, percent));
        }
    }
}
