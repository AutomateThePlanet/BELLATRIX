﻿// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using System.Collections.Generic;
using System.Text;
using Bellatrix.BugReporting;
using Bellatrix.BugReporting.Contracts;
using Bellatrix.BugReporting.Jira;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots.Plugins;
using Bellatrix.Plugins.Video.Plugins;

namespace Bellatrix;

public static class JiraBugReportingPlugin
{
    public static void Add()
    {
        ServicesCollection.Current.RegisterInstance(new BugReportingContextService());
        ServicesCollection.Current.RegisterType<IBugReportingService, JiraBugReportingService>();
        ServicesCollection.Current.RegisterType<Plugin, Bellatrix.BugReporting.Core.BugReportingPlugin>(Guid.NewGuid().ToString());
        ServicesCollection.Current.RegisterType<IScreenshotPlugin, Bellatrix.BugReporting.Core.BugReportingPlugin>(Guid.NewGuid().ToString());
        ServicesCollection.Current.RegisterType<IVideoPlugin, Bellatrix.BugReporting.Core.BugReportingPlugin>(Guid.NewGuid().ToString());
    }
}
