// <copyright file="FFmpegVideoRecorder.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Bellatrix.Core.Utilities;
using Bellatrix.Plugins.Video;
using Bellatrix.Plugins.Video.Contracts;
using Bellatrix.Utilities;

namespace Bellatrix.VideoRecording.FFmpeg;

public class FFmpegVideoRecorder : IVideoRecorder
{
    private Process _recorderProcess;
    private bool _isRunning;

    public void Dispose()
    {
        if (_isRunning)
        {
            // Control with setting. Waits a little bit after the recording has finished.
            Thread.Sleep(ConfigurationService.GetSection<VideoRecordingSettings>().WaitAfterFinishRecordingMilliseconds);
            if (!_recorderProcess.HasExited)
            {
                _recorderProcess?.Kill();
                _recorderProcess?.WaitForExit();
            }

            _isRunning = false;
        }

        GC.SuppressFinalize(this);
    }

    public string Record(string filePath, string fileName)
    {
        string videoPath = $"{Path.Combine(filePath, fileName)}";
        string videoFilePathWithExtension = GetFilePathWithExtensionByOS(videoPath);
        try
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"A problem occurred trying to initialize the create the directory you have specified. - {filePath}", ex);
        }

        if (!_isRunning)
        {
            // TODO: add different options for the different OS
            // TODO: add settings classes to control the quality
            var startInfo = GetProcessStartInfoByOS(videoFilePathWithExtension);

            _recorderProcess = new Process
                               {
                                   StartInfo = startInfo,
                               };
            ProcessProvider.StartProcess(_recorderProcess,
                s =>
                {
                    Debug.WriteLine(s);
                    Console.WriteLine(s);
                },
                er =>
                {
                    var exception = new Exception(er);
                    exception.PrintStackTrace();
                });
            _isRunning = true;
        }

        return videoFilePathWithExtension;
    }

    public void Stop()
    {
        Dispose();
    }

    private ProcessStartInfo GetProcessStartInfoByOS(string videoFilePathWithExtension)
    {
        var startInfo = new ProcessStartInfo
        {
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            startInfo.Arguments = $"-f gdigrab -framerate 30 -i desktop {videoFilePathWithExtension}";
            startInfo.FileName = FileDownloader.DownloadToAppData("https://github.com/AutomateThePlanet/BELLATRIX/releases/download/1.0/ffmpeg_windows.exe");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            startInfo.Arguments = $"-f avfoundation -framerate 10 -i \"0:0\" {videoFilePathWithExtension}";
            startInfo.FileName = FileDownloader.DownloadToAppData("https://github.com/AutomateThePlanet/BELLATRIX/releases/download/1.0/ffmpeg_osx");
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            startInfo.Arguments = $"-f x11grab -framerate 30 -i :0.0+100,200 {videoFilePathWithExtension}";
            startInfo.FileName = FileDownloader.DownloadToAppData("https://github.com/AutomateThePlanet/BELLATRIX/releases/download/1.0/ffmpeg_linux");
        }
        else
        {
            throw new NotSupportedException("The OS is not supported by FFmpeg video recorder. Currently supported OS are Windows, MacOS, Linux.");
        }

        return startInfo;
    }

    private string GetFilePathWithExtensionByOS(string videoPathNoExtension)
    {
        string videoPathWithExtension;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            videoPathWithExtension = $"{videoPathNoExtension}.mpg";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            videoPathWithExtension = $"{videoPathNoExtension}.mkv";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            videoPathWithExtension = $"{videoPathNoExtension}.mp4";
        }
        else
        {
            throw new NotSupportedException("The OS is not supported by FFmpeg video recorder. Currently supported OS are Windows, MacOS, Linux.");
        }

        return videoPathWithExtension;
    }
}
