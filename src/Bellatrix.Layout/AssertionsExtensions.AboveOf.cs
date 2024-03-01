// <copyright file="AssertionsExtensions.AboveOf.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedAboveOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedAboveOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedAboveOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedAboveOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedAboveOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedAboveOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedAboveOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedAboveOfApproximateEvent;

    public static void AssertAboveOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        double actualDistance = CalculateAboveOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= -1, $"{element.ComponentName} should be above of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedAboveOfNoExpectedValueEvent?.Invoke(element, new LayoutTwoComponentsNoExpectedActionEventArgs(element, secondElement));
    }

    public static void AssertAboveOf(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        double actualDistance = CalculateAboveOfDistance(element, secondElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(expected, actualDistance, $"{element.ComponentName} should be {expected} px above of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedAboveOfEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertAboveOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double from, double to)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= from && actualDistance <= to, $"{element.ComponentName} should be between {from}-{to} px above of {secondElement.ComponentName}, but {actualDistance}.");
        AssertedAboveOfBetweenEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, from, to));
    }

    public static void AssertAboveOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance > expected, $"{element.ComponentName} should be > {expected} px above of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedAboveOfGreaterThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertAboveOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance >= expected, $"{element.ComponentName} should be >= {expected} px above of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedAboveOfGreaterOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertAboveOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance < expected, $"{element.ComponentName} should be < {expected} px above of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedAboveOfLessThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertAboveOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double expected)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualDistance <= expected, $"{element.ComponentName} should be <= {expected} px above of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedAboveOfLessOrEqualThanEvent?.Invoke(element, new LayoutTwoComponentsActionEventArgs(element, secondElement, expected));
    }

    public static void AssertAboveOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double expected, double percent)
    {
        var actualDistance = CalculateAboveOfDistance(element, secondElement);
        var actualPercentDifference = CalculatePercentDifference(expected, actualDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{element.ComponentName} should be <= {percent}% of {expected} px above of {secondElement.ComponentName} but was {actualDistance} px.");
        AssertedAboveOfApproximateEvent?.Invoke(element, new LayoutTwoComponentsActionTwoValuesEventArgs(element, secondElement, expected, percent));
    }
}
