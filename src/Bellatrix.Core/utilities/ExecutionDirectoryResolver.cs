// <copyright file="ExecutionDirectoryResolver.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Bellatrix.Utilities;

public static class ExecutionDirectoryResolver
{
    private static string _driverExecutablePath = string.Empty;
    private static string _rootPath = string.Empty;

    public static string GetDriverExecutablePath()
    {
        if (string.IsNullOrEmpty(_driverExecutablePath))
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryInfo = new DirectoryInfo(assemblyFolder);
            var parentDirectories = GetAllParentDirectories(directoryInfo);

            if (parentDirectories.Count(x => x.Equals(directoryInfo.Name)) > 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    directoryInfo = directoryInfo?.Parent;
                }
            }

            _driverExecutablePath = directoryInfo?.FullName;
        }

        return _driverExecutablePath;
    }

    public static string GetRootPath()
    {
        if (string.IsNullOrEmpty(_rootPath))
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var directoryInfo = new DirectoryInfo(assemblyFolder);
            var currentDirectory = directoryInfo;
            var parentDirectory = directoryInfo.Parent;
            var childDirectories = directoryInfo.GetDirectories().ToList();

            while (parentDirectory != null && !childDirectories.Any(x => x.Name.Equals(".vs")))
            {
                currentDirectory = parentDirectory;
                parentDirectory = currentDirectory.Parent;
                childDirectories = currentDirectory.GetDirectories().ToList();
                _rootPath = currentDirectory.FullName;
            }
        }

        return _rootPath;
    }

    private static IEnumerable<string> GetAllParentDirectories(DirectoryInfo directoryToScan)
    {
        var parentFolders = new List<string>();
        GetAllParentDirectories(directoryToScan, parentFolders);
        return parentFolders;
    }

    private static void GetAllParentDirectories(DirectoryInfo directoryToScan, List<string> directories)
    {
        if (directoryToScan == null || directoryToScan.Name == directoryToScan.Root.Name)
        {
            return;
        }

        directories.Add(directoryToScan.Name);
        GetAllParentDirectories(directoryToScan.Parent, directories);
    }
}
