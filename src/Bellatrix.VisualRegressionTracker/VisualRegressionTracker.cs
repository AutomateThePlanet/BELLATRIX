// <copyright file="VisualRegressionTracker.cs" company="Automate The Planet Ltd.">
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

using System.Diagnostics;
using Bellatrix.Plugins.Screenshots.Contracts;
using VisualRegressionTracker;

namespace Bellatrix.VRT;

public static class VisualRegressionTracker
{
    private static VisualRegressionTrackerSettings Settings => ConfigurationService.GetSection<VisualRegressionTrackerSettings>();

    private static Config config = new Config {
    ApiUrl=Settings.ApiUrl,
    Project=Settings.Project,
    ApiKey=Settings.ApiKey,
    CiBuildId=Settings.CiBuildId,
    BranchName=Settings.Branch,
    EnableSoftAssert=Settings.EnableSoftAssert
    };

    private static global::VisualRegressionTracker.VisualRegressionTracker Tracker { get; set; }

    static VisualRegressionTracker()
    {
        Tracker = new global::VisualRegressionTracker.VisualRegressionTracker(config);
    }

    public static void TakeSnapshot()
    {
        var caller = new StackTrace().GetFrame(1);
        var name = caller?.GetMethod().Name;


        Tracker.Start().RunSynchronously();

        Tracker.Track(name, ServicesCollection.Current.Resolve<IScreenshotEngine>().TakeScreenshot(ServicesCollection.Current));

        Tracker.Stop();
    }

    public static void TakeSnapshot(string name)
    {
        Tracker.Start().RunSynchronously();

        Tracker.Track(name, ServicesCollection.Current.Resolve<IScreenshotEngine>().TakeScreenshot(ServicesCollection.Current));

        Tracker.Stop();
    }
}
