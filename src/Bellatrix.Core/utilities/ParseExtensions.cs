// <copyright file="ParseExtensions.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix;

public static class ParseExtensions
{
    public static int ToInt(this string value)
    {
        return int.Parse(value);
    }

    public static long ToLong(this string value)
    {
        return long.Parse(value);
    }

    public static float ToFloat(this string value)
    {
        return float.Parse(value);
    }

    public static double ToDouble(this string value)
    {
        return double.Parse(value);
    }

    public static Guid ToGuide(this string value)
    {
        return Guid.Parse(value);
    }

    public static TimeSpan ToTimeSpan(this string value)
    {
        return TimeSpan.Parse(value);
    }

    public static DateTime ToDateTime(this string value)
    {
        return DateTime.Parse(value);
    }
}
