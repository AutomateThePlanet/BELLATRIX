// <copyright file="BDDLoggingTimeEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.EventHandlers;
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop.BddLogging;

public class BDDLoggingTimeEventHandlers : TimeEventHandlers
{
    protected override void SettingTimeEventHandler(object sender, ComponentActionEventArgs arg) => Logger.LogInformation($"Set '{arg.ActionValue}' into {arg.Element.ComponentName} on {arg.Element.PageName}");
}
