// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bellatrix.Application;
using Bellatrix.TestWorkflowPlugins;
using Bellatrix.Web.Controls.EventHandlers;
using Bellatrix.Web.LoadTesting;
using Bellatrix.Web.TestExecutionExtensions.LoadTesting;

namespace Bellatrix.Web
{
    public static class AppRegistrationExtensions
    {
        // Should be set after WebProxy plug-in.
        public static BaseApp UseLoadTesting(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var loadTestingEnsureExtensions = new LoadTestingEnsureExtensionsService();
                loadTestingEnsureExtensions.SubscribeToAll();

                var elementEventHandlers = new List<ElementEventHandlers>()
                                           {
                                               new LoadTestingTextFieldEventHandlers(),
                                               new LoadTestingDateEventHandlers(),
                                               new LoadTestingColorEventHandlers(),
                                               new LoadTestingDateTimeLocalEventHandlers(),
                                               new LoadTestingEmailEventHandlers(),
                                               new LoadTestingInputFileEventHandlers(),
                                               new LoadTestingMonthEventHandlers(),
                                               new LoadTestingNumberEventHandlers(),
                                               new LoadTestingPasswordEventHandlers(),
                                               new LoadTestingPhoneEventHandlers(),
                                               new LoadTestingSearchEventHandlers(),
                                               new LoadTestingSelectEventHandlers(),
                                               new LoadTestingTextAreaEventHandlers(),
                                               new LoadTestingTimeEventHandlers(),
                                               new LoadTestingUrlEventHandlers(),
                                               new LoadTestingWeekEventHandlers(),
                                           };
                foreach (var elementEventHandler in elementEventHandlers)
                {
                    elementEventHandler.SubscribeToAll();
                }

                baseApp.RegisterType<TestWorkflowPlugin, LoadTestingWorkflowPlugin>(Guid.NewGuid().ToString());
            }

            return baseApp;
        }
    }
}
