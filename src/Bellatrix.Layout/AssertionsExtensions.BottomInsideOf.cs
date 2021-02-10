// <copyright file="AssertionsExtensions.BottomInsideOf.cs" company="Automate The Planet Ltd.">
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
using System;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Layout
{
    public static partial class AssertionsExtensions
    {
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedBottomInsideOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedBottomInsideOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedBottomInsideOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedBottomInsideOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedBottomInsideOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedBottomInsideOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedBottomInsideOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedBottomInsideOfApproximateEvent;

        public static void AssertBottomInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance >= 0, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} bottom but was {actualBottomDistance} px.");
            AssertedBottomInsideOfNoExpectedValueEvent?.Invoke(innerElement, new LayoutTwoElementsNoExpectedActionEventArgs(innerElement, outerElement));
        }

        public static void AssertBottomInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement, double bottom)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(bottom, actualBottomDistance, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} bottom padding = {bottom} px but was {actualBottomDistance} px.");
            AssertedBottomInsideOfEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, bottom));
        }

        public static void AssertBottomInsideOfBetween(this ILayoutElement innerElement, ILayoutElement outerElement, double from, double to)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance >= from && actualBottomDistance <= to, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} bottom padding between {from}-{to} px but was {actualBottomDistance} px.");
            AssertedBottomInsideOfBetweenEvent?.Invoke(innerElement, new LayoutTwoElementsActionTwoValuesEventArgs(innerElement, outerElement, from, to));
        }

        public static void AssertBottomInsideOfGreaterThan(this ILayoutElement innerElement, ILayoutElement outerElement, double bottom)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance > bottom, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} bottom padding > {bottom} px but was {actualBottomDistance} px.");
            AssertedBottomInsideOfGreaterThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, bottom));
        }

        public static void AssertBottomInsideOfGreaterThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double bottom)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance >= bottom, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} bottom padding >= {bottom} px but was {actualBottomDistance} px.");
            AssertedBottomInsideOfGreaterOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, bottom));
        }

        public static void AssertBottomInsideOfLessThan(this ILayoutElement innerElement, ILayoutElement outerElement, double bottom)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance < bottom, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} bottom padding < {bottom} px but was {actualBottomDistance} px.");
            AssertedBottomInsideOfLessThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, bottom));
        }

        public static void AssertBottomInsideOfLessThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double bottom)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance <= bottom, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} bottom padding <= {bottom} px but was {actualBottomDistance} px.");
            AssertedBottomInsideOfLessOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, bottom));
        }

        public static void AssertBottomInsideOfApproximate(this ILayoutElement innerElement, ILayoutElement outerElement, double bottom, double percent)
        {
            double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
            var actualPercentDifference = CalculatePercentDifference(bottom, actualBottomDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{innerElement.ElementName} should be <= {percent}% of {bottom} px bottom padding of {outerElement.ElementName} but was {actualBottomDistance} px.");
            AssertedBottomInsideOfApproximateEvent?.Invoke(innerElement, new LayoutTwoElementsActionTwoValuesEventArgs(innerElement, outerElement, bottom, percent));
        }
    }
}