// <copyright file="EnumExtensions.cs" company="Automate The Planet Ltd.">
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
using System.ComponentModel;

namespace Bellatrix.Utilities;

public static class EnumExtensions
{
    public static string GetEnumName(this Enum en, object value)
    {
        return Enum.GetName(en.GetType(), value);
    }

    public static string GetEnumDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());

        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0)
        {
            return attributes[0].Description;
        }

        return value.ToString();
    }

    public static TEnum ParseToEnum<TEnum>(this string enumValue)
        where TEnum : struct
    {
        if (int.TryParse(enumValue, out int result))
        {
            return result.ParseToEnum<TEnum>();
        }

        return (TEnum)Enum.Parse(typeof(TEnum), enumValue);
    }

    public static TEnum ParseToEnum<TEnum>(this int enumValue)
        where TEnum : struct
    {
        return (TEnum)Enum.ToObject(typeof(TEnum), enumValue);
    }
}