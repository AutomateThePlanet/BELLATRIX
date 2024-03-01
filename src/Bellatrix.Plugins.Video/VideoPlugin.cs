// <copyright file="VideoWorkflowPlugin.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Video.Contracts;
using Bellatrix.Plugins.Video.Plugins;
using Bellatrix.Utilities;
using HtmlAgilityPack;

namespace Bellatrix.Plugins.Video;

public class VideoPlugin : Plugin
{
    private readonly IVideoRecorderOutputProvider _videoRecorderOutputProvider;
    private readonly IVideoPluginProvider _videoPluginProvider;
    private IVideoRecorder _videoRecorder;
    private bool _isEnabled;

    public VideoPlugin(IVideoRecorder videoRecorder, IVideoRecorderOutputProvider videoRecorderOutputProvider, IVideoPluginProvider videoPluginProvider)
    {
        _isEnabled = ConfigurationService.GetSection<VideoRecordingSettings>().IsEnabled;
        _videoRecorder = videoRecorder;
        _videoRecorderOutputProvider = videoRecorderOutputProvider;
        _videoPluginProvider = videoPluginProvider;
        InitializeVideoProviderObservers();
    }

    protected override void PostTestInit(object sender, PluginEventArgs e)
    {
        if (_isEnabled)
        {
            string cleanTestName = e.TestName.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Replace(",", string.Empty).Replace("\"", string.Empty);
            var fullTestName = $"{e.TestMethodMemberInfo.DeclaringType.Name}.{cleanTestName}";
            var videoRecordingDir = _videoRecorderOutputProvider.GetOutputFolder();
            var videoRecordingFileName = _videoRecorderOutputProvider.GetUniqueFileName(fullTestName);

            string videoRecordingPath = _videoRecorder.Record(videoRecordingDir, videoRecordingFileName);
            e.Container.RegisterInstance(videoRecordingPath, "_videoRecordingPath");
            e.Container.RegisterInstance(_videoRecorder, "_videoRecorder");
        }
    }

    protected override void PostTestCleanup(object sender, PluginEventArgs e)
    {
        if (_isEnabled)
        {
            bool hasTestPassed = e.TestOutcome.Equals(TestOutcome.Passed);
            bool isFileDeleted = false;
            try
            {
                string videoRecordingPath = e.Container.Resolve<string>("_videoRecordingPath");
                isFileDeleted = DeleteVideoDependingOnTestOutcome(hasTestPassed, videoRecordingPath);
            }
            finally
            {
                _videoRecorder = e.Container.Resolve<IVideoRecorder>("_videoRecorder");
                _videoRecorder?.Dispose();
                if (!isFileDeleted)
                {
                    string videoRecordingPath = e.Container.Resolve<string>("_videoRecordingPath");
                    _videoPluginProvider.VideoGenerated(e, videoRecordingPath);
                }
            }
        }
    }

    private bool DeleteVideoDependingOnTestOutcome(bool haveTestPassed, string videoRecordingPath)
    {
        bool isFileDeleted = false;
        if (_isEnabled)
        {
            // Release the video file then delete it.
            _videoRecorder?.Stop();
            if (haveTestPassed && File.Exists(videoRecordingPath))
            {
                try
                {
                    File.Delete(videoRecordingPath);
                    isFileDeleted = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        return isFileDeleted;
    }

    private void InitializeVideoProviderObservers()
    {
        var observers = ServicesCollection.Current.ResolveAll<IVideoPlugin>();
        foreach (var observer in observers)
        {
            observer.SubscribeVideoPlugin(_videoPluginProvider);
        }
    }
}