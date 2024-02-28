// <copyright file="TestsInitialize.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Api;
using Bellatrix.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;

namespace Bellatrix.API.Tests;

[TestClass]
public class TestsInitialize
{
    private static Process _testApiProcess;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {
        AllurePlugin.Add();
        string workingDir = Path.Combine(ProcessProvider.GetEntryProcessApplicationPath(), "Demos", "TestAPI");
        _testApiProcess = ProcessProvider.StartProcess("dotnet", workingDir, " run", true);
        ProcessProvider.WaitPortToGetBusy(55215);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanUp()
    {
        ProcessProvider.CloseProcess(_testApiProcess);
    }
}