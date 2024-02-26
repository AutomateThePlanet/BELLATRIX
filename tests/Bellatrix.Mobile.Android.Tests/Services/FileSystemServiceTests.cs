// <copyright file="FileSystemServiceTests.cs" company="Automate The Planet Ltd.">
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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.Tests;

[TestClass]
[Android(Constants.AndroidNativeAppPath,
    Constants.AndroidNativeAppId,
    Constants.AndroidDefaultAndroidVersion,
    Constants.AndroidDefaultDeviceName,
    ".ApiDemos",
    Lifecycle.ReuseIfStarted)]
[AllureSuite("Services")]
[AllureFeature("FileSystemService")]
public class FileSystemServiceTests : MSTest.AndroidTest
{
    [TestMethod]
    [TestCategory(Categories.CI)]
    public void FileSavedToDevice_When_CallPushFile()
    {
        string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
        App.Files.PushFile("/data/local/tmp/remote.txt", data);

        byte[] returnDataBytes = App.Files.PullFile("/data/local/tmp/remote.txt");
        string returnedData = Encoding.UTF8.GetString(returnDataBytes);

        Assert.AreEqual(data, returnedData);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void FileSavedToDevice_When_CallPushFileFromBytes()
    {
        string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
        var bytes = Encoding.UTF8.GetBytes(data);

        App.Files.PushFile("/data/local/tmp/remote.txt", bytes);

        byte[] returnDataBytes = App.Files.PullFile("/data/local/tmp/remote.txt");
        string returnedData = Encoding.UTF8.GetString(returnDataBytes);

        Assert.AreEqual(data, returnedData);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void FileSavedToDevice_When_CallPushFileFromFileInfo()
    {
        string filePath = Path.GetTempPath();
        var fileName = Guid.NewGuid().ToString();
        string fullPath = Path.Combine(filePath, fileName);

        File.WriteAllText(fullPath,
            "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra");

        try
        {
            var file = new FileInfo(fullPath);
            App.Files.PushFile("/data/local/tmp/remote.txt", file);

            byte[] returnDataBytes = App.Files.PullFile("/data/local/tmp/remote.txt");
            string returnedData = Encoding.UTF8.GetString(returnDataBytes);
            Assert.AreEqual(
                "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra",
                returnedData);
        }
        finally
        {
            File.Delete(fullPath);
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.KnownIssue)]
    public void AllFilesReturned_When_CallPullFolder()
    {
        string data = "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
        App.Files.PushFile("\\data\\local\\tmp\\remote.txt", data);

        byte[] returnDataBytes = App.Files.PullFolder("\\data\\local\\tmp\\");

        Assert.IsTrue(returnDataBytes.Length > 0);
    }
}