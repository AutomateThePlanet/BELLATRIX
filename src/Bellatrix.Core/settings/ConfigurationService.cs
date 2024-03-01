// <copyright file="ConfigurationService.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Bellatrix.Settings;
using Bellatrix.Utilities;
using Microsoft.Extensions.Configuration;

// ReSharper disable All
namespace Bellatrix;

public sealed class ConfigurationService
{
    private static IConfigurationRoot _root;

    static ConfigurationService()
    {
        _root = InitializeConfiguration();
    }

    public static TSection GetSection<TSection>()
      where TSection : class, new()
    {
        string sectionName = MakeFirstLetterToLower(typeof(TSection).Name);
        return _root.GetSection(sectionName).Get<TSection>();
    }

    private static string MakeFirstLetterToLower(string text)
    {
        return char.ToLower(text[0]) + text.Substring(1);
    }

    private static IConfigurationRoot InitializeConfiguration()
    {
        var builder = new ConfigurationBuilder();
        var executionDir = ExecutionDirectoryResolver.GetDriverExecutablePath();
        var filesInExecutionDir = Directory.GetFiles(executionDir);
        var settingsFile =
#pragma warning disable CA1310 // Specify StringComparison for correctness
                filesInExecutionDir.FirstOrDefault(x => x.Contains("testFrameworkSettings") && x.EndsWith(".json"));
#pragma warning restore CA1310 // Specify StringComparison for correctness
        if (settingsFile != null)
        {
            builder.AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
        }

        builder.AddEnvironmentVariables();

        return builder.Build();
    }
}