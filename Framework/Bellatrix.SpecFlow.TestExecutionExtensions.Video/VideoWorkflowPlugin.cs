// <copyright file="VideoWorkflowPlugin.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System.IO;
using Bellatrix.SpecFlow;
using Bellatrix.SpecFlow.TestExecutionExtensions.Video;
using Bellatrix.SpecFlow.TestExecutionExtensions.Video;
using Bellatrix.SpecFlow.TestWorkflowPlugins;
using Bellatrix.TestExecutionExtensions.Video.Contracts;
using TechTalk.SpecFlow;

namespace Bellatrix.SpecFlow.TestExecutionExtensions.Video
{
    // The tag should be in the format VideoRecording-OnlyFail - no matter the casing.
    [Binding]
    public class VideoWorkflowPlugin : TestWorkflowPlugin
    {
        private readonly IVideoRecorder _videoRecorder;
        private readonly IVideoRecorderOutputProvider _videoRecorderOutputProvider;
        private readonly IVideoPluginProvider _videoPluginProvider;
        private string _videoRecordingPath;

        public VideoWorkflowPlugin(IVideoRecorder videoRecorder, IVideoRecorderOutputProvider videoRecorderOutputProvider, IVideoPluginProvider videoPluginProvider)
        {
            _videoRecorder = videoRecorder;
            _videoRecorderOutputProvider = videoRecorderOutputProvider;
            _videoPluginProvider = videoPluginProvider;
            InitializeVideoProviderObservers();
        }

        protected override void PreBeforeScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            VideoWorkflowPluginContext.RecordingMode = ConfigurationService.Instance.GetVideoSettings().IsEnabled ? VideoWorkflowPluginContext.RecordingMode : VideoRecordingMode.DoNotRecord;

            if (VideoWorkflowPluginContext.RecordingMode != VideoRecordingMode.DoNotRecord)
            {
                var videoRecordingDir = _videoRecorderOutputProvider.GetOutputFolder();
                var videoRecordingFileName = _videoRecorderOutputProvider.GetUniqueFileName(e.TestFullName).Replace(" ", "_");

                _videoRecordingPath = _videoRecorder.Record(videoRecordingDir, videoRecordingFileName);
            }
        }

        protected override void PostAfterScenario(object sender, TestWorkflowPluginEventArgs e)
        {
            if (VideoWorkflowPluginContext.RecordingMode != VideoRecordingMode.DoNotRecord)
            {
                try
                {
                    bool hasTestPassed = e.TestOutcome.Equals(TestOutcome.Passed);
                    DeleteVideoDependingOnTestOutcome(hasTestPassed);
                }
                finally
                {
                    _videoRecorder.Dispose();
                    if (File.Exists(_videoRecordingPath))
                    {
                        _videoPluginProvider.VideoGenerated(e, _videoRecordingPath);
                    }
                }
            }
        }

        private bool DeleteVideoDependingOnTestOutcome(bool haveTestPassed)
        {
            bool isFileDeleted = false;
            if (VideoWorkflowPluginContext.RecordingMode != VideoRecordingMode.DoNotRecord)
            {
                bool shouldRecordAlways = VideoWorkflowPluginContext.RecordingMode == VideoRecordingMode.Always;
                bool shouldRecordAllPassedTests = haveTestPassed && VideoWorkflowPluginContext.RecordingMode.Equals(VideoRecordingMode.OnlyPass);
                bool shouldRecordAllFailedTests = !haveTestPassed && VideoWorkflowPluginContext.RecordingMode.Equals(VideoRecordingMode.OnlyFail);
                if (!(shouldRecordAlways || shouldRecordAllPassedTests || shouldRecordAllFailedTests))
                {
                    // Release the video file then delete it.
                    _videoRecorder.Stop();
                    if (File.Exists(_videoRecordingPath))
                    {
                        File.Delete(_videoRecordingPath);
                        isFileDeleted = true;
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
}