// <copyright file="BugReportingElementEventHandlers.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Controls.EventHandlers;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright.Extensions.Controls.Controls.EventHandlers;

public class BugReportingElementEventHandlers : AnchorEventHandlers
{
    protected override void ScrollingToVisibleEventHandler(object sender, ComponentActionEventArgs arg) => BugReportingContextService.AddStep($"Scroll to visible {arg.Element.ComponentName}".AddDynamicTestCasesUsingLocatorsMessage(arg));

    protected override void ClickingEventHandler(object sender, ComponentActionEventArgs arg) => BugReportingContextService.AddStep($"Click {arg.Element.ComponentName}".AddDynamicTestCasesUsingLocatorsMessage(arg));

    protected override void HoveringEventHandler(object sender, ComponentActionEventArgs arg) => BugReportingContextService.AddStep($"Hover {arg.Element.ComponentName}".AddDynamicTestCasesUsingLocatorsMessage(arg));

    protected override void FocusingEventHandler(object sender, ComponentActionEventArgs arg) => BugReportingContextService.AddStep($"Focus {arg.Element.ComponentName}".AddDynamicTestCasesUsingLocatorsMessage(arg));
}
