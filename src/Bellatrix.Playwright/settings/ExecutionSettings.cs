// <copyright file="BrowserSettings.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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

using Microsoft.Extensions.Configuration;

namespace Bellatrix.Playwright.Settings;

public class ExecutionSettings
{
    public string ExecutionType { get; set; }
    public string DefaultBrowser { get; set; }
    public string BrowserVersion { get; set; }
    public string DefaultLifeCycle { get; set; }
    public string Resolution { get; set; }
    public string GridUrl { get; set; }
    public string FileRemoteLocation { get; set; }
    public List<Capabilities> Arguments { get; set; }
    public string PackedExtensionPath { get; set; }
    public string UnpackedExtensionPath { get; set; }
    public bool IsCloudRun { get; set; }
}

public class Capabilities
{
    public string BrowserName { get; set; }

    public string BrowserVersion { get; set; }

    public string PlatformName { get; set; }

    public string AcceptInsecureCerts { get; set; }

    public string PageLoadStrategy { get; set; }

    public string SetWindowRect { get; set; }

    public string UnhandledPromptBehaviour { get; set; }


    [ConfigurationKeyName("LT:Options")]
    public Dictionary<string, object> LtOptions { get; set; }


    [ConfigurationKeyName("browserstack.username")]
    public string Username { get; set; }


    [ConfigurationKeyName("browserstack.accessKey")]
    public string AccessKey { get; set; }
}