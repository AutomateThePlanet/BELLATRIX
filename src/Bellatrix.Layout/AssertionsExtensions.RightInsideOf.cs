// <copyright file="AssertionsExtensions.RightInsideOf.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<LayoutTwoElementsNoExpectedActionEventArgs> AssertedRightInsideOfNoExpectedValueEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightInsideOfEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedRightInsideOfBetweenEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightInsideOfGreaterThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightInsideOfGreaterOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightInsideOfLessThanEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedRightInsideOfLessOrEqualThanEvent;
        public static event EventHandler<LayoutTwoElementsActionTwoValuesEventArgs> AssertedRightInsideOfApproximateEvent;

        public static void AssertRightInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance >= 0, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} right padding but was {actualRightDistance} px.");
            AssertedRightInsideOfNoExpectedValueEvent?.Invoke(innerElement, new LayoutTwoElementsNoExpectedActionEventArgs(innerElement, outerElement));
        }

        public static void AssertRightInsideOf(this ILayoutElement innerElement, ILayoutElement outerElement, double right)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(right, actualRightDistance, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} right padding = {right} px but was {actualRightDistance} px.");
            AssertedRightInsideOfEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, right));
        }

        public static void AssertRightInsideOfBetween(this ILayoutElement innerElement, ILayoutElement outerElement, double from, double to)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance >= from && actualRightDistance <= to, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} right padding between {from}-{to} px but was {actualRightDistance} px.");
            AssertedRightInsideOfBetweenEvent?.Invoke(innerElement, new LayoutTwoElementsActionTwoValuesEventArgs(innerElement, outerElement, from, to));
        }

        public static void AssertRightInsideOfGreaterThan(this ILayoutElement innerElement, ILayoutElement outerElement, double right)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance > right, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} right padding > {right} px but was {actualRightDistance} px.");
            AssertedRightInsideOfGreaterThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, right));
        }

        public static void AssertRightInsideOfGreaterThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double right)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance >= right, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} right padding >= {right} px but was {actualRightDistance} px.");
            AssertedRightInsideOfGreaterOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, right));
        }

        public static void AssertRightInsideOfLessThan(this ILayoutElement innerElement, ILayoutElement outerElement, double right)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance < right, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} right padding < {right} px but was {actualRightDistance} px.");
            AssertedRightInsideOfLessThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, right));
        }

        public static void AssertRightInsideOfLessThanOrEqual(this ILayoutElement innerElement, ILayoutElement outerElement, double right)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance <= right, $"{innerElement.ElementName} should be inside of {outerElement.ElementName} right padding <= {right} px but was {actualRightDistance} px.");
            AssertedRightInsideOfLessOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoElementsActionEventArgs(innerElement, outerElement, right));
        }

        public static void AssertRightInsideOfApproximate(this ILayoutElement innerElement, ILayoutElement outerElement, double right, double percent)
        {
            double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
            var actualPercentDifference = CalculatePercentDifference(right, actualRightDistance);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{innerElement.ElementName} should be <= {percent}% of {right} px right padding of {outerElement.ElementName} but was {actualRightDistance} px.");
            AssertedRightInsideOfApproximateEvent?.Invoke(innerElement, new LayoutTwoElementsActionTwoValuesEventArgs(innerElement, outerElement, right, percent));
        }
    }
}