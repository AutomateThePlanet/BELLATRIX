﻿// <copyright file="WindowsSizeResolver.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using System.Drawing;
using Bellatrix.Desktop.Configuration;

namespace Bellatrix.Desktop.Services;

public static class WindowsSizeResolver
{
    public static Size GetWindowSize(string resolution)
    {
        var parts = resolution.Split('x');
        Size result = new Size(int.Parse(parts[0]), int.Parse(parts[1]));

        return result;
    }

    public static Size GetWindowSize(DesktopWindowSize windowSize)
    {
        Size result = default(Size);

        switch (windowSize)
        {
            case DesktopWindowSize._1366_768:
                result = new Size(1366, 768);
                break;
            case DesktopWindowSize._1920_1080:
                result = new Size(1920, 1080);
                break;
            case DesktopWindowSize._1440_900:
                result = new Size(1440, 900);
                break;
            case DesktopWindowSize._1600_900:
                result = new Size(1600, 900);
                break;
            case DesktopWindowSize._1280_800:
                result = new Size(1280, 800);
                break;
            case DesktopWindowSize._1280_1024:
                result = new Size(1280, 1024);
                break;
        }

        return result;
    }

    public static Size GetWindowSize(MobileWindowSize windowSize)
    {
        Size result = default(Size);
        switch (windowSize)
        {
            case MobileWindowSize._360_640:
                result = new Size(360, 640);
                break;
            case MobileWindowSize._375_667:
                result = new Size(375, 667);
                break;
            case MobileWindowSize._720_1280:
                result = new Size(720, 1280);
                break;
            case MobileWindowSize._320_568:
                result = new Size(320, 568);
                break;
            case MobileWindowSize._414_736:
                result = new Size(414, 736);
                break;
            case MobileWindowSize._320_534:
                result = new Size(320, 534);
                break;
        }

        return result;
    }

    public static Size GetWindowSize(TabletWindowSize windowSize)
    {
        Size result = default;
        switch (windowSize)
        {
            case TabletWindowSize._768_1024:
                result = new Size(768, 1024);
                break;
            case TabletWindowSize._1280_800:
                result = new Size(1280, 800);
                break;
            case TabletWindowSize._600_1024:
                result = new Size(600, 1024);
                break;
            case TabletWindowSize._601_962:
                result = new Size(601, 962);
                break;
            case TabletWindowSize._800_1280:
                result = new Size(800, 1280);
                break;
            case TabletWindowSize._1024_600:
                result = new Size(1024, 600);
                break;
        }

        return result;
    }

    public static string ConvertToString(this Size size) => $"{size.Width}x{size.Height}";

    public static string ConvertToStringWithColorDepth(this Size size, int colorDepth = 24) => $"{size.Width}x{size.Height}x{colorDepth}";
}
