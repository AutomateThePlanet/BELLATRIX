// <copyright file="TestsInitialize.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
// Licensed under the Royalty-free End-user License Agreement, Version 1.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://bellatrix.solutions/licensing-royalty-free/
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Bellatrix.LoadTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.LoadTesting.Tests
{
    [TestClass]
    public class TestsInitialize : LoadTest
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext testContext)
        {
            UnityInitializationService.Initialize();
            var app = new App(ServiceContainer.Provider);
            app.UseMsTestSettings();
            app.UseLogger();
            app.AssemblyInitialize();
            ////App.UseAllure();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            LoadTest.App.AssemblyCleanup();
        }
    }
}
