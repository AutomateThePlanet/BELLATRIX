// <copyright file="AssertionsExtensions.InsideOf.cs" company="Automate The Planet Ltd.">
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
    public static void AssertInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement, double left, double right, double top, double bottom)
    {
        AssertLeftInsideOf(innerElement, outerElement, left);
        AssertRightInsideOf(innerElement, outerElement, right);
        AssertTopInsideOf(innerElement, outerElement, top);
        AssertBottomInsideOf(innerElement, outerElement, bottom);
    }

    public static void AssertInsideOf(this ILayoutComponent innerElement, ILayoutComponent outerElement)
    {
        Console.WriteLine("Coordinates:");
        Console.WriteLine(innerElement.Location);
        Console.WriteLine(outerElement.Location);
        AssertLeftInsideOf(innerElement, outerElement);
        AssertRightInsideOf(innerElement, outerElement);
        AssertTopInsideOf(innerElement, outerElement);
        AssertBottomInsideOf(innerElement, outerElement);
    }
}