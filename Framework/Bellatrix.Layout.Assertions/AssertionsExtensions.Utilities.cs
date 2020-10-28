// <copyright file="AssertionsExtensions.Utilities.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
        internal static double CalculatePercentDifference(double num1, double num2)
        {
            var percentDifference = (num1 - num2) / ((num1 + num2) / 2) * 100;
            var actualPercentDifference = Math.Abs(percentDifference);

            return actualPercentDifference;
        }

        internal static double CalculateRightOfDistance(ILayoutElement element, ILayoutElement secondElement)
        {
            return secondElement.Location.X - (element.Location.X + element.Size.Width);
        }

        internal static double CalculateLeftOfDistance(ILayoutElement element, ILayoutElement secondElement)
        {
            return element.Location.X - (secondElement.Location.X + secondElement.Size.Width);
        }

        internal static double CalculateAboveOfDistance(ILayoutElement element, ILayoutElement secondElement)
        {
            return secondElement.Location.Y - (element.Location.Y + element.Size.Height);
        }

        internal static double CalculateBelowOfDistance(ILayoutElement element, ILayoutElement secondElement)
        {
            return element.Location.Y - (secondElement.Location.Y + secondElement.Size.Height);
        }

        internal static double CalculateTopInsideOfDistance(ILayoutElement innerElement, ILayoutElement outerElement)
        {
            return innerElement.Location.Y - outerElement.Location.Y;
        }

        internal static double CalculateBottomInsideOfDistance(ILayoutElement innerElement, ILayoutElement outerElement)
        {
            return (outerElement.Location.Y + outerElement.Size.Height) - (innerElement.Location.Y + innerElement.Size.Height);
        }

        internal static double CalculateLeftInsideOfDistance(ILayoutElement innerElement, ILayoutElement outerElement)
        {
            return innerElement.Location.X - outerElement.Location.X;
        }

        internal static double CalculateRightInsideOfDistance(ILayoutElement innerElement, ILayoutElement outerElement)
        {
            return (outerElement.Location.X + outerElement.Size.Width) - (innerElement.Location.X + innerElement.Size.Width);
        }
    }
}
