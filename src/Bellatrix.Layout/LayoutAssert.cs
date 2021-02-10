// <copyright file="LayoutAssert.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Layout
{
    public static class LayoutAssert
    {
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedHorizontallyAllEvent;
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedHorizontallyTopEvent;
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedHorizontallyBottomEvent;
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedHorizontallyCenteredEvent;
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedVerticallyAllEvent;
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedVerticallyLeftEvent;
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedVerticallyRightEvent;
        public static event EventHandler<LayoutElementsActionEventArgs> AssertedAlignedVerticallyCenteredEvent;

        public static void AssertAlignedVerticallyAll(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineLeftY = layoutElements.First().Location.X;
            var baseLineRightY = layoutElements.First().Location.X + layoutElements.First().Size.Width;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var leftX = layoutElements[i].Location.X;
                var rightX = layoutElements[i].Location.X + layoutElements[i].Size.Width;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineLeftY, leftX, $"{layoutElements.First().ElementName} should be aligned left vertically {layoutElements[i].ElementName} Y = {baseLineLeftY} px but was {leftX} px.");
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineRightY, rightX, $"{layoutElements.First().ElementName} should be aligned right vertically {layoutElements[i].ElementName} Y = {baseLineRightY} px but was {rightX} px.");
            }

            AssertedAlignedVerticallyAllEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        public static void AssertAlignedVerticallyCentered(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineRightY = layoutElements.First().Location.X + layoutElements.First().Size.Width / 2;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var rightX = layoutElements[i].Location.X + layoutElements[i].Size.Width / 2;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineRightY, rightX, $"{layoutElements.First().ElementName} should be aligned centered vertically {layoutElements[i].ElementName} Y = {baseLineRightY} px but was {rightX} px.");
            }

            AssertedAlignedVerticallyCenteredEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        public static void AssertAlignedVerticallyRight(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineRightY = layoutElements.First().Location.X + layoutElements.First().Size.Width;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var rightX = layoutElements[i].Location.X + layoutElements[i].Size.Width;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineRightY, rightX, $"{layoutElements.First().ElementName} should be aligned right vertically {layoutElements[i].ElementName} Y = {baseLineRightY} px but was {rightX} px.");
            }

            AssertedAlignedVerticallyRightEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        public static void AssertAlignedVerticallyLeft(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineLeftY = layoutElements.First().Location.X;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var leftX = layoutElements[i].Location.X;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineLeftY, leftX, $"{layoutElements.First().ElementName} should be aligned left vertically {layoutElements[i].ElementName} Y = {baseLineLeftY} px but was {leftX} px.");
            }

            AssertedAlignedVerticallyLeftEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        public static void AssertAlignedHorizontallyAll(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineTopY = layoutElements.First().Location.Y;
            var baseLineBottomY = layoutElements.First().Location.Y + layoutElements.First().Size.Height;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var topY = layoutElements[i].Location.Y;
                var bottomY = layoutElements[i].Location.Y + layoutElements[i].Size.Height;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineTopY, topY, $"{layoutElements.First().ElementName} should be aligned top horizontally {layoutElements[i].ElementName} Y = {baseLineTopY} px but was {topY} px.");
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineBottomY, bottomY, $"{layoutElements.First().ElementName} should be aligned bottom horizontally {layoutElements[i].ElementName} Y = {baseLineBottomY} px but was {bottomY} px.");
            }

            AssertedAlignedHorizontallyAllEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        public static void AssertAlignedHorizontallyCentered(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineTopY = layoutElements.First().Location.Y + layoutElements.First().Size.Height / 2;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var centeredY = layoutElements[i].Location.Y + layoutElements[i].Size.Height / 2;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineTopY, centeredY, $"{layoutElements.First().ElementName} should be aligned centered horizontally {layoutElements[i].ElementName} Y = {baseLineTopY} px but was {centeredY} px.");
            }

            AssertedAlignedHorizontallyCenteredEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        public static void AssertAlignedHorizontallyTop(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineTopY = layoutElements.First().Location.Y;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var topY = layoutElements[i].Location.Y;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineTopY, topY, $"{layoutElements.First().ElementName} should be aligned top horizontally {layoutElements[i].ElementName} Y = {baseLineTopY} px but was {topY} px.");
            }

            AssertedAlignedHorizontallyTopEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        public static void AssertAlignedHorizontallyBottom(params ILayoutElement[] layoutElements)
        {
            ValidateLayoutElementsCount(layoutElements);

            var baseLineBottomY = layoutElements.First().Location.Y + layoutElements.First().Size.Height;

            for (int i = 1; i < layoutElements.Length; i++)
            {
                var bottomY = layoutElements[i].Location.Y + layoutElements[i].Size.Height;
                BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineBottomY, bottomY, $"{layoutElements.First().ElementName} should be aligned bottom horizontally {layoutElements[i].ElementName} Y = {baseLineBottomY} px but was {bottomY} px.");
            }

            AssertedAlignedHorizontallyBottomEvent?.Invoke(layoutElements, new LayoutElementsActionEventArgs(layoutElements));
        }

        private static void ValidateLayoutElementsCount(params ILayoutElement[] layoutElements)
        {
            if (layoutElements.Length <= 1)
            {
                throw new ArgumentException("You need to pass at least two elements.");
            }
        }
    }
}
