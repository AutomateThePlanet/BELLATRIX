// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Plugins;
using Bellatrix.Plugins.Video;
using Bellatrix.Plugins.Video.Contracts;
using Bellatrix.Plugins.Video.Plugins;
using Bellatrix.Results.MSTest;
using Bellatrix.Results.NUnit;
using Bellatrix.VideoRecording.FFmpeg;

namespace Bellatrix;

public static class VideoRecorderPluginConfiguration
{
    public static void AddMSTest()
    {
        Add();
        ServicesCollection.Current.RegisterType<IVideoPlugin, MSTestResultsWorkflowPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddNUnit()
    {
        Add();
        ServicesCollection.Current.RegisterType<IVideoPlugin, NUnitResultsWorkflowPlugin>(Guid.NewGuid().ToString());
    }

    private static void Add()
    {
        ServicesCollection.Current.RegisterType<IVideoRecorder, FFmpegVideoRecorder>();
        ServicesCollection.Current.RegisterType<IVideoRecorderOutputProvider, VideoRecorderOutputProvider>();
        ServicesCollection.Current.RegisterType<IVideoPluginProvider, VideoPluginProvider>();
        ServicesCollection.Current.RegisterType<Plugin, VideoPlugin>(Guid.NewGuid().ToString());
    }
}
