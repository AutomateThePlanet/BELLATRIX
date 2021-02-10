// <copyright file="VideoWorkflowPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using Bellatrix.TestExecutionExtensions.Video.Contracts;
using Bellatrix.TestExecutionExtensions.Video.Plugins;
using Bellatrix.TestWorkflowPlugins;
using Bellatrix.Utilities;
using HtmlAgilityPack;

namespace Bellatrix.TestExecutionExtensions.Video
{
    public class VideoWorkflowPlugin : Plugin
    {
        private readonly IVideoRecorderOutputProvider _videoRecorderOutputProvider;
        private readonly IVideoPluginProvider _videoPluginProvider;
        private IVideoRecorder _videoRecorder;
        private VideoRecordingMode _recordingMode;

        public VideoWorkflowPlugin(IVideoRecorder videoRecorder, IVideoRecorderOutputProvider videoRecorderOutputProvider, IVideoPluginProvider videoPluginProvider)
        {
            _videoRecorder = videoRecorder;
            _videoRecorderOutputProvider = videoRecorderOutputProvider;
            _videoPluginProvider = videoPluginProvider;
            InitializeVideoProviderObservers();
        }

        protected override void PostTestInit(object sender, TestWorkflowPluginEventArgs e)
        {
            _recordingMode = ConfigureTestVideoRecordingMode(e.TestMethodMemberInfo);

            if (_recordingMode != VideoRecordingMode.DoNotRecord)
            {
                var fullTestName = $"{e.TestMethodMemberInfo.DeclaringType.Name}.{e.TestName}";
                var videoRecordingDir = _videoRecorderOutputProvider.GetOutputFolder();
                var videoRecordingFileName = _videoRecorderOutputProvider.GetUniqueFileName(fullTestName);

                string videoRecordingPath = _videoRecorder.Record(videoRecordingDir, videoRecordingFileName);
                e.Container.RegisterInstance(videoRecordingPath, "_videoRecordingPath");
                e.Container.RegisterInstance(_videoRecorder, "_videoRecorder");
            }
        }

        protected override void PostTestCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
            if (_recordingMode != VideoRecordingMode.DoNotRecord)
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
            if (_recordingMode != VideoRecordingMode.DoNotRecord)
            {
                bool shouldRecordAlways = _recordingMode == VideoRecordingMode.Always;
                bool shouldRecordAllPassedTests = haveTestPassed && _recordingMode.Equals(VideoRecordingMode.OnlyPass);
                bool shouldRecordAllFailedTests = !haveTestPassed && _recordingMode.Equals(VideoRecordingMode.OnlyFail);
                if (!(shouldRecordAlways || shouldRecordAllPassedTests || shouldRecordAllFailedTests))
                {
                    // Release the video file then delete it.
                    _videoRecorder?.Stop();

                    if (File.Exists(videoRecordingPath))
                    {
                        File.Delete(videoRecordingPath);
                        isFileDeleted = true;
                    }
                }
            }

            return isFileDeleted;
        }

        private VideoRecordingMode ConfigureTestVideoRecordingMode(MemberInfo memberInfo)
        {
            var methodRecordingMode = GetVideoRecordingModeByMethodInfo(memberInfo);
            var classRecordingMode = GetVideoRecordingModeType(memberInfo.DeclaringType);
            var videoRecordingMode = VideoRecordingMode.DoNotRecord;
            bool shouldTakeVideos = SettingsService.GetSection<VideoRecordingSettings>().IsEnabled;

            if (!shouldTakeVideos)
            {
                videoRecordingMode = VideoRecordingMode.DoNotRecord;
            }
            else if (methodRecordingMode != VideoRecordingMode.Ignore)
            {
                videoRecordingMode = methodRecordingMode;
            }
            else if (classRecordingMode != VideoRecordingMode.Ignore)
            {
                videoRecordingMode = classRecordingMode;
            }

            return videoRecordingMode;
        }

        private VideoRecordingMode GetVideoRecordingModeByMethodInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException();
            }

            var recordingModeMethodAttribute = memberInfo.GetCustomAttribute<VideoRecordingAttribute>(true);
            if (recordingModeMethodAttribute != null)
            {
                return recordingModeMethodAttribute.VideoRecording;
            }

            return VideoRecordingMode.Ignore;
        }

        private VideoRecordingMode GetVideoRecordingModeType(System.Type currentType)
        {
            if (currentType == null)
            {
                throw new System.ArgumentNullException();
            }

            var recordingModeClassAttribute = currentType.GetCustomAttribute<VideoRecordingAttribute>(true);
            if (recordingModeClassAttribute != null)
            {
                return recordingModeClassAttribute.VideoRecording;
            }

            return VideoRecordingMode.Ignore;
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
}