// <copyright file="AssertionsExtensions.NearBottomRightOf.cs" company="Automate The Planet Ltd.">
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
    public static void AssertNearBottomRightOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        AssertNearBottomOf(element, secondElement);
        AssertNearRightOf(secondElement, element);
    }

    public static void AssertNearBottomRightOf(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double right)
    {
        AssertNearBottomOf(element, secondElement, bottom);
        AssertNearRightOf(secondElement, element, right);
    }

    public static void AssertNearBottomRightOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double fromBottom, double toBottom, double fromRight, double toRight)
    {
        AssertNearBottomOfBetween(element, secondElement, fromBottom, toBottom);
        AssertNearRightOfBetween(secondElement, element, fromRight, toRight);
    }

    public static void AssertNearBottomRightOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double right)
    {
        AssertNearBottomOfGreaterThan(element, secondElement, bottom);
        AssertNearRightOfGreaterThan(secondElement, element, right);
    }

    public static void AssertNearBottomRightOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double right)
    {
        AssertNearBottomOfGreaterThanOrEqual(element, secondElement, bottom);
        AssertNearRightOfGreaterThanOrEqual(secondElement, element, right);
    }

    public static void AssertNearBottomRightOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double right)
    {
        AssertNearBottomOfLessThan(element, secondElement, bottom);
        AssertNearRightOfLessThan(secondElement, element, right);
    }

    public static void AssertNearBottomRightOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double right)
    {
        AssertNearBottomOfLessThanOrEqual(element, secondElement, bottom);
        AssertNearRightOfLessThanOrEqual(secondElement, element, right);
    }

    public static void AssertNearBottomRightOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double bottom, double right, double percent)
    {
        AssertNearBottomOfApproximate(element, secondElement, bottom, percent);
        AssertNearRightOfApproximate(secondElement, element, right, percent);
    }
}
