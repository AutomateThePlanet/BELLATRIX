// <copyright file="AssertionsExtensions.Height.cs" company="Automate The Planet Ltd.">
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
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedHeightEvent;
    public static event EventHandler<LayoutComponentTwoValuesActionEventArgs> AssertedHeightBetweenEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedHeightLessThanEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedHeightLessThanOrEqualEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedHeightGreaterThanEvent;
    public static event EventHandler<LayoutComponentActionEventArgs> AssertedHeightGreaterThanOrEqualEvent;
    public static event EventHandler<LayoutTwoComponentsActionEventArgs> AssertedHeightApproximateSecondElementEvent;

    public static void AssertHeight(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.AreEqual<LayoutAssertFailedException, double>(layoutComponent.Size.Height, expected, $"The height of {layoutComponent.ComponentName} was not {expected} px, but {layoutComponent.Size.Height} px.");
        AssertedHeightEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertHeightBetween(this ILayoutComponent layoutComponent, double from, double to)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Height >= from && layoutComponent.Size.Height <= to, $"The height of {layoutComponent.ComponentName} was not between {from} and {to} px, but {layoutComponent.Size.Height} px.");
        AssertedHeightBetweenEvent?.Invoke(layoutComponent, new LayoutComponentTwoValuesActionEventArgs(layoutComponent, from.ToString(), to.ToString()));
    }

    public static void AssertHeightGreaterThan(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Height > expected, $"The height of {layoutComponent.ComponentName} was not > {expected} px, but {layoutComponent.Size.Height} px.");
        AssertedHeightGreaterThanEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertHeightGreaterThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Height >= expected, $"The height of {layoutComponent.ComponentName} was not >= {expected} px, but {layoutComponent.Size.Height} px.");
        AssertedHeightGreaterThanOrEqualEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertHeightLessThan(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Height < expected, $"The height of {layoutComponent.ComponentName} was not < {expected} px, but {layoutComponent.Size.Height} px.");
        AssertedHeightLessThanEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertHeightLessThanOrEqual(this ILayoutComponent layoutComponent, double expected)
    {
        BA.Assert.IsTrue<LayoutAssertFailedException>(layoutComponent.Size.Height <= expected, $"The height of {layoutComponent.ComponentName} was not <= {expected} px, but {layoutComponent.Size.Height} px.");
        AssertedHeightLessThanOrEqualEvent?.Invoke(layoutComponent, new LayoutComponentActionEventArgs(layoutComponent, expected.ToString()));
    }

    public static void AssertHeightApproximate(this ILayoutComponent layoutComponent, ILayoutComponent secondElement, double expectedPercentDifference)
    {
        var actualPercentDifference = CalculatePercentDifference(layoutComponent.Size.Height, secondElement.Size.Height);
        BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= expectedPercentDifference, $"The height % difference between {layoutComponent.ComponentName} and {secondElement.ComponentName} was greater than {expectedPercentDifference}%, it was {actualPercentDifference} px.");
        AssertedHeightApproximateSecondElementEvent?.Invoke(layoutComponent, new LayoutTwoComponentsActionEventArgs(layoutComponent, secondElement, expectedPercentDifference.ToString()));
    }
}
