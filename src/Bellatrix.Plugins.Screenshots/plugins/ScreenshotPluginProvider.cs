// <copyright file="ScreenshotPluginProvider.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Plugins;

namespace Bellatrix.Plugins.Screenshots;

public class ScreenshotPluginProvider : IScreenshotPluginProvider
{
    public event EventHandler<ScreenshotPluginEventArgs> ScreenshotGeneratedEvent;

    public void ScreenshotGenerated(PluginEventArgs e, string screenshotPath) =>
        RaiseTestEvent(ScreenshotGeneratedEvent, e, screenshotPath);

    private void RaiseTestEvent(EventHandler<ScreenshotPluginEventArgs> eventHandler, PluginEventArgs e, string screenshotPath) =>
        eventHandler?.Invoke(this, new ScreenshotPluginEventArgs(e, screenshotPath));
}