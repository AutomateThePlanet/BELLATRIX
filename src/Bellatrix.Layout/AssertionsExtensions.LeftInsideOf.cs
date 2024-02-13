// <copyright file="AssertionsExtensions.LeftInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedLeftInsideOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftInsideOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedLeftInsideOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftInsideOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftInsideOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftInsideOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedLeftInsideOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedLeftInsideOfApproximateEvent;

    public static void AssertLeftInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualLeftDistance >= 0, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} left padding but was {actualLeftDistance} px.");
        AssertedLeftInsideOfNoExpectedValueEvent?.Invoke(innerElement, new LayoutTwoComponentsNoExpectedActionEventArgs(innerElement, outerElement));
    }

    public static void AssertLeftInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double left)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(left, actualLeftDistance, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} left padding = {left} px but was {actualLeftDistance} px.");
        AssertedLeftInsideOfEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, left));
    }

    public static void AssertLeftInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double from, double to)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualLeftDistance >= from && actualLeftDistance <= to, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} left padding between {from}-{to} px but was {actualLeftDistance} px.");
        AssertedLeftInsideOfBetweenEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, from, to));
    }

    public static void AssertLeftInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double left)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualLeftDistance > left, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} left padding > {left} px but was {actualLeftDistance} px.");
        AssertedLeftInsideOfGreaterThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, left));
    }

    public static void AssertLeftInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double left)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualLeftDistance >= left, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} left padding >= {left} px but was {actualLeftDistance} px.");
        AssertedLeftInsideOfGreaterOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, left));
    }

    public static void AssertLeftInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double left)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualLeftDistance < left, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} left padding < {left} px but was {actualLeftDistance} px.");
        AssertedLeftInsideOfLessThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, left));
    }

    public static void AssertLeftInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double left)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualLeftDistance <= left, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} left padding <= {left} px but was {actualLeftDistance} px.");
        AssertedLeftInsideOfLessOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, left));
    }

    public static void AssertLeftInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double left, double percent)
    {
        double actualLeftDistance = CalculateLeftInsideOfDistance(innerElement, outerElement);
        var actualPercentDifference = CalculatePercentDifference(left, actualLeftDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{innerElement.ComponentName} should be <= {percent}% of {left} px left padding of {outerElement.ComponentName} but was {actualLeftDistance} px.");
        AssertedLeftInsideOfApproximateEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, left, percent));
    }
}