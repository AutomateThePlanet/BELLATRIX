// <copyright file="AssertionsExtensions.RightInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedRightInsideOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightInsideOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedRightInsideOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightInsideOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightInsideOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightInsideOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedRightInsideOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedRightInsideOfApproximateEvent;

    public static void AssertRightInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance >= 0, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} right padding but was {actualRightDistance} px.");
        AssertedRightInsideOfNoExpectedValueEvent?.Invoke(innerElement, new LayoutTwoComponentsNoExpectedActionEventArgs(innerElement, outerElement));
    }

    public static void AssertRightInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double right)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(right, actualRightDistance, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} right padding = {right} px but was {actualRightDistance} px.");
        AssertedRightInsideOfEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, right));
    }

    public static void AssertRightInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double from, double to)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance >= from && actualRightDistance <= to, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} right padding between {from}-{to} px but was {actualRightDistance} px.");
        AssertedRightInsideOfBetweenEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, from, to));
    }

    public static void AssertRightInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double right)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance > right, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} right padding > {right} px but was {actualRightDistance} px.");
        AssertedRightInsideOfGreaterThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, right));
    }

    public static void AssertRightInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double right)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance >= right, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} right padding >= {right} px but was {actualRightDistance} px.");
        AssertedRightInsideOfGreaterOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, right));
    }

    public static void AssertRightInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double right)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance < right, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} right padding < {right} px but was {actualRightDistance} px.");
        AssertedRightInsideOfLessThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, right));
    }

    public static void AssertRightInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double right)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualRightDistance <= right, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} right padding <= {right} px but was {actualRightDistance} px.");
        AssertedRightInsideOfLessOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, right));
    }

    public static void AssertRightInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double right, double percent)
    {
        double actualRightDistance = CalculateRightInsideOfDistance(innerElement, outerElement);
        var actualPercentDifference = CalculatePercentDifference(right, actualRightDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{innerElement.ComponentName} should be <= {percent}% of {right} px right padding of {outerElement.ComponentName} but was {actualRightDistance} px.");
        AssertedRightInsideOfApproximateEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, right, percent));
    }
}