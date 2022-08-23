// <copyright file="StyleAssertions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Assertions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Bellatrix.Web.Assertions
{
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
            var elementCss = element.GetCssValue("font-size");

            Assert.AreEqual(ExtractDoubleValue(expectedFontSize), ExtractDoubleValue(elementCss), delta: 1);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedFontSize));
        }

        public static void AssertTextAlign(this Component element, string expectedTextAlign)
        {
            Assert.AreEqual(expectedTextAlign, element.GetCssValue("text-align"));
        }

        public static void AssertVerticalAlign(this Component element, string expectedVerticalAlign)
        {
            Assert.AreEqual(expectedVerticalAlign, element.GetCssValue("vertical-align"));
        }

        public static void AssertLineHeight(this Component element, string expectedLineHeight)
        {
            var elementCss = element.GetCssValue("line-height");

            Assert.AreEqual(ExtractDoubleValue(expectedLineHeight), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedLineHeight));
        }

        public static void AssertLetterSpacing(this Component element, string expectedLetterSpacing)
        {
            var elementCss = element.GetCssValue("letter-spacing");

            Assert.AreEqual(ExtractDoubleValue(expectedLetterSpacing), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedLetterSpacing));
        }

        public static void AssertMarginTop(this Component element, string expectedMarginTop)
        {
            var elementCss = element.GetCssValue("margin-top");

            Assert.AreEqual(ExtractDoubleValue(expectedMarginTop), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedMarginTop));
        }

        public static void AssertMarginBottom(this Component element, string expectedMarginBottom)
        {
            var elementCss = element.GetCssValue("margin-bottom");

            Assert.AreEqual(ExtractDoubleValue(expectedMarginBottom), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedMarginBottom));
        }

        public static void AssertMarginLeft(this Component element, string expectedMarginLeft)
        {
            var elementCss = element.GetCssValue("margin-left");

            Assert.AreEqual(ExtractDoubleValue(expectedMarginLeft), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedMarginLeft));
        }

        public static void AssertMarginRight(this Component element, string expectedMarginRight)
        {
            var elementCss = element.GetCssValue("margin-right");

            Assert.AreEqual(ExtractDoubleValue(expectedMarginRight), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedMarginRight));
        }

        public static void AssertPaddingTop(this Component element, string expectedPaddingTop)
        {
            var elementCss = element.GetCssValue("padding-top");

            Assert.AreEqual(ExtractDoubleValue(expectedPaddingTop), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedPaddingTop));
        }

        public static void AssertPaddingBottom(this Component element, string expectedPaddingBottom)
        {
            var elementCss = element.GetCssValue("padding-bottom");

            Assert.AreEqual(ExtractDoubleValue(expectedPaddingBottom), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedPaddingBottom));
        }

        public static void AssertPaddingLeft(this Component element, string expectedPaddingLeft)
        {
            var elementCss = element.GetCssValue("padding-left");

            Assert.AreEqual(ExtractDoubleValue(expectedPaddingLeft), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedPaddingLeft));
        }

        public static void AssertPaddingRight(this Component element, string expectedPaddingRight)
        {
            var elementCss = element.GetCssValue("padding-right");

            Assert.AreEqual(ExtractDoubleValue(expectedPaddingRight), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedPaddingRight));
        }

        public static void AssertPosition(this Component element, string expectedPosition)
        {
            Assert.AreEqual(expectedPosition, element.GetCssValue("position"));
        }

        public static void AssertHeight(this Component element, string expectedHeight)
        {
            var elementCss = element.GetCssValue("height");

            Assert.AreEqual(ExtractDoubleValue(expectedHeight), ExtractDoubleValue(elementCss), delta: 5);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedHeight));
        }

        public static void AssertWidth(this Component element, string expectedWidth)
        {
            var elementCss = element.GetCssValue("width");

            Assert.AreEqual(ExtractDoubleValue(expectedWidth), ExtractDoubleValue(elementCss), delta: 30);
            Assert.AreEqual(ExtractMeasureUnit(elementCss), ExtractMeasureUnit(expectedWidth));
        }

        private static string ExtractMeasureUnit(string stringCssValue)
        {
            return stringCssValue.Substring(stringCssValue.IndexOf(stringCssValue.FirstOrDefault(x => Char.IsLetter(x))));
        }

        private static double ExtractDoubleValue(string stringCssValue)
        {
            var regex = "([\\+\\-]?[0-9\\.]+)(%|px|rem|pt|em|in|cm|mm|ex|pc|vw)?";

            string doubleCssValue = Regex.Matches(stringCssValue, regex).FirstOrDefault().Groups[1].Value;

            return Convert.ToDouble(doubleCssValue);
        }
    }
}