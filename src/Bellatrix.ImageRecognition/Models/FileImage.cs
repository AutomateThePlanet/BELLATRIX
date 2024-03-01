// <copyright file="FileImage.cs" company="Automate The Planet Ltd.">
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
using System.Globalization;
using Bellatrix.ImageRecognition.Interfaces;

namespace Bellatrix.ImageRecognition.Models;

public class FileImage : Image, IImage
{
    private readonly double _similarity;

    public FileImage(string path, double similarity)
        : base(path)
    {
        if (similarity <= 0 || similarity >= 1)
        {
            throw new ArgumentOutOfRangeException(nameof(similarity));
        }

        _similarity = similarity;
    }

    public string ToSikuliScript(string command, double? commandParameter)
    {
        return $"print \"SIKULI#: YES\" if {command}({GeneratePatternString()}{ToSukuliFloat(commandParameter)}) else \"SIKULI#: NO\"";
    }

    public string GeneratePatternString()
    {
        return string.Format(NumberFormatInfo.InvariantInfo, $"Pattern(\"{Path}\").similar({_similarity})");
    }
}
