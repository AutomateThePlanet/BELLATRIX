// <copyright file="AssertionsExtensions.TopInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedTopInsideOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedTopInsideOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedTopInsideOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedTopInsideOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedTopInsideOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedTopInsideOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedTopInsideOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedTopInsideOfApproximateEvent;

    public static void AssertTopInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance >= 0, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} top padding but was {actualTopDistance} px.");
        AssertedTopInsideOfNoExpectedValueEvent?.Invoke(innerElement, new LayoutTwoComponentsNoExpectedActionEventArgs(innerElement, outerElement));
    }

    public static void AssertTopInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(top, actualTopDistance, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} top padding = {top} px but was {actualTopDistance} px.");
        AssertedTopInsideOfEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, top));
    }

    public static void AssertTopInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double from, double to)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance >= from && actualTopDistance <= to, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} top padding between {from}-{to} px but was {actualTopDistance} px.");
        AssertedTopInsideOfBetweenEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, from, to));
    }

    public static void AssertTopInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance > top, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} top padding > {top} px but was {actualTopDistance} px.");
        AssertedTopInsideOfGreaterThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, top));
    }

    public static void AssertTopInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance >= top, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} top padding >= {top} px but was {actualTopDistance} px.");
        AssertedTopInsideOfGreaterOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, top));
    }

    public static void AssertTopInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance < top, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} top padding < {top} px but was {actualTopDistance} px.");
        AssertedTopInsideOfLessThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, top));
    }

    public static void AssertTopInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualTopDistance <= top, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} top padding <= {top} px but was {actualTopDistance} px.");
        AssertedTopInsideOfLessOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, top));
    }

    public static void AssertTopInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top, double percent)
    {
        double actualTopDistance = CalculateTopInsideOfDistance(innerElement, outerElement);
        var actualPercentDifference = CalculatePercentDifference(top, actualTopDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{innerElement.ComponentName} should be <= {percent}% of {top} px top padding of {outerElement.ComponentName} but was {actualTopDistance} px.");
        AssertedTopInsideOfApproximateEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, top, percent));
    }
}