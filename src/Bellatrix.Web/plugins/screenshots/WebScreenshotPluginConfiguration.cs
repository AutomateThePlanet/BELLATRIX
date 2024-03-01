// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Contracts;
using Bellatrix.Web.Screenshots;

namespace Bellatrix.Web.Screenshots;

public static class WebScreenshotPluginConfiguration
{
    public static void UseFullPageScreenshotsOnFail()
    {
        ServicesCollection.Current.RegisterType<IScreenshotEngine, FullPageScreenshotEngine>();
        ServicesCollection.Current.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
        ServicesCollection.Current.RegisterType<IScreenshotPluginProvider, ScreenshotPluginProvider>();
        ServicesCollection.Current.RegisterType<Plugin, ScreenshotPlugin>(Guid.NewGuid().ToString());
    }

    public static void UseVanillaWebDriverScreenshotsOnFail()
    {
        ServicesCollection.Current.RegisterType<IScreenshotEngine, VanillaWebDriverScreenshotEngine>();
        ServicesCollection.Current.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
        ServicesCollection.Current.RegisterType<IScreenshotPluginProvider, ScreenshotPluginProvider>();
        ServicesCollection.Current.RegisterType<Plugin, ScreenshotPlugin>(Guid.NewGuid().ToString());
    }
}
