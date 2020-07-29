// <copyright file="FullPageScreenshotEngine.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Reflection;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using OpenQA.Selenium.Support.UI;

namespace Bellatrix.Web.Screenshots
{
    public sealed class FullPageScreenshotEngine : IScreenshotEngine
    {
        private const string GenerateScreenshotJavaScript = @"function genScreenshot () {
	                                                var canvasImgContentDecoded;
                                                    html2canvas(document.body).then(function(canvas) {
                                                         window.canvasImgContentDecoded = canvas.toDataURL(""image/png"");
                                                    });
                                                }
                                                genScreenshot();";

        public System.Drawing.Image TakeScreenshot(IServicesCollection serviceContainer) => TakeScreenshotHtml2Canvas(serviceContainer);

        public System.Drawing.Image TakeScreenshotHtml2Canvas(IServicesCollection serviceContainer)
        {
            var html2CanvasContent = GetEmbeddedResource("html2canvas.js", Assembly.GetExecutingAssembly());
            var javaScriptService = serviceContainer.Resolve<JavaScriptService>();
            var wait = new WebDriverWait(javaScriptService.WrappedDriver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(InvalidOperationException));
            wait.IgnoreExceptionTypes(typeof(ArgumentNullException));

            javaScriptService.Execute(html2CanvasContent);
            javaScriptService.Execute(GenerateScreenshotJavaScript);
            wait.Until(
                wd =>
                {
                    string response = (string)javaScriptService.Execute("return (typeof canvasImgContentDecoded === 'undefined' || canvasImgContentDecoded === null)");
                    if (string.IsNullOrEmpty(response))
                    {
                        return false;
                    }

                    return bool.Parse(response);
                });
            wait.Until(wd => !string.IsNullOrEmpty((string)javaScriptService.Execute("return canvasImgContentDecoded;")));
            var pngContent = (string)javaScriptService.Execute("return canvasImgContentDecoded;");
            pngContent = pngContent.Replace("data:image/png;base64,", string.Empty);
            var image = GetImageFromBase64String(pngContent);

            return image;
        }

        private System.Drawing.Image GetImageFromBase64String(string pngContent)
        {
            byte[] data = Convert.FromBase64String(pngContent);
            System.Drawing.Image image;
            using (var memoryStream = new MemoryStream(data))
            {
                image = System.Drawing.Image.FromStream(memoryStream);
            }

            return image;
        }

        public string GetEmbeddedResource(string resourceName, Assembly assembly)
        {
            resourceName = FormatResourceName(assembly, resourceName);
            using var resourceStream = assembly.GetManifestResourceStream(resourceName);
            if (resourceStream == null)
            {
                return null;
            }

            using var reader = new StreamReader(resourceStream);
            return reader.ReadToEnd();
        }

        private string FormatResourceName(Assembly assembly, string resourceName)
        {
            return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                               .Replace("\\", ".")
                                                               .Replace("/", ".");
        }
    }
}