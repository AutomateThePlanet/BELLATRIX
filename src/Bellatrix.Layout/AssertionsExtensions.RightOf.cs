// <copyright file="AssertionsExtensions.RightOf.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedRightOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedRightOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedRightOfApproximateEvent;

        public static void AssertRightOf(this ILayoutElement element, ILayoutElement secondElement)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ElementName} should be right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedRightOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoElementsNoExpectedActionEventArgs(element, secondElement));
        }

        public static void AssertRightOf(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ElementName} should be {expected} px right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedRightOfEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertRightOfBetween(this ILayoutElement element, ILayoutElement secondElement, double from, double to)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ElementName} should be between {from}-{to} px right from {secondElement.ElementName}, but was {actualDistance} px.");
            AssertedRightOfBetweenEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, from, to));
        }

        public static void AssertRightOfGreaterThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ElementName} should be > {expected} px right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedRightOfGreaterThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertRightOfGreaterThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ElementName} should be >= {expected} px right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedRightOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertRightOfLessThan(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ElementName} should be < {expected} px right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedRightOfLessThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertRightOfLessThanOrEqual(this ILayoutElement element, ILayoutElement secondElement, double expected)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ElementName} should be <= {expected} px right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedRightOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoElementsActionEventArgs(element, secondElement, expected));
        }

        public static void AssertRightOfApproximate(this ILayoutElement element, ILayoutElement secondElement, double expected, double percent)
        {
            var actualDistance = CalculateRightOfDistance(element, secondElement);
            var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ElementName} should be <= {percent}% of {expected} px right from {secondElement.ElementName} but was {actualDistance} px.");
            AssertedRightOfApproximateEvent?.Invoke(element, new LayoutTwoElementsActionTwoValuesEventArgs(element, secondElement, expected, percent));
        }
    }
}
