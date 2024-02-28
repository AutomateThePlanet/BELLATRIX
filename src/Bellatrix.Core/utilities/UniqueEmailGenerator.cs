// <copyright file="UniqueEmailGenerator.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Utilities;

public class UniqueEmailGenerator
{
    public static string BuildUniqueEmail(string prefix, string sufix)
    {
        var result = string.Concat(prefix, "_", TimestampBuilder.GenerateUniqueText(), "@", sufix, ".com");
        return result;
    }

    public static string BuildUniqueEmailTimestamp()
    {
        var result = $"bss-{TimestampBuilder.GenerateUniqueText()}@bss.com";
        return result;
    }

    public static string BuildUniqueEmailGuid()
    {
        var result = $"bss-{Guid.NewGuid()}@bss.com";
        return result;
    }

    public static string BuildUniqueEmail(string prefix)
    {
        var result = $"{prefix}{TimestampBuilder.GenerateUniqueText()}@sit.com";
        return result;
    }

    public static string BuildUniqueEmail(char specialSymbol)
    {
        var result = $"sit-{TimestampBuilder.GenerateUniqueText()}{specialSymbol}@sit.com";
        return result;
    }
}