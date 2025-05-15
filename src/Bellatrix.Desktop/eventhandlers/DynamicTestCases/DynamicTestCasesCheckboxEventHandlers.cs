﻿// <copyright file="DynamicTestCasesCheckboxEventHandlers.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.EventHandlers;
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop.DynamicTestCases;

public class DynamicTestCasesCheckboxEventHandlers : CheckboxEventHandlers
{
    protected override void CheckingEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddStep($"Check {arg.Element.ComponentName} on {arg.Element.PageName}");

    protected override void UncheckingEventHandler(object sender, ComponentActionEventArgs arg) => DynamicTestCasesService.AddStep($"Uncheck {arg.Element.ComponentName} on {arg.Element.PageName}");
}
