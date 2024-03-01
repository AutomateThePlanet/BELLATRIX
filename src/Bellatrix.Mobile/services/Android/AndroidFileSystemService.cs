// <copyright file="AndroidFileSystemService.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Services.Android;

public class AndroidFileSystemService : FileSystemService<AndroidDriver, AppiumElement>
{
    public AndroidFileSystemService(AndroidDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public void PushFile(string pathOnDevice, string stringData) => WrappedAppiumDriver.PushFile(pathOnDevice, stringData);
    public void PushFile(string pathOnDevice, FileInfo file) => WrappedAppiumDriver.PushFile(pathOnDevice, file);
    public void PushFile(string pathOnDevice, byte[] base64Data) => WrappedAppiumDriver.PushFile(pathOnDevice, base64Data);
}
