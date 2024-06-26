// <copyright file="BDDLoggingElementEventHandlers.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Controls.EventHandlers;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright.Extensions.Controls.Controls.EventHandlers;

public class BDDLoggingElementEventHandlers : AnchorEventHandlers
{
    protected override void ScrollingToVisibleEventHandler(object sender, ComponentActionEventArgs arg) => Logger.LogInformation($"Scroll to visible {arg.Element.ComponentName}".AddUrlOrPageToBddLogging(arg.Element.PageName));

    protected override void ClickingEventHandler(object sender, ComponentActionEventArgs arg) => Logger.LogInformation($"Click {arg.Element.ComponentName}".AddUrlOrPageToBddLogging(arg.Element.PageName));

    protected override void HoveringEventHandler(object sender, ComponentActionEventArgs arg) => Logger.LogInformation($"Hover {arg.Element.ComponentName}".AddUrlOrPageToBddLogging(arg.Element.PageName));

    protected override void FocusingEventHandler(object sender, ComponentActionEventArgs arg) => Logger.LogInformation($"Focus {arg.Element.ComponentName}".AddUrlOrPageToBddLogging(arg.Element.PageName));
}
