// <copyright file="AssertionsExtensions.Width.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedWidthEvent;
    public static event EventHandler<LayoutComponentTwoValuesActionEventArgs> AssertedWidthBetweenEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedWidthLessThanEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedWidthLessThanOrEqualEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedWidthGreaterThanEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedWidthGreaterThanOrEqualEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedWidthApproximateSecondElementEvent;

    public static void AssertWidth(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(layoutComponent.Size.Width, expected, $"The width of {layoutComponent.ComponentName} was not {expected} px, but {layoutComponent.Size.Width} px.");
        AssertedWidthEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertWidthBetween(this ILayoutComponent layoutComponent, double from, double to)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Width >= from && layoutComponent.Size.Width <= to, $"The width of {layoutComponent.ComponentName} was not between {from} and {to} px, but {layoutComponent.Size.Width} px.");
        AssertedWidthBetweenEvent?.Invoke(layoutComponent, new LayoutComponentTwoValuesActionEventArgs(layoutComponent, from.ToString(), to.ToString()));
    }

    public static void AssertWidthGreaterThan(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Width > expected, $"The width of {layoutComponent.ComponentName} was not > {expected} px, but {layoutComponent.Size.Width} px.");
        AssertedWidthGreaterThanEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertWidthGreaterThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Width >= expected, $"The width of {layoutComponent.ComponentName} was not >= {expected} px, but {layoutComponent.Size.Width} px.");
        AssertedWidthGreaterThanOrEqualEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertWidthLessThan(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Width < expected, $"The width of {layoutComponent.ComponentName} was not < {expected} px, but {layoutComponent.Size.Width} px.");
        AssertedWidthLessThanEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertWidthLessThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Width <= expected, $"The width of {layoutComponent.ComponentName} was not <= {expected} px, but {layoutComponent.Size.Width} px.");
        AssertedWidthLessThanOrEqualEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertWidthApproximate(this ILayoutComponent layoutComponent, ILayoutComponent secondElement, double expectedPercentDifference)
    {
        var actualPercentDifference = CalculatePercentDifference(layoutComponent.Size.Width, secondElement.Size.Width);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= expectedPercentDifference, $"The width % difference between {layoutComponent.ComponentName} and {secondElement.ComponentName} was greater than {expectedPercentDifference}%, it was {actualPercentDifference} px.");
        AssertedWidthApproximateSecondElementEvent?.Invoke(layoutComponent, new LayoutTwoComponentsActionEventArgs(layoutComponent, secondElement, expectedPercentDifference.ToString()));
    }
}
