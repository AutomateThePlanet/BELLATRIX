// <copyright file="AssertionsExtensions.NearTopRightOf.cs" company="Automate The Planet Ltd.">
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
    public static void AssertNearTopRightOf(this ILayoutComponent element, ILayoutComponent secondElement)
    {
        AssertNearTopOf(element, secondElement);
        AssertNearRightOf(secondElement, element);
    }

    public static void AssertNearTopRightOf(this ILayoutComponent element, ILayoutComponent secondElement, double top, double right)
    {
        AssertNearTopOf(element, secondElement, top);
        AssertNearRightOf(secondElement, element, right);
    }

    public static void AssertNearTopRightOfBetween(this ILayoutComponent element, ILayoutComponent secondElement, double fromTop, double toTop, double fromRight, double toRight)
    {
        AssertNearTopOfBetween(element, secondElement, fromTop, toTop);
        AssertNearRightOfBetween(secondElement, element, fromRight, toRight);
    }

    public static void AssertNearTopRightOfGreaterThan(this ILayoutComponent element, ILayoutComponent secondElement, double top, double right)
    {
        AssertNearTopOfGreaterThan(element, secondElement, top);
        AssertNearRightOfGreaterThan(secondElement, element, right);
    }

    public static void AssertNearTopRightOfGreaterThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double top, double right)
    {
        AssertNearTopOfGreaterThanOrEqual(element, secondElement, top);
        AssertNearRightOfGreaterThanOrEqual(secondElement, element, right);
    }

    public static void AssertNearTopRightOfLessThan(this ILayoutComponent element, ILayoutComponent secondElement, double top, double right)
    {
        AssertNearTopOfLessThan(element, secondElement, top);
        AssertNearRightOfLessThan(secondElement, element, right);
    }

    public static void AssertNearTopRightOfLessThanOrEqual(this ILayoutComponent element, ILayoutComponent secondElement, double top, double right)
    {
        AssertNearTopOfLessThanOrEqual(element, secondElement, top);
        AssertNearRightOfLessThanOrEqual(secondElement, element, right);
    }

    public static void AssertNearTopRightOfApproximate(this ILayoutComponent element, ILayoutComponent secondElement, double top, double right, double percent)
    {
        AssertNearTopOfApproximate(element, secondElement, top, percent);
        AssertNearRightOfApproximate(secondElement, element, right, percent);
    }
}
