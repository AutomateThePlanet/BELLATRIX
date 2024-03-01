// <copyright file="AssertionsExtensions.BottomInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutTwoComponentsNoExpectedActionEventArgs> AssertedBottomInsideOfNoExpectedValueEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedBottomInsideOfEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedBottomInsideOfBetweenEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedBottomInsideOfGreaterThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedBottomInsideOfGreaterOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedBottomInsideOfLessThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedBottomInsideOfLessOrEqualThanEvent;
    public static event EventHandler<LayoutTwoComponentsActionTwoValuesEventArgs> AssertedBottomInsideOfApproximateEvent;

    public static void AssertBottomInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance >= 0, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} bottom but was {actualBottomDistance} px.");
        AssertedBottomInsideOfNoExpectedValueEvent?.Invoke(innerElement, new LayoutTwoComponentsNoExpectedActionEventArgs(innerElement, outerElement));
    }

    public static void AssertBottomInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(bottom, actualBottomDistance, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} bottom padding = {bottom} px but was {actualBottomDistance} px.");
        AssertedBottomInsideOfEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, bottom));
    }

    public static void AssertBottomInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double from, double to)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance >= from && actualBottomDistance <= to, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} bottom padding between {from}-{to} px but was {actualBottomDistance} px.");
        AssertedBottomInsideOfBetweenEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, from, to));
    }

    public static void AssertBottomInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance > bottom, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} bottom padding > {bottom} px but was {actualBottomDistance} px.");
        AssertedBottomInsideOfGreaterThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, bottom));
    }

    public static void AssertBottomInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance >= bottom, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} bottom padding >= {bottom} px but was {actualBottomDistance} px.");
        AssertedBottomInsideOfGreaterOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, bottom));
    }

    public static void AssertBottomInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance < bottom, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} bottom padding < {bottom} px but was {actualBottomDistance} px.");
        AssertedBottomInsideOfLessThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, bottom));
    }

    public static void AssertBottomInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualBottomDistance <= bottom, $"{innerElement.ComponentName} should be inside of {outerElement.ComponentName} bottom padding <= {bottom} px but was {actualBottomDistance} px.");
        AssertedBottomInsideOfLessOrEqualThanEvent?.Invoke(innerElement, new LayoutTwoComponentsActionEventArgs(innerElement, outerElement, bottom));
    }

    public static void AssertBottomInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double percent)
    {
        double actualBottomDistance = CalculateBottomInsideOfDistance(innerElement, outerElement);
        var actualPercentDifference = CalculatePercentDifference(bottom, actualBottomDistance);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= percent, $"{innerElement.ComponentName} should be <= {percent}% of {bottom} px bottom padding of {outerElement.ComponentName} but was {actualBottomDistance} px.");
        AssertedBottomInsideOfApproximateEvent?.Invoke(innerElement, new LayoutTwoComponentsActionTwoValuesEventArgs(innerElement, outerElement, bottom, percent));
    }
}