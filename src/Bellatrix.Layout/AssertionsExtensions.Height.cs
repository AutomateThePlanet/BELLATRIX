// <copyright file="AssertionsExtensions.Height.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<LayoutElementActionEventArgs> AssertedHeightEvent;
        public static event EventHandler<LayoutElementTwoValuesActionEventArgs> AssertedHeightBetweenEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedHeightLessThanEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedHeightLessThanOrEqualEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedHeightGreaterThanEvent;
        public static event EventHandler<LayoutElementActionEventArgs> AssertedHeightGreaterThanOrEqualEvent;
        public static event EventHandler<LayoutTwoElementsActionEventArgs> AssertedHeightApproximateSecondElementEvent;

        public static void AssertHeight(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(layoutElement.Size.Height, expected, $"The height of {layoutElement.ElementName} was not {expected} px, but {layoutElement.Size.Height} px.");
            AssertedHeightEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertHeightBetween(this ILayoutElement layoutElement, double from, double to)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Height >= from && layoutElement.Size.Height <= to, $"The height of {layoutElement.ElementName} was not between {from} and {to} px, but {layoutElement.Size.Height} px.");
            AssertedHeightBetweenEvent?.Invoke(layoutElement, new LayoutElementTwoValuesActionEventArgs(layoutElement, from.ToString(), to.ToString()));
        }

        public static void AssertHeightGreaterThan(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Height > expected, $"The height of {layoutElement.ElementName} was not > {expected} px, but {layoutElement.Size.Height} px.");
            AssertedHeightGreaterThanEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertHeightGreaterThanOrEqual(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Height >= expected, $"The height of {layoutElement.ElementName} was not >= {expected} px, but {layoutElement.Size.Height} px.");
            AssertedHeightGreaterThanOrEqualEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertHeightLessThan(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Height < expected, $"The height of {layoutElement.ElementName} was not < {expected} px, but {layoutElement.Size.Height} px.");
            AssertedHeightLessThanEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertHeightLessThanOrEqual(this ILayoutElement layoutElement, double expected)
        {
            BA.Assert.IsTrue<LayoutAssertFailedException>(layoutElement.Size.Height <= expected, $"The height of {layoutElement.ElementName} was not <= {expected} px, but {layoutElement.Size.Height} px.");
            AssertedHeightLessThanOrEqualEvent?.Invoke(layoutElement, new LayoutElementActionEventArgs(layoutElement, expected.ToString()));
        }

        public static void AssertHeightApproximate(this ILayoutElement layoutElement, ILayoutElement secondElement, double expectedPercentDifference)
        {
            var actualPercentDifference = CalculatePercentDifference(layoutElement.Size.Height, secondElement.Size.Height);
            BA.Assert.IsTrue<LayoutAssertFailedException>(actualPercentDifference <= expectedPercentDifference, $"The height % difference between {layoutElement.ElementName} and {secondElement.ElementName} was greater than {expectedPercentDifference}%, it was {actualPercentDifference} px.");
            AssertedHeightApproximateSecondElementEvent?.Invoke(layoutElement, new LayoutTwoElementsActionEventArgs(layoutElement, secondElement, expectedPercentDifference.ToString()));
        }
    }
}
