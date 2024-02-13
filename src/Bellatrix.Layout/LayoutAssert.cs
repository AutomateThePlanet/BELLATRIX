// <copyright file="LayoutAssert.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using BA = Bellatrix.Assertions;

namespace Bellatrix.Layout;

public static class LayoutAssert
{
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedHorizontallyAllEvent;
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedHorizontallyTopEvent;
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedHorizontallyBottomEvent;
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedHorizontallyCenteredEvent;
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedVerticallyAllEvent;
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedVerticallyLeftEvent;
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedVerticallyRightEvent;
    public static event EventHandler<LayoutComponentsActionEventArgs> AssertedAlignedVerticallyCenteredEvent;

    public static void AssertAlignedVerticallyAll(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineLeftY = layoutComponents.First().Location.X;
        var baseLineRightY = layoutComponents.First().Location.X + layoutComponents.First().Size.Width;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var leftX = layoutComponents[i].Location.X;
            var rightX = layoutComponents[i].Location.X + layoutComponents[i].Size.Width;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineLeftY, leftX, $"{layoutComponents.First().ComponentName} should be aligned left vertically {layoutComponents[i].ComponentName} Y = {baseLineLeftY} px but was {leftX} px.");
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineRightY, rightX, $"{layoutComponents.First().ComponentName} should be aligned right vertically {layoutComponents[i].ComponentName} Y = {baseLineRightY} px but was {rightX} px.");
        }

        AssertedAlignedVerticallyAllEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    public static void AssertAlignedVerticallyCentered(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineRightY = layoutComponents.First().Location.X + layoutComponents.First().Size.Width / 2;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var rightX = layoutComponents[i].Location.X + layoutComponents[i].Size.Width / 2;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineRightY, rightX, $"{layoutComponents.First().ComponentName} should be aligned centered vertically {layoutComponents[i].ComponentName} Y = {baseLineRightY} px but was {rightX} px.");
        }

        AssertedAlignedVerticallyCenteredEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    public static void AssertAlignedVerticallyRight(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineRightY = layoutComponents.First().Location.X + layoutComponents.First().Size.Width;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var rightX = layoutComponents[i].Location.X + layoutComponents[i].Size.Width;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineRightY, rightX, $"{layoutComponents.First().ComponentName} should be aligned right vertically {layoutComponents[i].ComponentName} Y = {baseLineRightY} px but was {rightX} px.");
        }

        AssertedAlignedVerticallyRightEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    public static void AssertAlignedVerticallyLeft(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineLeftY = layoutComponents.First().Location.X;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var leftX = layoutComponents[i].Location.X;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineLeftY, leftX, $"{layoutComponents.First().ComponentName} should be aligned left vertically {layoutComponents[i].ComponentName} Y = {baseLineLeftY} px but was {leftX} px.");
        }

        AssertedAlignedVerticallyLeftEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    public static void AssertAlignedHorizontallyAll(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineTopY = layoutComponents.First().Location.Y;
        var baseLineBottomY = layoutComponents.First().Location.Y + layoutComponents.First().Size.Height;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var topY = layoutComponents[i].Location.Y;
            var bottomY = layoutComponents[i].Location.Y + layoutComponents[i].Size.Height;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineTopY, topY, $"{layoutComponents.First().ComponentName} should be aligned top horizontally {layoutComponents[i].ComponentName} Y = {baseLineTopY} px but was {topY} px.");
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineBottomY, bottomY, $"{layoutComponents.First().ComponentName} should be aligned bottom horizontally {layoutComponents[i].ComponentName} Y = {baseLineBottomY} px but was {bottomY} px.");
        }

        AssertedAlignedHorizontallyAllEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    public static void AssertAlignedHorizontallyCentered(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineTopY = layoutComponents.First().Location.Y + layoutComponents.First().Size.Height / 2;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var centeredY = layoutComponents[i].Location.Y + layoutComponents[i].Size.Height / 2;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineTopY, centeredY, $"{layoutComponents.First().ComponentName} should be aligned centered horizontally {layoutComponents[i].ComponentName} Y = {baseLineTopY} px but was {centeredY} px.");
        }

        AssertedAlignedHorizontallyCenteredEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    public static void AssertAlignedHorizontallyTop(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineTopY = layoutComponents.First().Location.Y;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var topY = layoutComponents[i].Location.Y;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineTopY, topY, $"{layoutComponents.First().ComponentName} should be aligned top horizontally {layoutComponents[i].ComponentName} Y = {baseLineTopY} px but was {topY} px.");
        }

        AssertedAlignedHorizontallyTopEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    public static void AssertAlignedHorizontallyBottom(params ILayoutComponent[] layoutComponents)
    {
        ValidateLayoutComponentsCount(layoutComponents);

        var baseLineBottomY = layoutComponents.First().Location.Y + layoutComponents.First().Size.Height;

        for (int i = 1; i < layoutComponents.Length; i++)
        {
            var bottomY = layoutComponents[i].Location.Y + layoutComponents[i].Size.Height;
            BA.Assert.AreEqual<LayoutAssertFailedException, double>(baseLineBottomY, bottomY, $"{layoutComponents.First().ComponentName} should be aligned bottom horizontally {layoutComponents[i].ComponentName} Y = {baseLineBottomY} px but was {bottomY} px.");
        }

        AssertedAlignedHorizontallyBottomEvent?.Invoke(layoutComponents, new LayoutComponentsActionEventArgs(layoutComponents));
    }

    private static void ValidateLayoutComponentsCount(params ILayoutComponent[] layoutComponents)
    {
        if (layoutComponents.Length <= 1)
        {
            throw new ArgumentException("You need to pass at least two elements.");
        }
    }
}
