// <copyright file="DisposeDriverService.cs" company="Automate The Planet Ltd.">
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
using System;
using Bellatrix.Utilities;
using OpenQA.Selenium;

namespace Bellatrix.Web.Services
{
    public static class DisposeDriverService
    {
        public static DateTime? TestRunStartTime { get; set; }

        public static void Dispose()
        {
            try
            {
                var driver = ServicesCollection.Current.Resolve<IWebDriver>();
                driver?.Close();
                driver?.Quit();
                driver?.Dispose();
                ServicesCollection.Current?.UnregisterSingleInstance<IWebDriver>();
            }
            catch (Exception ex)
            {
                DebugInformation.PrintStackTrace(ex);
            }

            ProcessCleanupService.KillPreviousDriversAndBrowsersOsAgnostic(TestRunStartTime);
        }

        public static void Dispose(IWebDriver webDriver, ServicesCollection container)
        {
            try
            {
                webDriver?.Close();
                webDriver?.Quit();
                webDriver?.Dispose();
                container?.UnregisterSingleInstance<IWebDriver>();
            }
            catch (Exception ex)
            {
                DebugInformation.PrintStackTrace(ex);
            }

            ProcessCleanupService.KillPreviousDriversAndBrowsersOsAgnostic(TestRunStartTime);
        }

        public static void DisposeAll()
        {
            foreach (var childContainer in ServicesCollection.Main.GetChildServicesCollections())
            {
                try
                {
                    var driver = childContainer.Resolve<IWebDriver>();
                    driver?.Close();
                    driver?.Quit();
                    driver?.Dispose();
                    childContainer?.UnregisterSingleInstance<IWebDriver>();
                }
                catch (Exception ex)
                {
                    DebugInformation.PrintStackTrace(ex);
                }
            }
        }
    }
}