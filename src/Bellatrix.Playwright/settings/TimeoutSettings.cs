﻿// <copyright file="TimeoutSettings.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

namespace Bellatrix.Playwright.Settings;

public class TimeoutSettings
{
    public int PageLoadTimeout { get; set; } = 30;
    public int ScriptTimeout { get; set; } = 30;
    public int WaitUntilReadyTimeout { get; set; } = 30;
    public int WaitForJavaScriptAnimationsTimeout { get; set; } = 30;
    public int WaitForAngularTimeout { get; set; } = 30;
    public int WaitForPartialUrl { get; set; } = 30;
    public int ValidationsTimeout { get; set; } = 30;

    public int WaitForAjaxTimeout { get; set; } = 30;
    public int SleepInterval { get; set; } = 1;

    public int ElementToBeVisibleTimeout { get; set; } = 30;
    public int ElementToExistTimeout { get; set; } = 30;
    public int ElementToNotExistTimeout { get; set; } = 10;
    public int ElementToBeClickableTimeout { get; set; } = 30;
    public int ElementNotToBeVisibleTimeout { get; set; } = 10;
    public int ElementToHaveContentTimeout { get; set; } = 30;
    public int ActionTimeoutWhenHandlingDialogs { get; set; } = 10;
}