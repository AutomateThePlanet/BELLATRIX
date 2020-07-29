// <copyright file="BugReportingCheckboxEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.BugReporting;
using Bellatrix.Desktop.EventHandlers;
using Bellatrix.Desktop.Events;
using Bellatrix.Logging;

namespace Bellatrix.Desktop.BddLogging
{
    public class BugReportingCheckboxEventHandlers : CheckboxEventHandlers
    {
        protected BugReportingContextService BugReportingContextService => ServicesCollection.Current.Resolve<BugReportingContextService>();

        protected override void CheckingEventHandler(object sender, ElementActionEventArgs arg) => BugReportingContextService.AddStep($"Check {arg.Element.ElementName} on {arg.Element.PageName}");

        protected override void UncheckingEventHandler(object sender, ElementActionEventArgs arg) => BugReportingContextService.AddStep($"Uncheck {arg.Element.ElementName} on {arg.Element.PageName}");

        protected override void HoveringEventHandler(object sender, ElementActionEventArgs arg) => BugReportingContextService.AddStep($"Hover {arg.Element.ElementName} on {arg.Element.PageName}");
    }
}
