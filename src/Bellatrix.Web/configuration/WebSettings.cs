// <copyright file="WebSettings.cs" company="Automate The Planet Ltd.">
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
namespace Bellatrix.Web
{
    public sealed class WebSettings
    {
        public bool AddUrlToBddLogging { get; set; }
        public bool ShouldScrollToVisibleOnElementFound { get; set; }
        public bool ShouldWaitUntilReadyOnElementFound { get; set; }
        public bool ShouldWaitForAngular { get; set; }
        public bool ShouldhighlightComponents { get; set; }
        public bool FullPageScreenshotsEnabled { get; set; }
        public BrowserSettings Chrome { get; set; }
        public BrowserSettings ChromeHeadless { get; set; }
        public BrowserSettings Firefox { get; set; }
        public BrowserSettings FirefoxHeadless { get; set; }
        public BrowserSettings Edge { get; set; }
        public BrowserSettings Opera { get; set; }
        public BrowserSettings InternetExplorer { get; set; }
        public BrowserSettings Safari { get; set; }
        public RemoteSettings Remote { get; set; }
        public CloudRemoteSettings SauceLabs { get; set; }
        public CloudRemoteSettings BrowserStack { get; set; }
        public CloudRemoteSettings CrossBrowserTesting { get; set; }
    }
}