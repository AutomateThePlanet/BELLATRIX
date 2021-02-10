// <copyright file="AssertionsExtensions.Width.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

namespace Bellatrix.Layout
{
    public static partial class AssertionsExtensions
    {
        public static event EventHandler<LayoutElementActionEventArgs> AssertedWidthEvent;
        public static event EventHandler<LayoutElementTwoValuesActionEventArgs> AssertedWidthBetweenEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedWidthLessThanEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedWidthLessThanOrEqualEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedWidthGreaterThanEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedWidthGreaterThanOrEqualEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedWidthApproximateSecondElementEvent;

        public static void AssertWidth(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(layoutElement.Size.Width, expected, $"The width of {layoutElement.ElementName} was not {expected} px, but {layoutElement.Size.Width} px.");
            AssertedWidthEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertWidthBetween(this ILayoutElement layoutElement, double from, double to)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Width >= from && layoutElement.Size.Width <= to, $"The width of {layoutElement.ElementName} was not between {from} and {to} px, but {layoutElement.Size.Width} px.");
            AssertedWidthBetweenEvent?.Invoke(layoutElement, new LayoutElementTwoValuesActionEventArgs(layoutElement, from.ToString(), to.ToString()));
        }

        public static void AssertWidthGreaterThan(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Width > expected, $"The width of {layoutElement.ElementName} was not > {expected} px, but {layoutElement.Size.Width} px.");
            AssertedWidthGreaterThanEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertWidthGreaterThanOrEqual(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Width >= expected, $"The width of {layoutElement.ElementName} was not >= {expected} px, but {layoutElement.Size.Width} px.");
            AssertedWidthGreaterThanOrEqualEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertWidthLessThan(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Width < expected, $"The width of {layoutElement.ElementName} was not < {expected} px, but {layoutElement.Size.Width} px.");
            AssertedWidthLessThanEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertWidthLessThanOrEqual(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Width <= expected, $"The width of {layoutElement.ElementName} was not <= {expected} px, but {layoutElement.Size.Width} px.");
            AssertedWidthLessThanOrEqualEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertWidthApproximate(this ILayoutElement layoutElement, ILayoutElement secondElement, double expectedPercentDifference)
        {
            var actualPercentDifference = CalculatePercentDifference(layoutElement.Size.Width, secondElement.Size.Width);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= expectedPercentDifference, $"The width % difference between {layoutElement.ElementName} and {secondElement.ElementName} was greater than {expectedPercentDifference}%, it was {actualPercentDifference} px.");
            AssertedWidthApproximateSecondElementEvent?.Invoke(layoutElement, new LayoutTwoElementsActionEventArgs(layoutElement, secondElement, expectedPercentDifference.ToString()));
        }
    }
}
