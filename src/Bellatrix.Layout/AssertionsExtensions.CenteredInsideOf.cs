// <copyright file="AssertionsExtensions.CenteredInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static void AssertCenteredInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        AssertLeftInsideOf(innerElement, outerElement);
        AssertRightInsideOf(innerElement, outerElement);
        AssertTopInsideOf(innerElement, outerElement);
        AssertBottomInsideOf(innerElement, outerElement);
    }

    public static void AssertCenteredInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double distance)
    {
        AssertLeftInsideOf(innerElement, outerElement, distance);
        AssertRightInsideOf(innerElement, outerElement, distance);
        AssertTopInsideOf(innerElement, outerElement, distance);
        AssertBottomInsideOf(innerElement, outerElement, distance);
    }

    public static void AssertCenteredInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double from, double to)
    {
        AssertLeftInsideOfBetween(innerElement, outerElement, from, to);
        AssertRightInsideOfBetween(innerElement, outerElement, from, to);
        AssertTopInsideOfBetween(innerElement, outerElement, from, to);
        AssertBottomInsideOfBetween(innerElement, outerElement, from, to);
    }

    public static void AssertCenteredInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double distance)
    {
        AssertLeftInsideOfGreaterThan(innerElement, outerElement, distance);
        AssertRightInsideOfGreaterThan(innerElement, outerElement, distance);
        AssertTopInsideOfGreaterThan(innerElement, outerElement, distance);
        AssertBottomInsideOfGreaterThan(innerElement, outerElement, distance);
    }

    public static void AssertCenteredInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double distance)
    {
        AssertLeftInsideOfGreaterThanOrEqual(innerElement, outerElement, distance);
        AssertRightInsideOfGreaterThanOrEqual(innerElement, outerElement, distance);
        AssertTopInsideOfGreaterThanOrEqual(innerElement, outerElement, distance);
        AssertBottomInsideOfGreaterThanOrEqual(innerElement, outerElement, distance);
    }

    public static void AssertCenteredInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double distance)
    {
        AssertLeftInsideOfLessThan(innerElement, outerElement, distance);
        AssertRightInsideOfLessThan(innerElement, outerElement, distance);
        AssertTopInsideOfLessThan(innerElement, outerElement, distance);
        AssertBottomInsideOfLessThan(innerElement, outerElement, distance);
    }

    public static void AssertCenteredInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double distance)
    {
        AssertLeftInsideOfLessThanOrEqual(innerElement, outerElement, distance);
        AssertRightInsideOfLessThanOrEqual(innerElement, outerElement, distance);
        AssertTopInsideOfLessThanOrEqual(innerElement, outerElement, distance);
        AssertBottomInsideOfLessThanOrEqual(innerElement, outerElement, distance);
    }

    public static void AssertCenteredInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double distance, double percent)
    {
        AssertLeftInsideOfApproximate(innerElement, outerElement, distance, percent);
        AssertRightInsideOfApproximate(innerElement, outerElement, distance, percent);
        AssertTopInsideOfApproximate(innerElement, outerElement, distance, percent);
        AssertBottomInsideOfApproximate(innerElement, outerElement, distance, percent);
    }
}