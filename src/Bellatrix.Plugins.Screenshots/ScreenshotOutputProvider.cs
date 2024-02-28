// <copyright file="ScreenshotOutputProvider.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Plugins.Screenshots.Contracts;
using Bellatrix.Utilities;

namespace Bellatrix.Plugins.Screenshots;

public class ScreenshotOutputProvider : IScreenshotOutputProvider
{
    public virtual string GetOutputFolder()
    {
        var outputDir = ConfigurationService.GetSection<ScreenshotsSettings>().FilePath;

        if (outputDir.StartsWith("ApplicationData", StringComparison.Ordinal))
        {
            var folders = outputDir.Split('\\').ToList();
            folders.RemoveAt(0);
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string newFolderToBeCreated = Path.GetDirectoryName(appData);
            foreach (var currentFolder in folders)
            {
                newFolderToBeCreated = Path.Combine(newFolderToBeCreated ?? throw new InvalidOperationException(), currentFolder);
            }

            outputDir = newFolderToBeCreated;
        }

        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir ?? throw new InvalidOperationException());
        }

        return outputDir;
    }

    public virtual string GetUniqueFileName(string testName)
    {
        string testShortName = string.Concat(testName.Where(c => char.IsUpper(c)));
        return string.Concat(testShortName, Guid.NewGuid().ToString().Substring(0, 4), ".png");
    }
}
