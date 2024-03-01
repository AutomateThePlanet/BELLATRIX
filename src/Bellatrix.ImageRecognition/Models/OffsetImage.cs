// <copyright file="OffsetImage.cs" company="Automate The Planet Ltd.">
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

using System.Drawing;
using Bellatrix.ImageRecognition.Interfaces;

namespace Bellatrix.ImageRecognition.Models;

public class OffsetImage : Image, IImage
{
    private readonly IImage _pattern;
    private readonly Point _offset;

    public OffsetImage(IImage pattern, Point offset)
        : base(pattern.Path)
    {
        _pattern = pattern;
        _offset = offset;
    }

    public string ToSikuliScript(string command, double? commandParameter)
    {
        return $"print \"SIKULI#: YES\" if {command}({GeneratePatternString()}{ToSukuliFloat(commandParameter)}) else \"SIKULI#: NO\"";
    }

    public string GeneratePatternString()
    {
        return $"{_pattern.GeneratePatternString()}.targetOffset({_offset.X}, {_offset.Y})";
    }
}
