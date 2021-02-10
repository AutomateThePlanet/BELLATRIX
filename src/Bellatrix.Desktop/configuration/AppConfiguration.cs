// <copyright file="AppConfiguration.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using System.Drawing;
using System.IO;
using System.Reflection;
using Bellatrix.Utilities;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop.Configuration
{
    public class AppConfiguration : IEquatable<AppConfiguration>
    {
        private string _appPath;

        public AppConfiguration()
        {
        }

        public AppConfiguration(string appPath, AppBehavior appBehavior, Size size, string classFullName, DesiredCapabilities appiumOptions = null)
        {
            AppPath = appPath;
            AppBehavior = appBehavior;
            Size = size;
            ClassFullName = classFullName;
            DesiredCapabilities = appiumOptions;
        }

        public AppBehavior AppBehavior { get; set; } = AppBehavior.RestartEveryTime;

        public Size Size { get; set; }

        public string ClassFullName { get; set; }

        public string AppPath { get => NormalizeAppPath(); set => _appPath = value; }

        public DesiredCapabilities DesiredCapabilities { get; set; }

        public bool Equals(AppConfiguration other)
        {
            return AppPath.Equals(other.AppPath) && AppBehavior.Equals(other.AppBehavior) && Size.Equals(other.Size);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as AppConfiguration);
        }

        private string NormalizeAppPath()
        {
            if (string.IsNullOrEmpty(_appPath))
            {
                return _appPath;
            }
            else if (_appPath.StartsWith("AssemblyFolder", StringComparison.Ordinal))
            {
                var executionFolder = ExecutionDirectoryResolver.GetDriverExecutablePath();
                _appPath = _appPath.Replace("AssemblyFolder", executionFolder);
            }

            return _appPath;
        }
    }
}