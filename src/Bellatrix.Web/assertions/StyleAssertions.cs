// <copyright file="StyleAssertions.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using Bellatrix.Assertions;

namespace Bellatrix.Web.Assertions;

public static class StyleAssertions
{
    public static void AssertBackgroundColor(this Component element, string expectedBackgroundColor)
    {
        var actualColor = element.GetCssValue("background-color");
        Assert.AreEqual(expectedBackgroundColor, actualColor);
    }

    public static void AssertBorderColor(this Component element, string expectedBorderColor)
    {
        Assert.AreEqual(expectedBorderColor, element.GetCssValue("border-color"));
    }

    public static void AssertColor(this Component element, string expectedColor)
    {
        Assert.AreEqual(expectedColor, element.GetCssValue("color"));
    }

    public static void AssertFontFamily(this Component element, string expectedFontFamily)
    {
        Assert.AreEqual(expectedFontFamily, element.GetCssValue("font-family"));
    }

    public static void AssertFontWeight(this Component element, string expectedFontWeight)
    {
        Assert.AreEqual(expectedFontWeight, element.GetCssValue("font-weight"));
    }

    public static void AssertFontSize(this Component element, string expectedFontSize)
    {
        Assert.AreEqual(expectedFontSize, element.GetCssValue("font-size"));
    }

    public static void AssertTextAlign(this Component element, string expectedTextAlign)
    {
        Assert.AreEqual(expectedTextAlign, element.GetCssValue("text-align"));
    }

    public static void AssertVerticalAlign(this Component element, string expectedVerticalAlign)
    {
        Assert.AreEqual(expectedVerticalAlign, element.GetCssValue("vertical-align"));
    }
}