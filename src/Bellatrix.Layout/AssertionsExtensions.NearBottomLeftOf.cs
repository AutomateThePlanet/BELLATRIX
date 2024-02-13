// <copyright file="AssertionsExtensions.NearBottomLeftOf.cs" company="Automate The Planet Ltd.">
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
using BA = Bellatrix.Assertions;

namespace Bellatrix.Layout;

public static partial class AssertionsExtensions
{
    public static void AssertNearBottomLeftOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        AssertNearBottomOf(element, secondElement);
        AssertNearLeftOf(secondElement, element);
    }

    public static void AssertNearBottomLeftOf(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double left)
    {
        AssertNearBottomOf(element, secondElement, bottom);
        AssertNearLeftOf(secondElement, element, left);
    }

    public static void AssertNearBottomLeftOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double fromBottom, double toBottom, double fromLeft, double toLeft)
    {
        AssertNearBottomOfBetween(element, secondElement, fromBottom, toBottom);
        AssertNearLeftOfBetween(secondElement, element, fromLeft, toLeft);
    }

    public static void AssertNearBottomLeftOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double left)
    {
        AssertNearBottomOfGreaterThan(element, secondElement, bottom);
        AssertNearLeftOfGreaterThan(secondElement, element, left);
    }

    public static void AssertNearBottomLeftOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double left)
    {
        AssertNearBottomOfGreaterThanOrEqual(element, secondElement, bottom);
        AssertNearLeftOfGreaterThanOrEqual(secondElement, element, left);
    }

    public static void AssertNearBottomLeftOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double left)
    {
        AssertNearBottomOfLessThan(element, secondElement, bottom);
        AssertNearLeftOfLessThan(secondElement, element, left);
    }

    public static void AssertNearBottomLeftOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double left)
    {
        AssertNearBottomOfLessThanOrEqual(element, secondElement, bottom);
        AssertNearLeftOfLessThanOrEqual(secondElement, element, left);
    }

    public static void AssertNearBottomLeftOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double left, double percent)
    {
        AssertNearBottomOfApproximate(element, secondElement, bottom, percent);
        AssertNearLeftOfApproximate(secondElement, element, left, percent);
    }
}
