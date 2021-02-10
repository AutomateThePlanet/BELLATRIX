// <copyright file="AssertionsExtensions.NearRightOf.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedNearRightOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearRightOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearRightOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearRightOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearRightOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearRightOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedNearRightOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedNearRightOfApproximateEvent;

        public static void AssertNearRightOf(this ILayoutElement element, ILayoutElement secondElement)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ElementName} should be near right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearRightOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoElementsNoExpectedActionEventArgs(element, secondElement));
        }

        public static void AssertNearRightOf(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ElementName} should be {expected} px near right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearRightOfEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearRightOfBetween(this ILayoutElement element, ILayoutElement secondElement, double from, double to)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ElementName} should be between {from}-{to} px near right from {secondElement.ElementName}, but {actualDistance}.");
            AssertedNearRightOfBetweenEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, from, to));
        }

        public static void AssertNearRightOfGreaterThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ElementName} should be > {expected} px near right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearRightOfGreaterThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearRightOfGreaterThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ElementName} should be >= {expected} px near right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearRightOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearRightOfLessThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ElementName} should be < {expected} px near right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearRightOfLessThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearRightOfLessThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ElementName} should be <= {expected} px near right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearRightOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertNearRightOfApproximate(this ILayoutElement element, ILayoutElement secondElement, double expected, double percent)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ElementName} should be <= {percent}% of {expected} px near right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedNearRightOfApproximateEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, expected, percent));
        }
    }
}
