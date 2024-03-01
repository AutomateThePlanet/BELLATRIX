// <copyright file="AssertionsExtensions.BottomLeftInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static void AssertBottomLeftInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        AssertLeftInsideOf(innerElement, outerElement);
        AssertBottomInsideOf(innerElement, outerElement);
    }

    public static void AssertBottomLeftInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double left)
    {
        AssertLeftInsideOf(innerElement, outerElement, left);
        AssertBottomInsideOf(innerElement, outerElement, bottom);
    }

    public static void AssertBottomLeftInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottomFrom, double bottomTo, double leftFrom, double leftTo)
    {
        AssertLeftInsideOfBetween(innerElement, outerElement, leftFrom, leftTo);
        AssertBottomInsideOfBetween(innerElement, outerElement, bottomFrom, bottomTo);
    }

    public static void AssertBottomLeftInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double left)
    {
        AssertLeftInsideOfGreaterThan(innerElement, outerElement, left);
        AssertBottomInsideOfGreaterThan(innerElement, outerElement, bottom);
    }

    public static void AssertBottomLeftInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double left)
    {
        AssertLeftInsideOfGreaterThanOrEqual(innerElement, outerElement, left);
        AssertBottomInsideOfGreaterThanOrEqual(innerElement, outerElement, bottom);
    }

    public static void AssertBottomLeftInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double left)
    {
        AssertLeftInsideOfLessThan(innerElement, outerElement, left);
        AssertBottomInsideOfLessThan(innerElement, outerElement, bottom);
    }

    public static void AssertBottomLeftInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double left)
    {
        AssertLeftInsideOfLessThanOrEqual(innerElement, outerElement, left);
        AssertBottomInsideOfLessThanOrEqual(innerElement, outerElement, bottom);
    }

    public static void AssertBottomLeftInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double left, double percent)
    {
        AssertLeftInsideOfApproximate(innerElement, outerElement, left, percent);
        AssertBottomInsideOfApproximate(innerElement, outerElement, bottom, percent);
    }
}