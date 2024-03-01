// <copyright file="AssertionsExtensions.RightOf.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedRightOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedRightOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedRightOfApproximateEvent;

    public static void AssertRightOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > 0, $"{element.ComponentName} should be right from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedRightOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoComponentsNoExpectedActionEventArgs(element, secondElement));
    }

    public static void AssertRightOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ComponentName} should be {expected} px right from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedRightOfEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertRightOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ComponentName} should be between {from}-{to} px right from {secondElement.ComponentName}, but was {actualDistance} px.");
        AssertedRightOfBetweenEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, from, to));
    }

    public static void AssertRightOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ComponentName} should be > {expected} px right from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedRightOfGreaterThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertRightOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ComponentName} should be >= {expected} px right from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedRightOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertRightOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ComponentName} should be < {expected} px right from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedRightOfLessThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertRightOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ComponentName} should be <= {expected} px right from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedRightOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertRightOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateRightOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ComponentName} should be <= {percent}% of {expected} px right from {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedRightOfApproximateEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, expected, percent));
    }
}
