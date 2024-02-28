// <copyright file="AssemblyFacade.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bellatrix.Infrastructure;

public class AssemblyFacade
{
    public Assembly GetExecutingAssembly() => Assembly.GetExecutingAssembly();

    public Assembly LoadFile(string path)
    {
        var assemblyBytes = File.ReadAllBytes(path);
        var assembly = Assembly.Load(assemblyBytes);
        return assembly;
    }

    public string GetAssemblyDirectory()
    {
        var codeBase = Assembly.GetExecutingAssembly().Location;
        var uri = new UriBuilder(codeBase);
        var path = Uri.UnescapeDataString(uri.Path);
        return Path.GetDirectoryName(path);
    }

    public List<Assembly> GetAssembliesCallChain()
    {
        var trace = new StackTrace();
        var assemblies = new List<Assembly>();
        var frames = trace.GetFrames();

        if (frames == null)
        {
            throw new Exception("Couldn't get the stack trace");
        }

        foreach (var frame in frames)
        {
            var method = frame.GetMethod();
            var declaringType = method.DeclaringType;

            if (declaringType == null)
            {
                continue;
            }

            var declaringTypeAssembly = declaringType.Assembly;
            var lastAssembly = assemblies.LastOrDefault();

            if (declaringTypeAssembly != lastAssembly)
            {
                assemblies.Add(declaringTypeAssembly);
            }
        }

        foreach (var currentAssembly in assemblies)
        {
            Debug.WriteLine(currentAssembly.ManifestModule.Name);
        }

        return assemblies;
    }
}