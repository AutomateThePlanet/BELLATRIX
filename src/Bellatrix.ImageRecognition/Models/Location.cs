// <copyright file="Location.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;

namespace Bellatrix.ImageRecognition.Models;

public class Location
{
    private readonly Point _point;

    public Location(int x, int y)
        : this(new Point(x, y))
    {
    }

    private Location(Point point)
    {
        _point = point;
    }

    public void Validate()
    {
        if (_point.X < 0 || _point.Y < 0)
        {
            throw new ArgumentException("Cannot target a negative position");
        }
    }

    public string ToSikuliScript()
    {
        return $"Location({_point.X},{_point.Y})";
    }
}