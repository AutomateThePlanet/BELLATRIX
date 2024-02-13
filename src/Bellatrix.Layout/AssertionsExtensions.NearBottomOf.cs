// <copyright file="AssertionsExtensions.NearBottomOf.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedNearBottomOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedNearBottomOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedNearBottomOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedNearBottomOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedNearBottomOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedNearBottomOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedNearBottomOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedNearBottomOfApproximateEvent;

    public static void AssertNearBottomOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        double actualDistance = CalculateBelowOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ComponentName} should be below of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedNearBottomOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoComponentsNoExpectedActionEventArgs(element, secondElement));
    }

    public static void AssertNearBottomOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        double actualDistance = CalculateBelowOfDistance(element, secondElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ComponentName} should be {expected} px below of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedNearBottomOfEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertNearBottomOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ComponentName} should be between {from}-{to} px below of {secondElement.ComponentName}, but {actualDistance} px.");
        AssertedNearBottomOfBetweenEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, from, to));
    }

    public static void AssertNearBottomOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ComponentName} should be > {expected} px below of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedNearBottomOfGreaterThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertNearBottomOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ComponentName} should be >= {expected} px below of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedNearBottomOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertNearBottomOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ComponentName} should be < {expected} px below of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedNearBottomOfLessThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertNearBottomOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ComponentName} should be <= {expected} px below of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedNearBottomOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertNearBottomOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateBelowOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ComponentName} should be <= {percent}% of {expected} px below of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedNearBottomOfApproximateEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, expected, percent));
    }
}
