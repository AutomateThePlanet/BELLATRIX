// <copyright file="AssertionsExtensions.TopLeftInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static void AssertTopLeftInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        AssertLeftInsideOf(innerElement, outerElement);
        AssertTopInsideOf(innerElement, outerElement);
    }

    public static void AssertTopLeftInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top, double left)
    {
        AssertLeftInsideOf(innerElement, outerElement, left);
        AssertTopInsideOf(innerElement, outerElement, top);
    }

    public static void AssertTopLeftInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double topFrom, double topTo, double leftFrom, double leftTo)
    {
        AssertLeftInsideOfBetween(innerElement, outerElement, leftFrom, leftTo);
        AssertTopInsideOfBetween(innerElement, outerElement, topFrom, topTo);
    }

    public static void AssertTopLeftInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top, double left)
    {
        AssertLeftInsideOfGreaterThan(innerElement, outerElement, left);
        AssertTopInsideOfGreaterThan(innerElement, outerElement, top);
    }

    public static void AssertTopLeftInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top, double left)
    {
        AssertLeftInsideOfGreaterThanOrEqual(innerElement, outerElement, left);
        AssertTopInsideOfGreaterThanOrEqual(innerElement, outerElement, top);
    }

    public static void AssertTopLeftInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top, double left)
    {
        AssertLeftInsideOfLessThan(innerElement, outerElement, left);
        AssertTopInsideOfLessThan(innerElement, outerElement, top);
    }

    public static void AssertTopLeftInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top, double left)
    {
        AssertLeftInsideOfLessThanOrEqual(innerElement, outerElement, left);
        AssertTopInsideOfLessThanOrEqual(innerElement, outerElement, top);
    }

    public static void AssertTopLeftInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double top, double left, double percent)
    {
        AssertLeftInsideOfApproximate(innerElement, outerElement, left, percent);
        AssertTopInsideOfApproximate(innerElement, outerElement, top, percent);
    }
}