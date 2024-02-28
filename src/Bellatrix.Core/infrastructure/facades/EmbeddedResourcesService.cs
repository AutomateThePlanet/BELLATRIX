// <copyright file="EmbeddedResourcesService.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Reflection;

namespace Bellatrix.Infrastructure.SystemFacades;

public class EmbeddedResourcesService
{
    private readonly AssemblyFacade _assemblyFacade;
    private Assembly _currentExecutingAssembly;

    public EmbeddedResourcesService()
    {
        _assemblyFacade = new AssemblyFacade();
    }

    public string FromFile(string name, string fileExtension)
    {
        var resourceName = GetResourceByFileName($"{name}.{fileExtension}");
        return FromFile(resourceName);
    }

    public string FromFile(string name)
    {
        _currentExecutingAssembly ??= _assemblyFacade.GetAssembliesCallChain()[2];
        string currentFileTempPath;
        using (var resourceStream = _currentExecutingAssembly.GetManifestResourceStream(name))
        {
            var tempFolder = Path.GetTempPath();
            currentFileTempPath = Path.Combine(tempFolder, name).Replace("\\", "/");

            using Stream file = File.Create(currentFileTempPath);
            CopyStream(resourceStream, file);
        }

        if (!File.Exists(currentFileTempPath))
        {
            throw new ArgumentException($"Image {name} was not found. Please add the base line image as embedded resource.");
        }

        return currentFileTempPath;
    }

    private string GetResourceByFileName(string name)
    {
        try
        {
            _currentExecutingAssembly ??= _assemblyFacade.GetAssembliesCallChain()[2];
#pragma warning disable CA1310 // Specify StringComparison for correctness
            var resourceName = _currentExecutingAssembly.GetManifestResourceNames().First(str => str.EndsWith(name));
#pragma warning restore CA1310 // Specify StringComparison for correctness
            return resourceName;
        }
        catch (InvalidOperationException e)
        {
            throw new InvalidOperationException($"Resource with name = {name} was not found. Please make sure to include the resource and set it as embedded resource to the test project.", e);
        }
    }

    private void CopyStream(Stream input, Stream output)
    {
        var buffer = new byte[8 * 1024];
        int len;
        while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
        {
            output.Write(buffer, 0, len);
        }
    }
}