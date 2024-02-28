// <copyright file="ImageRecognitionService.cs" company="Automate The Planet Ltd.">
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
// <author>Ventsislav Ivanov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Bellatrix.ImageRecognition.Interfaces;
using Bellatrix.Infrastructure;

namespace Bellatrix.ImageRecognition.Utilities;

internal class ImageRecognitionService : IImageRecognitionService
{
    public virtual Process Start(string args)
    {
        var javaPath = GetJavaPath();
        var sikuliHome = GetSikuliPath();
        var javaArguments = $"-jar \"{sikuliHome}\" {args}";

        var process = new Process();
        process.StartInfo.FileName = javaPath;
        process.StartInfo.Arguments = javaArguments;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();

        return process;
    }

    private string GetSikuliPath()
    {
        var assemblyPath = new AssemblyFacade().GetAssemblyDirectory();
        string sikulixDir = Path.Combine(assemblyPath, "sikulix");
        if (Directory.Exists(sikulixDir))
        {
            return Path.Combine(sikulixDir, "sikulix.jar");
        }

        throw new FileNotFoundException("sikulix.jar not found in the solution. Please install the Bellatrix.ImageRecognition.SikuliX NuGet package.");
    }

    private string GetJavaPath()
    {
        string javaHomePath = Environment.GetEnvironmentVariable("JAVA_HOME", EnvironmentVariableTarget.Machine);

        if (string.IsNullOrEmpty(javaHomePath))
        {
            throw new FileNotFoundException("Java path not found. Is it installed? If yes, set the JAVA_HOME environment variable.");
        }

        if (!javaHomePath.Contains("\\bin") && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            javaHomePath = Path.Combine(javaHomePath, "bin");
        }

        var javaPath = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? Path.Combine(javaHomePath, "java.exe") : Path.Combine(javaHomePath, "java");

        if (!File.Exists(javaPath))
        {
            throw new Exception($"Java executable not found in expected folder: {javaPath}. If you have multiple Java installations, you may want to set the JAVA_HOME environment variable.");
        }

        return javaPath;
    }
}
