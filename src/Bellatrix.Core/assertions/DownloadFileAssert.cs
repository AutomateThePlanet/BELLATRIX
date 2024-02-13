// <copyright file="DownloadFileAssert.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Bellatrix.Utilities;

namespace Bellatrix.Assertions;

public static class DownloadFileAssert
{
    private static bool _isCreated;
    private static ThreadLocal<FileSystemWatcher> _fileSystemWatcher;
    private static ThreadLocal<string> _fileNameToBeDownloaded;

    public static string DownloadedFileFullPath { get; private set; }

    public static void DownloadFile(Action downloadFile, string fileNameToBeDownloaded)
    {
        InitializeDownloadFileWatcher();

        _fileNameToBeDownloaded = new ThreadLocal<string>(() => fileNameToBeDownloaded);

        downloadFile();
    }

    public static void WaitForFileToBeDownloaded(int timeoutInSeconds = 30)
    {
        Wait.Until(() => _isCreated, timeoutInSeconds: timeoutInSeconds, "The downloaded file took too long to be downloaded or wasn't downloaded at all.");
    }

    public static FileInfo WaitForFileToBeDownloaded(string fileNameToBeDownloaded, int timeoutInSeconds = 30)
    {
        DownloadedFileFullPath = Path.Combine(GetSystemDownloadsPath(), fileNameToBeDownloaded);
        Wait.Until(() => File.Exists(DownloadedFileFullPath), timeoutInSeconds: timeoutInSeconds, "The downloaded file took too long to be downloaded or wasn't downloaded at all.");

        return new FileInfo(DownloadedFileFullPath);
    }

    public static void DeleteDownloadedFile()
    {
        if (File.Exists(DownloadedFileFullPath))
        {
            File.Delete(DownloadedFileFullPath);
            DownloadedFileFullPath = string.Empty;
        }
    }
    public static FileInfo GetLastDownloadedFile(string fileExtention, int timeoutInSeconds = 30)
    {
        var directory = new DirectoryInfo(DownloadFileAssert.GetSystemDownloadsPath());
        var actualFile = directory.GetFiles().Where(x => x.Extension == fileExtention).OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
        DownloadedFileFullPath = Path.Combine(GetSystemDownloadsPath(), actualFile.Name);
        Wait.Until(() => File.Exists(DownloadedFileFullPath), timeoutInSeconds: timeoutInSeconds, "The downloaded file took too long to be downloaded or wasn't downloaded at all.");

        return new FileInfo(DownloadedFileFullPath);
    }

    public static void Dispose()
    {
        _fileSystemWatcher?.Dispose();
        _isCreated = false;
        _fileNameToBeDownloaded.Value = string.Empty;
    }

    private static void InitializeDownloadFileWatcher()
    {
        _fileSystemWatcher = new ThreadLocal<FileSystemWatcher>(() => new FileSystemWatcher(GetSystemDownloadsPath()));

        _fileSystemWatcher.Value.Changed += FilesChanged;
        _fileSystemWatcher.Value.EnableRaisingEvents = true;
    }

    private static void FilesChanged(object sender, FileSystemEventArgs e)
    {
        string fileName = Path.GetFileName(_fileNameToBeDownloaded.Value);
        string changedFileName = Path.GetFileName(e.FullPath);

        if (_isCreated || !string.IsNullOrEmpty(DownloadedFileFullPath))
        {
            return;
        }

        if (File.Exists(e.FullPath) && !string.IsNullOrEmpty(changedFileName) && changedFileName.Equals(fileName))
        {
            DownloadedFileFullPath = e.FullPath;
            _isCreated = true;
        }
    }

    public static string GetSystemDownloadsPath()
    {
        string path;

        bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        bool isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

        if (isWindows)
        {
            path = Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads");
        }
        else if (isLinux)
        {
            path = Path.Combine("home", Environment.UserName, "Downloads");
        }
        else
        {
            path = Path.Combine("Users", Environment.UserName, "Downloads");
        }

        return path;
    }
}