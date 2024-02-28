// <copyright file="AssertionsExtensions.BottomRightInsideOf.cs" company="Automate The Planet Ltd.">
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
    public static void AssertBottomRightInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        AssertRightInsideOf(innerElement, outerElement);
        AssertBottomInsideOf(innerElement, outerElement);
    }

    public static void AssertBottomRightInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double right)
    {
        AssertRightInsideOf(innerElement, outerElement, right);
        AssertBottomInsideOf(innerElement, outerElement, bottom);
    }

    public static void AssertBottomRightInsideOfBetween(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottomFrom, double bottomTo, double rightFrom, double rightTo)
    {
        AssertRightInsideOfBetween(innerElement, outerElement, rightFrom, rightTo);
        AssertBottomInsideOfBetween(innerElement, outerElement, bottomFrom, bottomTo);
    }

    public static void AssertBottomRightInsideOfGreaterThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double right)
    {
        AssertRightInsideOfGreaterThan(innerElement, outerElement, right);
        AssertBottomInsideOfGreaterThan(innerElement, outerElement, bottom);
    }

    public static void AssertBottomRightInsideOfGreaterThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double right)
    {
        AssertRightInsideOfGreaterThanOrEqual(innerElement, outerElement, right);
        AssertBottomInsideOfGreaterThanOrEqual(innerElement, outerElement, bottom);
    }

    public static void AssertBottomRightInsideOfLessThan(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double right)
    {
        AssertRightInsideOfLessThan(innerElement, outerElement, right);
        AssertBottomInsideOfLessThan(innerElement, outerElement, bottom);
    }

    public static void AssertBottomRightInsideOfLessThanOrEqual(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double right)
    {
        AssertRightInsideOfLessThanOrEqual(innerElement, outerElement, right);
        AssertBottomInsideOfLessThanOrEqual(innerElement, outerElement, bottom);
    }

    public static void AssertBottomRightInsideOfApproximate(this ILayoutComponent innerElement, ILayoutComponent outerElement, double bottom, double right, double percent)
    {
        AssertRightInsideOfApproximate(innerElement, outerElement, right, percent);
        AssertBottomInsideOfApproximate(innerElement, outerElement, bottom, percent);
    }
}