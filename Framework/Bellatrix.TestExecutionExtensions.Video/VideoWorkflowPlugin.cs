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
    public class VideoWorkflowPlugin : TestWorkflowPlugin
    {
        private readonly IVideoRecorder _videoRecorder;
        private readonly IVideoRecorderOutputProvider _videoRecorderOutputProvider;
        private readonly IVideoPluginProvider _videoPluginProvider;
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

                bool isParallelRun = ServicesCollection.Current.Resolve<bool>("isParallelRun");
                if (!isParallelRun)
                {
                    string videoRecordingPath = _videoRecorder.Record(videoRecordingDir, videoRecordingFileName);
                    e.Container.RegisterInstance(videoRecordingPath, "_videoRecordingPath");
                }
            }
        }

        protected override void PostTestCleanup(object sender, TestWorkflowPluginEventArgs e)
        {
            bool isParallelRun = ServicesCollection.Current.Resolve<bool>("isParallelRun");

            if (isParallelRun)
            {
                try
                {
                    var gridUri = e.Container.Resolve<string>("GridUri")?.Replace("wd/hub", string.Empty);
                    if (string.IsNullOrEmpty(gridUri))
                    {
                        return;
                    }

                    var gridVideoUri = new Uri(new Uri(gridUri), "video");
                    var sessionId = e.Container.Resolve<string>("SessionId");

                    // It is Selenoid Run. Later add SauceLabs and others.
                    if (Ping(gridVideoUri))
                    {
                        var fullTestName = $"{e.TestMethodMemberInfo.DeclaringType.Name}.{e.TestName}";
                        var videoRecordingDir = _videoRecorderOutputProvider.GetOutputFolder();
                        var videoRecordingFileName = _videoRecorderOutputProvider.GetUniqueFileName(fullTestName);
                        string videoPath = $"{Path.Combine(videoRecordingDir, videoRecordingFileName)}.mp4";
                        e.Container.RegisterInstance(videoPath, "_videoRecordingPath");
                        Wait.Until(() =>
                            {
                                var hw = new HtmlWeb();
                                HtmlDocument doc = hw.Load(gridVideoUri);
                                var lastNode = doc.DocumentNode.SelectNodes("//a[@href]");
                                string videoName = lastNode.Last().InnerText;
                                return !videoName.Contains("selenoid");
                            }, retryRateDelay: 30);
                        var hw = new HtmlWeb();
                        HtmlDocument doc = hw.Load(gridVideoUri);
                        var lastNode = doc.DocumentNode.SelectNodes("//a[@href]");
                        string videoName = lastNode.Last().InnerText;
                        var sessionVideoUri = new Uri(gridVideoUri, $"video/{videoName}");
                        DownloadFile(sessionVideoUri, videoPath);

                        // Cut the video to the latest test time- https://stackoverflow.com/questions/5041910/how-to-cut-crop-trim-a-video-in-respect-with-time-or-percentage-and-save-output
                        // Save test time in the container by testFullName
                        DeleteVideo(sessionVideoUri);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }

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
                    _videoRecorder.Dispose();
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
            bool shouldTakeVideos = ConfigurationService.Instance.GetVideoSettings().IsEnabled;

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

        private void DownloadFile(Uri uri, string filePath)
        {
            using var myWebClient = new WebClient();
            myWebClient.DownloadFile(uri, filePath);
        }

        private void DeleteVideo(Uri uri)
        {
            var request = WebRequest.Create(uri);
            request.Method = "DELETE";
            var response = (HttpWebResponse)request.GetResponse();
        }

        private bool Ping(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            using var response = (HttpWebResponse)request.GetResponse();
            using var stream = response.GetResponseStream();
            using var reader = new StreamReader(stream);
            return ((int)response.StatusCode >= 200) && ((int)response.StatusCode <= 299);
        }
    }
}