// <copyright file="IOSDriverScreenshotEngine.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using System.IO;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Screenshots
{
    public sealed class IOSDriverScreenshotEngine : IScreenshotEngine
    {
        public Image TakeScreenshot(IServicesCollection serviceContainer) => TakeScreenshotIOSDriver(serviceContainer);

        public Image TakeScreenshotIOSDriver(IServicesCollection serviceContainer)
        {
            var driver = serviceContainer.Resolve<IOSDriver<IOSElement>>();
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var image = GetImageFromBase64String(screenshot.AsBase64EncodedString);

            return image;
        }

        private Image GetImageFromBase64String(string pngContent)
        {
            byte[] data = Convert.FromBase64String(pngContent);
            Image image;
            using (var memoryStream = new MemoryStream(data))
            {
                image = Image.FromStream(memoryStream);
            }

            return image;
        }
    }
}