// <copyright file="WaitStrategy.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Services.Browser;
using Bellatrix.Playwright.Settings.Extensions;
using Bellatrix.Playwright.Settings;

namespace Bellatrix.Playwright.WaitStrategies;

public abstract class WaitStrategy
{
    protected WaitStrategy(int? timeoutInterval = null, int? sleepInterval = null)
    {
        WrappedBrowser = ServicesCollection.Current.Resolve<WrappedBrowser>();
        TimeoutInterval = timeoutInterval;
        SleepInterval = sleepInterval ?? ConfigurationService.GetSection<WebSettings>().TimeoutSettings.InMilliseconds().SleepInterval;
    }

    protected WrappedBrowser WrappedBrowser { get; }

    protected int? TimeoutInterval { get; set; }

    protected int? SleepInterval { get; }

    public abstract void WaitUntil<TComponent>(TComponent by)
        where TComponent : Component;
}