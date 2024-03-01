// <copyright file="AssertionsExtensions.LeftOf.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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

namespace Bellatrix.Layout;

public static partial class AssertionsExtensions
{
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedLeftOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedLeftOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedLeftOfApproximateEvent;

    public static void AssertLeftOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ComponentName} should be left from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedLeftOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoComponentsNoExpectedActionEventArgs(element, secondElement));
    }

    public static void AssertLeftOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ComponentName} should be {expected} px left from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedLeftOfEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertLeftOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ComponentName} should be between {from}-{to} px left from {secondElement.ComponentName}, but {actualDistance}.");
        AssertedLeftOfBetweenEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, from, to));
    }

    public static void AssertLeftOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ComponentName} should be > {expected} px left from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedLeftOfGreaterThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertLeftOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ComponentName} should be >= {expected} px left from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedLeftOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertLeftOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ComponentName} should be < {expected} px left from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedLeftOfLessThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertLeftOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ComponentName} should be <= {expected} px left from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedLeftOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertLeftOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateLeftOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ComponentName} should be <= {percent}% of {expected} px left from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedLeftOfApproximateEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, expected, percent));
    }
}
