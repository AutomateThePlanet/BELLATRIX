// <copyright file="CustomDelimiterFormatter.cs" company="Automate The Planet Ltd.">
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
using System.Globalization;

namespace Bellatrix.Utilities;

public static class CustomDelimiterFormatter
{
    private static readonly CultureInfo _usCultureInfo = CultureInfo.CreateSpecificCulture("en-US");

    /// <summary>
    ///     It is used to format double digits to specific US culture string. You can control the formatting options through
    ///     DigitsFormattingSettings enum.
    ///     You can set multiple values: ToStringUsDigitsFormatting.PrefixDollar | PrefixDollar.PrefixMinus | SufixDollar in
    ///     single method call.
    ///     Also, you can control the precision of your number through the precision parameter. By default it is set to 2.
    /// </summary>
    /// <param name="digitsFormattingSettings">The digits formatting settings.</param>
    /// <param name="number">The number.</param>
    /// <param name="precision">The precision.</param>
    /// <returns>The formatted number's string.</returns>
    public static string ToStringUsDigitsFormatting(
        this double number,
        DigitsFormatting digitsFormattingSettings = DigitsFormatting.None,
        int precision = 2)
    {
        var result = ToStringUsDigitsFormattingInternal(number, digitsFormattingSettings, precision);

        return result;
    }

    /// <summary>
    ///     It is used to format decimal digits to specific US culture string. You can control the formatting options through
    ///     DigitsFormattingSettings enum.
    ///     You can set multiple values: ToStringUsDigitsFormatting.PrefixDollar | PrefixDollar.PrefixMinus | SuffixDollar in
    ///     single method call.
    ///     Also, you can control the precision of your number through the precision parameter. By default it is set to 2.
    /// </summary>
    /// <param name="digitsFormattingSettings">The digits formatting settings.</param>
    /// <param name="number">The number.</param>
    /// <param name="precision">The precision.</param>
    /// <returns>The formatted number's string.</returns>
    public static string ToStringUsDigitsFormatting(
        this decimal number,
        DigitsFormatting digitsFormattingSettings = DigitsFormatting.None,
        int precision = 2)
    {
        var result = ToStringUsDigitsFormattingInternal(number, digitsFormattingSettings, precision);

        return result;
    }

    private static string ToStringUsDigitsFormattingInternal<T>(
        T number,
        DigitsFormatting digitsFormattingSettings = DigitsFormatting.None,
        int precision = 2)
        where T : struct,
        IComparable,
        IComparable<T>,
        IConvertible,
        IEquatable<T>,
        IFormattable
    {
        var currentNoCommaFormatSpecifier = string.Concat("0.", new string('0', precision));
        var currentComaFormatSpecifier = string.Concat("##,0.", new string('0', precision));
        var formattedDigits = digitsFormattingSettings.HasFlag(
            DigitsFormatting.NoComma)
            ? number.ToString(currentNoCommaFormatSpecifier, _usCultureInfo)
            : number.ToString(currentComaFormatSpecifier, _usCultureInfo);
        if (digitsFormattingSettings.HasFlag(DigitsFormatting.PrefixDollar))
        {
            formattedDigits = string.Concat("$", formattedDigits);
        }

        if (digitsFormattingSettings.HasFlag(DigitsFormatting.PrefixMinus))
        {
            formattedDigits = string.Concat("-", formattedDigits);
        }

        if (digitsFormattingSettings.HasFlag(DigitsFormatting.SufixDollar))
        {
            formattedDigits = string.Concat(formattedDigits, "$");
        }

        return formattedDigits;
    }
}