// <copyright file="Image.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.ImageRecognition.Models;

public abstract class Image
{
    private string _path;

    protected Image(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            throw new ArgumentNullException(nameof(path));
        }

        Path = path;
    }

    public string Path
    {
        get => _path;
        set
        {
            ValidatePath(value);
            _path = value;
        }
    }

    protected string ToSukuliFloat(double? timeoutInSeconds)
    {
        return timeoutInSeconds > 0.0 ? ", " + ((double)timeoutInSeconds).ToString("0.####") : string.Empty;
    }

    private void ValidatePath(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Could not find image file specified in pattern: " + path, _path);
        }
    }
}
