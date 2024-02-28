// <copyright file="ImageFactory.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using Bellatrix.ImageRecognition.Configuration;
using Bellatrix.ImageRecognition.Interfaces;
using Bellatrix.ImageRecognition.Models;
using Bellatrix.Infrastructure;
using Bellatrix.Infrastructure.SystemFacades;

namespace Bellatrix.ImageRecognition.Utilities;

public static class ImageFactory
{
    private static readonly EmbeddedResourcesService EmbeddedResourcesService;
    private static readonly double DefaultSimilarity = ConfigurationService.GetSection<ImageRecognitionSettings>().DefaultSimilarity;

    static ImageFactory()
    {
        EmbeddedResourcesService = new EmbeddedResourcesService();
    }

    public static IImage FromFile(string name, double? similarity = null)
    {
        if (similarity == null)
        {
            similarity = DefaultSimilarity;
        }

        string currentFileTempPath = EmbeddedResourcesService.FromFile(name, "png");

        if (!currentFileTempPath.EndsWith(".png"))
        {
            string destinationFileName = Path.ChangeExtension(currentFileTempPath, ".png");
            if (File.Exists(destinationFileName))
            {
                File.Delete(destinationFileName);
            }

            File.Move(currentFileTempPath, destinationFileName);
            currentFileTempPath = Path.ChangeExtension(currentFileTempPath, "png");
        }

        if (!File.Exists(currentFileTempPath))
        {
            throw new ArgumentException($"Image {name} was not found. Please add the base line image as embedded resource.");
        }

        return new FileImage(currentFileTempPath, (double)similarity);
    }

    public static Location Location(int x, int y)
    {
        return new Location(x, y);
    }
}